using Microsoft.AspNetCore.Mvc;
using ProjectService.Db;
using ProjectService.Services;
using ProjectViewModels;

namespace ProjectService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FileUploadController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IHostEnvironment _hostEnvironment;
        private readonly ProjectAttachmentService _attachmentService;
        private readonly ProjectAllService _projectService;

        public FileUploadController(IConfiguration configuration, IHostEnvironment hostEnvironment, ProjectAttachmentService projectAttachmentService, ProjectAllService projectService)
        {
            _configuration = configuration;
            _hostEnvironment = hostEnvironment;
            _attachmentService = projectAttachmentService;
            _projectService = projectService;
        }

        // 帮助方法，用于解析配置中的路径为绝对路径  
        private string ResolveUploadPath()
        {
            var uploadPathConfig = _configuration["FileUpload:UploadPath"];
            // 如果路径是绝对路径，直接返回；否则，将其解析为相对于内容根目录的路径  
            if (!Path.IsPathRooted(uploadPathConfig))
            {
                uploadPathConfig = Path.Combine(_hostEnvironment.ContentRootPath, uploadPathConfig);
            }
            return uploadPathConfig;
        }

        [HttpPost("UploadMultiple")]
        public async Task<IActionResult> UploadMultipleFiles(Guid projectId, [FromForm] List<IFormFile> files)
        {
            var project = await _projectService.GetProjectById(projectId);
            if (project == null) return Ok(false);
            var uploadPath = ResolveUploadPath();
            var uploadProjectPath = Path.Combine(uploadPath, project.Contract.ContractNumber);
            // 确保上传目录存在  
            Directory.CreateDirectory(uploadProjectPath);
            var list = new List<ProjectAttachment>();
            
            foreach (var formFile in files)
            {
                if (formFile.Length > 0)
                {
                    var fileName = project.Contract.ContractNumber + "_" + formFile.FileName;
                    var filePath = Path.Combine(uploadProjectPath, fileName);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await formFile.CopyToAsync(stream);
                    }
                    var dir = Directory.GetCurrentDirectory();
                    var downloadLink = $"{uploadProjectPath.Replace(_hostEnvironment.ContentRootPath, string.Empty).TrimStart('\\')}/{fileName}";  
            
                    list.Add(new ProjectAttachment
                    {
                        FileAddress = downloadLink,
                        FileName = fileName,
                        ProjectId = projectId,
                        UploadDate = DateTime.Now,
                        FileType = filePath.Substring(filePath.LastIndexOf('.') + 1, filePath.Length - 1 - filePath.LastIndexOf('.'))
                    });
                }
            }

            await _attachmentService.SaveProjectAttachment(list);
            return Ok(true);
        }
    }
}
