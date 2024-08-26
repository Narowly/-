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
    public class DailyProcessService
    {
        private readonly IRestClient restClient;
        public DailyProcessService(IRestClient restClient)
        {
            this.restClient = restClient;
        }

        public async Task<List<ProjectDailyProcessHistoryVm>?> DailyProcessHistory(Guid projectId)
        {
            return await RestClientHelper.ExecuteRequestAsync<List<ProjectDailyProcessHistoryVm>?>(restClient, Method.Get, ApiSettings.GetProjectDailyProcessHistory, queryParameters: new Dictionary<string, string> { { nameof(projectId), projectId.ToString() } });
        }

        public async Task<bool> SaveProjectDailyProcess(List<ProjectDailyProcessHistoryVm> history)
        {
            return await RestClientHelper.ExecuteRequestAsync<bool>(restClient, Method.Post, ApiSettings.SaveProjectDailyProcess, body: history);
        }

        public async Task<bool> UpdateProjectDailyProcess(List<ProjectDailyProcessVm> list)
        {
            return await RestClientHelper.ExecuteRequestAsync<bool>(restClient, Method.Post, ApiSettings.UpdateProjectDailyProcess, body: list);
        }

        public async Task<bool> RemoveProjectDailyProcess(List<ProjectDailyProcessVm> list)
        {
            return await RestClientHelper.ExecuteRequestAsync<bool>(restClient, Method.Post, ApiSettings.RemoveProjectDailyProcess, body: list);
        }
    }
}
