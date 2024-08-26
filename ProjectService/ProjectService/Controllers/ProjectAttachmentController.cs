using Microsoft.AspNetCore.Mvc;
using ProjectService.Services;
using ProjectService.ViewModels;
using ProjectViewModels;

namespace ProjectService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectAttachmentController : ControllerBase
    {
        private readonly ProjectAttachmentService _projectAttachmentService;
        public ProjectAttachmentController(ProjectAttachmentService projectAttachmentService)
        {
            _projectAttachmentService = projectAttachmentService;
        }
        [HttpGet("GetProjectAttachments")]
        public async Task<IActionResult> GetProjectAttachments(Guid projectId)
        {
            var list = await _projectAttachmentService.GetProjectAttachments(projectId);
            return Ok(list.Select(m => m.ToViewModel()).OrderBy(m => m.FileName).ToList());
        }
        [HttpPost("RemoveProjectAttachment")]
        public async Task<IActionResult> RemoveProjectAttachment(ProjectAttachmentVm vm)
        {
            return Ok(await _projectAttachmentService.RemoveProjectAttachment(vm));
        }

        [HttpPost("PaginatedProjectAttachments")]
        public async Task<IActionResult> PaginatedProjectAttachments(ProjectReqs req)
        {
            var result = await _projectAttachmentService.PaginatedProjectAttachments(req);
            return Ok(result.ToViewModelPaginatedList(m => m.ToViewModel()));
        }
        [HttpGet("GetAttachmentRequirementList")]
        public async Task<IActionResult> GetAttachmentRequirementList()
        {
            return Ok((await _projectAttachmentService.GetAttachmentRequirementList()).Select(m=>m.ToViewModel()).ToList());
        }
    }
}
