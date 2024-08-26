using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectViewModels
{
    public class EarlyWarningHistoryVm : ObservableObject
    {
        private long id;
        public long Id
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
        private DictDataVm? warningTypeData;
        public DictDataVm? WarningTypeData
        {
            get => warningTypeData;
            set => SetProperty(ref warningTypeData, value);
        }

        private double? warningValue;
        public double? WarningValue
        {
            get => warningValue; set => SetProperty(ref warningValue, value);
        }
        private string? warningMessage;
        public string? WarningMessage
        {
            get => warningMessage; set => SetProperty(ref warningMessage, value);
        }
        private ProjectVm? project;
        public ProjectVm? Project
        {
            get => project;
            set => SetProperty(ref project, value);
        }
        private DateTime? createTime;
        public DateTime? CreateTime
        {
            get => createTime;
            set => SetProperty(ref createTime, value);
        }
        private int? status;
        public int? Status
        {
            get => status;
            set => SetProperty(ref status, value);
        }
        private DictDataVm? statusData;
        public DictDataVm? StatusData
        {
            get => statusData;
            set => SetProperty(ref statusData, value);
        }
        private string? staffReason;
        public string? StaffReason
        {
            get => staffReason; set => SetProperty(ref staffReason, value);
        }
        private string? managerReason;
        public string? ManagerReason
        {
            get => managerReason; set => SetProperty(ref managerReason, value);
        }
        private string? suggestions;
        public string? Suggestions
        {
            get => suggestions; set => SetProperty(ref suggestions, value);
        }
        
    }
}
