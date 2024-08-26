using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectViewModels;
using Project.Common;

namespace Project.Services.DataServices
{
    public class ProcessService
    {
        private readonly IRestClient restClient;
        public ProcessService(IRestClient restClient)
        {
            this.restClient = restClient;
        }
        public async Task<List<ProcessVm>> GetProcessList()
        {
            return await RestClientHelper.ExecuteRequestAsync<List<ProcessVm>>(restClient, Method.Get, ApiSettings.GetProcessList);
        }
        public async Task<List<ProcessUnitVm>> GetProcessUnitList()
        {
            return await RestClientHelper.ExecuteRequestAsync<List<ProcessUnitVm>>(restClient, Method.Get, ApiSettings.GetProcessUnitList);
        }

        public async Task<List<ProcessTemplateVm>> GetProcessTemplateList()
        {
            return await RestClientHelper.ExecuteRequestAsync<List<ProcessTemplateVm>>(restClient, Method.Get, ApiSettings.GetProcessTemplateList);
        }

        public async Task<ProcessTemplateVm> GetProcessTemplate(int templateId)
        {
            return await RestClientHelper.ExecuteRequestAsync<ProcessTemplateVm>(restClient, Method.Get, ApiSettings.GetProcessTemplate,
                queryParameters: new Dictionary<string, string> { { nameof(templateId), templateId.ToString() } });
        }

        public async Task<bool> SaveProcessTemplate(ProcessTemplateVm vm)
        {
            return await RestClientHelper.ExecuteRequestAsync<bool>(restClient, Method.Post, ApiSettings.SaveProcessTemplate, body: vm);
        }

        public async Task<bool> SaveProjectProcessStaffRelated(List<ProcessStaffRelatedVm> list)
        {
            return await RestClientHelper.ExecuteRequestAsync<bool>(restClient, Method.Post, ApiSettings.SaveProjectProcessStaffRelated, body: list);
        }
        public async Task<List<ProcessStaffRelatedSettingsVm>> GetProjectProcessStaffRelatedSettings(Guid projectId)
        {
            return await RestClientHelper.ExecuteRequestAsync<List<ProcessStaffRelatedSettingsVm>>(restClient, Method.Get, ApiSettings.GetProjectProcessStaffRelatedSettings, queryParameters:
                new Dictionary<string, string> { { nameof(projectId), projectId.ToString() } });
        }
        public async Task<List<ProcessStaffRelatedVm>> GetProjectProcessStaffRelatedList(Guid projectId)
        {
            return await RestClientHelper.ExecuteRequestAsync<List<ProcessStaffRelatedVm>>(restClient, Method.Get, ApiSettings.GetProjectProcessStaffRelatedList, queryParameters:
                new Dictionary<string, string> { { nameof(projectId), projectId.ToString() } });
        }
    }
}
