using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectViewModels
{
    public class EarlyWarningVm : ObservableObject
    {
        private ProjectEarlyWarningVm? startWarningDays;
        public ProjectEarlyWarningVm? StartWarningDays
        {
            get => startWarningDays;
            set => SetProperty(ref startWarningDays, value);
        }

        private ProjectEarlyWarningVm? totalCountWarning;
        public ProjectEarlyWarningVm? TotalCountWarning
        {
            get => totalCountWarning;
            set => SetProperty(ref totalCountWarning, value);
        }

        private ProjectEarlyWarningVm? scheduleWarning;
        public ProjectEarlyWarningVm? ScheduleWarning
        {
            get => scheduleWarning;
            set => SetProperty(ref scheduleWarning, value);
        }

        private ProjectEarlyWarningVm? efficiencyWarning;
        public ProjectEarlyWarningVm? EfficiencyWarning
        {
            get => efficiencyWarning;
            set => SetProperty(ref efficiencyWarning, value);
        }

    }

    public class ProjectEarlyWarningVm : ObservableObject
    {
        private int? id;
        public int? Id
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
        private int warningType;
        public int WarningType
        {
            get => warningType;
            set => SetProperty(ref warningType, value);
        }

        private double? warningValue;
        public double? WarningValue
        {
            get => warningValue; set => SetProperty(ref warningValue, value);
        }        
    }
}
