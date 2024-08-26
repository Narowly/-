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
    public class EarlyWarningService
    {
        private readonly IRestClient restClient;
        public EarlyWarningService(IRestClient restClient)
        {
            this.restClient = restClient;
        }

        public async Task<bool> SaveProjectEarlyWarnings(List<ProjectEarlyWarningVm> list)
        {
            return await RestClientHelper.ExecuteRequestAsync<bool>(restClient, Method.Post, ApiSettings.SaveProjectEarlyWarnings, body: list);
        }

        public async Task<EarlyWarningVm> GetProjectEarlyWarnings(Guid projectId)
        {
            return await RestClientHelper.ExecuteRequestAsync<EarlyWarningVm>(restClient, Method.Get, ApiSettings.GetProjectEarlyWarnings,
                queryParameters: new Dictionary<string, string>
                                {
                                    { nameof(projectId), projectId.ToString() }
                                });
        }

        public async Task<PaginatedList<EarlyWarningHistoryVm>> PaginatedWarningHistory(ProjectReqs req)
        {
            return await RestClientHelper.ExecuteRequestAsync<PaginatedList<EarlyWarningHistoryVm>>(restClient, Method.Post, ApiSettings.PaginatedWarningHistory, body: req);
        }
            
        public async Task<EarlyWarningHistoryVm> GetEarlyWarningHistoryById(int id)
        {
            return await RestClientHelper.ExecuteRequestAsync<EarlyWarningHistoryVm>(restClient, Method.Get, ApiSettings.GetEarlyWarningHistoryById, queryParameters: new Dictionary<string, string>
            {
                { nameof(id), id.ToString() }
            });
        }

        public async Task<bool> SaveEarlyWarningHistory(EarlyWarningHistoryVm vm)
        {
            return await RestClientHelper.ExecuteRequestAsync<bool>(restClient, Method.Post, ApiSettings.SaveEarlyWarningHistory, body: vm);
        }
    }
}
