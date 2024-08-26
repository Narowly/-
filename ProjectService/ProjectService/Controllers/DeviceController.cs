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
    public class DeviceController : ControllerBase
    {
        private readonly DeviceService _deviceService;
        public DeviceController(DeviceService deviceService)
        {
            _deviceService = deviceService;
        }
        [HttpGet("DeviceTypeList")]
        public async Task<IActionResult> DeviceTypeList()
        {
            return Ok((await _deviceService.GetDeviceTypeList()).Select(m=>m.ToViewModel()));
        }

        [HttpPost("SaveDeviceType")]
        public async Task<IActionResult> SaveDeviceType([FromBody]DeviceTypeVm vm)
        {
            return Ok(await _deviceService.SaveDeviceType(vm));
        }

        [HttpPost("UpdateDeviceType")]
        public async Task<IActionResult> UpdateDeviceType([FromBody]DeviceTypeVm vm)
        {
            return Ok((await _deviceService.UpdateDeviceType(vm)).ToViewModel());
        }
        [HttpPost("DevicePaginatedList")]
        public async Task<IActionResult> DevicePaginatedList([FromBody] DeviceReqs req)
        {
            var list = await _deviceService.GetDevicePaginatedList(req);
            var result = list.ToViewModelPaginatedList(m => m.ToStockViewModel());
            return Ok(result);
        }
        [HttpPost("DeviceList")]
        public async Task<IActionResult> DeviceList([FromBody] DeviceReqs req)
        {
            return Ok((await _deviceService.GetDeviceList(req)));
        }

        [HttpPost("SaveDevice")]
        public async Task<IActionResult> SaveDevice(DeviceVm vm)
        {
            return Ok(await _deviceService.SaveDevice(vm));
        }
        [HttpGet("RemoveDevice")]
        public async Task<IActionResult> RemoveDevice(Guid deviceId)
        {
            return Ok(await _deviceService.RemoveDevice(deviceId));
        }
        [HttpGet("GetDeviceById")]
        public async Task<IActionResult> GetDeviceById(Guid deviceId)
        {
            return Ok((await _deviceService.GetDeviceById(deviceId))?.ToViewModel());
        }
        [HttpPost("UpdateDevice")]
        public async Task<IActionResult> UpdateDevice(DeviceVm vm)
        {
            return Ok(await _deviceService.UpdateDevice(vm));
        }

        [HttpGet("ProjectDeviceByProjectId")]
        public async Task<IActionResult> ProjectDeviceByProjectId(Guid projectId)
        {
            return Ok((await _deviceService.GetProjectDeviceList(projectId)).Select(m => m.ToViewModel()).ToList());
        }
        [HttpPost("SaveProjectDevice")]
        public async Task<IActionResult> SaveProjectDevice([FromBody] ProjectVm vm)
        {
            return Ok(await _deviceService.SaveProjectDevice(vm));
        }
        [HttpPost("PaginatedProjectDeviceHistory")]
        public async Task<IActionResult> PaginatedProjectDeviceHistory([FromBody]DeviceReqs req)
        {
            return Ok(await _deviceService.PaginatedProjectDeviceHistory(req));
        }
        [HttpPost("PaginatedDeviceType")]
        public async Task<IActionResult> PaginatedDeviceType([FromBody]CommonReqs req)
        {
            return Ok((await _deviceService.PaginatedDeviceType(req)).ToViewModelPaginatedList(m => m.ToViewModel()));
        }
        [HttpGet("GetDeviceTypeById")]
        public async Task<IActionResult> GetDeviceTypeById(Guid deviceTypeId)
        {
            return Ok((await _deviceService.GetDeviceTypeById(deviceTypeId))?.ToViewModel());
        }
        [HttpGet("ReturnNextNumber")]
        public async Task<IActionResult> ReturnMaxNumber(Guid deviceTypeId)
        {
            return Ok(await _deviceService.ReturnNextNumber(deviceTypeId));
        }
        [HttpPost("BatchSaveDevice")]
        public async Task<IActionResult> BatchSaveDevice(BatchSaveDeviceReq req)
        {
            return Ok(await _deviceService.BatchSaveDevice(req));
        }
        [HttpGet("SetHandleBy")]
        public async Task<IActionResult> SetHandleBy(Guid deviceId, Guid staffId)
        {
            return Ok(await _deviceService.SetHandleBy(deviceId, staffId));
        }
    }
}
