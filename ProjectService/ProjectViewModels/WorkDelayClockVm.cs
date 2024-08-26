using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectViewModels
{
    public partial class WorkDelayClockVm : ObservableObject
    {
        private string _delayClockId;
        public string DelayClockId
        {
            get => _delayClockId;
            set => SetProperty(ref _delayClockId, value);
        }

        private Guid _staffId;
        public Guid StaffId
        {
            get => _staffId;
            set => SetProperty(ref _staffId, value);
        }
        private string _staffAccount;
        public string StaffAccount
        {
            get => _staffAccount;
            set => SetProperty(ref _staffAccount, value);
        }

        private string _reason;
        public string Reason
        {
            get => _reason;
            set => SetProperty(ref _reason, value);
        }

        private DateTime _delayClockTime;
        public DateTime DelayClockTime
        {
            get => _delayClockTime;
            set => SetProperty(ref _delayClockTime, value);
        }

        private string _applyWorkTime;
        public string ApplyWorkTime
        {
            get => _applyWorkTime;
            set => SetProperty( ref _applyWorkTime, value);
        }
        private string _workYearMonth;
        public string WorkYearMonth
        {
            get => _workYearMonth;
            set => SetProperty(ref _workYearMonth, value);
        }
    }
}
