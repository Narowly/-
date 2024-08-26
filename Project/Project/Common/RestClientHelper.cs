using Newtonsoft.Json;
using Project.Model;
using ProjectViewModels;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Common
{
    internal class RestClientHelper
    {
        public static RestRequest RequestBulder(Method httpMethod, string url, object? requestBody = null, string? bearerToken = null, IDictionary<string, string>? queryParameters = null, IEnumerable<string>? files = null)
        {
            var request = new RestRequest(url, httpMethod);

            // 如果提供了requestBody且HTTP方法是POST或PUT，则添加请求体  
            if (requestBody != null && (httpMethod == Method.Post || httpMethod == Method.Put))
            {
                request.AddJsonBody(requestBody);
            }

            // 如果提供了queryParameters且HTTP方法是GET，则添加到URL中  
            if (queryParameters != null)
            {
                foreach (var param in queryParameters)
                {
                    request.AddQueryParameter(param.Key, param.Value);
                }
            }

            if (files != null && files.Count() > 0)
            {
                request.AlwaysMultipartFormData = true;
                foreach (var filePath in files)
                {
                    request.AddFile("files", filePath, "text/plain");
                }
            }

            // 如果提供了Bearer Token，则添加到Authorization头部  
            if (!string.IsNullOrEmpty(bearerToken))
            {
                request.AddHeader("Authorization", $"Bearer {bearerToken}");
            }

            return request;
        }
        public async static Task<T> ExecuteRequestAsync<T>(IRestClient restClient, Method method, string resource, object? body = null, IDictionary<string, string>? queryParameters = null, IEnumerable<string>? files = null)
        {
            var request = RequestBulder(method, resource, body, bearerToken: TokenStorage.RetrieveToken(), queryParameters,files);
            var response = await restClient.ExecuteAsync<T>(request);
            return await response.HandleResponseAsync<T>();   
        }
    }
    internal static class RestResponseExtensions
    {
        public static async Task<T> HandleResponseAsync<T>(this RestResponse response)
        {
            return await Task.Run(() =>
            {
                if (response.IsSuccessStatusCode)
                {
                    var apiResponse = JsonConvert.DeserializeObject<ApiResponse<T>>(response.Content);
                    return apiResponse.Data;
                }
                else if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                {
                    throw new UnauthorizedAccessException("用户验证失败，请重新登录。");
                }
                else
                {
                    throw new Exception(response.ErrorException?.Message ?? "系统服务错误");
                }
            });
            
        }
    }
}
