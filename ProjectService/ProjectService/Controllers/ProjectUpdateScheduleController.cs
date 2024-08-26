using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProjectService.Services;
using ProjectService.ViewModels;
using ProjectViewModels;

namespace ProjectService.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectUpdateScheduleController : ControllerBase
    {
        private readonly ProjectUpdateScheduleService _updateScheduleService;
        private readonly DictService _dictService;
        public ProjectUpdateScheduleController(ProjectUpdateScheduleService updateScheduleService, DictService dictService)
        {
            _updateScheduleService = updateScheduleService;
            _dictService = dictService;
        }
        [HttpPost("AddProjectUpdateSchedule")]
        public async Task<IActionResult> AddProjectUpdateSchedule([FromBody] ProjectUpdateScheduleVm vm)
        {
            return Ok(await _updateScheduleService.AddProjectUpdateSchedule(vm));
        }
        [HttpPost("PaginatedProjectUpdateSchedule")]
        public async Task<IActionResult> PaginatedProjectUpdateSchedule([FromBody] ProjectReqs req)
        {
            var result = await _updateScheduleService.PaginatedProjectUpdateSchedule(req);
            
            var vmList = result.ToViewModelPaginatedList(m => m.ToViewModel());
            
            return Ok(vmList);
        }
    }
}
