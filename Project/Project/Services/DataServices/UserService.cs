using CommunityToolkit.Mvvm.Messaging;
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
using System.Windows;

namespace Project.Services.DataServices
{
    public class UserService
    {
        private readonly IRestClient _restClient;
        public UserService(IRestClient restClient)
        {
            _restClient = restClient;
        }
        public async Task Login(string username, string password)
        {
            var result = await RestClientHelper.ExecuteRequestAsync<LoginResVm>(_restClient, Method.Post, ApiSettings.Login, body: new { Username = username, Password = password });
            TokenStorage.SaveToken(result.Token);            
        }
    }
}
