using CommunityToolkit.Mvvm.ComponentModel;
using Project.Services.DataServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectViewModels;
using System.Windows.Input;
using CommunityToolkit.Mvvm.Input;
using Project.Model;
using Autofac;
using System.Collections.ObjectModel;
using System.Windows.Controls;
using Project.Common;
using CommunityToolkit.Mvvm.Messaging;
using Project.Services;
using HandyControl.Collections;
using MessageBox = HandyControl.Controls.MessageBox;

namespace Project.ViewModel
{
    public partial class ProcessViewModel : ObservableObject
    {
        private readonly ProcessService _service;
        private readonly ProjectService _projectService;
        [ObservableProperty]
        private Guid? projectId;
        private List<ProcessVm> ProcessList = null!;
        [ObservableProperty]
        private ObservableCollection<ProjectProcessVm>? projectProcessList;
        [ObservableProperty]
        private ObservableCollection<ProcessUnitGroup>? projectProcessUnitGroups;
        [ObservableProperty]
        private ObservableCollection<ProcessTemplateVm>? processTemplateList;
        [ObservableProperty]
        private ProcessTemplateVm? selectedTemplate;
        private List<ProcessUnitVm> ProcessUnitList = null!;

        private List<ProjectAutoCompleteModel> ProjectNameList = null!;

        private string searchProjectText = string.Empty;
        public string SearchProjectText
        {
            get => searchProjectText;
            set
            {
                SetProperty(ref searchProjectText, value);
                if (!string.IsNullOrWhiteSpace(searchProjectText))
                {
                    var list = ProjectNameList.Where(m => m.Name.Contains(searchProjectText) || m.Number.Contains(searchProjectText)).ToList();
                    if (list != null) ProjectSource = new ObservableCollection<ProjectAutoCompleteModel>(list);
                }
            }
        }
        [ObservableProperty]
        private ObservableCollection<ProjectAutoCompleteModel>? projectSource;
        
        private ProjectVm? selectedProject;
        public ProjectVm? SelectedProject
        {
            get => selectedProject;
            set
            {
                SetProperty(ref selectedProject, value);
            }
        }
        [ObservableProperty]
        private ObservableCollection<ProcessUnitGroup> processUnitGroups = new ObservableCollection<ProcessUnitGroup>();
        [ObservableProperty]
        private bool canAddFlag;

        public ProcessViewModel(ProcessService service,ProjectService projectService, Guid? projectId = null, ObservableCollection<ProjectProcessVm>? pList=null)
        {
            _service = service;
            _projectService = projectService;
            ProjectId = projectId;
            ProjectProcessList = pList;
            
            Task.Run(LoadDataAsync);
        }
        private async Task LoadDataAsync()
        {
            await LoadProcessUnit();
            await LoadProcessTemplate();
            await LoadProjectNames();
        }
        private async Task LoadProcessUnit()
        {

            ProcessUnitList = await _service.GetProcessUnitList();
            
            ProcessList = ProcessUnitList.Select(m => new { pId = m.Process.ProcessId }).Distinct().Select(anon => ProcessUnitList.First(u => u.Process.ProcessId == anon.pId).Process).ToList();
            CanAddFlag = true;
            if (ProjectProcessList != null)
            {
                
                ProjectProcessUnitGroups = new ObservableCollection<ProcessUnitGroup>();
                foreach (var item in ProjectProcessList)
                {
                    var processUnit = ProcessUnitList.First(m => m.Id == item.ProcessUnitId);
                    item.ProcessUnit = processUnit;
                    var processUnitGroup = new ProcessUnitGroup();
                    processUnitGroup.ProcessUnitList = ProcessUnitList;
                    processUnitGroup.ProcessList = ProcessList;
                    processUnitGroup.ProcessSource = new ObservableCollection<ProcessVm>(ProcessList);
                    processUnitGroup.SelectedProcess = processUnit.Process;
                    processUnitGroup.SelectedProUnit = processUnit.ProUnit;
                    
                    App.Current.Dispatcher.Invoke(() =>
                    {
                        ProjectProcessUnitGroups.Add(processUnitGroup);
                    });                    
                }
            }
        }

        private async Task LoadProcessTemplate()
        {
            var list = await _service.GetProcessTemplateList();
            ProcessTemplateList = new ObservableCollection<ProcessTemplateVm>(list);
        }
        [RelayCommand]
        private async Task LoadTemplateData()
        {
            if (SelectedTemplate != null)
            {

                var template = await _service.GetProcessTemplate(SelectedTemplate.Id.Value);
                App.Current.Dispatcher.Invoke(() =>
                {
                    if (template.ProcessTemplateDetails != null)
                    {
                        foreach (var item in template.ProcessTemplateDetails)
                        {

                            var processUnit = ProcessUnitList.FirstOrDefault(m => m.ProcessId == item.ProcessUnit.ProcessId && m.UnitId == item.ProcessUnit.UnitId);
                            
                            var process = new ProjectProcessVm
                            {
                                ProcessUnitId = processUnit.Id
                            };
                            if (ProjectId != null) process.ProjectId = ProjectId.Value;
                            if (ProjectProcessList == null) ProjectProcessList = new ObservableCollection<ProjectProcessVm>();
                            process.ProcessUnit = ProcessUnitList.First(m => m.Id == item.ProcessUnitId);
                            ProjectProcessList.Add(process);
                        }
                    }
                });
            }            
        }
        [RelayCommand]
        private async Task LoadProjectTemplateData()
        {
            if (SelectedProject != null)
            {
                var projectProcess = await _projectService.GetProjectProcesses(SelectedProject.ProjectId.Value);
                ProjectProcessList = new ObservableCollection<ProjectProcessVm>(projectProcess);
            }
        }

        [RelayCommand]
        private void AddItem()
        {
            var item = new ProcessUnitGroup();
            item.ProcessUnitList = ProcessUnitList;
            item.ProcessList = ProcessList;
            item.ProcessSource = new ObservableCollection<ProcessVm>(ProcessList);
            ProcessUnitGroups.Add(item);
        }

        [RelayCommand]
        private void ReturnProjectProcess()
        {
            if (ProjectProcessList == null) ProjectProcessList = new ObservableCollection<ProjectProcessVm>();
            if (ProcessUnitGroups.Count > 0)
            {
                foreach (var item in ProcessUnitGroups)
                {
                    if (item.SelectedProcess == null) continue;
                    if (item.SelectedProUnit == null)
                    {
                        MessageBox.Warning("未选择单位");
                        return;
                    }
                    var processUnit = ProcessUnitList.FirstOrDefault(m => m.ProcessId == item.SelectedProcess.ProcessId && m.UnitId == item.SelectedProUnit.UnitId);
                    if (processUnit == null)
                    {
                        MessageBox.Warning("工序单位不存在");
                        return;
                    }
                    var process = new ProjectProcessVm
                    {
                        ProcessUnitId = processUnit.Id,
                        Workload = item.Workload
                    };
                    if (ProjectId != null) process.ProjectId = ProjectId.Value;
                    
                    ProjectProcessList.Add(process);
                }
            }
            WeakReferenceMessenger.Default.Send(ProjectProcessList, MessageToken.ReturnProjectProcess);

        }
        [RelayCommand]
        public void ProjectProcessUnitRemove(ProcessUnitVm removeItem)
        {
            if (ProjectProcessUnitGroups != null)
            {
                var item = ProjectProcessUnitGroups.FirstOrDefault(m => m.ProcessUnitList.Any(w => w.ProcessId == removeItem.ProcessId && w.UnitId == removeItem.UnitId));
                if (item != null) ProjectProcessUnitGroups.Remove(item);
            }            
            var ppitem = ProjectProcessList.FirstOrDefault(m => m.ProcessUnit == removeItem);
            ProjectProcessList.Remove(ppitem);                     
        }

        private async Task LoadProjectNames()
        {
            ProjectNameList = await _projectService.LoadProjectNames();
        }

        private async Task<List<ProjectProcessVm>> GetProjectProcesses(Guid projectId)
        {
            return await _projectService.GetProjectProcesses(projectId);
        }






    }
}
