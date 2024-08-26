using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using ProjectService.Services;
using ProjectService.ViewModels;
using ProjectViewModels;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ProjectService.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class DictController : ControllerBase
    {
        private readonly DictService _dictService;
        public DictController(DictService dictService)
        {
            _dictService = dictService;
        }

        [HttpPost("AddDictType")]
        public async Task<IActionResult> AddDictType([FromBody] DictTypeVm vm)
        {
            return Ok(await _dictService.AddDictType(vm));
        }
        
        [HttpPost("UpdateDictType")]
        public async Task<IActionResult> UpdateDictType([FromBody] DictTypeVm vm)
        {
            var dictType = await _dictService.UpdateDictType(vm);
            return Ok(dictType.ToViewModel());
        }

        [HttpPost("GetDictTypeList")]
        public async Task<IActionResult> GetDictTypeList([FromBody] PaginationParams req)
        {
            var list = await _dictService.GetDictTypeList(req);
            return Ok(list.ToViewModelPaginatedList(m => m.ToViewModel()));
        }
        [HttpGet("GetDictDataByTypeName")]
        public async Task<IActionResult> GetDictDataByTypeName(string name)
        {
            var type = await _dictService.GetDictTypeByName(name);
            if (type != null)
            {
                var data = await _dictService.GetDictDataByType(type.DictId);
                return Ok(data?.Select(m => m.ToViewModel()).ToList());
            }
            return Ok();
        }
        [HttpGet("GetDictDataId")]
        public async Task<IActionResult> GetDictDataId(string typeName, string label)
        {
            return Ok(await _dictService.GetDictDataId(typeName, label));
        }

        [HttpGet("GetDictData")]
        public async Task<IActionResult> GetDictData(int dictCode)
        {
            return Ok((await _dictService.GetDictData(dictCode))?.ToViewModel());
        }
    }
}
