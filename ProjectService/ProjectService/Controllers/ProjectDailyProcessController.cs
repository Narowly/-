using Microsoft.AspNetCore.Mvc;
using ProjectService.Db;
using ProjectService.Services;
using ProjectService.ViewModels;
using ProjectViewModels;

namespace ProjectService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectDailyProcessController : ControllerBase
    {
        private ProjectDailyProcessService _projectDailyProcessService;
        public ProjectDailyProcessController(ProjectDailyProcessService projectDailyProcessService)
        {
            _projectDailyProcessService = projectDailyProcessService;
        }
        [HttpGet("GetProjectDailyProcessHistory")]
        public async Task<IActionResult> GetProjectDailyProcessHistory(Guid projectId)
        {
            var history = await _projectDailyProcessService.GetProjectDailyProcessHistory(projectId);
            var result = new List<ProjectDailyProcessHistoryVm>();
            if (history != null)
            {
                foreach (var h in history)
                {
                    result.Add(new ProjectDailyProcessHistoryVm
                    {
                        StartDate = h.Key,
                        DailyProcessList = h.Value.Select(m => m.ToViewModel()).ToList(),
                        Remarks = h.Value.Where(m => !string.IsNullOrWhiteSpace(m.Remarks)).Select(m => m.Remarks).FirstOrDefault()
                    });
                }
            }
            return Ok(result.OrderByDescending(m => m.StartDate).ToList());
        }
        [HttpPost("SaveProjectDailyProcess")]
        public async Task<IActionResult> SaveProjectDailyProcess(List<ProjectDailyProcessHistoryVm> history)
        {
            bool result = false;
            foreach (var h in history)
            {
                var index = 0;
                foreach (var item in h.DailyProcessList)
                {
                    item.StartDate = h.StartDate;
                    if (index == 0)
                        item.Remarks = h.Remarks;
                    index++;
                }
                result = await _projectDailyProcessService.SaveProjectDailyProcess(h.DailyProcessList);
            }
            return Ok(result);
        }

        [HttpPost("UpdateProjectDailyProcess")]
        public async Task<IActionResult> UpdateProjectDailyProcess(List<ProjectDailyProcessVm> list)
        {
            return Ok(await _projectDailyProcessService.UpdateProjectDailyProcess(list));
        }

        [HttpPost("RemoveProjectDailyProcess")]
        public async Task<IActionResult> RemoveProjectDailyProcess(List<ProjectDailyProcessVm> list)
        {
            return Ok(await _projectDailyProcessService.RemoveProjectDailyProcess(list));
        }
    }
}
