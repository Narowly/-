using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProjectService.Services;
using ProjectViewModels;

namespace ProjectService.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    public class ConsumableAskForController : ControllerBase
    {
        private readonly ConsumableAskForService _consumableAskForService;
        public ConsumableAskForController(ConsumableAskForService consumableAskForService)
        {
            _consumableAskForService = consumableAskForService;
        }

        [HttpPost("PaginatedConsumableAskFor")]
        public async Task<IActionResult> PaginatedConsumableAskFor([FromBody] ConsumableAskForReq req)
        {
            return Ok(await _consumableAskForService.PaginatedConsumableAskFor(req));
        }

        [HttpPost("SaveConsumableAskFor")]
        public async Task<IActionResult> SaveConsumableAskFor([FromBody] ConsumableAskForVm vm)
        {
            return Ok(await _consumableAskForService.SaveConsumableAskFor(vm));
        }

        [HttpGet("GetConsumableAskForById")]
        public async Task<IActionResult> GetConsumableAskForById(Guid id)
        {
            return Ok(await _consumableAskForService.GetConsumableAskForById(id));
        }
    }
}
