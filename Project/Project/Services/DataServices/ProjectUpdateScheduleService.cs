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
    public class ProjectUpdateScheduleService
    {
        private readonly IRestClient restClient;
        public ProjectUpdateScheduleService(IRestClient client)
        {
            this.restClient = client;
        }
        public async Task<bool> AddProjectUpdateSchedule(ProjectUpdateScheduleVm vm)
        {
            return await RestClientHelper.ExecuteRequestAsync<bool>(restClient, Method.Post, ApiSettings.AddProjectUpdateSchedule, body: vm);
        }
        public async Task<PaginatedList<ProjectUpdateScheduleVm>> PaginatedProjectUpdateSchedule(ProjectReqs req)
        {
            return await RestClientHelper.ExecuteRequestAsync<PaginatedList<ProjectUpdateScheduleVm>>(restClient, Method.Post, ApiSettings.PaginatedProjectUpdateSchedule, body: req);
        }
    }
}
