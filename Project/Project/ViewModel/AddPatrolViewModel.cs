using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using HandyControl.Controls;
using Project.Services;
using Project.Services.DataServices;
using Project.Views.UserControls;
using ProjectViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MessageBox = HandyControl.Controls.MessageBox;

namespace Project.ViewModel
{
    public partial class AddPatrolViewModel : ObservableObject
    {
        private readonly PatrolService _patrolService;
        private readonly ProjectService _projectService;
        private readonly StaffService _staffService;
        private readonly DictService _dictService;

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
            set
            {
                SetProperty(ref _selectedProject, value);
            }
        }
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
        private ProjectPatrolVm? _patrol;
        public ProjectPatrolVm? Patrol
        {
            get => _patrol;
            set => SetProperty(ref _patrol, value);
        }
        private List<DictDataVm> _patrolStatusTypeSource = null!;
        public List<DictDataVm> PatrolStatusTypeSource
        {
            get => _patrolStatusTypeSource;
            set => SetProperty(ref _patrolStatusTypeSource, value);
        }
        private DictDataVm? _selectedPatrolStatus;
        public DictDataVm? SelectedPatrolStatus
        {
            get => _selectedPatrolStatus;
            set => SetProperty(ref _selectedPatrolStatus, value);
        }

        public AddPatrolViewModel(long? id, PatrolService patrolService, ProjectService projectService, StaffService staffService, DictService dictService)
        {
            _dictService = dictService;
            _patrolService = patrolService;
            _projectService = projectService;
            _staffService = staffService;
            if (Patrol == null) Patrol = new ProjectPatrolVm { Id = id };
            Task.Run(LoadDataAsync);
        }
        private async Task LoadDataAsync()
        {
            //await LoadProjectNames();
            await Task.WhenAll(LoadProjectNames(), LoadStaff(), LoadPatrolTypeDict())
                .ContinueWith(c => LoadPatrol());
        }
        private async Task LoadProjectNames()
        {
            ProjectNameList = await _projectService.LoadProjectNames();
        }
        private async Task LoadStaff()
        {
            StaffList = await _staffService.GetStaffList();
        }
        private async Task LoadPatrolTypeDict()
        {
            PatrolStatusTypeSource = await _dictService.GetDictDataByTypeName(DictSettings.PatrolStatusTypeName);
        }

        private async Task LoadPatrol()
        {
            if (Patrol?.Id != null)
            {
                Patrol = await _patrolService.GetPatrolById(Patrol.Id.Value);
                if (Patrol != null)
                {
                    SelectedPatrolStatus = PatrolStatusTypeSource.FirstOrDefault(m => m.DictCode == Patrol.Status);
                    SelectedStaff = StaffList?.FirstOrDefault(m => m.StaffId == Patrol.StaffId);
                    SelectedProject = ProjectNameList?.FirstOrDefault(m=>m.Id == Patrol.ProjectId);
                    SearchProjectNameText = SelectedProject?.Name;
                    SearchStaffText = SelectedStaff?.StaffName;
                }
            }
                
        }

        [RelayCommand]
        private async Task Save()
        {
            if(SelectedProject == null || SelectedProject.Id == null)
            {
                MessageBox.Warning("项目不能为空");
                return;
            }
            if(SelectedStaff == null)
            {
                MessageBox.Warning("巡查人不能为空");
                return;
            }
            if(SelectedPatrolStatus == null)
            {
                MessageBox.Warning("整改状态不能为空");
                return;
            }
            if (Patrol.PatrolDate == null)
            {
                MessageBox.Warning("巡查日期不能为空");
                return;
            }
            Patrol.ProjectId = SelectedProject.Id;
            Patrol.StaffId = SelectedStaff.StaffId;
            Patrol.Status = SelectedPatrolStatus.DictCode;
            var result = await _patrolService.SavePatrol(Patrol);
            if (result)
            {
                Growl.Success("保存成功");
            }
        }
    }
}
