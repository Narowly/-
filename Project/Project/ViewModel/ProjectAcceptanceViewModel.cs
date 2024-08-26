using Autofac.Core;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using HandyControl.Controls;
using Project.Services;
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
    public partial class ProjectAcceptanceViewModel : ObservableObject
    {
        private readonly Guid _projectId;
        private readonly ProjectService _projectService;
        private ProjectVm _project = null!;
        public ProjectVm Project
        {
            get => _project;
            set => SetProperty(ref _project, value);
        }
        private ObservableCollection<string>? _sumWorkloads;
        public ObservableCollection<string>? SumWorkloads
        {
            get => _sumWorkloads;
            set => SetProperty(ref _sumWorkloads, value);
        }
        private AcceptanceReq? _acceptanceReq;
        public AcceptanceReq? Req
        {
            get => _acceptanceReq;
            set => SetProperty(ref _acceptanceReq, value);
        }
        public ProjectAcceptanceViewModel(Guid projectId, ProjectService projectService)
        {
            _projectId = projectId;
            _projectService = projectService;
            Req = new AcceptanceReq();
            Task.Run(LoadDataAsync);
        }
        private async Task LoadDataAsync()
        {
            await LoadProject();
        }

        private async Task LoadProject()
        {            
            SumWorkloads = new ObservableCollection<string>();
            Project = await _projectService.GetProjectById(_projectId);
            Req.ProjectId = Project.ProjectId;
            if (Project.AcceptanceDate != null) Req.AcceptanceDate = Project.AcceptanceDate.Value;
            Application.Current.Dispatcher.Invoke(() =>
            {
                if (Project.ProjectProcesses != null)
                {
                    foreach (var process in Project.ProjectProcesses)
                    {
                        if (process.ProjectDailyWorks != null && process.ProcessUnit != null)
                        {
                            var sumworkload = process.ProjectDailyWorks.Sum(m => m.Workload);
                            SumWorkloads.Add($"{process.ProcessUnit.Process.ProcessName}：{sumworkload}");
                        }
                    }
                }
            });            
        }
        [RelayCommand]
        private async Task AcceptanceProject()
        {
            var result = await _projectService.AcceptanceProject(Req);
            if (result)
            {
                Growl.Info("验收更改成功");
            }
        }
    }
}
