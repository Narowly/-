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
    public class ConsumableAskForService
    {
        private readonly IRestClient restClient;
        public ConsumableAskForService(IRestClient restClient)
        {
            this.restClient = restClient;
        }
        public async Task<bool> SaveConsumableAskFor(ConsumableAskForVm vm)
        {
            return await RestClientHelper.ExecuteRequestAsync<bool>(restClient, Method.Post, ApiSettings.SaveConsumableAskFor, body: vm);
        }

        public async Task<PaginatedList<ConsumableAskForVm>> PaginatedConsumableAskFor(ConsumableAskForReq req)
        {
            return await RestClientHelper.ExecuteRequestAsync<PaginatedList<ConsumableAskForVm>>(restClient, Method.Post, ApiSettings.PaginatedConsumableAskFor, body: req);
        }

        public async Task<ConsumableAskForVm?> GetConsumableAskForById(Guid id)
        {
            return await RestClientHelper.ExecuteRequestAsync<ConsumableAskForVm?>(restClient, Method.Get, ApiSettings.GetConsumableAskForById, queryParameters:
                new Dictionary<string, string>
                {
                    { nameof(id), id.ToString() }
                });
        }
    }
}
