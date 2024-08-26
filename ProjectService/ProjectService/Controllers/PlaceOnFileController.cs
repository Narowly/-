using Microsoft.AspNetCore.Mvc;
using ProjectService.Services;
using ProjectViewModels;

namespace ProjectService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlaceOnFileController : ControllerBase
    {
        private readonly PlaceOnFileService _placeOnFileService;
        public PlaceOnFileController(PlaceOnFileService placeOnFileService)
        {
            _placeOnFileService = placeOnFileService;
        }

        [HttpPost("SavePlaceOnFile")]
        public async Task<IActionResult> SavePlaceOnFile([FromBody] PlaceOnFileVm vm)
        {
            return Ok(await _placeOnFileService.SavePlaceOnFile(vm));
        }

        [HttpPost("PaginatedApplyPlaceOnFileProject")]
        public async Task<IActionResult> PaginatedApplyPlaceOnFileProject([FromBody] ProjectReqs req)
        {
            return Ok(await _placeOnFileService.PaginatedApplyPlaceOnFileProject(req));
        }
    }
}
