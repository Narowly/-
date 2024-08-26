using Autofac.Core;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Irony;
using Project.Common;
using Project.Services;
using Project.Services.DataServices;
using Project.Views.Windows;
using ProjectViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using Project.Views.UserControls;

namespace Project.ViewModel
{
    public partial class ConsumableAskForViewModel : ObservableObject
    {
        private readonly IContainer? _container = App.Current.Properties[MessageToken.AppContainer] as IContainer;
        private readonly ConsumableAskForService _consumableAskForService;
        private readonly ConsumableService _consumableService;
        private readonly ProjectService _projectService;
        private readonly StaffService _staffService;
        private readonly DictService _dictService;

        private ConsumableAskForReq _req = new();
        public ConsumableAskForReq Req
        {
            get => _req;
            set => SetProperty(ref _req, value);
        }
        
        private PaginatedList<ConsumableAskForVm> _paginatedList = null!;
        public PaginatedList<ConsumableAskForVm> PaginatedList
        {
            get => _paginatedList;
            set => SetProperty(ref _paginatedList, value);
        }

        private ConsumableAskForVm? _selectedDto;
        public ConsumableAskForVm? SelectedDto
        {
            get => _selectedDto;
            set => SetProperty(ref _selectedDto, value);
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

        private List<StaffVm>? StaffList;
        [ObservableProperty]
        private ObservableCollection<StaffVm>? bindingStaffList;
        [ObservableProperty]
        private StaffVm? selectedStaff;
        private string? _searchStaffText;
        public string? SearchStaffText
        {
            get => _searchStaffText;
            set
            {
                SetProperty(ref _searchStaffText, value);
                if (!string.IsNullOrWhiteSpace(_searchStaffText))
                {
                    var list = StaffList?.Where(m => m.StaffName.Contains(_searchStaffText)).ToList();
                    if (list != null) BindingStaffList = new ObservableCollection<StaffVm>(list);
                }
                else
                {
                    BindingStaffList = null;
                }
            }
        }

        private List<DictDataVm>? _askForStatusList = null!;
        public List<DictDataVm>? AskForStatusList
        {
            get => _askForStatusList;
            set => SetProperty(ref _askForStatusList, value);
        }

        private DictDataVm? _selectedStatus;
        public DictDataVm? SelectedStatus
        {
            get => _selectedStatus;
            set => SetProperty(ref _selectedStatus, value);
        }

        private List<ConsumableTypeVm>? _typeList;
        public List<ConsumableTypeVm>? TypeList
        {
            get => _typeList;
            set => SetProperty(ref _typeList, value);
        }
        private ConsumableTypeVm? _selectedType;
        public ConsumableTypeVm? SelectedType
        {
            get => _selectedType;
            set => SetProperty(ref _selectedType, value);
        }

        public ConsumableAskForViewModel(ConsumableAskForService consumableAskForService, ConsumableService consumableService, ProjectService projectService, StaffService staffService, DictService dictService)
        {
            _consumableAskForService = consumableAskForService;
            _consumableService = consumableService;            
            _projectService = projectService;
            _staffService = staffService;
            _dictService = dictService;
            Task.Run(LoadDataAsync);
        }

        private async Task LoadDataAsync()
        {
            await LoadPaginatedList();
            await LoadConsumableTypeList();
            await LoadProjectNames();
            await LoadStaff();
            await LoadAskForStatusList();
        }
        [RelayCommand]
        private async Task LoadPaginatedList()
        {
            Req.ProjectId = SelectedProject?.Id;
            Req.Staff = SelectedStaff?.StaffId;
            Req.Status = SelectedStatus?.DictCode;
            Req.ConsumableTypeId = SelectedType?.ConsumableTypeId;
            PaginatedList = await _consumableAskForService.PaginatedConsumableAskFor(Req);
        }

        private async Task LoadConsumableTypeList()
        {
            TypeList = await _consumableService.GetConsumableTypeList(new CommonReqs());
        }
        private async Task LoadProjectNames()
        {
            ProjectNameList = await _projectService.LoadProjectNames();
        }
        private async Task LoadStaff()
        {
            StaffList = await _staffService.GetStaffList();
        }
        private async Task LoadAskForStatusList()
        {
            AskForStatusList = await _dictService.GetDictDataByTypeName(DictSettings.ConsumableAskForStatusTypeName);
        }

        private PopWindow? addWindow;
        [RelayCommand]
        private void AddConsumableAskFor()
        {
            ResolvedParameter consumableAskForId = new(
                (pi, ctx) => pi.Name == "consumableAskForId",
                (pi, ctx) => SelectedDto?.ConsumableAskForId);
            if (_container == null) return;
            var vm = _container.Resolve<AddConsumableAskForViewModel>(consumableAskForId);
            var view = _container.Resolve<AddConsumableAskForView>();
            view.DataContext = vm;
            addWindow = new PopWindow();
            addWindow.controlHost.Content = view;
            addWindow.Title = $"申购";
            addWindow.Closed += SubWindowClosed;
            addWindow.ShowDialog();
        }
        private async void SubWindowClosed(object? sender, EventArgs e)
        {
            if (sender is PopWindow window)
            {
                window.Closed -= SubWindowClosed;
                await LoadPaginatedList();
            }
        }
    }
}
