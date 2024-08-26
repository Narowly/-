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
    public class ProjectDailyWorkService
    {
        private readonly IRestClient restClient;
        public ProjectDailyWorkService(IRestClient restClient)
        {
            this.restClient = restClient;
        }

        public async Task<bool> SaveProjectDailyWork(ProjectDailyWorkVm vm)
        {
            return await RestClientHelper.ExecuteRequestAsync<bool>(restClient, Method.Post, ApiSettings.SaveProjectDailyWork, body: vm);
        }

        public async Task<PaginatedList<ProjectDailyWorkVm>> PaginatedSearchProjectDailyWork(ProjectWithStaffReq req)
        {
            return await RestClientHelper.ExecuteRequestAsync<PaginatedList<ProjectDailyWorkVm>>(restClient, Method.Post, ApiSettings.PaginatedSearchProjectDailyWork, body: req);
        }

        public async Task<ProjectDailyWorkVm?> GetDailyWorkById(Guid id)
        {
            return await RestClientHelper.ExecuteRequestAsync<ProjectDailyWorkVm?>(restClient, Method.Get, ApiSettings.GetDailyWorkById, queryParameters: new Dictionary<string, string>
            {
                {nameof(id), id.ToString()}
            });
        }

        public async Task<PaginatedList<DailyWorkSummaryVm>> GetDailyWorkSummary(ProjectWithStaffReq req)
        {
            return await RestClientHelper.ExecuteRequestAsync<PaginatedList<DailyWorkSummaryVm>>(restClient, Method.Post, ApiSettings.GetDailyWorkSummary, body: req);
        }
        public async Task<bool> SaveBatchDailyWork(List<BatchProjectDailyWorkVm> list)
        {
            return await RestClientHelper.ExecuteRequestAsync<bool>(restClient, Method.Post, ApiSettings.SaveBatchDailyWork, body: list);
        }

        public async Task<bool> SaveExcelDailyWork(List<ProjectDailyWorkExcelVm> list)
        {
            return await RestClientHelper.ExecuteRequestAsync<bool>(restClient, Method.Post, ApiSettings.SaveExcelDailyWork, body: list);
        }
    }
}
