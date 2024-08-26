using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProjectService.Services;
using ProjectService.ViewModels;
using ProjectViewModels;

namespace ProjectService.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    public class PatrolController : ControllerBase
    {
        private readonly PatrolService _patrolService;
        public PatrolController(PatrolService patrolService)
        {
            _patrolService = patrolService;
        }
        [HttpPost("PaginatedPatrol")]
        public async Task<IActionResult> PaginatedPatrol([FromBody]ProjectWithStaffReq req)
        {
            var result = await _patrolService.PaginatedPatrol(req);
            return Ok(result);
        }
        [HttpPost("GetPatrolList")]
        public async Task<IActionResult> GetPatrolList([FromBody]ProjectWithStaffReq req)
        {
            var list = await _patrolService.GetPatrolList(req);
            return Ok(list);
        }
        [HttpPost("SavePatrol")]
        public async Task<IActionResult> SavePatrol([FromBody]ProjectPatrolVm vm)
        {
            return Ok(await _patrolService.SavePatrol(vm));
        }
        [HttpGet("GetPatrolById")]
        public async Task<IActionResult> GetPatrolById(long id)
        {
            return Ok((await _patrolService.GetPatrolById(id))?.ToViewModel());
        }
        [HttpPost("SavePatrolByExcel")]
        public async Task<IActionResult> SavePatrolByExcel([FromBody]List<ProjectPatrolExcelVm> list)
        {
            return Ok(await _patrolService.SavePatrolByExcelData(list));
        }
    }
}
