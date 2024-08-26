using Autofac.Core;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Project.Common;
using Project.Services;
using Project.Services.DataServices;
using Project.Views.Windows;
using ProjectViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.Serialization.Formatters;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using Project.Views.UserControls;
using HandyControl.Controls;
using System.Diagnostics;
using System.Windows.Documents;

namespace Project.ViewModel
{
    public partial class DeviceStockViewModel : ObservableObject
    {
        private IContainer? _container = App.Current.Properties[MessageToken.AppContainer] as IContainer;
        private readonly DeviceService _deviceService;
        private readonly ProjectService _projectService;
        private readonly StaffService _staffService;
        private readonly DictService _dictService;
        private DeviceReqs _req = null!;
        public DeviceReqs Req
        {
            get => _req;
            set => SetProperty(ref _req, value);
        }
        private PaginatedList<DeviceStockVm>? _paginatedList;
        public PaginatedList<DeviceStockVm>? PaginatedList
        {
            get => _paginatedList;
            set => SetProperty(ref _paginatedList, value);
        }
        private List<ProjectAutoCompleteModel> ProjectNameList = null!;
        [ObservableProperty]
        private ObservableCollection<ProjectAutoCompleteModel>? _projectNamesSource;
        private string? _searchProjectNameText;
        public string? SearchProjectNameText
        {
            get => _searchProjectNameText;
            set
            {
                SetProperty(ref _searchProjectNameText, value);
                if (!string.IsNullOrWhiteSpace(_searchProjectNameText))
                {
                    var list = ProjectNameList?.Where(m => m.Name.Contains(_searchProjectNameText) || m.Number.Contains(_searchProjectNameText)).ToList();
                    if (list != null) ProjectNamesSource = new ObservableCollection<ProjectAutoCompleteModel>(list);
                }
                else
                {
                    ProjectNamesSource = null;
                }

            }
        }
        private ProjectAutoCompleteModel? _selectedProject;
        public ProjectAutoCompleteModel? SelectedProject
        {
            get => _selectedProject;
            set => SetProperty(ref _selectedProject, value);
        }
        private List<StaffVm>? ManagerList;
        [ObservableProperty]
        private ObservableCollection<StaffVm>? bindingManagerList;
        [ObservableProperty]
        private StaffVm? selectedManager;
        private string? _searchManagerText;
        public string? SearchManagerText
        {
            get => _searchManagerText;
            set
            {
                SetProperty(ref _searchManagerText, value);
                if (!string.IsNullOrWhiteSpace(_searchManagerText))
                {
                    var list = ManagerList?.Where(m => m.StaffName.Contains(_searchManagerText)).ToList();
                    if (list != null) BindingManagerList = new ObservableCollection<StaffVm>(list);
                }
                else
                {
                    BindingManagerList = null;
                }
            }
        }
        private DeviceStockVm? _selectedDevice;
        public DeviceStockVm? SelectedDevice
        {
            get => _selectedDevice;
            set => SetProperty(ref _selectedDevice, value);
        }
        private List<DictDataVm> _statusDictList = [];
        public List<DictDataVm> StatusDictList
        {
            get => _statusDictList;
            set => SetProperty(ref _statusDictList, value);
        }
        private DictDataVm? _selectedStatus;
        public DictDataVm? SelectedStatus
        {
            get => _selectedStatus;
            set => SetProperty(ref _selectedStatus, value);
        }
        public DeviceStockViewModel(DeviceService deviceService, ProjectService projectService, StaffService staffService, DictService dictService)
        {
            _deviceService = deviceService;
            _projectService = projectService;
            _staffService = staffService;
            _dictService = dictService;

            Task.Run(LoadDataAsync);
        }
        private async Task LoadProjectNames()
        {
            ProjectNameList = await _projectService.LoadProjectNames();
            //ProjectNameList = list.Select(m => new ProjectAutoCompleteModel { Id = m.ProjectId, Name = string.Format("{0}|{1}", m.Contract.ContractNumber, m.ProjectName), Number = m.Contract.ContractNumber }).ToList();
        }
        private async Task LoadManagerList()
        {
            ManagerList = await _staffService.GetStaffListByDuty(ApiSettings.ProjectManagerDuty);
        }
        public async Task LoadDataAsync()
        {
            Req = new DeviceReqs { Pagination = new PaginationParams { Page = 1, PageSize = 10 } };
            
            await LoadManagerList();
            await LoadProjectNames();
            await LoadDeviceStatsDict().ContinueWith(c=> LoadDevices());
        }
        [RelayCommand]
        public async Task LoadDevices()
        {
            if (SelectedProject != null) Req.ProjectId = SelectedProject.Id;
            else Req.ProjectId = null;
            if (SelectedManager != null) Req.ProjectManagerId = SelectedManager.StaffId;
            else Req.ProjectManagerId = null;
            if (SelectedStatus != null) Req.Status = SelectedStatus.DictCode;
            else Req.Status = StatusDictList.First().DictCode;
            PaginatedList = await _deviceService.DevicePaginatedList(Req);
        }
        private async Task LoadDeviceStatsDict()
        {
            var list = await _dictService.GetDictDataByTypeName("DeviceStatus");
            StatusDictList = new List<DictDataVm>(list);
        }
        private PopWindow? addDeviceWindow;
        [RelayCommand]
        private void OpenAddWindow(object type)
        {
            Guid? selectedDeviceId = null;
            if (Convert.ToInt32(type) == 1)
            {
                if (SelectedDevice == null || SelectedDevice.Device == null) return;
                else selectedDeviceId = SelectedDevice.Device.DeviceId;
            }
            ResolvedParameter deviceId = new(
                (pi, ctx) => pi.Name == "deviceId",
                (pi, ctx) => selectedDeviceId);
            if (_container == null) return;
            var vm = _container.Resolve<AddDeviceViewModel>(deviceId);
            var view = _container.Resolve<AddDeviceView>();
            view.DataContext = vm;
            addDeviceWindow = new PopWindow();
            addDeviceWindow.controlHost.Content = view;
            addDeviceWindow.Title = "设备";
            addDeviceWindow.Width = 600;
            addDeviceWindow.Height = 600;
            addDeviceWindow.Closed += addWindowClosed;
            addDeviceWindow.ShowDialog();
        }
        private PopWindow? historyWindow;
        [RelayCommand]
        private void OpenHistoryWindow()
        {
            var selectedDeviceId = SelectedDevice?.Device?.DeviceId;
            ResolvedParameter deviceId = new(
                (pi, ctx) => pi.Name == "deviceId",
                (pi, ctx) => selectedDeviceId);
            if (_container == null) return;
            var vm = _container.Resolve<DeviceTransferHistoryViewModel>(deviceId);
            var view = _container.Resolve<DeviceTransferHistoryView>();
            view.DataContext = vm;
            historyWindow = new PopWindow();
            historyWindow.controlHost.Content = view;
            historyWindow.Title = "调动记录";
            historyWindow.Closed += addWindowClosed;
            historyWindow.ShowDialog();
        }
        private async void addWindowClosed(object? sender, EventArgs e)
        {
            if (sender is PopWindow window)
            {
                window.Closed -= addWindowClosed;
                await LoadDevices();
            }
            //if (addDeviceWindow == null) return;
            //addDeviceWindow.Closed -= addWindowClosed;
            //await LoadDevices();
        }
        [RelayCommand]
        private async Task RemoveDevice()
        {
            if (SelectedDevice == null|| SelectedDevice.Device == null|| SelectedDevice.Device.DeviceId == null) return;
            var result = await _deviceService.RemoveDevice(SelectedDevice.Device.DeviceId.Value);
            if (result)
            {
                Growl.Success("删除成功");
                await LoadDevices();
            }
        }
        private PopWindow? batchAddWindow;
        [RelayCommand]
        private void OpenBatchAddWindow()
        {
            if (_container == null) return;
            var vm = _container.Resolve<AddBatchDeviceViewModel>();
            var view = _container.Resolve<AddBatchDeviceView>();
            view.DataContext = vm;
            batchAddWindow = new PopWindow();
            batchAddWindow.controlHost.Content = view;
            batchAddWindow.Title = "设备添加";
            batchAddWindow.Width = 600;
            batchAddWindow.Height = 600;
            batchAddWindow.Closed += addWindowClosed;
            batchAddWindow.ShowDialog();
        }

        //private PopWindow? setHandleByWindow;
        //[RelayCommand]
        //private void OpenHandleByWindow
        //{

        //}
    }
}
