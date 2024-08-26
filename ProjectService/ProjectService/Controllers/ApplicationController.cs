using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProjectService.Services;
using ProjectService.ViewModels;
using ProjectViewModels;

namespace ProjectService.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    public class ApplicationController : ControllerBase
    {
        private readonly ApplicationService _applicationService;
        private readonly StaffService _staffService;
        private readonly DeviceService _deviceService;
        private readonly ConsumableService _consumableService;
        public ApplicationController(ApplicationService applicationService, StaffService staffService, DeviceService deviceService, ConsumableService consumableService)
        {
            _applicationService = applicationService;
            _staffService = staffService;
            _deviceService = deviceService;
            _consumableService = consumableService;
        }
        [HttpPost("SaveApplication")]
        public async Task<IActionResult> SaveApplication([FromBody] ApplicationVm vm)
        {
            return Ok(await _applicationService.SaveApplication(vm));
        }
        [HttpPost("PaginatedApplication")]
        public async Task<IActionResult> PaginatedApplication([FromBody] ApplicationReq req)
        {
            var result = await _applicationService.PaginatedApplication(req);
            return Ok(result);
        }
        [HttpGet("GetApplicationById")]
        public async Task<IActionResult> GetApplicationById(Guid applicationId, bool includeProject = false)
        {
            return Ok(await _applicationService.GetApplicationById(applicationId, includeProject));
        }

        [HttpPost("ApproveApplication")]
        public async Task<IActionResult> Approve([FromBody]ApplicationVm vm)
        {
            return Ok(await _applicationService.Approve(vm));
        }
        [HttpPost("ProcessStaffApplication")]
        public async Task<IActionResult> ProcessStaffApplication([FromBody] ApplicationProcessdReq req)
        {
            var processStaffResult = await _staffService.SaveProjectStaffs(req.Project);
            if (processStaffResult)
            {
                var result = await _applicationService.Processd(req.ApplicationId);
                return Ok(result);
            }
            return Ok(processStaffResult);
        }
        [HttpPost("ProcessDeviceApplication")]
        public async Task<IActionResult> ProcessDeviceApplication([FromBody] ApplicationProcessdReq req)
        {
            var processDeviceResult = await _deviceService.SaveProjectDevice(req.Project);
            if (processDeviceResult)
            {
                var result = await _applicationService.Processd(req.ApplicationId);
                return Ok(result);
            }
            return Ok(processDeviceResult);
        }
        [HttpPost("ProcessConsumableApplication")]
        public async Task<IActionResult> ProcessConsumableApplication([FromBody] ApplicationProcessdReq req)
        {
            var processConsumableResult = await _consumableService.SaveStockOutBoundList(req.OutConsumableList);
            if (processConsumableResult)
            {
                var result = await _applicationService.Processd(req.ApplicationId);
                return Ok(result);
            }
            return Ok(processConsumableResult);
        }
    }
}
