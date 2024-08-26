using Autofac.Core;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using HandyControl.Controls;
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
using System.Printing;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using MessageBox = HandyControl.Controls.MessageBox;
using Autofac;
using CommunityToolkit.Mvvm.Messaging;
using Microsoft.Win32;
using RestSharp;

namespace Project.ViewModel
{
    public partial class ProjectSettingsViewModel : ObservableObject
    {
        private Guid _projectId;
        private ProjectService _projectService;
        private DailyProcessService _dailyProcessService;
        private ProjectBonusService _bonusService;
        private EarlyWarningService _earlyWarningService;
        private StaffService _staffService;
        private DeviceService _deviceService;
        private AttachmentsService _attachmentsService;
        private readonly IContainer? _container = App.Current.Properties[MessageToken.AppContainer] as IContainer;
        private ProjectVm _project = null!;        
        public ProjectVm Project
        {
            get => _project;
            set => SetProperty(ref _project, value);
        }
        private decimal? _totalProcessWeight;
        public decimal? TotalProcessWeight
        {
            get => _totalProcessWeight;
            set
            {
                SetProperty(ref _totalProcessWeight, value);
            }
        }
        private ObservableCollection<ProjectDailyProcessHistoryVm>? dailyProcessHistory;
        public ObservableCollection<ProjectDailyProcessHistoryVm>? DailyProcessHistory
        {
            get => dailyProcessHistory;
            set => SetProperty(ref dailyProcessHistory, value);
        }

        private ObservableCollection<ProjectBonusSettingsVm>? projectBonus;
        public ObservableCollection<ProjectBonusSettingsVm>? ProjectBonus
        {
            get => projectBonus;
            set => SetProperty(ref projectBonus, value);
        }

        private ProjectBonusExVm? projectBonusEx;
        public ProjectBonusExVm? ProjectBonusEx
        {
            get => projectBonusEx;
            set => SetProperty(ref projectBonusEx, value);
        }

        private EarlyWarningVm earlyWarnings = null!;
        public EarlyWarningVm EarlyWarnings
        {
            get => earlyWarnings;
            set => SetProperty(ref earlyWarnings, value);
        }
        private ObservableCollection<string>? inProjectStaffNames;
        public ObservableCollection<string>? InProjectStaffNames
        {
            get => inProjectStaffNames;
            set => SetProperty(ref inProjectStaffNames, value);
        }

        private ObservableCollection<string>? inProjectDeviceNames;
        public ObservableCollection<string>? InProjectDeviceNames
        {
            get => inProjectDeviceNames;
            set => SetProperty(ref inProjectDeviceNames, value);
        }

        public ProjectSettingsViewModel(Guid projectId, ProjectService projectService, DailyProcessService dailyProcessService, 
            ProjectBonusService bonusService, EarlyWarningService earlyWarningService, StaffService staffService, 
            DeviceService deviceService,AttachmentsService fileUploadService )
        {
            _projectId = projectId;
            _projectService = projectService;
            _dailyProcessService = dailyProcessService;
            _earlyWarningService = earlyWarningService;
            _bonusService = bonusService;
            _staffService = staffService;
            _deviceService = deviceService;
            _attachmentsService = fileUploadService;
            Task.Run(LoadDataAsync);
            RegistMessager();
            
        }
        private async Task LoadDataAsync()
        {
            await LoadProject();
            await LoadDailyProcessHistory();
            await LoadProjectBonus();
            await LoadEarlyWarnings();
            await LoadProjectAttachments();
            await LoadProjectBonusEx();
        }
        private void RegistMessager()
        {
            WeakReferenceMessenger.Default.Register(this, MessageToken.ReturnProjectStaff, (MessageHandler<ProjectSettingsViewModel, ObservableCollection<StaffVm>>)(async (r, m) =>
            {
                Project.InProjectStaffs = m;
                var result = await _staffService.SaveProjectStaffs(Project);
                if (result)
                {

                    WeakReferenceMessenger.Default.Send(string.Empty, MessageToken.CloseProjectStaff);
                    Growl.Success("保存成功");
                    await LoadProject();
                }                
            }));
            WeakReferenceMessenger.Default.Register(this, MessageToken.CloseProjectStaff, (MessageHandler<ProjectSettingsViewModel, string>)((r, m) =>
            {
                ProcessStaffPopWindow?.Close();
            }));
            WeakReferenceMessenger.Default.Register(this, MessageToken.ReturnProjectDevice, (MessageHandler<ProjectSettingsViewModel, ObservableCollection<DeviceVm>>)(async (r, m) =>
            {
                Project.InProjectDevice = m;
                var result = await _deviceService.SaveProjectDevice(Project);
                if (result)
                {
                    WeakReferenceMessenger.Default.Send(string.Empty, MessageToken.CloseProjectDevice);
                    Growl.Success("保存成功");
                    await LoadProject();
                }                
            }));
            WeakReferenceMessenger.Default.Register(this, MessageToken.CloseProjectDevice, (MessageHandler<ProjectSettingsViewModel, string>)((r, m) =>
            {
                ProjectDevicePopWindow?.Close();
            }));
        }
        
        private async Task LoadProjectBonusEx()
        {
            ProjectBonusEx = await _bonusService.GetBonusEx(_projectId);
            if (ProjectBonusEx == null) ProjectBonusEx = new ProjectBonusExVm { ProjectId = _projectId };
        }
        private async Task LoadProject()
        {
            await Application.Current.Dispatcher.Invoke(async () =>
            {
                Project = await _projectService.GetProjectById(_projectId);
                if (Project.InProjectStaffs != null)
                {
                    InProjectStaffNames = new ObservableCollection<string>(Project.InProjectStaffs.Select(m => m.StaffName));
                }
                if (Project.InProjectDevice != null)
                {
                    InProjectDeviceNames = new ObservableCollection<string>(Project.InProjectDevice.Select(m => string.Format("{0} {1} {2}", m.DeviceNumber, m.DeviceType.DeviceTypeName, m.DeviceType.DeviceModel)));
                }                
            });
        }
        [RelayCommand]
        private async Task UpdateProjectStartDate()
        {
            await Application.Current.Dispatcher.Invoke(async () =>
            {
                await _projectService.UpdateStartDate(Project);
                Growl.Success("保存成功");
            });
        }
        private bool ProcessWeightCheck()
        {
            var totalWeight = Project.ProjectProcesses?.Sum(m => m.Weight);
            if (totalWeight != 100)
            {
                MessageBox.Warning($"权重相加必须等于100，目前工序总和:{totalWeight}", "工序设置");
                return false;
            }
            return true;
        }
        [RelayCommand]
        private async Task UpdateProjectProcess()
        {
            if (ProcessWeightCheck())
            {
                await Application.Current.Dispatcher.Invoke(async () =>
                {
                    await _projectService.UpdateProjectProcess(Project);
                    Growl.Success("保存成功");
                });
            }
        }

        private async Task LoadDailyProcessHistory()
        {
            await Application.Current.Dispatcher.Invoke(async () =>
            {
                var result = await _dailyProcessService.DailyProcessHistory(_projectId);
                if (result != null && result.Count > 0) { result[0].IsFirstItem = true; }
                DailyProcessHistory = new ObservableCollection<ProjectDailyProcessHistoryVm>(result);
            });
        }
        [RelayCommand]
        private async Task SaveProjectDailyProcess()
        {
            var result = await _dailyProcessService.SaveProjectDailyProcess(DailyProcessHistory.ToList());
            if (result)
            {
                Growl.Success("保存成功");
            }
        }
        [RelayCommand]
        private async Task UpdateProjectDailyProcess(object param)
        {
            if (param is ProjectDailyProcessHistoryVm history)
            {
                history.DailyProcessList = SetDailyProcessList(history);
                var result = await _dailyProcessService.UpdateProjectDailyProcess(history.DailyProcessList);
                if (result) Growl.Success("保存成功");
            }
        }

        private List<ProjectDailyProcessVm> SetDailyProcessList(ProjectDailyProcessHistoryVm history)
        {
            var index = 0;
            foreach (var item in history.DailyProcessList)
            {
                item.StartDate = history.StartDate;
                if (index == 0) item.Remarks = history.Remarks;
                index++;
            }
            return history.DailyProcessList;
        }
        [RelayCommand]
        private void AddProjectDailyProcess()
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                var cloneHistory = DailyProcessHistory.First();
                foreach (var item in dailyProcessHistory)
                {
                    item.IsFirstItem = false;
                }
                var newHistory = new ProjectDailyProcessHistoryVm();
                newHistory.StartDate = DateTime.Now.Date;
                newHistory.IsFirstItem = true;
                newHistory.DailyProcessList = new List<ProjectDailyProcessVm>();
                foreach (var item in cloneHistory.DailyProcessList)
                {
                    newHistory.DailyProcessList.Add(new ProjectDailyProcessVm
                    {
                        ProjectProcessId = item.ProjectProcessId,
                        DailyWorkload = item.DailyWorkload,
                        StartDate = DateTime.Now.Date,
                        ProjectProcess = item.ProjectProcess
                    });
                }
                DailyProcessHistory.Add(newHistory);
                DailyProcessHistory = new ObservableCollection<ProjectDailyProcessHistoryVm>(DailyProcessHistory.OrderByDescending(m => m.StartDate));
            });
        }

        [RelayCommand]
        private async Task RemoveProjectDailyProcess(ProjectDailyProcessHistoryVm history)
        {
            history.DailyProcessList = SetDailyProcessList(history);
            if (history.DailyProcessList.FirstOrDefault().Id == null)
            {
                DailyProcessHistory.Remove(history);
            }
            else
            {
                if (DailyProcessHistory.Count <= 1) { MessageBox.Warning("不能删除唯一的日工作量设置"); return; }
                var result = await _dailyProcessService.RemoveProjectDailyProcess(history.DailyProcessList);
                if (result)
                {
                    Growl.Success("删除成功");
                    DailyProcessHistory.Remove(history);
                }
            }
            DailyProcessHistory[0].IsFirstItem = true;
        }
        [RelayCommand]
        private async Task SaveBonusEx()
        {
            if (ProjectBonusEx == null || ProjectBonusEx.Bonus == null || ProjectBonusEx.PlanPersonDays == null
                || ProjectBonusEx.Rewards == null || ProjectBonusEx.Penalty == null)
            {
                MessageBox.Warning("字段不能为空");
                return;                
            }
            var result = await _bonusService.SaveBonusEx(ProjectBonusEx);
            if (result)
            {
                await LoadProjectBonusEx().ContinueWith(t => Growl.Success("保存成功"));
            }
        }

        private async Task LoadProjectBonus()
        {
            await Application.Current.Dispatcher.Invoke(async () =>
            {
                var list = await _bonusService.GetBonusList(_projectId);

                ProjectBonus = new ObservableCollection<ProjectBonusSettingsVm>();
                if (Project != null && Project.ProjectProcesses != null)
                {
                    var projectProcessList = Project.ProjectProcesses.ToList();
                    foreach (var process in projectProcessList)
                    {
                        var existProjectProcess = list.FirstOrDefault(m => m.ProjectProcess.Id == process.Id);
                        if (existProjectProcess == null)
                        {
                            ProjectBonus.Add(new ProjectBonusSettingsVm
                            {
                                ProjectProcess = process,
                                ProjectBonusList = new ObservableCollection<ProjectBonusVm>
                                {
                                    new ProjectBonusVm
                                    {
                                        ProjectProcessId=process.Id
                                    }
                                }
                            });
                        }
                        else
                        {
                            ProjectBonus.Add(existProjectProcess);
                        }
                    }
                }
            });
        }

        [RelayCommand]
        private async Task SaveBonus()
        {
            var result = await _bonusService.SaveBonus(ProjectBonus.SelectMany(m => m.ProjectBonusList).ToList());
            if (result) Growl.Success("保存成功");
        }
        [RelayCommand]
        private void AddProjectProcessBonus(ProjectBonusSettingsVm vm)
        {
            if (vm.ProjectBonusList == null) return;
            vm.ProjectBonusList.Add(new ProjectBonusVm
            {
                ProjectProcessId = vm.ProjectProcess.Id

            });
        }

        [RelayCommand]
        private void RemoveProjectProcessBonus(ProjectBonusVm vm)
        {
            if (ProjectBonus == null) return;
            var pp = ProjectBonus.First(m => m.ProjectProcess.Id == vm.ProjectProcessId);
            if (pp.ProjectBonusList != null && pp.ProjectBonusList.Count > 1)
            {
                var removeItem = pp.ProjectBonusList.Where(m => m.Id == vm.Id && m.Bonus == vm.Bonus && m.ProjectProcessId == vm.ProjectProcessId && m.Workload == vm.Workload).First();
                pp.ProjectBonusList.Remove(removeItem);
            }
            else
            {
                MessageBox.Warning("不能删除唯一项");
            }
        }

        private async Task LoadEarlyWarnings()
        {
            var warnings = await _earlyWarningService.GetProjectEarlyWarnings(_projectId);
            if (warnings != null)
            {
                EarlyWarnings = warnings;
            }
            else
            {
                MessageBox.Warning("数据库预警类型未设置");
            }
        }
        [RelayCommand]
        private async Task SaveEarlyWarning()
        {
            if (EarlyWarnings == null || EarlyWarnings.TotalCountWarning == null
                || EarlyWarnings.StartWarningDays == null
                || EarlyWarnings.EfficiencyWarning == null
                || EarlyWarnings.ScheduleWarning == null) return;
            var list = new List<ProjectEarlyWarningVm>
            {
                EarlyWarnings.TotalCountWarning,
                EarlyWarnings.StartWarningDays,
                EarlyWarnings.EfficiencyWarning,
                EarlyWarnings.ScheduleWarning
            };

            var result = await _earlyWarningService.SaveProjectEarlyWarnings(list);
            if (result)
            {
                Growl.Success("保存成功");
            }
        }
        private PopWindow? ProcessStaffPopWindow;
        [RelayCommand]
        private void OpenProjectStaff()
        {
            ProcessStaffPopWindow = new PopWindow();
            var projectId = new ResolvedParameter(
                (pi, ctx) => pi.Name == "projectId",
                (pi, ctx) => _projectId);
            var projectStaffList = new ResolvedParameter(
                (pi, ctx) => pi.Name == "inProjectStaffs",
                (pi, ctx) => Project.InProjectStaffs);
            if (_container != null)
            {
                var vm = _container.Resolve<ProjectStaffSetViewModel>(projectId, projectStaffList);
                var view = new ProjectStaffUc
                {
                    DataContext = vm
                };
                ProcessStaffPopWindow.controlHost.Content = view;
                ProcessStaffPopWindow.ShowDialog();
            }            
        }
        private PopWindow? ProjectDevicePopWindow;
        [RelayCommand]
        private void OpenProjectDevice()
        {
            ProjectDevicePopWindow = new PopWindow();
            var projectId = new ResolvedParameter(
                (pi, ctx) => pi.Name == "projectId",
                (pi, ctx) => _projectId);
            var projectDeviceList = new ResolvedParameter(
                (pi, ctx) => pi.Name == "inProjectDevices",
                (pi, ctx) => Project.InProjectDevice);
            if (_container != null)
            {
                var vm = _container.Resolve<ProjectDeviceViewModel>(projectId, projectDeviceList);
                var view = new ProjectDeviceUc
                {
                    DataContext = vm
                };
                ProjectDevicePopWindow.controlHost.Content = view;
                ProjectDevicePopWindow.ShowDialog();
            }
        }
        private PopWindow? ProcessStaffRelatedWindow;
        [RelayCommand]
        private void OpenProcessStaffRelated()
        {
            var projectId = new ResolvedParameter(
                (pi, ctx) => pi.Name == "projectId",
                (pi, ctx) => _projectId);
            if (_container == null) return;
            var vm=_container.Resolve<ProcessStaffRelatedViewModel>(projectId);
            var view = _container.Resolve<ProcessStaffRelatedView>();
            view.DataContext = vm;
            ProcessStaffRelatedWindow = new PopWindow()
            {
                Width = 600,
                Height = 600
            };
            ProcessStaffRelatedWindow.controlHost.Content = view;
            ProcessStaffRelatedWindow.ShowDialog();
        }
        [RelayCommand]
        private async Task UpdateProjectPlanData()
        {
            var result = await _projectService.UpdateProjectPlanData(Project);
            if (result)
            {
                Growl.Success("保存成功");
            }
        }
        private ObservableCollection<string>? selectedFiles;
        public ObservableCollection<string>? SelectedFiles
        {
            get => selectedFiles;
            set=> SetProperty(ref selectedFiles, value);
        }
        
        [RelayCommand]
        private void SelectFiles()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Multiselect = true,
                Filter = "All files (*.*)|*.*"
            };

            if (openFileDialog.ShowDialog() == true)
            {
                if (SelectedFiles == null) SelectedFiles = new ObservableCollection<string>();
                SelectedFiles.Clear();
                foreach (var item in openFileDialog.FileNames)
                {
                    SelectedFiles.Add(item);
                }                
            }
        }
        [RelayCommand]
        private async Task UploadMultipleFiles()
        {
            if (selectedFiles == null) return;
            var result = await _attachmentsService.UploadMultipleFilesAsync(SelectedFiles, _projectId);
            if (result)
            {
                await LoadProjectAttachments().ContinueWith(m => {
                    Application.Current.Dispatcher.Invoke(() =>
                    {
                        SelectedFiles.Clear();
                    });                    
                    Growl.Success("上传成功");
                });
            }
        }
        private ObservableCollection<ProjectAttachmentVm>? projectAttachments;
        public ObservableCollection<ProjectAttachmentVm>? ProjectAttachments
        {
            get => projectAttachments;
            set => SetProperty(ref projectAttachments, value);
        }
        private ProjectAttachmentVm? selectAttachment;
        public ProjectAttachmentVm? SelectAttachment
        {
            get => selectAttachment;
            set => SetProperty(ref selectAttachment, value);
        }

        private async Task LoadProjectAttachments()
        {
            var list = await _attachmentsService.GetProjectAttachments(_projectId);
            ProjectAttachments = new ObservableCollection<ProjectAttachmentVm>(list);
        }
        [RelayCommand]
        private async Task RemoveAttachment()
        {
            if (SelectAttachment != null)
            {
                var result = await _attachmentsService.RemoveProjectAttachment(SelectAttachment);
                if (result)
                {
                    projectAttachments.Remove(SelectAttachment);
                    Growl.Success("删除成功");
                }
            }            
        }

        [RelayCommand]
        private async Task DownloadAttachment(ProjectAttachmentVm vm)
        {
            var result = await _attachmentsService.DownloadAttachment(vm);
            if (result)
            {
                MessageBox.Success("下载完成！");
            }
        }
    }
}
