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
    public class ProjectPaymentTermService
    {
        private readonly IRestClient restClient;
        public ProjectPaymentTermService(IRestClient restClient)
        {
            this.restClient = restClient;
        }

        public async Task<List<ProjectPaymentTermVm>> GetProjectPaymentTermList(Guid projectId)
        {
            return await RestClientHelper.ExecuteRequestAsync<List<ProjectPaymentTermVm>>(restClient, Method.Get, ApiSettings.ProjectPaymentTermList,
                queryParameters: new Dictionary<string, string> { { nameof(projectId), projectId.ToString() } });
        }

        public async Task<bool> SaveProjectPaymentTerm(ProjectPaymentTermVm vm)
        {
            return await RestClientHelper.ExecuteRequestAsync<bool>(restClient, Method.Post, ApiSettings.SaveProjectPaymentTerm, body:vm);
        }
    }
}
