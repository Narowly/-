using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.ObjectModel;

namespace ProjectViewModels
{
    public partial class ProjectVm : ObservableObject
    {
        private Guid? _projectId;
        public Guid? ProjectId
        {
            get => _projectId;
            set => SetProperty(ref _projectId, value);
        }

        private string _projectName = null!;
        public string ProjectName
        {
            get => _projectName;
            set => SetProperty(ref _projectName, value);
        }

        private Guid _contractId;
        public Guid ContractId
        {
            get => _contractId;
            set => SetProperty(ref _contractId, value);
        }

        private DateTime? _startDate;
        public DateTime? StartDate
        {
            get => _startDate;
            set => SetProperty(ref _startDate, value);
        }

        private DateTime? _createTime;
        public DateTime? CreateTime
        {
            get => _createTime;
            set => SetProperty(ref _createTime, value);
        }

        private int _status;
        public int Status
        {
            get => _status;
            set => SetProperty(ref _status, value);
        }

        private DateTime? _planEndDate;
        public DateTime? PlanEndDate
        {
            get => _planEndDate;
            set => SetProperty(ref _planEndDate, value);
        }

        private int? _planPersonDays;
        public int? PlanPersonDays
        {
            get => _planPersonDays;
            set => SetProperty(ref _planPersonDays, value);
        }

        private int? _regionId;
        public int? RegionId
        {
            get => _regionId;
            set => SetProperty(ref _regionId, value);
        }

        private DateTime? _acceptanceDate;
        public DateTime? AcceptanceDate
        {
            get => _acceptanceDate;
            set => SetProperty(ref _acceptanceDate, value);
        }

        private ContractVm? _contract;
        public ContractVm? Contract
        {
            get => _contract;
            set => SetProperty(ref _contract, value);
        }

        //private RegionVm? _region;
        //public RegionVm? Region
        //{
        //    get => _region;
        //    set => SetProperty(ref _region, value);
        //}

        private StaffVm? _projectManager;
        public StaffVm? ProjectManager
        {
            get => _projectManager;
            set => SetProperty(ref _projectManager, value);
        }

        private StaffVm? _salesManager = null!;
        public StaffVm? SalesManager
        {
            get => _salesManager;
            set => SetProperty(ref _salesManager, value);
        }
        [ObservableProperty]
        private string? remarks;

        
        private ObservableCollection<ProjectProcessVm>? projectProcesses;
        public ObservableCollection<ProjectProcessVm>? ProjectProcesses
        {
            get => projectProcesses;
            set => SetProperty(ref projectProcesses, value);
        }

        private ObservableCollection<StaffVm>? inProjectStaffs;
        public ObservableCollection<StaffVm>? InProjectStaffs
        {
            get => inProjectStaffs;
            set => SetProperty(ref inProjectStaffs, value);
        }
        
        private ObservableCollection<DeviceVm>? inProjectDevice;
        public ObservableCollection<DeviceVm>? InProjectDevice
        {
            get => inProjectDevice; 
            set => SetProperty(ref inProjectDevice, value);
        }

        private DictDataVm? region;
        public DictDataVm? Region
        {
            get => region; 
            set => SetProperty(ref region, value);
        }
        private string? address;
        public string? Address
        {
            get => address;
            set => SetProperty(ref address, value);
        }
        public int ProjectStaffCount
        {
            get => InProjectStaffs?.Count ?? 0;
        }
        public int ProjectDeviceCount
        {
            get => InProjectDevice?.Count ?? 0;
        }
        public double UsedDays
        {
            get
            {
                //if (StartDate != null && DateTime.Now > StartDate)
                //{
                //    return Math.Floor((DateTime.Now - StartDate.Value).TotalDays);
                //}
                //else
                //{
                //    return 0;
                //}
                if (ProjectProcesses != null && ProjectProcesses.Count > 0)
                {
                    var dailyWorkList = ProjectProcesses.Where(m => m.ProjectDailyWorks != null).SelectMany(m => m.ProjectDailyWorks).ToList();
                    return dailyWorkList.GroupBy(m => new { m.BillDate, m.StaffId }).Count();
                }
                return 0;
            }
        }
        private double timePercentage;
        public double TimePercentage
        {
            get
            {                
                if (StartDate != null && DateTime.Now > StartDate && PlanEndDate != null)
                {
                    DateTime current = DateTime.Now;
                    long startToCurrentTimeMillis = (current - StartDate.Value).Ticks / TimeSpan.TicksPerMillisecond;
                    long startToEndMillis = (PlanEndDate.Value - StartDate.Value).Ticks / TimeSpan.TicksPerMillisecond;
                    double percentage = (double)startToCurrentTimeMillis / startToEndMillis * 100;
                    timePercentage = percentage;
                    return timePercentage;
                }
                else
                {
                    return 0;
                }
            }
            set =>SetProperty(ref timePercentage, value);
        }
        private double workloadPercentage;
        public double WorkloadPercentage
        {
            get
            {
                double result = 0;
                ProcessWorkloadPercentage = [];
                if (ProjectProcesses != null)
                {                    
                    foreach (var process in ProjectProcesses)
                    {
                        if (process.ProjectDailyWorks != null && process.ProjectDailyWorks.Count > 0)
                        {
                            var processSumWorkload = process.ProjectDailyWorks.Sum(m => m.Workload);
                            result += (double)(process.Weight / 100) * (processSumWorkload / process.Workload) * 100;
                            ProcessWorkloadPercentage.Add($"工序：{process.ProcessUnit.ShowName}，完成：{(processSumWorkload / process.Workload)*100}%");
                        }
                    }
                }
                workloadPercentage = result;
                return workloadPercentage;
            }
            set => SetProperty(ref workloadPercentage, value);
        }

        private List<string> _processWorkloadPercentage = [];
        public List<string> ProcessWorkloadPercentage
        {
            get => _processWorkloadPercentage;
            set => SetProperty(ref _processWorkloadPercentage, value);
        }

        private ObservableCollection<ProjectPaymentTermVm>? projectPaymentTerms;
        public ObservableCollection<ProjectPaymentTermVm>? ProjectPyamentTerms
        {
            get => projectPaymentTerms;
            set => SetProperty(ref projectPaymentTerms, value);
        }
        private string? computerCountLabel;
        public string? ComputerCountLabel
        {
            get
            {
                var count = 0;
                if (InProjectDevice != null&&InProjectDevice.Count>0)
                {
                    count = InProjectDevice.Where(m => m.DeviceType.DeviceTypeName.Contains("主机") && (m.DeviceType.DeviceUnit == null || m.DeviceType.DeviceUnit == "台")).Count();
                }
                computerCountLabel = string.Format("主机:{0}", count);
                return computerCountLabel;
            }
            set => SetProperty(ref computerCountLabel, value);
        }
        private string? printerCountLabel;
        public string? PrinterCountLabel
        {
            get
            {
                var count = 0;
                if (InProjectDevice != null && InProjectDevice.Count > 0)
                {
                    count = InProjectDevice.Where(m => m.DeviceType.DeviceTypeName.Contains("打印") && (m.DeviceType.DeviceUnit == null || m.DeviceType.DeviceUnit == "台")).Count();
                }
                printerCountLabel = string.Format("打印:{0}", count);
                return printerCountLabel;
            }
            set => SetProperty(ref printerCountLabel, value);
        }
        private string? scannerCountLabel;
        public string? ScannerCountLabel
        {
            get
            {
                var count = 0;
                if (InProjectDevice != null && InProjectDevice.Count > 0)
                {
                    count = InProjectDevice.Where(m => m.DeviceType.DeviceTypeName.Contains("扫描") && (m.DeviceType.DeviceUnit == null || m.DeviceType.DeviceUnit == "台")).Count();
                }
                scannerCountLabel = string.Format("扫描:{0}", count);
                return scannerCountLabel;
            }
            set => SetProperty(ref scannerCountLabel, value);
        }

        public List<DateOnly>? CountUniqueDaysOfWorkAcrossProcesses()
        {
            if (ProjectProcesses == null || !ProjectProcesses.Any())
            {
                return null;
            }

            var uniqueDays = ProjectProcesses
                .SelectMany(process => process.ProjectDailyWorks?.Select(work => work.BillDate) ?? Enumerable.Empty<DateOnly>())
                .Distinct().ToList();

            return uniqueDays;
        }

        public int VerifyWorkDays
        {
            get
            {
                if (ProjectProcesses == null || !ProjectProcesses.Any())
                {
                    return 0;
                }
                foreach (var process in ProjectProcesses)
                {
                }
                return ProjectProcesses.Sum(m => m.ProjectDailyWorks?.Count) ?? 0;
            }            
        }

        private string? _lastUpdateScheduleReason;
        public string? LastUpdateScheduleReason
        {
            get => _lastUpdateScheduleReason;
            set => SetProperty(ref _lastUpdateScheduleReason, value);
        }
    }

    public class ProjectAutoCompleteModel : ObservableObject
    {
        private string _name = null!;
        public string Name
        {
            get => _name;
            set => SetProperty(ref _name, value);
        }

        private string _number = null!;
        public string Number
        {
            get => _number;
            set => SetProperty(ref _number, value);
        }
        private Guid? _id;
        public Guid? Id
        {
            get => _id;
            set => SetProperty(ref _id, value);
        }
        private Guid _projectManagerId;
        public Guid ProjectManagerId
        {
            get => _projectManagerId;
            set => SetProperty(ref _projectManagerId, value);
        }
    }
}