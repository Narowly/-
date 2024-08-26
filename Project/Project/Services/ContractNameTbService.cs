using Newtonsoft.Json;
using Project.Common;
using Project.Model;
using ProjectViewModels;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Services
{
    public class ContractNameTbService
    {
        private readonly IRestClient restClient;
        public ContractNameTbService(IRestClient restClient)
        {
            this.restClient = restClient;
        }
        public async Task<List<ContractVm>> GetContractNames()
        {
            //var queryParam = new Dictionary<string, string> { { "word", word } };
            var request = RestClientHelper.RequestBulder(Method.Get, ApiSettings.GetContractName, bearerToken: TokenStorage.RetrieveToken());
            var response = await restClient.ExecuteAsync(request);
            return await response.HandleResponseAsync<List<ContractVm>>();
            //if (response.IsSuccessful)
            //{
            //    var apiResponse = JsonConvert.DeserializeObject<ApiResponse<List<ContractVm>>>(response.Content);
            //    return apiResponse.Data;
            //}
            //else if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
            //{
            //    throw new UnauthorizedAccessException("用户验证失败");
            //}
            //else
            //{
            //    throw new Exception(response.ErrorException.Message);
            //}
        }

        public ProjectReqs CreateRequest(DateTime? startDate, DateTime? endDate, string? content, Guid? managerId)
        {
            return new ProjectReqs
            {
                StartDate = startDate,
                EndDate = endDate,
                Content = content,
                ProjectManagerId = managerId
            };
        }
    }
    
}
