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
    public class ConsumableController : ControllerBase
    {
        private readonly ConsumableService _consumableService;
        public ConsumableController(ConsumableService consumableService)
        {
            _consumableService = consumableService;
        }
        [HttpGet("GetConsumableTypeById")]
        public async Task<IActionResult> GetConsumableTypeById(Guid consumableTypeId)
        {
            return Ok((await _consumableService.GetConsumableTypeById(consumableTypeId))?.ToViewModel());
        }

        [HttpPost("SaveConsumableType")]
        public async Task<IActionResult> SaveConsumableType([FromBody]ConsumableTypeVm vm)
        {
            return Ok(await _consumableService.SaveConsumableType(vm));
        }

        [HttpPost("PaginatedConsumableType")]
        public async Task<IActionResult> PaginatedConsumableType([FromBody]CommonReqs req)
        {
            return Ok((await _consumableService.PaginatedConsumableType(req)).ToViewModelPaginatedList(m => m.ToViewModel()));
        }
        [HttpPost("GetConsumableTypeList")]
        public async Task<IActionResult> GetConsumableTypeList([FromBody] CommonReqs req)
        {
            return Ok((await _consumableService.GetConsumableTypeList(req)).Select(m => m.ToViewModel()));
        }
        [HttpPost("PaginatedConsumable")]
        public async Task<IActionResult> PaginatedConsumable([FromBody]ConsumableReqs req)
        {
            return Ok((await _consumableService.PaginatedConsumable(req)).ToViewModelPaginatedList(m => m.ToViewModel()));
        }
        [HttpPost("GetConsumableList")]
        public async Task<IActionResult> GetConsumableList([FromBody]ConsumableReqs req)
        {
            return Ok((await _consumableService.GetConsumableList(req)).Select(m => m.ToViewModel()));
        }
        [HttpGet("GetConsumableById")]
        public async Task<IActionResult> GetConsumableById(Guid consumableId)
        {
            return Ok((await _consumableService.GetConsumableById(consumableId))?.ToViewModel());
        }
        [HttpPost("SaveConsumable")]
        public async Task<IActionResult> SaveConsumable([FromBody]ConsumableVm vm)
        {
            return Ok(await _consumableService.SaveConsumable(vm));
        }
        [HttpPost("SaveStockInBound")]
        public async Task<IActionResult> SaveStockInBound([FromBody]StockInBoundVm vm)
        {
            return Ok(await _consumableService.SaveStockInBound(vm));
        }
        [HttpPost("SaveStockOutBound")]
        public async Task<IActionResult> SaveStockOutBound([FromBody]StockOutBoundVm vm)
        {
            return Ok(await _consumableService.SaveStockOutBound(vm));
        }
        
        [HttpPost("PaginatedConsumableBound")]
        public async Task<IActionResult> PaginatedConsumableBound([FromBody]ConsumableReqs req)
        {
            return Ok(await _consumableService.PaginatedConsumableBound(req));
        }
        [HttpGet("GetStockOutBoundById")]
        public async Task<IActionResult> GetStockOutBoundById(Guid id)
        {
            return Ok((await _consumableService.GetStockOutBoundById(id)).ToViewModel());
        }
        [HttpGet("GetStockInBoundById")]
        public async Task<IActionResult> GetStockInBoundById(Guid id)
        {
            return Ok((await _consumableService.GetStockInBoundById(id)).ToViewModel());
        }
    }
}
