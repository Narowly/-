using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Project.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using ProjectViewModels;
using Project.Common;
using CommunityToolkit.Mvvm.Messaging;
using Project.Services.DataServices;
using System.Collections.ObjectModel;

namespace Project.ViewModel
{
    public partial class ContractNameTbViewModel : ObservableObject
    {
        private readonly ContractNameTbService _contractNameTbService;
        private readonly DictService _dictService;
        private readonly StaffService _staffService;
        private List<string>? _contractNames;
        public List<string>? ContractNames
        {
            get => _contractNames;
            set => SetProperty(ref _contractNames, value);
        }
        private List<ContractVm>? _contracts;
        public List<ContractVm>? Contracts
        {
            get => _contracts;
            set => SetProperty(ref _contracts, value);
        }
        private string? _searchText;
        public string? SearchText
        {
            get => _searchText;
            set
            {
                SetProperty(ref _searchText, value);
                if (_searchText?.Length > 3)
                    SearchContract();
            }
        }
        private DateTime? _startDate;
        public DateTime? StartDate
        {
            get => _startDate;
            set => SetProperty(ref _startDate, value);
        }
        private DateTime? _endDate;
        public DateTime? EndDate
        {
            get => _endDate;
            set => SetProperty(ref _endDate, value);
        }

        [ObservableProperty]
        public StatusComboBoxModel? selectedStatus;

        private ObservableCollection<StatusComboBoxModel> bindingStatus;
        public ObservableCollection<StatusComboBoxModel> BindingStatus
        {
            get => bindingStatus;
            set => SetProperty(ref bindingStatus, value);
        }
        

        [ObservableProperty]
        public ObservableCollection<StaffVm>? managerList;
        [ObservableProperty]
        public ObservableCollection<StaffVm>? bindingManagerList;
        [ObservableProperty]
        public StaffVm selectedManager;
        
        private string _searchManagerName;
        public string SearchManagerName
        {
            get => _searchManagerName;
            set
            {
                SetProperty(ref _searchManagerName, value);
                if (string.IsNullOrWhiteSpace(_searchManagerName))
                    BindingManagerList = ManagerList;
                else if (_searchManagerName?.Length > 1)
                    BindingManagerList = new ObservableCollection<StaffVm>(ManagerList?.Where(m => m.StaffName.Contains(SearchManagerName)).ToList());

            }
        }
        public ICommand SearchCmd { get; }
        public Task InitTask;

        public ContractNameTbViewModel(ContractNameTbService service, DictService dictService, StaffService staffService)
        {
            _contractNameTbService = service;
            _dictService = dictService;
            _staffService = staffService;
            InitTask = Task.Run(() => LoadDataAsync());            
            SearchCmd = new RelayCommand(SendSearchReq);
            BindingStatus = new ObservableCollection<StatusComboBoxModel>
            {
                new StatusComboBoxModel { Name = "未立项" }, new StatusComboBoxModel { Name = "已立项" }
            };
            selectedStatus = bindingStatus[0];
        }
        private async Task LoadDataAsync()
        {
            await GetContractNames();
            //await GetStatus();
            await GetManagerList();
        }
        
        private void SendSearchReq()
        {

            var req = _contractNameTbService.CreateRequest(StartDate, EndDate, SearchText, SelectedManager?.StaffId);            
            if (SelectedStatus?.Name == "已立项")
            {
                req.Status = 1;
                WeakReferenceMessenger.Default.Send(req, MessageToken.SearchProject);
            }
            else
            {
                req.Status = 0;
                WeakReferenceMessenger.Default.Send(req, MessageToken.SearchContract);
            }
            
        }


        private async Task GetContractNames()
        {
            try
            {
                var response = await _contractNameTbService.GetContractNames();
                Contracts = response;
            }
            catch (UnauthorizedAccessException)
            {
                HandyControl.Controls.MessageBox.Show("登录失败，请检查您的用户名和密码。", "登录错误", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (Exception ex)
            {
                HandyControl.Controls.MessageBox.Show($"{ex.Message}", "系统错误", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void SearchContract()
        {
            if (string.IsNullOrWhiteSpace(SearchText)) return;
            var result = Contracts?.Where(m => m.ContractName.Contains(SearchText) || m.ContractNumber.Contains(SearchText)).Select(m => m.ContractName).Distinct().ToList();
            ContractNames = result;
        }

        private async Task GetStatus()
        {
            var list = await _dictService.GetDictDataByTypeName(ApiSettings.ProjectStatus);
            //StatusList = new ObservableCollection<DictDataVm>(list);
        }

        private async Task GetManagerList()
        {
            var list = await _staffService.GetStaffListByDuty(ApiSettings.ProjectManagerDuty);
            ManagerList = new ObservableCollection<StaffVm>(list);
        }
        
    }

    //public class StatusComboBoxModel
    //{
    //    public string Name { get; set; } = null!;
    //    public string Value { get; set; } = null!;
    //}
}
