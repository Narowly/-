using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectViewModels
{
    public class ProjectDailyWorkVm : ObservableObject
    {
        private Guid? id;
        public Guid? Id
        {
            get => id;
            set => SetProperty(ref id, value);
        }
        private Guid? projectProcessId;
        public Guid? ProjectProcessId
        {
            get => projectProcessId;
            set => SetProperty(ref projectProcessId, value);
        }

        private Guid? staffId;
        public Guid? StaffId
        {
            get => staffId;
            set => SetProperty(ref staffId, value);
        }
        private double workload;
        public double Workload
        {
            get => workload; set => SetProperty(ref workload, value);
        }
        private DateTime? createTime;
        public DateTime? CreateTime
        {
            get => createTime; set => SetProperty(ref createTime, value);
        }
        private DateOnly billDate;
        public DateOnly BillDate
        {
            get => billDate; set => SetProperty(ref billDate, value);
        }
        private string? remarks;
        public string? Remarks
        {
            get => remarks; set => SetProperty(ref remarks, value);
        }
        private ProjectProcessVm? projectProcess;
        public ProjectProcessVm? ProjectProcess
        {
            get => projectProcess;
            set => SetProperty(ref projectProcess, value);
        }
        private StaffVm? staff;
        public StaffVm? Staff
        {
            get => staff;
            set => SetProperty(ref  staff, value);
        }
        private double? dailyWorkloadStandard;
        public double? DailyWorkloadStandard
        {
            get => dailyWorkloadStandard;
            set => SetProperty(ref dailyWorkloadStandard, value);
        }
        public string? WorkloadQualify
        {
            get
            {
                if (DailyWorkloadStandard.HasValue)
                {
                    if (DailyWorkloadStandard <= Workload)
                    {
                        return "是";
                    }
                    else
                    {
                        return "否";
                    }
                }
                else
                {
                    return null;
                }
            }
        }
    }
    public class BatchProjectDailyWorkVm : ProjectDailyWorkVm
    {
        private ProcessUnitVm? _processUnit;
        public ProcessUnitVm? ProcessUnit
        {
            get => _processUnit;
            set => SetProperty(ref _processUnit, value);
        }
        private Guid _projectId;
        public Guid ProjectId
        {
            get => _projectId;
            set => SetProperty(ref _projectId, value);
        }
    }

    public class ProjectDailyWorkExcelVm : ObservableObject
    {
        private string _contractNumber;
        public string ContractNumber
        {
            get => _contractNumber;
            set => SetProperty(ref _contractNumber, value);
        }

        private string _staffCard;
        public string StaffCard
        {
            get => _staffCard;
            set => SetProperty(ref _staffCard, value);
        }

        private string _processName;
        public string ProcessName
        {
            get => _processName;
            set => SetProperty(ref _processName, value);
        }

        private string _unitName;
        public string UnitName
        {
            get => _unitName;
            set => SetProperty(ref _unitName, value);
        }

        private double _workload;
        public double Workload
        {
            get => _workload;
            set => SetProperty(ref _workload, value);
        }

        private DateOnly _billDate;
        public DateOnly BillDate
        {
            get => _billDate; 
            set => SetProperty(ref _billDate, value);
        }

        private string? _remarks;
        public string? Remarks
        {
            get => _remarks;
            set => SetProperty(ref _remarks, value);
        }
    }
}
