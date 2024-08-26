using CommunityToolkit.Mvvm.ComponentModel;
using Project.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using Project.Services;
using Project.Services.DataServices;
using ProjectViewModels;
using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.Input;
using Project.Views.Windows;
using Autofac.Core;
using Project.Views.UserControls;

namespace Project.ViewModel
{
    public partial class ProjectUpdateScheduleViewModel : ObservableObject
    {
        private readonly IContainer? _container = App.Current.Properties[MessageToken.AppContainer] as IContainer;
        private readonly ProjectService _projectService;
        private readonly StaffService _staffService;
        private readonly DictService _dictService;

        private PaginatedList<ProjectVm> _paginatedList = null!;
        public PaginatedList<ProjectVm> PaginatedList
        {
            get => _paginatedList;
            set => SetProperty(ref _paginatedList, value);
        }
        private ProjectReqs _req = new ();
        public ProjectReqs Req
        {
            get => _req;
            set => SetProperty(ref _req, value);
        }
        private int _searchType = 0;
        public int SearchType
        {
            get => _searchType;
            set => SetProperty(ref _searchType, value);
        }
        private List<ComboxVm> _searchTypeList = new List<ComboxVm>
        {
            new ComboxVm{ Text = "", Value=0},
            new ComboxVm{ Text = "快到期", Value=1},
            new ComboxVm{ Text = "已延期", Value=2},
            new ComboxVm{ Text = "进度预警", Value=3}
        };
        public List<ComboxVm> SearchTypeList
        {
            get => _searchTypeList;
            set => SetProperty(ref _searchTypeList, value);
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
        private ProjectVm? _selectedProjectData;
        public ProjectVm? SelectedProjectData
        {
            get => _selectedProjectData;
            set => SetProperty(ref _selectedProjectData, value);
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
        public ProjectUpdateScheduleViewModel(ProjectService projectService, StaffService staffService, DictService dictService)
        {
            _projectService = projectService;
            _staffService = staffService;
            _dictService = dictService;
            Task.Run(LoadDataAsync);
        }
        private async Task LoadDataAsync()
        {
            await LoadProjects();
            await LoadProjectNames();
            await LoadManagerList();
        }
        [RelayCommand]
        private async Task LoadProjects()
        {
            switch (SearchType)
            {
                default:
                case 0:
                    if (SelectedProject != null) Req.ProjectId = SelectedProject.Id;
                    else Req.ProjectId = null;
                    if (SelectedManager != null) Req.ProjectManagerId = SelectedManager.StaffId;
                    else Req.ProjectManagerId = null;
                    PaginatedList = await _projectService.PaginatedOpenningProject(Req);
                    break;
                case 1:
                    PaginatedList = await _projectService.ApproachingPlanDateSearch(Req);
                    break;
                case 2:
                    PaginatedList = await _projectService.DelayedPlanDateSearch(Req);
                    break;
                case 3:
                    PaginatedList = await _projectService.ProcessWarningDateSearch(Req);
                    break;
            }            
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
        private PopWindow? addWindow;
        [RelayCommand]
        private void OpenAddWindow()
        {
            if (SelectedProjectData == null) return;
            ResolvedParameter projectId = new(
                (pi, ctx) => pi.Name == "projectId",
                (pi, ctx) => SelectedProjectData.ProjectId);
            if (_container == null) return;
            var vm = _container.Resolve<AddProjectUpdateScheduleViewModel>(projectId);
            var view = _container.Resolve<AddProjectUpdateScheduleView>();
            view.DataContext = vm;
            addWindow = new PopWindow();
            addWindow.controlHost.Content = view;
            addWindow.Title = SelectedProjectData.ProjectName;
            addWindow.Width = 600;
            addWindow.Height = 600;
            addWindow.Closed += PopWindowReloadClosed;
            addWindow.ShowDialog();
        }
        private async void PopWindowReloadClosed(object? sender, EventArgs e)
        {
            if (sender is PopWindow window)
            {
                window.Closed -= PopWindowReloadClosed;
                await LoadProjects();
            }
        }
        
        private PopWindow? historyWindow;
        [RelayCommand]
        private void OpenHistoryWindow()
        {
            ResolvedParameter projectId = new(
                (pi, ctx) => pi.Name == "projectId",
                (pi, ctx) => SelectedProjectData?.ProjectId);
            if(_container == null) return;
            var vm = _container.Resolve<ProjectUpdateScheduleHistoryViewModel>(projectId);
            var view = _container.Resolve<ProjectUpdateScheduleHistoryView>();
            view.DataContext = vm;
            historyWindow = new PopWindow() { 
                Title = "进度调整历史"
            };
            historyWindow.controlHost.Content = view;
            historyWindow.ShowDialog();
        }
    }
}
