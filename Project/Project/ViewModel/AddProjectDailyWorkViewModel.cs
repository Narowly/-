using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using HandyControl.Controls;
using Project.Services;
using Project.Services.DataServices;
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
    public partial class AddProjectDailyWorkViewModel : ObservableObject
    {
        private readonly ProjectDailyWorkService _dailyWorkService;
        private readonly ProjectService _projectService;
        private DateTime? _bindingStartDate = DateTime.Now.AddDays(-7).Date;
        public DateTime? BindingStartDate
        {
            get => _bindingStartDate;
            set => SetProperty(ref _bindingStartDate, value);
        }
        private DateTime? _bindingEndDate = DateTime.Now.Date;
        public DateTime? BindingEndDate
        {
            get => _bindingEndDate;
            set => SetProperty(ref _bindingEndDate, value);
        }
        private List<ProjectAutoCompleteModel> ProjectNameList = null!;
        private ObservableCollection<ProjectAutoCompleteModel>? _projectNamesSource;
        public ObservableCollection<ProjectAutoCompleteModel>? ProjectNamesSource
        {
            get => _projectNamesSource;
            set => SetProperty(ref _projectNamesSource, value);
        }
        private bool _canProjectSelect = true;
        public bool CanProjectSelect
        {
            get => _canProjectSelect;
            set => SetProperty(ref _canProjectSelect, value);
        }
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
                Task.Run(GetProjectById);
            }
        }

        private ProjectDailyWorkVm _projectDailyWork = new ProjectDailyWorkVm();
        public ProjectDailyWorkVm ProjectDailyWork
        {
            get => _projectDailyWork;
            set => SetProperty(ref _projectDailyWork, value);
        }
        private DateTime? _selectedDate;
        public DateTime? SelectedDate
        {
            get => _selectedDate;
            set => SetProperty(ref _selectedDate, value);
        }

        private ProjectVm? _project;
        public ProjectVm? Project
        {
            get => _project;
            set => SetProperty(ref _project, value);
        }
        private StaffVm? _selectedStaff;
        public StaffVm? SelectedStaff
        {
            get => _selectedStaff;
            set => SetProperty(ref _selectedStaff, value);
        }
        private ProjectProcessVm? _selectedProcess;
        public ProjectProcessVm? SelectedProcess
        {
            get => _selectedProcess;
            set => SetProperty(ref _selectedProcess, value);
        }
        private Guid? _id;

        public AddProjectDailyWorkViewModel(Guid? id,ProjectDailyWorkService dailyWorkService, ProjectService projectService)
        {
            _id = id;
            _dailyWorkService = dailyWorkService;
            _projectService = projectService;
            Task.Run(LoadDataAsync);
        }

        private async Task LoadDataAsync()
        {
            if(_id != null)
            {
                await LoadDailyWorkById();
            }
            else
            {
                await LoadProjectNames();
            }
        }

        private async Task LoadProjectNames()
        {
            ProjectNameList = await _projectService.LoadProjectNames();
        }
        private async Task LoadDailyWorkById()
        {
            if (_id != null)
            {
                var result = await _dailyWorkService.GetDailyWorkById(_id.Value);
                
                if (result != null)
                {
                    if (result.ProjectProcess != null)
                    {
                        Project = await _projectService.GetProjectById(result.ProjectProcess.ProjectId);
                    }
                    
                    ProjectDailyWork = result;
                    BindingStartDate = null;
                    BindingEndDate = null;
                    SelectedDate = result.BillDate.ToDateTime(TimeOnly.MinValue);
                    if (Project != null && Project.ProjectProcesses != null)
                        SelectedProcess = Project.ProjectProcesses.FirstOrDefault(m => m.Id == result.ProjectProcessId);
                    if (Project != null && Project.InProjectStaffs != null)
                        SelectedStaff = Project.InProjectStaffs.FirstOrDefault(m => m.StaffId == result.StaffId);
                    ProjectNameList = new List<ProjectAutoCompleteModel>();
                    if (Project != null)
                    {
                        ProjectNameList.Add(new ProjectAutoCompleteModel
                        {
                            Id = Project.ProjectId,
                            Name = Project.ProjectName,
                            Number = Project.Contract.ContractNumber
                        });
                        ProjectNamesSource = new ObservableCollection<ProjectAutoCompleteModel>(ProjectNameList);
                        SelectedProject = ProjectNamesSource.First();
                        CanProjectSelect = false;
                    }
                }
            }            
        }

        private async Task GetProjectById()
        {
            if (SelectedProject == null || SelectedProject.Id == null || _id != null) return;
            Project = await _projectService.GetProjectById(SelectedProject.Id.Value);
        }

        [RelayCommand]
        private async Task Save()
        {
            if (SelectedDate == null || SelectedStaff == null||SelectedProcess==null)
            {
                MessageBox.Warning("字段不能为空");
                return;
            }
            ProjectDailyWork.BillDate = DateOnly.FromDateTime(SelectedDate.Value.Date);
            ProjectDailyWork.StaffId = SelectedStaff.StaffId;
            ProjectDailyWork.ProjectProcessId = SelectedProcess.Id;
            
            var result = await _dailyWorkService.SaveProjectDailyWork(ProjectDailyWork);
            if (result)
            {
                Growl.Success("保存成功");
            }
        }

    }
}
