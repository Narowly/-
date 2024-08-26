using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using DocumentFormat.OpenXml.Bibliography;
using HandyControl.Controls;
using Project.Services;
using Project.Services.DataServices;
using ProjectViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MessageBox = HandyControl.Controls.MessageBox;

namespace Project.ViewModel
{
    public partial class AddBatchProjectDailyWorkViewModel : ObservableObject
    {
        private readonly ProcessService _processService;
        private readonly ProjectService _projectService;
        private readonly StaffService _staffService;
        private readonly ProjectDailyWorkService _projectDailyWorkService;
        private Guid? _projectId;
        private List<ProcessStaffRelatedSettingsVm> _relatedList = null!;
        public List<ProcessStaffRelatedSettingsVm> RelatedList
        {
            get => _relatedList;
            set => SetProperty(ref _relatedList, value);
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
            set
            {
                SetProperty(ref _selectedProject, value);
                if (_selectedProject != null)
                {
                    _projectId = _selectedProject.Id;
                    Task.Run(LoadProjectStaff);
                    Task.Run(LoadProjectProcess);
                }
            }
        }


        private DateTime? _selectedDate;
        public DateTime? SelectedDate
        {
            get => _selectedDate;
            set => SetProperty(ref _selectedDate, value);
        }
        private ObservableCollection<BatchProjectDailyWorkVm> _batchList = new ObservableCollection<BatchProjectDailyWorkVm>();
        public ObservableCollection<BatchProjectDailyWorkVm> BatchList
        {
            get => _batchList;
            set => SetProperty(ref _batchList, value);
        }
        //private ProcessStaffRelatedSettingsVm? _singleSettings;
        //public ProcessStaffRelatedSettingsVm? SingleSettings
        //{
        //    get => _singleSettings;
        //    set => SetProperty(ref _singleSettings, value);
        //}

        private DateTime? _singleSelectedDate;
        public DateTime? SingleSelectedDate
        {
            get => _singleSelectedDate;
            set => SetProperty(ref _singleSelectedDate, value);
        }
        private ObservableCollection<StaffVm> _projectStaffList = [];
        public ObservableCollection<StaffVm> ProjectStaffList
        {
            get => _projectStaffList;
            set => SetProperty(ref _projectStaffList, value);
        }
        private ObservableCollection<ProcessUnitVm> _processUnitList = [];
        public ObservableCollection<ProcessUnitVm> ProcessUnitList
        {
            get => _processUnitList;
            set => SetProperty(ref _processUnitList, value);
        }
        private StaffVm? _singleSelectedStaff;
        public StaffVm? SingleSelectedStaff
        {
            get => _singleSelectedStaff;
            set => SetProperty(ref _singleSelectedStaff, value);
        }
        private ProcessUnitVm? _singleProcessUnit;
        public ProcessUnitVm? SingleProcessUnit
        {
            get => _singleProcessUnit;
            set => SetProperty(ref _singleProcessUnit, value);
        }
        private double _singleWorkload;
        public double SingleWorkload
        {
            get => _singleWorkload;
            set => SetProperty(ref _singleWorkload, value);
        }

        public AddBatchProjectDailyWorkViewModel(ProcessService processService, ProjectService projectService, ProjectDailyWorkService projectDailyWorkService, StaffService staffService )
        {
            _processService = processService;
            _projectService = projectService;
            _staffService = staffService;
            _projectDailyWorkService = projectDailyWorkService;
            Task.Run(LoadDataAsync);
        }
        private async Task LoadDataAsync()
        {
            await LoadProjectNames();
            //await LoadProcessStaffRelated();
        }
        [RelayCommand]
        private async Task LoadProcessStaffRelated()
        {
            if (_projectId == null)
            {
                MessageBox.Warning("项目不能为空");
                return;
            }
            if (RelatedList == null || RelatedList.FirstOrDefault().ProjectId != _projectId)
            {
                RelatedList = await _processService.GetProjectProcessStaffRelatedSettings(_projectId.Value);
                BatchList = new ObservableCollection<BatchProjectDailyWorkVm>();
                //if (SingleSettings == null&&RelatedList.Count>0)
                //{
                //    var firstVm = RelatedList.First();
                //    SingleSettings = new ProcessStaffRelatedSettingsVm();
                //    SingleSettings.AvailableStaffOptions = firstVm.AvailableStaffOptions;
                //    SingleSettings.AvailableProcessUnitOptions = firstVm.AvailableProcessUnitOptions;
                //    SingleSettings.ProjectId = firstVm.ProjectId;
                //}
            }
            
            if (SelectedDate != null) Add();
        }
        private async Task LoadProjectProcess()
        {
            await System.Windows.Application.Current.Dispatcher.Invoke(async () =>
            {
                if (_projectId == null)
                {
                    MessageBox.Warning("项目不能为空");
                    return;
                }
                var list = await _projectService.GetProjectProcesses(_projectId.Value);
                ProcessUnitList = [];
                foreach (var item in list)
                {
                    ProcessUnitList.Add(item.ProcessUnit);
                }

            });
        }

        private async Task LoadProjectStaff()
        {
            await System.Windows.Application.Current.Dispatcher.Invoke(async () =>
            {
                if (_projectId == null)
                {
                    MessageBox.Warning("项目不能为空");
                    return;
                }
                var list = await _staffService.GetProjectStaffs(_projectId.Value);
                ProjectStaffList = [];
                foreach (var item in list)
                {
                    ProjectStaffList.Add(item);
                }
            });
        }
        private async Task LoadProjectNames()
        {
            ProjectNameList = await _projectService.LoadProjectNames();
            //ProjectNameList = list.Select(m => new ProjectAutoCompleteModel { Id = m.ProjectId, Name = string.Format("{0}|{1}", m.Contract.ContractNumber, m.ProjectName), Number = m.Contract.ContractNumber }).ToList();
        }
        
        private void Add()
        {
            if (SelectedDate == null || RelatedList == null) return;
            //if (BatchList.Where(m => m.BillDate == DateOnly.FromDateTime(SelectedDate.Value)).Count() > 0)
            //{
            //    MessageBox.Warning("日期重复,请先删除重复日期项");
            //    return;
            //}
            foreach (var item in RelatedList)
            {
                if (item.SelectedProcessUnit != null && item.SelectedStaff != null)
                {
                    if(!BatchList.Any(m=>m.BillDate == DateOnly.FromDateTime(SelectedDate.Value) && m.ProjectId == item.ProjectId && m.ProcessUnit == item.SelectedProcessUnit && m.Staff == item.SelectedStaff))
                    BatchList.Add(new BatchProjectDailyWorkVm
                    {
                        ProjectId = item.ProjectId,
                        BillDate = DateOnly.FromDateTime(SelectedDate.Value),
                        ProcessUnit = item.SelectedProcessUnit,
                        Staff = item.SelectedStaff
                    });
                }                    
            }
        }
        [RelayCommand]
        private void AddSingle()
        {
            if (SingleSelectedDate == null || SingleSelectedStaff == null || SingleProcessUnit == null) return;
            var dailyWork = BatchList.FirstOrDefault(m => SingleSelectedDate != null && DateOnly.FromDateTime(SingleSelectedDate.Value) == m.BillDate && m.Staff.StaffId == SingleSelectedStaff.StaffId && m.ProcessUnit.Id == SingleProcessUnit.Id);
            if (dailyWork != null) { dailyWork.Workload = SingleWorkload; }
            else
            {
                dailyWork = new BatchProjectDailyWorkVm
                {
                    ProjectId = _projectId.Value,
                    BillDate = DateOnly.FromDateTime(SingleSelectedDate.Value),
                    ProcessUnit = SingleProcessUnit,
                    Staff = SingleSelectedStaff
                };
                BatchList.Add(dailyWork);
            }
        }
        [RelayCommand]
        private async Task Save()
        {
            if (BatchList != null && BatchList.Count > 0)
            {
                var result = await _projectDailyWorkService.SaveBatchDailyWork(BatchList.ToList());
                if (result)
                {
                    Growl.Success("保存成功");
                }
            }            
        }
        [RelayCommand]
        private void Remove(BatchProjectDailyWorkVm vm)
        {
            BatchList.Remove(vm);
        }

    }

}
