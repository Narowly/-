using Project.Common;
using ProjectViewModels;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Services.DataServices
{
    public class ApplicationService
    {
        private readonly IRestClient restClient;
        public ApplicationService(IRestClient restClient)
        {
            this.restClient = restClient;
        }

        public async Task<bool> SaveApplication(ApplicationVm vm)
        {
            return await RestClientHelper.ExecuteRequestAsync<bool>(restClient, Method.Post, ApiSettings.SaveApplication, body: vm);
        }

        public async Task<PaginatedList<ApplicationVm>> PaginatedApplication(ApplicationReq req)
        {
            return await RestClientHelper.ExecuteRequestAsync<PaginatedList<ApplicationVm>>(restClient, Method.Post, ApiSettings.PaginatedApplication, body: req);
        }

        public async Task<ApplicationVm?> GetApplicationById(Guid applicationId, bool includeProject = false)
        {
            var parameters = new Dictionary<string, string>
            {
                { nameof(applicationId), applicationId.ToString() }
            };
            if (includeProject)
            {
                parameters.Add(nameof(includeProject), true.ToString());
            }
            return await RestClientHelper.ExecuteRequestAsync<ApplicationVm?>(restClient, Method.Get, ApiSettings.GetApplicationById, queryParameters: parameters);
        }

        public async Task<bool> Approve(ApplicationVm vm)
        {
            return await RestClientHelper.ExecuteRequestAsync<bool>(restClient, Method.Post, ApiSettings.ApproveApplication, body: vm);
        }

        public async Task<bool> ProcessDeviceApplication(ApplicationProcessdReq req)
        {
            return await RestClientHelper.ExecuteRequestAsync<bool>(restClient, Method.Post, ApiSettings.ProcessDeviceApplication, body: req);
        }
        public async Task<bool> ProcessStaffApplication(ApplicationProcessdReq req)
        {
            return await RestClientHelper.ExecuteRequestAsync<bool>(restClient, Method.Post, ApiSettings.ProcessStaffApplication, body: req);
        }
        public async Task<bool> ProcessConsumableApplication(ApplicationProcessdReq req)
        {
            return await RestClientHelper.ExecuteRequestAsync<bool>(restClient, Method.Post, ApiSettings.ProcessConsumableApplication, body: req);
        }
    }
}
