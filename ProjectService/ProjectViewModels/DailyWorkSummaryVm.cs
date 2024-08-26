using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectViewModels
{
    public class DailyWorkSummaryVm : ObservableObject
    {
        private string? _staffName;
        public string? StaffName
        {
            get => _staffName;
            set => SetProperty(ref _staffName, value);
        }

        private string? _staffCard;
        public string? StaffCard
        {
            get => _staffCard;
            set => SetProperty(ref _staffCard, value);
        }
        private string? _projectName;
        public string? ProjectName
        {
            get => _projectName;
            set => SetProperty(ref _projectName, value);
        }

        private string? _processName;
        public string? ProcessName
        {
            get => _processName;
            set => SetProperty(ref _processName, value);
        }
        private string? _unitName;
        public string? UnitName
        {
            get => _unitName;
            set => SetProperty(ref _unitName, value);
        }
        private double? _sumWorkload;
        public double? SumWorkload
        {
            get => _sumWorkload;
            set => SetProperty(ref _sumWorkload, value);
        }
    }
}
