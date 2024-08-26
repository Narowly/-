using Microsoft.AspNetCore.Mvc;
using ProjectService.Services;
using ProjectService.ViewModels;
using ProjectViewModels;
namespace ProjectService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StaffController: ControllerBase
    {
        private readonly StaffService _staffService;
        public StaffController(StaffService service)
        {
            _staffService = service;
        }

        [HttpGet("GetStaffById")]
        public async Task<IActionResult> GetStaffById(Guid id)
        {
            return Ok((await _staffService.GetStaffById(id))?.ToViewModel());
        }

        [HttpGet("StaffList")]
        public async Task<IActionResult> StaffList()
        {
            var result = (await _staffService.GetStaffAllList()).Select(m => m.ToViewModel()).ToList();
            return Ok(result);
        }
        [HttpGet("GetStaffListWithProjectName")]
        public async Task<IActionResult> GetStaffListWithProjectName()
        {
            return Ok(await _staffService.GetStaffListWithProjectName());
        }

        [HttpPost("StaffByDuty")]
        public async Task<IActionResult> GetStaffByDuty([FromBody] List<int> duty)
        {
            return Ok((await _staffService.GetStaffByDuty(duty)).Select(m => m.ToViewModel()).ToList());
        }

        [HttpPost("IdleProjectStaff")]
        public async Task<IActionResult> GetIdleProjectStaff([FromBody] List<int> duty)
        {
            var idleProjectStaffs = await _staffService.GetIdleStaffId(duty);
            return Ok(idleProjectStaffs.Select(m => m.ToViewModel()).ToList());
        }
        [HttpGet("SpeedupProjectStaff")]
        public async Task<IActionResult> GetSpeedUpProjectStaff()
        {
            var speedupProjectStaffs = await _staffService.GetSpeedUpProjectStaff();
            return Ok(speedupProjectStaffs);
        }

        [HttpGet("ProjectStaffByProjectId")]
        public async Task<IActionResult> GetProjectStaffByProjectId(Guid projectId)
        {
            var staffs = await _staffService.GetProjectStaff(projectId);
            return Ok(staffs.Select(m => m.ToViewModel()).ToList());
        }

        [HttpPost("SaveProjectStaffs")]
        public async Task<IActionResult> SaveProjectStaffs([FromBody]ProjectVm vm)
        {
            var result = await _staffService.SaveProjectStaffs(vm);
            return Ok(result);
        }
    }
}
