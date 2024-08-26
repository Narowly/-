using Project.Common;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectViewModels;
using System.Collections.ObjectModel;

namespace Project.Services.DataServices
{
    public class ProjectBonusService
    {
        private readonly IRestClient restClient;
        public ProjectBonusService(IRestClient restClient)
        {
            this.restClient = restClient;
        }
        public async Task<ObservableCollection<ProjectBonusSettingsVm>?> GetBonusList(Guid projectId)
        {
            return await RestClientHelper.ExecuteRequestAsync<ObservableCollection<ProjectBonusSettingsVm>?>(restClient, Method.Get, ApiSettings.GetBonusList, queryParameters:
                new Dictionary<string, string>
                {
                    { nameof(projectId), projectId.ToString() }
                });
        }

        public async Task<bool> SaveBonus(List<ProjectBonusVm> list)
        {
            return await RestClientHelper.ExecuteRequestAsync<bool>(restClient, Method.Post, ApiSettings.SaveBonus, body: list);
        }

        public async Task<ProjectBonusExVm> GetBonusEx(Guid projectId)
        {
            return await RestClientHelper.ExecuteRequestAsync<ProjectBonusExVm>(restClient, Method.Get, ApiSettings.GetBonusEx, queryParameters: new Dictionary<string, string>
            {
                { nameof(projectId),projectId.ToString() }
            });
        }

        public async Task<bool> SaveBonusEx(ProjectBonusExVm vm)
        {
            return await RestClientHelper.ExecuteRequestAsync<bool>(restClient, Method.Post, ApiSettings.SaveBonusEx, body: vm);
        }

        public async Task<List<StaffAchievementBonusVm>> AchievementBonusCalculate(AchievementBonusCalculateReq req)
        {
            return await RestClientHelper.ExecuteRequestAsync<List<StaffAchievementBonusVm>>(restClient, Method.Post, ApiSettings.AchievementBonusCalculate, body: req);
        }

        public async Task<ProjectAchievementBonusVm?> ProjectBonusCalculate(Guid projectId)
        {
            return await RestClientHelper.ExecuteRequestAsync<ProjectAchievementBonusVm?>(restClient, Method.Get, ApiSettings.ProjectBonusCalculate, queryParameters: new Dictionary<string, string>
            {
                { nameof(projectId),projectId.ToString() } 
            });
        }
    }
}
