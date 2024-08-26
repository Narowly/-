using Autofac;
using Autofac.Core;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Newtonsoft.Json.Linq;
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
using System.Windows;

namespace Project.ViewModel
{
    public partial class ProjectListViewModel : ObservableRecipient
    {
        private readonly IContainer? _container = App.Current.Properties[MessageToken.AppContainer] as IContainer;
        private readonly StaffService _staffService;
        private readonly ProjectService _projectService;
        private PaginatedList<ProjectVm>? _paginatedList;
        public PaginatedList<ProjectVm>? PaginatedList
        {
            get => _paginatedList;
            set => SetProperty(ref _paginatedList, value);
        }
        private ObservableCollection<ProjectVm>? _projects;
        public ObservableCollection<ProjectVm>? Projects
        {
            get => _projects;
            set => SetProperty(ref _projects, value);
        }

        private ProjectVm? _selectedProject;
        public ProjectVm? SelectedProject
        {
            get => _selectedProject;
            set => SetProperty(ref _selectedProject, value);
        }
        private ProjectReqs? _projectReqs;
        public ProjectReqs? ProjectReqs
        {
            get => _projectReqs;
            set => SetProperty(ref _projectReqs, value);
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
        private PopWindow? _settingsUcWindow;
        public PopWindow? SettingsUcWindow
        {
            get => _settingsUcWindow;
            set => SetProperty(ref _settingsUcWindow, value);
        }
        private List<ProjectAutoCompleteModel> ProjectNameList = null!;
        private ObservableCollection<ProjectAutoCompleteModel>? _projectNamesSource;
        public ObservableCollection<ProjectAutoCompleteModel>? ProjectNamesSource
        {
            get => _projectNamesSource;
            set => SetProperty(ref _projectNamesSource, value);
        }
        private string? _searchProjectNameText;
        public string? SearchProjectNameText
        {
            get => _searchProjectNameText;
            set
            {
                //SetProperty(ref _searchProjectNameText, value);
                //if (!string.IsNullOrWhiteSpace(value))
                //{
                //    List<ProjectAutoCompleteModel> sourceList; 
                //    sourceList = ProjectNameList.Where(m => m.ProjectName.Contains(value) || m.Contract.ContractNumber.Contains(value)).Select(m => new ProjectAutoCompleteModel { Id = m.ProjectId, Name = string.Format("{0}|{1}", m.Contract.ContractNumber, m.ProjectName), Number = m.Contract.ContractNumber }).ToList();
                //    ProjectNamesSource = new ObservableCollection<ProjectAutoCompleteModel>(sourceList);
                //}
                //else
                //{
                //    ProjectNamesSource = null;
                //}
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
        public ProjectListViewModel(StaffService staffService, ProjectService projectService)
        {
            _staffService = staffService;
            _projectService = projectService;
            ProjectReqs = new ProjectReqs
            {
                Pagination = new PaginationParams
                {
                    Page = 1,
                    PageSize = 10
                }
            };
            Task.Run(() => LoadDataAsync());
        }
        private async Task LoadDataAsync()
        {
            await LoadManagerList();
            await LoadProject();
            await LoadProjectNames();
        }

        private async Task LoadManagerList()
        {
            await Application.Current.Dispatcher.Invoke(async () =>
            {
                ManagerList = await _staffService.GetStaffListByDuty(ApiSettings.ProjectManagerDuty);
            });            
        }
        private async Task LoadProjectNames()
        {
            ProjectNameList = await _projectService.LoadProjectNames();
        }
        [RelayCommand]
        private async Task LoadProject()
        {
            await Application.Current.Dispatcher.Invoke(async () =>
            {                
                if (SelectedManager != null) ProjectReqs.ProjectManagerId = SelectedManager.StaffId;
                ProjectReqs.Content = SearchProjectNameText;
                if (PaginatedList != null && ProjectReqs != null && ProjectReqs.Pagination != null)
                    ProjectReqs.Pagination.Page = PaginatedList.PageIndex;
                var list = await _projectService.GetProjectsAsync(ProjectReqs);
                PaginatedList = list;
                Projects = new ObservableCollection<ProjectVm>(PaginatedList.Items);
            });
        }

        [RelayCommand]
        private void OpenSettingsUc()
        {
            if (SelectedProject == null) return;
            SettingsUcWindow = new PopWindow();
            var projectId = new ResolvedParameter(
                (pi, ctx) => pi.Name == "projectId",
                (pi, ctx) => SelectedProject.ProjectId);
            var vm = _container.Resolve<ProjectSettingsViewModel>(projectId);
            var settingsUc = new ProjectSettingsUc(vm);
            SettingsUcWindow.controlHost.Content = settingsUc;
            SettingsUcWindow.Title = SelectedProject.ProjectName;
            SettingsUcWindow.ShowDialog();
        }

        private PopWindow? AcceptanceWindow;
        [RelayCommand]
        private void OpenAcceptanceUc()
        {
            if (SelectedProject == null) return;
            AcceptanceWindow = new PopWindow();
            var projectId = new ResolvedParameter(
                (pi, ctx) => pi.Name == "projectId",
                (pi, ctx) => SelectedProject.ProjectId);
            var vm = _container.Resolve<ProjectAcceptanceViewModel>(projectId);
            var acceptanceUc = new ProjectAcceptanceView(vm);
            AcceptanceWindow.controlHost.Content = acceptanceUc;
            AcceptanceWindow.Title = SelectedProject.ProjectName;
            AcceptanceWindow.ShowDialog();
        }

        PopWindow? placeOnFileWindow;
        [RelayCommand]
        private void AddPlaceOnFile()
        {
            if (SelectedProject == null) return;
            ResolvedParameter projectId = new(
                (pi, ctx) => pi.Name == "projectId",
                (pi, ctx) => SelectedProject?.ProjectId);
            if (_container == null) return;
            var vm = _container.Resolve<AddPlaceOnFileViewModel>(projectId);
            var view = _container.Resolve<AddPlaceOnFileView>();
            view.DataContext = vm;
            placeOnFileWindow = new PopWindow();
            placeOnFileWindow.controlHost.Content = view;
            placeOnFileWindow.ShowDialog();
        }
    }
}
