using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Project.Services;
using Project.Services.DataServices;
using ProjectViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Project.Common;
using HandyControl.Collections;
using System.Windows.Controls.Primitives;

namespace Project.ViewModel
{
    public partial class ProjectDynamicsViewModel : ObservableObject
    {
        private readonly ProjectService _projectService;
        private readonly StaffService _staffService;
        private PaginatedList<ProjectVm>? _paginatedList;
        public PaginatedList<ProjectVm>? PaginatedList
        {
            get => _paginatedList;
            set => SetProperty(ref _paginatedList, value);
        }
        private ObservableCollection<ProjectVm>? _projects = null!;
        public ObservableCollection<ProjectVm>? Projects
        {
            get => _projects;
            set => SetProperty(ref _projects, value);
        }
        private ProjectReqs? _projectReqs = null!;
        public ProjectReqs? ProjectReqs
        {
            get => _projectReqs;
            set => SetProperty(ref _projectReqs, value);
        }
        private ProjectVm? _selectedProject;
        public ProjectVm? SelectedProject
        {
            get => _selectedProject;
            set => SetProperty(ref _selectedProject, value);
        }
        private List<StaffVm>? ManagerList;
        public ObservableCollection<StaffVm>? _bindingManagerList;
        public ObservableCollection<StaffVm>? BindingManagerList
        {
            get => _bindingManagerList;
            set => SetProperty(ref _bindingManagerList, value);
        }

        private string? _searchManagerText;
        public string? SearchManagerText
        {
            get => _searchManagerText;
            set
            {
                SetProperty(ref _searchManagerText, value);
                if (_searchManagerText != null)
                {
                    var list = ManagerList?.Where(m => m.StaffName.Contains(_searchManagerText)).ToList();
                    if (list != null) BindingManagerList = new ObservableCollection<StaffVm>(list);
                }
            }
        }
        private List<ProjectAutoCompleteModel> ProjectNameList = null!;
        private ObservableCollection<ProjectAutoCompleteModel>? _projectNamesSource;
        public ObservableCollection<ProjectAutoCompleteModel>? ProjectNamesSource
        {
            get => _projectNamesSource;
            set => SetProperty(ref _projectNamesSource, value);
        }
        [ObservableProperty]
        private StaffVm? selectedManager;
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
        public ProjectDynamicsViewModel(ProjectService projectService, StaffService staffService)
        {
            _projectService = projectService;
            _staffService = staffService;
            ProjectReqs = new ProjectReqs
            {
                Pagination = new PaginationParams
                {
                    Page = 1,
                    PageSize = 10
                }
            };
            Task.Run(LoadDataAsync);
        }

        private async Task LoadDataAsync()
        {
            await LoadProject();
            await LoadManagerList();
            await LoadProjectNames();
        }
        private async Task LoadProjectNames()
        {
            ProjectNameList = await _projectService.LoadProjectNames();
        }
        [RelayCommand]
        private async Task LoadProject()
        {
            if (SelectedManager != null) ProjectReqs.ProjectManagerId = SelectedManager.StaffId;
            ProjectReqs.Content = SearchProjectNameText;
            if (PaginatedList != null && ProjectReqs != null && ProjectReqs.Pagination != null)
                ProjectReqs.Pagination.Page = PaginatedList.PageIndex;
            var list = await _projectService.GetProjectDynamics(ProjectReqs);
            PaginatedList = list;
            Projects = new ObservableCollection<ProjectVm>(PaginatedList.Items);
        }
        private async Task LoadManagerList()
        {
            await Application.Current.Dispatcher.Invoke(async () =>
            {
                ManagerList = await _staffService.GetStaffListByDuty(ApiSettings.ProjectManagerDuty);
            });
        }
    }
}
