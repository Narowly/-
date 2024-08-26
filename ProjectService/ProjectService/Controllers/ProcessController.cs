using Microsoft.AspNetCore.Mvc;
using ProjectService.Db;
using ProjectService.Services;
using ProjectService.ViewModels;
using ProjectViewModels;

namespace ProjectService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProcessController : ControllerBase
    {
        private readonly ProcessService _processService;
        public ProcessController(ProcessService processService)
        {
            _processService = processService;
        }
        [HttpGet("GetProcessList")]
        public async Task<IActionResult> GetProcessList()
        {
            return Ok((await _processService.GetProcessList()).Select(m => m.ToViewModel()));
        }
        [HttpGet("GetProcessUnitList")]
        public async Task<IActionResult> GetProcessUnitList()
        {
            return Ok((await _processService.GetProcessUnitList()).Select(m => m.ToViewModel()));
        }
        [HttpPost("SaveProcessTemplate")]
        public async Task<IActionResult> SaveProcessTemplate([FromBody]ProcessTemplateVm processTemplateVm)
        {
            return Ok(await _processService.SaveProcessTemplate(processTemplateVm));
        }
        [HttpGet("GetProcessTemplateList")]
        public async Task<IActionResult> GetProcessTemplateList()
        {
            return Ok((await _processService.GetProcessTemplateList()).Select(m => m.ToViewModel()));
        }
        [HttpGet("GetProcessTemplate")]
        public async Task<IActionResult> GetProcessTemplate(int templateId)
        {
            return Ok((await _processService.GetProcessTemplate(templateId))?.ToViewModel());
        }

        [HttpGet("GetProjectProcesses")]
        public async Task<IActionResult> GetProjectProcesses(Guid projectId)
        {
            return Ok((await _processService.GetProjectProcesses(projectId)).Select(m => m.ToViewModel()));
        }
        [HttpGet("GetProjectProcessStaffRelatedSettings")]
        public async Task<IActionResult> GetProjectProcessStaffRelatedSettings(Guid projectId)
        {
            return Ok(await _processService.GetProjectProcessStaffRelatedSettings(projectId));
        }
        //[HttpPost("SaveProjectProcessStaffRelated")]
        //public async Task<IActionResult> SaveProjectProcessStaffRelated([FromBody]List<ProcessStaffRelatedSettingsVm> list)
        //{
        //    return Ok(await _processService.SaveProjectProcessStaffRelated(list));
        //}
        [HttpGet("GetProjectProcessStaffRelatedList")]
        public async Task<IActionResult> GetProjectProcessStaffRelatedList(Guid projectId)
        {
            return Ok(await _processService.GetProjectProcessStaffRelatedList(projectId));
        }

        [HttpPost("SaveProjectProcessStaffRelated")]
        public async Task<IActionResult> SaveProjectProcessStaffRelated([FromBody]List<ProcessStaffRelatedVm> list)
        {
            return Ok(await _processService.SaveProjectProcessStaffRelated(list));
        }

    }
}
