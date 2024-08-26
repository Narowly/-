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
    public partial class ApplicationViewModel: ObservableObject
    {
        private readonly IContainer? _container = App.Current.Properties[MessageToken.AppContainer] as IContainer;
        private readonly ApplicationService _applicationService;
        private readonly ProjectService _projectService;
        private readonly StaffService _staffService;
        private readonly DictService _dictService;
        private ApplicationReq _req = new ();
        public ApplicationReq Req
        {
            get => _req;
            set => SetProperty(ref _req, value);
        }
        private PaginatedList<ApplicationVm> _paginatedList = null!;
        public PaginatedList<ApplicationVm> PaginatedList
        {
            get => _paginatedList;
            set => SetProperty(ref _paginatedList, value);
        }
        private ApplicationVm? _selectedDto;
        public ApplicationVm? SelectedDto
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
        private List<DictDataVm> _applicationStatusList = null!;
        public List<DictDataVm> ApplicationStatusList
        {
            get => _applicationStatusList;
            set => SetProperty(ref _applicationStatusList, value);
        }
        private DictDataVm? _selectedStatus;
        public DictDataVm? SelectedStatus
        {
            get => _selectedStatus;
            set => SetProperty(ref _selectedStatus, value);
        }
        private List<DictDataVm> _applicationTypeList = null!;
        public List<DictDataVm> ApplicationTypeList
        {
            get => _applicationTypeList;
            set => SetProperty(ref _applicationTypeList, value);
        }
        private DictDataVm? _selectedApplicationType;
        public DictDataVm? SelectedApplicationType
        {
            get => _selectedApplicationType;
            set => SetProperty(ref _selectedApplicationType, value);
        }

        public ApplicationViewModel(ApplicationService applicationService, ProjectService projectService, StaffService staffService, DictService dictService)
        {
            _applicationService = applicationService;
            _projectService = projectService;
            _staffService = staffService;
            _dictService = dictService;
            Task.Run(LoadDataAsync);            
        }

        private async Task LoadDataAsync()
        {
            await LoadPaginatedList();
            await LoadStaff();
            await LoadProjectNames();
            await LoadApplicationStatusList();
            await LoadApplicationTypeList();
        }
        [RelayCommand]
        private async Task LoadPaginatedList()
        {
            if (SelectedProject != null) Req.ProjectId = SelectedProject.Id;
            else Req.ProjectId = null;
            if (SelectedStaff != null) Req.ApplicationStaff = SelectedStaff.StaffId;
            else Req.ApplicationStaff = null;
            if (SelectedStatus != null) Req.Status = SelectedStatus.DictCode;
            else Req.Status = null;
            if(SelectedApplicationType!=null )Req.ApplicationType = SelectedApplicationType.DictCode;
            else Req.ApplicationType = null;
            PaginatedList = await _applicationService.PaginatedApplication(Req);
        }
        private async Task LoadProjectNames()
        {
            ProjectNameList = await _projectService.LoadProjectNames();
        }
        private async Task LoadStaff()
        {
            StaffList = await _staffService.GetStaffList();
        }
        private async Task LoadApplicationStatusList()
        {
            ApplicationStatusList = await _dictService.GetDictDataByTypeName(DictSettings.ApplicationStatusTypeName);
        }
        private async Task LoadApplicationTypeList()
        {
            ApplicationTypeList = await _dictService.GetDictDataByTypeName(DictSettings.ApplicationTypeName);
        }
        private PopWindow? addWindow;
        [RelayCommand]
        private void AddApplication()
        {
            ResolvedParameter applicationId = new(
                (pi, ctx) => pi.Name == "applicationId",
                (pi, ctx) => SelectedDto?.ApplicationId);
            if (_container == null) return;
            var vm = _container.Resolve<AddApplicationViewModel>(applicationId);
            var view = _container.Resolve<AddApplicationView>();
            view.DataContext = vm;
            addWindow = new PopWindow();
            addWindow.controlHost.Content = view;
            addWindow.Title = $"添加申请";
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
