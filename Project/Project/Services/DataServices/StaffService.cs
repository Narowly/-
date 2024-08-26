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
    public class StaffService
    {
        private readonly IRestClient restClient;
        public StaffService(IRestClient restClient)
        {
            this.restClient = restClient;
        }

        public async Task<List<StaffVm>> GetStaffList()
        {
            //var request = RestClientHelper.RequestBulder(Method.Get, ApiSettings.StaffList, bearerToken: TokenStorage.RetrieveToken());
            //var response = await restClient.ExecuteAsync<List<StaffVm>>(request);
            //return await response.HandleResponseAsync<List<StaffVm>>();
            return await RestClientHelper.ExecuteRequestAsync<List<StaffVm>>(restClient, Method.Get, ApiSettings.StaffList);
        }
        public async Task<List<StaffVm>> GetStaffListWithProjectName()
        {
            return await RestClientHelper.ExecuteRequestAsync<List<StaffVm>>(restClient, Method.Get, ApiSettings.GetStaffListWithProjectName);
        }

        public async Task<List<StaffVm>> GetStaffListByDuty(List<int> dutyList)
        {
            return await RestClientHelper.ExecuteRequestAsync<List<StaffVm>>(restClient, Method.Post, ApiSettings.StaffByDuty, dutyList);
        }

        public async Task<List<StaffVm>> GetIdleStaffs(List<int> dutyList)
        {
            return await RestClientHelper.ExecuteRequestAsync<List<StaffVm>>(restClient, Method.Post, ApiSettings.IdleProjectStaff, dutyList);
        }

        public async Task<List<StaffVm>> GetProjectStaffs(Guid projectId)
        {
            return await RestClientHelper.ExecuteRequestAsync<List<StaffVm>>(restClient, Method.Get, ApiSettings.ProjectStaffByProjectId,
                queryParameters: new Dictionary<string, string> { { nameof(projectId), projectId.ToString() } }
                );
        }

        public async Task<bool> SaveProjectStaffs(ProjectVm vm)
        {
            return await RestClientHelper.ExecuteRequestAsync<bool>(restClient, Method.Post, ApiSettings.SaveProjectStaffs, body: vm);
        }

        public async Task<List<StaffVm>> SpeedupProjectStaff()
        {
            return await RestClientHelper.ExecuteRequestAsync<List<StaffVm>>(restClient, Method.Get, ApiSettings.SpeedupProjectStaff);
        }
    }
}
