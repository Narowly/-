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
    public class DictService
    {
        private readonly IRestClient restClient;
        public DictService(IRestClient client)
        {
            restClient = client;
        }

        public async Task<List<DictDataVm>> GetDictDataByTypeName(string typeName)
        {
            var query = new Dictionary<string, string> { { "name", typeName.ToString() } };
            var request = RestClientHelper.RequestBulder(Method.Get, ApiSettings.GetDictDataByTypeName, bearerToken: TokenStorage.RetrieveToken(), queryParameters: query);
            var response = await restClient.ExecuteAsync(request);
            return await response.HandleResponseAsync<List<DictDataVm>>();
        }

        public async Task<int?> GetDictDataId(string typeName, string label)
        {
            return await RestClientHelper.ExecuteRequestAsync<int?>(restClient, Method.Get, ApiSettings.GetDictDataId, queryParameters:
                new Dictionary<string, string> { { nameof(typeName), typeName }, { nameof(label), label } });
        }

        public async Task<DictDataVm?> GetDictData(int dictCode)
        {
            return await RestClientHelper.ExecuteRequestAsync<DictDataVm?>(restClient, Method.Get, ApiSettings.GetDictData, queryParameters:
                new Dictionary<string, string> { { nameof(dictCode), dictCode.ToString() } });
        }
    }
}
