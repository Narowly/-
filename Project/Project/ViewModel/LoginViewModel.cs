using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using RestSharp;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Project.Model;
using Newtonsoft.Json;
using ProjectViewModels;
using Newtonsoft.Json.Linq;
using Project.Common;
using CommunityToolkit.Mvvm.Messaging;
using Project.Services.DataServices;
using MessageBox = HandyControl.Controls.MessageBox;

namespace Project.ViewModel
{
    public partial class LoginViewModel : ObservableRecipient
    {
        private UserService _userService;
      
        private string? _username;
        public string? Username
        {
            get => _username;
            set => SetProperty(ref _username, value);
        }

        private string? _password;
        public string? Password
        {
            get => _password;
            set => SetProperty(ref _password, value);
        }
        public LoginViewModel(UserService userService)
        {
            _userService = userService;
        }
        [RelayCommand]
        private async Task Login()
        {
            if(Username == null || Password == null)
            {
                MessageBox.Warning("用户名密码不能为空","提示");
                return;
            }
            await _userService.Login(Username, Password);
            WeakReferenceMessenger.Default.Send(string.Empty, MessageToken.OpenMainWindow);            
        }

    }
}
