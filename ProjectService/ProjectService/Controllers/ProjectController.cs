using Microsoft.AspNetCore.Mvc;
using ProjectService.Services;
using ProjectService.ViewModels;
using ProjectViewModels;
using static PaginationExtensions;
namespace ProjectService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectController : ControllerBase
    {
        private readonly ProjectAllService _projectService;
        private readonly DictService _dictService;

        public ProjectController(ProjectAllService projectService, DictService dictService)
        {
            _projectService = projectService;
            _dictService = dictService;
        }

        [HttpPost("GetContracts")]
        public async Task<IActionResult> GetContracts([FromBody] ProjectReqs req)
        {
            var contracts = await _projectService.GetNotInitiatedContracts(req);
            return Ok(contracts.ToViewModelPaginatedList(m => m?.ToViewModel()));
        }

        [HttpGet("GetContractName")]
        public async Task<IActionResult> GetContractName()
        {
            var contracts = await _projectService.GetContractNames();
            return Ok(contracts.Select(m => m.ToViewModel()).ToList());
        }

        [HttpGet("GetContractById")]
        public async Task<IActionResult> GetContractById(Guid id)
        {
            var contract = await _projectService.GetContractById(id);
            return Ok(contract?.ToViewModel());
        }

        [HttpGet("CustomerContactList")]
        public async Task<IActionResult> CustomerContactList(Guid id)
        {
            var contactList = await _projectService.GetCustomerContactList(id);
            return Ok(contactList.Select(m => m.ToViewModel()));
        }
        [HttpPost("SaveProject")]
        public async Task<IActionResult> SaveProject([FromBody] ProjectVm vm)
        {
            var statusDictType = await _dictService.GetDictTypeByName("ProjectStatus");
            var statusDict = statusDictType?.DictData.FirstOrDefault(m => m.DictLabel == "已立项");
            var result = await _projectService.SaveProject(vm, statusDict);
            return Ok(result?.ToViewModel());
        }

        [HttpPost("PaginatedSearchProject")]
        public async Task<IActionResult> PaginatedSearchProject([FromBody] ProjectReqs req)
        {
            var result = await _projectService.PaginatedSearchProject(req);
            return Ok(result.ToViewModelPaginatedList(m => m.ToViewModel()));
        }

        [HttpGet("GetProjectById")]
        public async Task<IActionResult> GetProjectById(Guid id)
        {
            var vm = (await _projectService.GetProjectById(id))?.ToViewModel(includeProjectProcess: true);

            return Ok(vm);
        }
        
        [HttpGet("GetSimpleProjectById")]
        public async Task<IActionResult> GetSimpleProjectById(Guid id)
        {
            var project = await _projectService.GetProjectById(id);
            return Ok(project?.ToSimpleViewModel());
        }
        [HttpPost("UpdateProjectStartDate")]
        public async Task<IActionResult> UpdateProjectStartDate(ProjectVm vm)
        {
            return Ok(await _projectService.UpdateProjectStartDate(vm));
        }
        [HttpPost("UpdateProjectProcess")]
        public async Task<IActionResult> UpdateProjectProcess(ProjectVm vm)
        {
            return Ok(await _projectService.UpdateProjectProcess(vm));
        }
        [HttpGet("LoadProjectNames")]
        public async Task<IActionResult> LoadProjectNames()
        {
            var projectList = await _projectService.LoadProjectNames();
            //var result = projectList.Select(m => new ProjectAutoCompleteModel { Id = m.ProjectId, Name = string.Format("{0}|{1}", m.Contract?.ContractNumber, m.ProjectName), Number = m.Contract.ContractNumber }).ToList();
            return Ok(projectList);
        }
        [HttpPost("UpdateProjectPlanData")]
        public async Task<IActionResult> UpdateProjectPlanData([FromBody] ProjectVm vm)
        {
            return Ok(await _projectService.UpdateProjectPlanData(vm));
        }
        [HttpPost("GetProjectDynamics")]
        public async Task<IActionResult> GetProjectDynamics([FromBody] ProjectReqs req)
        {
            var list = await _projectService.PaginatedSearchProject(req);
            var result = list.ToViewModelPaginatedList(m => m.ToViewModel(includeProjectProcess: true));
            return Ok(result);
        }
        [HttpPost("AcceptanceProject")]
        public async Task<IActionResult> AcceptanceProject([FromBody] AcceptanceReq req)
        {
            return Ok(await _projectService.AcceptanceProject(req));
        }
        [HttpPost("PaginatedOpenningProject")]
        public async Task<IActionResult> PaginatedOpenningProject([FromBody] ProjectReqs req)
        {
            var statusDictType = await _dictService.GetDictTypeByName("ProjectStatus");
            var statusDict = statusDictType?.DictData.FirstOrDefault(m => m.DictLabel == "已开工");
            req.Status = statusDict.DictCode;
            var result = await _projectService.PaginatedSearchProject(req);
            return Ok(result.ToViewModelPaginatedList(m => m.ToSimpleViewModel()));
        }
        [HttpPost("ApproachingPlanDateSearch")]
        public async Task<IActionResult> ApproachingPlanDateSearch([FromBody] ProjectReqs req)
        {
            return Ok((await _projectService.ApproachingPlanDateSearch(req)).ToViewModelPaginatedList(m => m.ToSimpleViewModel()));
        }

        [HttpPost("DelayedPlanDateSearch")]
        public async Task<IActionResult> DelayedPlanDateSearch([FromBody] ProjectReqs req)
        {
            return Ok((await _projectService.DelayedPlanDateSearch(req)).ToViewModelPaginatedList(m => m.ToSimpleViewModel()));
        }

        [HttpPost("ProcessWarningDateSearch")]
        public async Task<IActionResult> ProcessWarningDateSearch([FromBody] ProjectReqs req)
        {
            return Ok((await _projectService.ProcessWarningDateSearch(req)).ToViewModelPaginatedList(m => m.ToSimpleViewModel()));
        }

    }
}
