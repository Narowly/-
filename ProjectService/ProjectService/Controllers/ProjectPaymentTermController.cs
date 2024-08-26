using Microsoft.AspNetCore.Mvc;
using ProjectService.Services;
using ProjectService.ViewModels;
using ProjectViewModels;

namespace ProjectService.Controllers
{
    public class ProjectPaymentTermController : ControllerBase
    {
        private readonly ProjectPaymentTermService _termService;
        public ProjectPaymentTermController(ProjectPaymentTermService termService)
        {
            _termService = termService;
        }
        [HttpGet("ProjectPaymentTermList")]
        public async Task<IActionResult> GetProjectPaymentTermList(Guid projectId)
        {
            var list = await _termService.GetProjectPaymentTermList(projectId);
            return Ok(list.Select(m => m.ToViewModel()).ToList());
        }
        [HttpPost("SaveProjectPaymentTerm")]
        public async Task<IActionResult> SaveProjectPaymentTerm(ProjectPaymentTermVm vm)
        {
            return Ok(await _termService.SaveProjectPaymentTerm(vm));
        }
    }
}
