using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Project.Common;
using Project.Services;
using Project.Services.DataServices;
using Project.Views.UserControls;
using Project.Views.Windows;
using ProjectViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using Autofac.Core;

namespace Project.ViewModel
{
    public partial class DeviceTransferHistoryViewModel : ObservableObject
    {
        private IContainer? _container = App.Current.Properties[MessageToken.AppContainer] as IContainer;
        private readonly Guid? _deviceId;
        private readonly DeviceService _deviceService;
        private readonly ProjectService _projectService;
        private readonly StaffService _staffService;
        private PaginatedList<ProjectDeviceVm>? _paginatedList;
        public PaginatedList<ProjectDeviceVm>? PaginatedList
        {
            get => _paginatedList;
            set => SetProperty(ref _paginatedList, value);
        }
        private ProjectDeviceVm? _selectedHistory;
        public ProjectDeviceVm? SelectedHistory
        {
            get => _selectedHistory;
            set => SetProperty(ref _selectedHistory, value);
        }

        private DeviceReqs _req = new DeviceReqs { Pagination = new PaginationParams { Page = 1, PageSize = 10 } };
        public DeviceReqs Req
        {
            get => _req;
            set => SetProperty(ref _req, value);
        }
        private List<ProjectAutoCompleteModel> ProjectNameList = null!;
        [ObservableProperty]
        private ObservableCollection<ProjectAutoCompleteModel>? _projectNamesSource;
        //public ObservableCollection<ProjectAutoCompleteModel>? ProjectNamesSource
        //{
        //    get => _projectNamesSource;
        //    set => SetProperty(ref _projectNamesSource, value);
        //}
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
        [ObservableProperty]
        private ProjectAutoCompleteModel? _selectedProject;
        //public ProjectAutoCompleteModel? SelectedProject
        //{
        //    get => _selectedProject;
        //    set => SetProperty(ref _selectedProject, value);
        //}
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
        public DeviceTransferHistoryViewModel(Guid? deviceId, DeviceService deviceService,ProjectService projectService, StaffService staffService)
        {
            _deviceId = deviceId;
            Req.DeviceId = deviceId;
            _deviceService = deviceService;
            _projectService = projectService;
            _staffService = staffService;
            Task.Run(LoadDataAsync);
        }

        private async Task LoadDataAsync()
        {
            await LoadHistory();
            await LoadProjectNames();
            await LoadManagerList();
        }

        [RelayCommand]
        private async Task LoadHistory()
        {
            if(SelectedProject!=null) Req.ProjectId = SelectedProject.Id;
            else Req.ProjectId = null;
            if (SelectedManager != null) Req.ProjectManagerId = SelectedManager.StaffId;
            else Req.ProjectManagerId = null;

            PaginatedList = await _deviceService.PaginatedProjectDeviceHistory(Req);
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

        private PopWindow? handleByWindow;
        [RelayCommand]
        private void OpenHandleByWindow()
        {
            if (SelectedHistory == null) return;
            if (_container == null) return;
            ResolvedParameter deviceId = new(
                (pi, ctx) => pi.Name == "deviceId",
                (pi, ctx) => SelectedHistory.DeviceId);
            if (_container == null) return;
            var vm = _container.Resolve<AddDeviceHandleByViewModel>(deviceId);
            var view = _container.Resolve<AddDeviceHandleBy>();
            view.DataContext = vm;
            handleByWindow = new PopWindow();
            handleByWindow.controlHost.Content = view;
            handleByWindow.Title = SelectedHistory.Device.DeviceNumber;
            handleByWindow.Width = 400;
            handleByWindow.Height = 400;
            handleByWindow.Closed += addWindowClosed;
            handleByWindow.ShowDialog();
        }
        private async void addWindowClosed(object? sender, EventArgs e)
        {
            if (sender is PopWindow window)
            {
                window.Closed -= addWindowClosed;
                await LoadHistory();
            }
        }
    }
}
