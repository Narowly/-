using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProjectService.Services;
using ProjectService.ViewModels;
using ProjectViewModels;
using System.Collections.ObjectModel;

namespace ProjectService.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectBonusController : ControllerBase
    {
        private readonly ProjectBonusService _bonusService;
        private readonly ProjectAllService _projectService;
        public ProjectBonusController(ProjectBonusService projectBonusService, ProjectAllService projectAllService)
        {
            _bonusService = projectBonusService;
            _projectService = projectAllService;
        }
        [HttpPost("SaveBonus")]
        public async Task<IActionResult> SaveBonus(List<ProjectBonusVm> list)
        {
            return Ok(await _bonusService.SaveBonus(list));
        }
        [HttpGet("GetBonusList")]
        public async Task<IActionResult> GetBonusList(Guid projectId)
        {
            var list = await _bonusService.GetBonusList(projectId);
            if (list == null) return Ok(null);
            var settingsList = new ObservableCollection<ProjectBonusSettingsVm>();


            var grouplist = list.GroupBy(m => m.ProjectProcessId).ToList();
            foreach (var group in grouplist)
            {
                var settingsVm = new ProjectBonusSettingsVm();
                settingsVm.ProjectBonusList = new ObservableCollection<ProjectBonusVm>(group.Select(m => m.ToViewModel()));
                settingsVm.ProjectProcess = group.First().ProjectProcess.ToViewModel();
                settingsList.Add(settingsVm);
            }

            return Ok(settingsList);
        }

        [HttpGet("GetBonusEx")]
        public async Task<IActionResult> GetBonusEx(Guid projectId)
        {
            var bonus = await _bonusService.GetBonusEx(projectId);
            if(bonus== null) return Ok(null);
            var project = await _projectService.GetProjectById(projectId);
            if (project == null) return Ok(null);
            var result = bonus.ToViewModel();
            if (project.AcceptanceDate != null)
            {
                var projectVm = project.ToViewModel(includeProjectProcess: true);
                //var verifyWorkDays = projectVm.CountUniqueDaysOfWorkAcrossProcesses();
                //result.VerifyWorkDays = verifyWorkDays;
                result.VerifyPersonDays = projectVm.VerifyWorkDays;//verifyWorkDays?.Count();
                result.VerifyBonus = _bonusService.CalculateBonusEx(result);
            }
            
            return Ok(result);
        }

        [HttpPost("SaveBonusEx")]
        public async Task<IActionResult> SaveBonusEx(ProjectBonusExVm vm)
        {
            return Ok(await _bonusService.SaveBonusEx(vm));
        }

        [HttpPost("AchievementBonusCalculate")]
        public async Task<IActionResult> AchievementBonusCalculate([FromBody] AchievementBonusCalculateReq req)
        {
            return Ok(await _bonusService.AchievementBonusCalculate(req));
        }

        [HttpGet("ProjectBonusCalculate")]
        public async Task<IActionResult> ProjectBonusCalculate(Guid projectId)
        {
            return Ok(await _bonusService.ProjectBonusCalculate(projectId));
        }
    }
}
