using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectViewModels
{
    public class WorkApplyLeaveVm : ObservableObject
    {
        private string _workApplyLeaveId;
        public string WorkApplyLeaveId
        {
            get => _workApplyLeaveId;
            set => SetProperty(ref _workApplyLeaveId, value);
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
        private string _leaveType;
        public string LeaveType
        {
            get => _leaveType;
            set => SetProperty(ref _leaveType, value);
        }
        private DateTime _startTime;
        public DateTime StartTime
        {
            get => _startTime;
            set => SetProperty(ref _startTime, value);
        }
        private DateTime _endTime;
        public DateTime EndTime
        {
            get => _endTime;
            set => SetProperty(ref _endTime, value);
        }
        private string _workYearMonth;
        public string WorkYearMonth
        {
            get => _workYearMonth;
            set => SetProperty(ref _workYearMonth, value);
        }
    }
}
