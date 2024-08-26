using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectViewModels
{
    public class ProjectDailyProcessVm : ObservableObject
    {
        private long? id;
        public long? Id
        {
            get => id;
            set => SetProperty(ref id, value);
        }
        private Guid projectProcessId;
        public Guid ProjectProcessId
        {
            get => projectProcessId;
            set => SetProperty(ref projectProcessId, value);
        }
        private ProjectProcessVm? projectProcess;
        public ProjectProcessVm? ProjectProcess
        {
            get => projectProcess;
            set => SetProperty(ref projectProcess, value);
        }
        private double dailyWorkload;
        public double DailyWorkload
        {
            get => dailyWorkload; set => SetProperty(ref dailyWorkload, value);
        }
        private DateTime startDate;
        public DateTime StartDate
        {
            get => startDate;set => SetProperty(ref startDate, value);
        }
        private string? remarks;
        public string? Remarks
        {
            get => remarks;
            set => SetProperty(ref  remarks, value);
        }
    }

    public class ProjectDailyProcessHistoryVm:ObservableObject
    {
        private List<ProjectDailyProcessVm> dailyProcessList = null!;
        public List<ProjectDailyProcessVm> DailyProcessList
        {
            get => dailyProcessList;
            set => SetProperty(ref dailyProcessList, value);
        }
        private DateTime startDate;
        public DateTime StartDate
        {
            get => startDate;
            set => SetProperty(ref startDate, value);
        }
        private string? remarks;
        public string? Remarks
        {
            get => remarks;
            set => SetProperty(ref remarks, value);
        }
        private bool isFirstItem;
        public bool IsFirstItem
        {
            get => isFirstItem;
            set => SetProperty(ref isFirstItem, value);
        }
    }
}
