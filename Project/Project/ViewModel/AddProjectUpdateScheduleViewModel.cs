using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using HandyControl.Controls;
using Project.Services;
using Project.Services.DataServices;
using ProjectViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MessageBox = HandyControl.Controls.MessageBox;

namespace Project.ViewModel
{
    public partial class AddProjectUpdateScheduleViewModel : ObservableObject
    {
        private readonly ProjectUpdateScheduleService _updateScheduleService;
        private readonly ProjectService _projectService;
        private readonly DictService _dictService;
        //private readonly Guid? _projectId;
        private ProjectUpdateScheduleVm _projectUpdateSchedule = new ();
        public ProjectUpdateScheduleVm ProjectUpdateSchedule
        {
            get => _projectUpdateSchedule;
            set => SetProperty(ref _projectUpdateSchedule, value);
        }
        private ProjectVm _project = null!;
        public ProjectVm Project
        {
            get => _project;
            set => SetProperty(ref _project, value);
        }
        private DateTime? _selectedDate;
        public DateTime? SelectedDate
        {
            get => _selectedDate;
            set => SetProperty(ref _selectedDate, value);
        }
        private List<DictDataVm> _reasonTypeList = null!;
        public List<DictDataVm> ReasonTypeList
        {
            get => _reasonTypeList;
            set => SetProperty(ref _reasonTypeList, value);
        }
        private DictDataVm? _selectedReason;
        public DictDataVm? SelectedReason
        {
            get => _selectedReason;
            set => SetProperty(ref _selectedReason, value);
        }
        public AddProjectUpdateScheduleViewModel(Guid projectId, ProjectUpdateScheduleService updateScheduleService, ProjectService projectService, DictService dictService)
        {
            _updateScheduleService = updateScheduleService;
            _projectService = projectService;
            _dictService = dictService;
            ProjectUpdateSchedule.ProjectId = projectId;
            Task.Run(LoadDataAsync);
        }
        private async Task LoadDataAsync()
        {
            await LoadProject();
            await LoadDictData();
        }
        private async Task LoadProject()
        {
            Project = await _projectService.GetSimpleProjectById(ProjectUpdateSchedule.ProjectId);
        }
        private async Task LoadDictData()
        {
            ReasonTypeList = await _dictService.GetDictDataByTypeName("UpdateScheduleReasonType");
        }
        [RelayCommand]
        private async Task Save()
        {
            if (SelectedReason == null)
            {
                MessageBox.Warning("原因类型不能为空");
                return;
            }
            if (SelectedDate == null)
            {
                MessageBox.Warning("更新日期不能为空");
                return;
            }
            ProjectUpdateSchedule.UpdateEndDate = SelectedDate.Value;
            if (SelectedDate != null && SelectedDate == Project.PlanEndDate)
            {
                MessageBox.Warning("更新日期不能与之前相同");
                return;
            }
            ProjectUpdateSchedule.ReasonType = SelectedReason.DictCode;
            ProjectUpdateSchedule.PlanEndDate = Project.PlanEndDate.Value;
            var result = await _updateScheduleService.AddProjectUpdateSchedule(ProjectUpdateSchedule);
            if (result)
            {
                Growl.Success("保存成功");
            }
        }
    }
}
