using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectViewModels
{
    public class ProjectBonusExVm : ObservableObject
    {
        private long? _id;
        public long? Id
        {
            get => _id; set => SetProperty(ref _id, value);
        }
        private Guid? _projectId;
        public Guid? ProjectId
        {
            get => _projectId; set => SetProperty(ref _projectId, value);
        }

        private int? _planPersonDays;
        public int? PlanPersonDays
        {
            get => _planPersonDays; set => SetProperty(ref _planPersonDays, value);
        }
        private double? _bonus;
        public double? Bonus
        {
            get => _bonus; set => SetProperty(ref _bonus, value);
        }

        private double? _rewards;
        public double? Rewards
        {
            get => _rewards; set => SetProperty(ref _rewards, value);
        }
        private double? _penalty;
        public double? Penalty
        {
            get => _penalty; set => SetProperty(ref _penalty, value);
        }

        private int? _verifyPersonDays;
        public int? VerifyPersonDays
        {
            get => _verifyPersonDays; set => SetProperty(ref _verifyPersonDays, value);
        }
        private double? _verifyBonus;
        public double? VerifyBonus
        {
            get => _verifyBonus; set => SetProperty(ref _verifyBonus, value);
        }
        private List<DateOnly>? _verifyWorkDays;
        public List<DateOnly>? VerifyWorkDays
        {
            get => _verifyWorkDays; set => SetProperty(ref _verifyWorkDays, value);
        }
    }
}
