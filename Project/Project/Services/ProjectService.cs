using Newtonsoft.Json;
using Project.Common;
using ProjectViewModels;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Project.Model;

namespace Project.Services
{
    public class ProjectService
    {
        private readonly IRestClient restClient;

        public ProjectService(IRestClient restClient)
        {
            this.restClient = restClient;
        }

        public async Task<PaginatedList<ContractVm>> GetContractsAsync(ProjectReqs req)
        {
            var request = RestClientHelper.RequestBulder(Method.Post, ApiSettings.GetContracts, req, TokenStorage.RetrieveToken());
          
            var response = await restClient.ExecuteAsync(request);
            return await response.HandleResponseAsync<PaginatedList<ContractVm>>();
        }

        public async Task<PaginatedList<ProjectVm>> GetProjectsAsync(ProjectReqs req)
        {
            return await RestClientHelper.ExecuteRequestAsync<PaginatedList<ProjectVm>>(restClient, Method.Post, ApiSettings.PaginatedSearchProject, body: req);
        }

        public async Task<ProjectVm> GetProjectById(Guid id)
        {
            return await RestClientHelper.ExecuteRequestAsync<ProjectVm>(restClient, Method.Get, ApiSettings.GetProjectById,
                queryParameters: new Dictionary<string, string>
                {
                    { nameof(id), id.ToString() }
                });
        }

        public async Task<bool> UpdateStartDate(ProjectVm vm)
        {
            return await RestClientHelper.ExecuteRequestAsync<bool>(restClient, Method.Post, ApiSettings.UpdateProjectStartDate, body: vm);
        }

        public async Task<bool> UpdateProjectProcess(ProjectVm vm)
        {
            return await RestClientHelper.ExecuteRequestAsync<bool>(restClient, Method.Post, ApiSettings.UpdateProjectProcess, body:vm);
        }

        public async Task<List<CustomerContactVm>> GetCustomerContactList(Guid customerId)
        {
            var query = new Dictionary<string, string> { { "id", customerId.ToString() } };
            var request = RestClientHelper.RequestBulder(Method.Get, ApiSettings.CustomerContactList, bearerToken: TokenStorage.RetrieveToken(), queryParameters: query);

            var response = await restClient.ExecuteAsync(request);
            return await response.HandleResponseAsync<List<CustomerContactVm>>();
        }

        public async Task<List<ProjectAutoCompleteModel>> LoadProjectNames()
        {
            return await RestClientHelper.ExecuteRequestAsync<List<ProjectAutoCompleteModel>>(restClient, Method.Get, ApiSettings.LoadProjectNames);
        }

        public async Task<List<ProjectProcessVm>> GetProjectProcesses(Guid projectId)
        {
            return await RestClientHelper.ExecuteRequestAsync<List<ProjectProcessVm>>(restClient, Method.Get, ApiSettings.GetProjectProcesses,
                queryParameters: new Dictionary<string, string> { { nameof(projectId), projectId.ToString() } });
        }

        public async Task<bool> UpdateProjectPlanData(ProjectVm vm)
        {
            return await RestClientHelper.ExecuteRequestAsync<bool>(restClient, Method.Post, ApiSettings.UpdateProjectPlanData, body: vm);
        }

        public async Task<PaginatedList<ProjectVm>> GetProjectDynamics(ProjectReqs req)
        {
            return await RestClientHelper.ExecuteRequestAsync<PaginatedList<ProjectVm>>(restClient, Method.Post, ApiSettings.GetProjectDynamics, body: req);
        }

        public async Task<bool> AcceptanceProject(AcceptanceReq req)
        {
            return await RestClientHelper.ExecuteRequestAsync<bool>(restClient, Method.Post, ApiSettings.AcceptanceProject, body: req);
        }

        public async Task<List<ContractVm>> GetContractNames()
        {
            return await RestClientHelper.ExecuteRequestAsync<List<ContractVm>>(restClient, Method.Get, ApiSettings.GetContractName);
        }
        public async Task<PaginatedList<ProjectVm>> PaginatedOpenningProject(ProjectReqs req)
        {
            return await RestClientHelper.ExecuteRequestAsync<PaginatedList<ProjectVm>>(restClient, Method.Post, ApiSettings.PaginatedOpenningProject, body: req);
        }
        public async Task<PaginatedList<ProjectVm>> ApproachingPlanDateSearch(ProjectReqs req)
        {
            return await RestClientHelper.ExecuteRequestAsync<PaginatedList<ProjectVm>>(restClient, Method.Post, ApiSettings.ApproachingPlanDateSearch, body: req);
        }
        public async Task<PaginatedList<ProjectVm>> DelayedPlanDateSearch(ProjectReqs req)
        {
            return await RestClientHelper.ExecuteRequestAsync<PaginatedList<ProjectVm>>(restClient, Method.Post, ApiSettings.DelayedPlanDateSearch, body: req);
        }
        public async Task<PaginatedList<ProjectVm>> ProcessWarningDateSearch(ProjectReqs req)
        {
            return await RestClientHelper.ExecuteRequestAsync<PaginatedList<ProjectVm>>(restClient, Method.Post, ApiSettings.ProcessWarningDateSearch, body: req);
        }
        public async Task<ProjectVm> GetSimpleProjectById(Guid id)
        {
            return await RestClientHelper.ExecuteRequestAsync<ProjectVm>(restClient, Method.Get, ApiSettings.GetSimpleProjectById, queryParameters: new Dictionary<string, string>
            {
                { nameof(id), id.ToString() }
            });
        }
    }
}
