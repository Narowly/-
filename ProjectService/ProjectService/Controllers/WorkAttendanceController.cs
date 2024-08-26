using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProjectService.Services;
using ProjectViewModels;

namespace ProjectService.Controllers
{
    //[Authorize]
    [Route("api/[controller]")]
    public class WorkAttendanceController : ControllerBase
    {
        private readonly WorkAttendanceService _workAttendanceService;
        public WorkAttendanceController(WorkAttendanceService workAttendanceService)
        {
            _workAttendanceService = workAttendanceService;
        }
        [HttpPost("InsertYearMonthWorkAttendanceExcel")]
        public async Task<IActionResult> InsertYearMonthWorkAttendanceExcel([FromBody] List<WorkAttendanceVm> list)
        {
            return Ok(await _workAttendanceService.InsertYearMonthWorkAttendanceExcel(list));
        }

        [HttpPost("InsertDelayClockExcel")]
        public async Task<IActionResult> InsertDelayClockExcel([FromBody] List<WorkDelayClockVm> list)
        {
            return Ok(await _workAttendanceService.InsertDelayClockExcel(list));
        }

        [HttpPost("InsertOutClockExcel")]
        public async Task<IActionResult> InsertOutClockExcel([FromBody] List<WorkOutClockVm> list)
        {
            return Ok(await _workAttendanceService.InsertOutClockExcel(list));
        }

        [HttpPost("InsertApplyLeaveExcel")]
        public async Task<IActionResult> InsertApplyLeaveExcel([FromBody] List<WorkApplyLeaveVm> list)
        {
            return Ok(await _workAttendanceService.InsertApplyLeaveExcel(list));
        }
        [HttpGet("GetYearMonthWorkAttendance")]
        public async Task<IActionResult> GetYearMonthWorkAttendance(string yearMonth)
        {
            return Ok(await _workAttendanceService.GetYearMonthWorkAttendance(yearMonth));
        }
    }
}
