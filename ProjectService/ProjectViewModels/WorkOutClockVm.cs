using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectViewModels
{
    public class WorkOutClockVm : ObservableObject
    {
        private Guid _workOutClockId;
        public Guid WorkOutClockId
        {
            get => _workOutClockId;
            set => SetProperty(ref _workOutClockId, value);
        }
        private DateTime _outClockDateTime;
        public DateTime OutClockDateTime
        {
            get => _outClockDateTime;
            set => SetProperty(ref _outClockDateTime, value);
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
        private string _workYearMonth;
        public string WorkYearMonth
        {
            get => _workYearMonth;
            set => SetProperty(ref _workYearMonth, value);
        }
    }
}
