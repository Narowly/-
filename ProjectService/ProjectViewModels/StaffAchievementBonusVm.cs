using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectViewModels
{
    public class StaffAchievementBonusVm : ObservableObject
    {
        //    {
        //        StaffId = group.Key.StaffId,
        //        ProjectProcessId = group.Key.ProjectProcessId,
        //        Year = group.Key.Year,
        //        Month = group.Key.Month,
        //        AverageWorkloadPerDay = group.Sum(pdw => pdw.Workload) / group.Count()
        //    }).ToList();
        private Guid _staffId;
        public Guid StaffId
        {
            get => _staffId;
            set => SetProperty(ref _staffId, value);
        }
        private Guid _projectProcessId;
        public Guid ProjectProcessId
        {
            get => _projectProcessId;
            set => SetProperty(ref _projectProcessId, value);
        }

        private int _year;
        public int Year
        {
            get => _year;
            set => SetProperty(ref _year, value);
        }
        private int _month;
        public int Month
        {
            get => _month;
            set => SetProperty(ref _month, value);
        }
        
        public int AverageWorkloadPerDay
        {
            get
            {
                if (SumWorkload != 0 && UsedDays != 0)
                {
                    return (int)SumWorkload / (int)UsedDays;
                }
                return 0;
            }
        }

        private double? _standard;
        public double? Standard
        {
            get => _standard;
            set => SetProperty(ref _standard, value);
        }
        //private ProjectDailyProcessVm? _projectDailyProcess;
        //public ProjectDailyProcessVm? ProjectDailyProcess
        //{
        //    get => _projectDailyProcess;
        //    set => SetProperty(ref _projectDailyProcess, value);
        //}
        private double _sumworkload;
        public double SumWorkload
        {
            get => _sumworkload;
            set => SetProperty(ref _sumworkload, value);
        }
        private double _usedDays;
        public double UsedDays
        {
            get => _usedDays;
            set => SetProperty(ref _usedDays, value);
        }
        private decimal _sumBonus;
        public decimal SumBonus
        {
            get => _sumBonus;
            set => SetProperty(ref _sumBonus, value);
        }
        private string _projectName;
        public string ProjectName
        {
            get => _projectName;
            set => SetProperty(ref _projectName, value);
        }
        private string _staffName;
        public string StaffName
        {
            get => _staffName;
            set => SetProperty(ref _staffName, value);
        }
        private string _processShowName;
        public string ProcessShowName
        {
            get => _processShowName;
            set => SetProperty(ref _processShowName, value);
        }
        private List<ProjectBonusVm> _bonusList = [];
        public List<ProjectBonusVm> BonusList
        {
            get => _bonusList;
            set => SetProperty(ref _bonusList, value);
        }
    }

    public class ProjectAchievementBonusVm : ObservableObject
    {
        private Guid _projectId;
        public Guid ProjectId
        {
            get => _projectId;
            set => SetProperty(ref _projectId, value);
        }
        private string _projectName;
        public string ProjectName
        {
            get => _projectName;
            set => SetProperty(ref _projectName, value);
        }
        private Guid _staffId;
        public Guid StaffId
        {
            get => _staffId;
            set => SetProperty(ref _staffId, value);
        }
        private string _staffName;
        public string StaffName
        {
            get => _staffName;
            set => SetProperty(ref _staffName, value);
        }
        private double _bonus;
        public double Bonus
        {
            get => _bonus;
            set => SetProperty(ref _bonus, value);
        }
        private int _planPersonDays;
        public int PlanPersonDays
        {
            get => _planPersonDays;
            set => SetProperty(ref _planPersonDays, value);
        }
        private int _verifyPersonDays;
        public int VerifyPersonDays
        {
            get => _verifyPersonDays;
            set => SetProperty(ref _verifyPersonDays, value);
        }

    }
}
