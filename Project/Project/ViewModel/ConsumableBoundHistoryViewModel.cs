using Autofac.Core;
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

namespace Project.ViewModel
{
    public partial class ConsumableBoundHistoryViewModel : ObservableObject
    {
        private readonly IContainer? _container = App.Current.Properties[MessageToken.AppContainer] as IContainer;
        private readonly ConsumableService _consumableService;
        private readonly ProjectService _projectService;
        private readonly StaffService _staffService;
        private PaginatedList<ConsumableBoundDto> _paginatedList = null!;
        public PaginatedList<ConsumableBoundDto> PaginatedList
        {
            get => _paginatedList;
            set => SetProperty(ref _paginatedList, value);
        }
        private ConsumableBoundDto? _selectedDto;
        public ConsumableBoundDto? SelectedDto
        {
            get => _selectedDto;
            set => SetProperty(ref _selectedDto, value);
        }
        private ConsumableReqs _req = new ConsumableReqs();
        public ConsumableReqs Req
        {
            get => _req;
            set => SetProperty(ref _req, value);
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
        [ObservableProperty]
        private ProjectAutoCompleteModel? _selectedProject;
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
        
        public ConsumableBoundHistoryViewModel(Guid? consumableId, ConsumableService consumableService, ProjectService projectService, StaffService staffService)
        {
            Req.ConsumableId = consumableId;
            _consumableService = consumableService;
            _projectService = projectService;
            _staffService = staffService;
            Task.Run(LoadDataAsync);
        }

        private async Task LoadDataAsync()
        {
            await LoadConsumableBoundList();
            await LoadProjectNames();
            await LoadManagerList();
        }
        [RelayCommand]
        private async Task LoadConsumableBoundList()
        {
            if (SelectedProject != null) Req.ProjectId = SelectedProject.Id;
            else Req.ProjectId = null;
            if (SelectedManager != null) Req.ProjectManagerId = SelectedManager.StaffId;
            else Req.ProjectManagerId = null;
            PaginatedList = await _consumableService.PaginatedConsumableBound(Req);
        }
        private async Task LoadProjectNames()
        {
            ProjectNameList = await _projectService.LoadProjectNames();
        }
        private async Task LoadManagerList()
        {
            ManagerList = await _staffService.GetStaffListByDuty(ApiSettings.ProjectManagerDuty);
        }
        private PopWindow? updateWindow;
        [RelayCommand]
        private void OpenUpdateConsumableBound()
        {
            if (SelectedDto == null) return;
            ResolvedParameter boundId = new(
                (pi, ctx) => pi.Name == "boundId",
                (pi, ctx) => SelectedDto?.BoundId);
            if (_container == null) return;
            UpdateConsumableBoundView view;
            if (SelectedDto.Type == "入库")
            {
                var vm = _container.Resolve<UpdateConsumableInBoundViewModel>(boundId);
                view = _container.Resolve<UpdateConsumableBoundView>();
                view.DataContext = vm;                
            }
            else
            {
                var vm = _container.Resolve<UpdateConsumableOutBoundViewModel>(boundId);
                view = _container.Resolve<UpdateConsumableBoundView>();
                view.DataContext = vm;
            }
            updateWindow = new PopWindow();
            updateWindow.controlHost.Content = view;
            updateWindow.Title = $"消耗品{SelectedDto.Type}";
            updateWindow.Closed += addWindowClosed;
            updateWindow.ShowDialog();
        }
        private async void addWindowClosed(object? sender, EventArgs e)
        {
            if (sender is PopWindow window)
            {
                window.Closed -= addWindowClosed;
                await LoadConsumableBoundList();
            }
        }
    }
}
