using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;

namespace ProjectViewModels
{    
    public partial class ProjectProcessVm : ObservableObject
    {        
        private Guid id;
        public Guid Id
        {
            get => id;
            set => SetProperty(ref id, value);
        }

        
        private Guid projectId;
        public Guid ProjectId
        {
            get => projectId;
            set => SetProperty(ref projectId, value);
        }
        private ProjectVm? project;
        public ProjectVm? Project
        {
            get => project;
            set => SetProperty(ref project, value);
        }
        
        private int processUnitId;
        public int ProcessUnitId
        {
            get => processUnitId;
            set => SetProperty(ref processUnitId, value);
        }
        
        private decimal weight;
        public decimal Weight
        {
            get => weight;
            set => SetProperty(ref weight, value);
        }
        
        private int sequence;
        public int Sequence
        {
            get => sequence;
            set => SetProperty(ref sequence, value);
        }
        
        private double workload;
        public double Workload
        {
            get => workload;
            set => SetProperty(ref workload, value);
        }
        
        private double startingWorkload;
        public double StartingWorkload
        {
            get => startingWorkload;
            set => SetProperty(ref startingWorkload, value);
        }
        
        private ProcessUnitVm? processUnit;
        public ProcessUnitVm? ProcessUnit
        {
            get => processUnit;
            set => SetProperty(ref processUnit, value);
        }
        
        private string? remarks;
        public string? Remarks
        {
            get => remarks;
            set => SetProperty(ref remarks, value);
        }
        private ObservableCollection<ProjectDailyWorkVm>? projectDailyWorks;
        public ObservableCollection<ProjectDailyWorkVm>? ProjectDailyWorks
        {
            get => projectDailyWorks;
            set => SetProperty(ref projectDailyWorks, value);
        }

        
    }
}
