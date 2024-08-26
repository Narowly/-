using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using HandyControl.Controls;
using Microsoft.Windows.Themes;
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

namespace Project.ViewModel
{
    public partial class ProcessStaffRelatedViewModel: ObservableObject
    {
        private ProcessService _processService;
        private ProjectService _projectService;
        private StaffService _staffService;
        private Guid _projectId;
        //private ObservableCollection<ProcessStaffRelatedSettingsVm> _relatedList = null!;
        //public ObservableCollection<ProcessStaffRelatedSettingsVm> RelatedList
        //{
        //    get => _relatedList;
        //    set => SetProperty(ref _relatedList, value);
        //}
        //private ObservableCollection<ProcessUnitVm>? _processUnitOptions;
        //public ObservableCollection<ProcessUnitVm>? ProcessUnitOptions
        //{
        //    get => _processUnitOptions;
        //    set => SetProperty(ref _processUnitOptions, value);
        //}
        //private ObservableCollection<StaffVm>? _staffOptions;
        //public ObservableCollection<StaffVm>? StaffOptions
        //{
        //    get => _staffOptions;
        //    set => SetProperty(ref _staffOptions, value);
        //}
        private List<ProcessStaffRelatedVm> _processStaffRelatedList = [];
        public List<ProcessStaffRelatedVm> ProcessStaffRelatedList
        {
            get => _processStaffRelatedList;
            set => SetProperty(ref _processStaffRelatedList, value);
        }
        //private List<ProjectProcessVm> _projectProcessList = [];
        //public List<ProjectProcessVm> ProjectProcessList
        //{
        //    get => _projectProcessList;
        //    set => SetProperty(ref _projectProcessList, value);
        //}
        private ObservableCollection<SelectionItem<ProjectProcessVm>> _projectProcessList = [];
        public ObservableCollection<SelectionItem<ProjectProcessVm>> ProjectProcessList
        {
            get => _projectProcessList;
            set => SetProperty(ref _projectProcessList, value);
        }
        private ProjectProcessVm? _selectedProjectProcess;
        public ProjectProcessVm? SelectedProjectProcess
        {
            get => _selectedProjectProcess;
            set => SetProperty(ref _selectedProjectProcess, value);
        }
        //private List<StaffVm> _projectStaffList = [];
        //public List<StaffVm> ProjectStaffList
        //{
        //    get => _projectStaffList;
        //    set => SetProperty(ref _projectStaffList, value);
        //}
        private ObservableCollection<SelectionItem<StaffVm>> _projectStaffList = [];
        public ObservableCollection<SelectionItem<StaffVm>> ProjectStaffList
        {
            get => _projectStaffList;
            set => SetProperty(ref _projectStaffList, value);
        }
        public ProcessStaffRelatedViewModel(Guid projectId, ProcessService processService, ProjectService projectService, StaffService staffService)
        {
            _projectId = projectId;
            _processService = processService;        
            _projectService = projectService;
            _staffService = staffService;

            Task.Run(LoadDataAsync);
        }

        private async Task LoadDataAsync()
        {
            await Task.WhenAll(LoadProjectStaff(), LoadProjectProcess()).ContinueWith(c => LoadRelatedList());
        }
        //[RelayCommand]
        //private async Task LoadRelatedList()
        //{
        //    var list = await _processService.GetProjectProcessStaffRelatedSettings(_projectId);
        //    RelatedList = new ObservableCollection<ProcessStaffRelatedSettingsVm>(list);
        //    foreach (var item in RelatedList)
        //    {
        //        if(item.SelectedProcessUnit!=null)
        //            item.SelectedProcessUnit = item.AvailableProcessUnitOptions?.FirstOrDefault(m => m.Id == item.SelectedProcessUnit.Id);
        //        if (item.SelectedStaff != null)
        //            item.SelectedStaff = item.AvailableStaffOptions?.FirstOrDefault(m => m.StaffId == item.SelectedStaff.StaffId);
        //    }
        //    StaffOptions = RelatedList[0].AvailableStaffOptions;
        //    ProcessUnitOptions = RelatedList[0].AvailableProcessUnitOptions;
        //}

        private async Task LoadRelatedList()
        {
            ProcessStaffRelatedList = await _processService.GetProjectProcessStaffRelatedList(_projectId);
            if (ProjectProcessList.Count > 0)
            {
                ProjectProcessList.First().Selected = true;
                SelectedProjectProcess = ProjectProcessList.First().Item;
                foreach (var staff in ProjectStaffList)
                {
                    var related = ProcessStaffRelatedList.FirstOrDefault(m => m.StaffId == staff.Item.StaffId && m.ProcessUnitId == SelectedProjectProcess.ProcessUnitId);
                    if(related != null) staff.Selected = true;
                }
            }
            
        }

        private async Task LoadProjectProcess()
        {
            await System.Windows.Application.Current.Dispatcher.Invoke(async () =>
            {
                var list = await _projectService.GetProjectProcesses(_projectId);
                var selectionList = list.Select(m => new SelectionItem<ProjectProcessVm> { Item = m, Selected = false });
                foreach (var item in selectionList)
                {

                    ProjectProcessList.Add(item);
                }
                
            });            
        }

        private async Task LoadProjectStaff()
        {
            await System.Windows.Application.Current.Dispatcher.Invoke(async () =>
            {
                var list = await _staffService.GetProjectStaffs(_projectId);
                var selectionList = list.Select(m => new SelectionItem<StaffVm> { Item = m, Selected = false });
                foreach (var item in selectionList)
                {
                    ProjectStaffList.Add(item);
                }
            });
        }

        [RelayCommand]
        private async Task Save()
        {
            var result = await _processService.SaveProjectProcessStaffRelated(ProcessStaffRelatedList);
            if (result)
            {
                Growl.Success("保存成功");
            }
        }
        //[RelayCommand]
        //private void AddRelated()
        //{
        //    RelatedList.Add(new ProcessStaffRelatedSettingsVm
        //    {
        //         AvailableProcessUnitOptions = ProcessUnitOptions,
        //         AvailableStaffOptions = StaffOptions,
        //         ProjectId = _projectId,
        //    });
        //}

        //[RelayCommand]
        //private void RemoveRelated(ProcessStaffRelatedSettingsVm vm)
        //{
        //    RelatedList.Remove(vm);
        //}

        [RelayCommand]
        private void ProcessOptionSelected(object parameter)
        {
            var selectedOption = parameter as SelectionItem<ProjectProcessVm>;
            if (selectedOption != null)
            {
                SelectedProjectProcess = selectedOption.Item;
                var staffIdList = ProcessStaffRelatedList.Where(m => m.ProcessUnitId == SelectedProjectProcess.ProcessUnitId).Select(m => m.StaffId).ToList();
                foreach (var staff in ProjectStaffList)
                {
                    if (staffIdList.Contains(staff.Item.StaffId))
                    {
                        staff.Selected = true;
                    }
                    else
                    {
                        staff.Selected = false;
                    }
                }
            }            
        }

        [RelayCommand]
        private void StaffOptionSelected()
        {
            if (SelectedProjectProcess == null) return;
            foreach (var staff in ProjectStaffList)
            {
                var related = ProcessStaffRelatedList.FirstOrDefault(m => m.StaffId == staff.Item.StaffId && m.ProcessUnitId == SelectedProjectProcess.ProcessUnitId);
                if (staff.Selected)
                {
                    if (related == null) ProcessStaffRelatedList.Add(new ProcessStaffRelatedVm
                    {
                        ProcessUnitId = SelectedProjectProcess.ProcessUnitId,
                        ProjectId = SelectedProjectProcess.ProjectId,
                        StaffId = staff.Item.StaffId
                    });
                }
                else
                {
                    if (related != null)
                        ProcessStaffRelatedList.Remove(related);
                }
            }
        }
    }

    public class SelectionItem<T> : ObservableObject where T : class 
    {
        private bool _selected;
        public bool Selected
        {
            get => _selected;
            set => SetProperty(ref _selected, value);
        }

        private T _item;
        public T Item
        {
            get => _item;
            set => SetProperty(ref _item, value);
        }
    }
}
