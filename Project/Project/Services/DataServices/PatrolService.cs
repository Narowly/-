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
    public class PatrolService
    {
        private readonly IRestClient restClient;
        public PatrolService(IRestClient restClient)
        {
            this.restClient = restClient;
        }
        public async Task<PaginatedList<ProjectPatrolVm>> PaginatedPatrol(ProjectWithStaffReq req)
        {
            return await RestClientHelper.ExecuteRequestAsync<PaginatedList<ProjectPatrolVm>>(restClient, Method.Post, ApiSettings.PaginatedPatrol, body: req);
        }
        public async Task<List<ProjectPatrolVm>> GetPatrolList(ProjectWithStaffReq req)
        {
            return await RestClientHelper.ExecuteRequestAsync<List<ProjectPatrolVm>>(restClient, Method.Post, ApiSettings.GetPatrolList, body: req);
        }
        public async Task<bool> SavePatrol(ProjectPatrolVm vm)
        {
            return await RestClientHelper.ExecuteRequestAsync<bool>(restClient, Method.Post, ApiSettings.SavePatrol, body: vm);
        }
        public async Task<ProjectPatrolVm?> GetPatrolById(long id)
        {
            return await RestClientHelper.ExecuteRequestAsync<ProjectPatrolVm?>(restClient, Method.Get, ApiSettings.GetPatrolById,
                queryParameters: new Dictionary<string, string>
                {
                    { nameof(id),id.ToString() }
                });
        }

        public async Task<bool> SavePatrolByExcel(List<ProjectPatrolExcelVm> list)
        {
            return await RestClientHelper.ExecuteRequestAsync<bool>(restClient, Method.Post, ApiSettings.SavePatrolByExcel, body: list);
        }
    }
}
