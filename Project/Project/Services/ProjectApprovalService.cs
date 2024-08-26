using Newtonsoft.Json;
using Project.Common;
using Project.Model;
using Project.Services.DataServices;
using ProjectViewModels;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Services
{
    public class ProjectApprovalService
    {
        private readonly IRestClient restClient;

        public ProjectApprovalService(IRestClient restClient)
        {
            this.restClient = restClient;
        }

        public async Task<ContractVm> GetContractById(Guid id)
        {
            var queryParam = new Dictionary<string, string> { { "id", id.ToString() } };
            var request = RestClientHelper.RequestBulder(Method.Get, ApiSettings.GetContractById, bearerToken: TokenStorage.RetrieveToken(), queryParameters: queryParam);
            var response = await restClient.ExecuteAsync(request);
            return await response.HandleResponseAsync<ContractVm>();
        }

        

        public async Task<ProjectVm?> SaveProject(ProjectVm vm)
        {
            return await RestClientHelper.ExecuteRequestAsync<ProjectVm?>(restClient, Method.Post, ApiSettings.SaveProject, body: vm);
        }
        
    }
}
