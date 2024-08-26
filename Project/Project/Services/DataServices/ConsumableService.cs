using Project.Common;
using ProjectViewModels;
using RestSharp;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Services.DataServices
{
    public class ConsumableService
    {
        private readonly IRestClient restClient;
        public ConsumableService(IRestClient client)
        {
            restClient = client;
        }

        public async Task<PaginatedList<ConsumableTypeVm>> PaginatedConsumableType(CommonReqs req)
        {
            return await RestClientHelper.ExecuteRequestAsync<PaginatedList<ConsumableTypeVm>>(restClient, Method.Post, ApiSettings.PaginatedConsumableType, body: req);
        }
        public async Task<List<ConsumableTypeVm>> GetConsumableTypeList(CommonReqs req)
        {
            return await RestClientHelper.ExecuteRequestAsync<List<ConsumableTypeVm>>(restClient, Method.Post, ApiSettings.GetConsumableTypeList, body: req);
        }
        public async Task<ConsumableTypeVm> GetConsumableTypeById(Guid consumableTypeId)
        {
            return await RestClientHelper.ExecuteRequestAsync<ConsumableTypeVm>(restClient, Method.Get, ApiSettings.GetConsumableTypeById, queryParameters: new Dictionary<string, string> {
                { nameof(consumableTypeId), consumableTypeId.ToString()}});
        }
        public async Task<bool> SaveConsumableType(ConsumableTypeVm vm)
        {
            return await RestClientHelper.ExecuteRequestAsync<bool>(restClient, Method.Post, ApiSettings.SaveConsumableType, body: vm);
        }
        public async Task<PaginatedList<ConsumableVm>> PaginatedConsumable(ConsumableReqs req)
        {
            return await RestClientHelper.ExecuteRequestAsync<PaginatedList<ConsumableVm>>(restClient, Method.Post, ApiSettings.PaginatedConsumable, body: req);
        }
        public async Task<List<ConsumableVm>> GetConsumableList(ConsumableReqs req)
        {
            return await RestClientHelper.ExecuteRequestAsync<List<ConsumableVm>>(restClient, Method.Post, ApiSettings.GetConsumableList, body: req);
        }
        public async Task<ConsumableVm> GetConsumableById(Guid consumableId)
        {
            return await RestClientHelper.ExecuteRequestAsync<ConsumableVm>(restClient, Method.Get, ApiSettings.GetConsumableById, queryParameters: new Dictionary<string, string>
            {
                { nameof(consumableId), consumableId.ToString() }
            });
        }
        public async Task<bool> SaveConsumable(ConsumableVm vm)
        {
            return await RestClientHelper.ExecuteRequestAsync<bool>(restClient, Method.Post, ApiSettings.SaveConsumable, body: vm);
        }

        public async Task<bool> SaveStockOutBound(StockOutBoundVm vm)
        {
            return await RestClientHelper.ExecuteRequestAsync<bool>(restClient, Method.Post, ApiSettings.SaveStockOutBound, body: vm);
        }
        public async Task<bool> SaveStockInBound(StockInBoundVm vm)
        {
            return await RestClientHelper.ExecuteRequestAsync<bool>(restClient, Method.Post, ApiSettings.SaveStockInBound, body: vm);
        }

        public async Task<PaginatedList<ConsumableBoundDto>> PaginatedConsumableBound(ConsumableReqs req)
        {
            return await RestClientHelper.ExecuteRequestAsync<PaginatedList<ConsumableBoundDto>>(restClient, Method.Post, ApiSettings.PaginatedConsumableBound, body: req);
        }

        public async Task<StockInBoundVm> GetStockInBoundById(Guid id)
        {
            return await RestClientHelper.ExecuteRequestAsync<StockInBoundVm>(restClient, Method.Get, ApiSettings.GetStockInBoundById, queryParameters: new Dictionary<string, string>
            {
                { nameof(id), id.ToString() }
            });
        }
        public async Task<StockOutBoundVm> GetStockOutBoundById(Guid id)
        {
            return await RestClientHelper.ExecuteRequestAsync<StockOutBoundVm>(restClient, Method.Get, ApiSettings.GetStockOutBoundById, queryParameters: new Dictionary<string, string>
            {
                { nameof(id), id.ToString() }
            });
        }
    }
}
