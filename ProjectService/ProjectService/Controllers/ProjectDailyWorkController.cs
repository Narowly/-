using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProjectService.Services;
using ProjectService.ViewModels;
using ProjectViewModels;

namespace ProjectService.Controllers
{
    //[Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectDailyWorkController : ControllerBase
    {
        private readonly ProjectDailyWorkService _projectDailyWorkService;
        public ProjectDailyWorkController(ProjectDailyWorkService projectDailyWorkService)
        {
            _projectDailyWorkService = projectDailyWorkService;
        }

        [HttpPost("PaginatedSearchProjectDailyWork")]
        public async Task<IActionResult> PaginatedSearchProjectDailyWork([FromBody]ProjectWithStaffReq req)
        {
            var result = await _projectDailyWorkService.PaginatedSearchProjectDailyWork(req);
            return Ok(result.ToViewModelPaginatedList(m => m?.ToViewModel(true)));
        }
        [HttpPost("SaveProjectDailyWork")]
        public async Task<IActionResult> SaveProjectDailyWork([FromBody] ProjectDailyWorkVm vm)
        {
            return Ok(await _projectDailyWorkService.SaveProjectDailyWork(vm));
        }

        [HttpGet("GetDailyWorkById")]
        public async Task<IActionResult> GetDailyWorkById(Guid id)
        {
            return Ok((await _projectDailyWorkService.GetDailyWorkById(id))?.ToViewModel(true));
        }

        [HttpPost("GetDailyWorkSummary")]
        public async Task<IActionResult> GetDailyWorkSummary([FromBody]ProjectWithStaffReq req)
        {
            return Ok(await _projectDailyWorkService.GetDailyWorkSummary(req));
        }

        [HttpPost("SaveBatchDailyWork")]
        public async Task<IActionResult> SaveBatchDailyWork([FromBody]List<BatchProjectDailyWorkVm> list)
        {
            return Ok(await _projectDailyWorkService.SaveBatchDailyWork(list));
        }

        [HttpPost("SaveExcelDailyWork")]
        public async Task<IActionResult> SaveExcelDailyWork([FromBody] List<ProjectDailyWorkExcelVm> list)
        {
            return Ok(await _projectDailyWorkService.SaveExcelDailyWork(list));
        }
    }
}
