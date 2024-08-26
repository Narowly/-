using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Diagnostics;
using ProjectService.Db;
using ProjectService.Services;
using ProjectService.ViewModels;
using ProjectViewModels;
using System.Collections.Specialized;

namespace ProjectService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EarlyWarningController : ControllerBase
    {
        private readonly EarlyWarningService _earlyWarningService;
        private readonly DictService _dictService;
        public EarlyWarningController(EarlyWarningService earlyWarningService, DictService dictService)
        {
            _earlyWarningService = earlyWarningService;
            _dictService = dictService;
        }

        [HttpGet("GetProjectEarlyWarnings")]
        public async Task<IActionResult> GetProjectEarlyWarnings(Guid projectId)
        {
            var warnings = await _earlyWarningService.GetProjectEarlyWarnings(projectId);
            var type = await _dictService.GetDictTypeByName("EarlyWarningType");
            if (type != null)
            {
                var typeData = await _dictService.GetDictDataByType(type.DictId);
                if (typeData != null)
                {
                    var startWarningDaysType = typeData.First(f => f.DictLabel == "开始预警天数").DictCode;
                    var totalCountType = typeData.First(f => f.DictLabel == "项目总量预警").DictCode;
                    var scheduleWarningType = typeData.First(f => f.DictLabel == "项目进度预警").DictCode;
                    var efficiencyWarningType = typeData.First(f => f.DictLabel == "项目效率预警").DictCode;
                    var vm = new EarlyWarningVm();
                    if (warnings != null && warnings.Count > 0)
                    {
                        var startWarningDays = warnings.First(m => m.WarningType == startWarningDaysType);
                        var totalCount = warnings.First(m => m.WarningType == totalCountType);
                        var scheduleWarning = warnings.First(m => m.WarningType == scheduleWarningType);
                        var efficiencyWarning = warnings.First(m => m.WarningType == efficiencyWarningType);
                        vm.StartWarningDays = startWarningDays.ToViewModel();
                        vm.TotalCountWarning = totalCount.ToViewModel();
                        vm.ScheduleWarning = scheduleWarning.ToViewModel();
                        vm.EfficiencyWarning = efficiencyWarning.ToViewModel();
                    }
                    else
                    {
                        vm.StartWarningDays = new ProjectEarlyWarningVm { ProjectId = projectId, WarningType = startWarningDaysType };
                        vm.TotalCountWarning = new ProjectEarlyWarningVm { ProjectId = projectId, WarningType = totalCountType };
                        vm.ScheduleWarning = new ProjectEarlyWarningVm { ProjectId = projectId, WarningType = scheduleWarningType };
                        vm.EfficiencyWarning = new ProjectEarlyWarningVm { ProjectId = projectId, WarningType = efficiencyWarningType };
                    }
                    return Ok(vm);
                }            
            }
            return Ok(null);
        }

        [HttpPost("SaveProjectEarlyWarnings")]
        public async Task<IActionResult> SaveProjectEarlyWarnings([FromBody] List<ProjectEarlyWarningVm> list)
        {
            return Ok(await _earlyWarningService.SaveProjectEarlyWarnings(list));
        }
        [HttpPost("PaginatedWarningHistory")]
        public async Task<IActionResult> PaginatedWarningHistory(ProjectReqs req)
        {

            //var type = await _dictService.GetDictTypeByName("EarlyWarningType");
            //if (type == null) return Ok(null);
            //var typeData = await _dictService.GetDictDataByType(type.DictId);
            //if (typeData == null) return Ok(null);
            var typeData = await GetDictData("EarlyWarningType");
            //var status = await _dictService.GetDictTypeByName("EarlyWarningHandlingStatus");
            //if (status == null) return Ok(null);
            //var statusData = await _dictService.GetDictDataByType(status.DictId);
            //if (statusData == null) return Ok(null);
            var statusData = await GetDictData("EarlyWarningHandlingStatus");
            if(typeData==null||statusData==null) return Ok(null);
            var result = await _earlyWarningService.PaginatedWarningHistory(req);
            return Ok(result.ToViewModelPaginatedList(m => m.ToViewModel(typeData, statusData)));
        }

        private async Task<List<DictDatum>?> GetDictData(string typeName)
        {
            var type = await _dictService.GetDictTypeByName(typeName);
            if (type == null) return null;
            var typeData = await _dictService.GetDictDataByType(type.DictId);
            if (typeData == null) return null;
            return typeData;
        }
        [HttpGet("GetEarlyWarningHistoryById")]
        public async Task<IActionResult> GetEarlyWarningHistoryById(int id)
        {
            var typeData = await GetDictData("EarlyWarningType");
            var statusData = await GetDictData("EarlyWarningHandlingStatus");
            if (typeData == null || statusData == null) return Ok(null);
            var history = await _earlyWarningService.GetEarlyWarningHistoryById(id);
            return Ok(history?.ToViewModel(typeData, statusData));
        }
        [HttpPost("SaveEarlyWarningHistory")]
        public async Task<IActionResult> SaveEarlyWarningHistory(EarlyWarningHistoryVm vm)
        {
            return Ok(await _earlyWarningService.SaveEarlyWarningHistory(vm));
        }
    }
}
