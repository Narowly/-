using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectViewModels
{
    public class WorkAttendanceVm : ObservableObject
    {
        private Guid _workAttendanceId;
        public Guid WorkAttendanceId
        {
            get => _workAttendanceId;
            set => SetProperty(ref _workAttendanceId, value);
        }

        private string _workYearMonth;
        public string WorkYearMonth
        {
            get => _workYearMonth;
            set => SetProperty(ref _workYearMonth, value);
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
        private string _staffName;
        public string StaffName
        {
            get => _staffName;
            set => SetProperty(ref _staffName, value);
        }

        private string _projectName;
        public string ProjectName
        {
            get => _projectName; 
            set => SetProperty(ref _projectName, value); 
        }

        private double _totalWorkDays;
        public double TotalWorkDays
        {
            get => _totalWorkDays;
            set => SetProperty(ref _totalWorkDays, value);
        }

        private int _totalProcessDays;
        public int TotalProcessDays
        {
            get => _totalProcessDays;
            set => SetProperty(ref _totalProcessDays, value);
        }

        private string? _abnormal;
        public string? Abnormal
        {
            get => _abnormal;
            set => SetProperty(ref _abnormal, value);
        }

        private string? _remarks;
        public string? Remarks
        {
            get => _remarks;
            set => SetProperty(ref _remarks, value);
        }

        private List<string> _processDays = [];
        public List<string> ProcessDays
        {
            get => _processDays;
            set => SetProperty(ref _processDays, value);
        }

        private string? _clockIn01;
        public string? ClockIn01
        {
            get => _clockIn01;
            set => SetProperty(ref _clockIn01, value);
        }

        private string? _clockOut01;
        public string? ClockOut01
        {
            get => _clockOut01;
            set => SetProperty(ref _clockOut01, value);
        }

        private string? _clockIn02;
        public string? ClockIn02
        {
            get => _clockIn02;
            set => SetProperty(ref _clockIn02, value);
        }

        private string? _clockOut02;
        public string? ClockOut02
        {
            get => _clockOut02;
            set => SetProperty(ref _clockOut02, value);
        }

        private string? _clockIn03;
        public string? ClockIn03
        {
            get => _clockIn03;
            set => SetProperty(ref _clockIn03, value);
        }

        private string? _clockOut03;
        public string? ClockOut03
        {
            get => _clockOut03;
            set => SetProperty(ref _clockOut03, value);
        }

        private string? _clockIn04;
        public string? ClockIn04
        {
            get => _clockIn04;
            set => SetProperty(ref _clockIn04, value);
        }

        private string? _clockOut04;
        public string? ClockOut04
        {
            get => _clockOut04;
            set => SetProperty(ref _clockOut04, value);
        }

        private string? _clockIn05;
        public string? ClockIn05
        {
            get => _clockIn05;
            set => SetProperty(ref _clockIn05, value);
        }

        private string? _clockOut05;
        public string? ClockOut05
        {
            get => _clockOut05;
            set => SetProperty(ref _clockOut05, value);
        }
        private string? _clockIn06;
        public string? ClockIn06
        {
            get => _clockIn06;
            set => SetProperty(ref _clockIn06, value);
        }

        private string? _clockOut06;
        public string? ClockOut06
        {
            get => _clockOut06;
            set => SetProperty(ref _clockOut06, value);
        }
        private string? _clockIn07;
        public string? ClockIn07
        {
            get => _clockIn07;
            set => SetProperty(ref _clockIn07, value);
        }

        private string? _clockOut07;
        public string? ClockOut07
        {
            get => _clockOut07;
            set => SetProperty(ref _clockOut07, value);
        }

        private string? _clockIn08;
        public string? ClockIn08
        {
            get => _clockIn08;
            set => SetProperty(ref _clockIn08, value);
        }

        private string? _clockOut08;
        public string? ClockOut08
        {
            get => _clockOut08;
            set => SetProperty(ref _clockOut08, value);
        }

        private string? _clockIn09;
        public string? ClockIn09
        {
            get => _clockIn09;
            set => SetProperty(ref _clockIn09, value);
        }

        private string? _clockOut09;
        public string? ClockOut09
        {
            get => _clockOut09;
            set => SetProperty(ref _clockOut09, value);
        }

        private string? _clockIn10;
        public string? ClockIn10
        {
            get => _clockIn10;
            set => SetProperty(ref _clockIn10, value);
        }

        private string? _clockOut10;
        public string? ClockOut10
        {
            get => _clockOut10;
            set => SetProperty(ref _clockOut10, value);
        }

        private string? _clockIn11;
        public string? ClockIn11
        {
            get => _clockIn11;
            set => SetProperty(ref _clockIn11, value);
        }

        private string? _clockOut11;
        public string? ClockOut11
        {
            get => _clockOut11;
            set => SetProperty(ref _clockOut11, value);
        }
        private string? _clockIn12;
        public string? ClockIn12
        {
            get => _clockIn12;
            set => SetProperty(ref _clockIn12, value);
        }

        private string? _clockOut12;
        public string? ClockOut12
        {
            get => _clockOut12;
            set => SetProperty(ref _clockOut12, value);
        }
        private string? _clockIn13;
        public string? ClockIn13
        {
            get => _clockIn13;
            set => SetProperty(ref _clockIn13, value);
        }

        private string? _clockOut13;
        public string? ClockOut13
        {
            get => _clockOut13;
            set => SetProperty(ref _clockOut13, value);
        }

        private string? _clockIn14;
        public string? ClockIn14
        {
            get => _clockIn14;
            set => SetProperty(ref _clockIn14, value);
        }

        private string? _clockOut14;
        public string? ClockOut14
        {
            get => _clockOut14;
            set => SetProperty(ref _clockOut14, value);
        }
        private string? _clockIn15;
        public string? ClockIn15
        {
            get => _clockIn15;
            set => SetProperty(ref _clockIn15, value);
        }
        private string? _clockOut15;
        public string? ClockOut15
        {
            get => _clockOut15;
            set => SetProperty(ref _clockOut15, value);
        }
        private string? _clockIn16;
        public string? ClockIn16
        {
            get => _clockIn16;
            set => SetProperty(ref _clockIn16, value);
        }
        private string? _clockOut16;
        public string? ClockOut16
        {
            get => _clockOut16;
            set => SetProperty(ref _clockOut16, value);
        }
        private string? _clockIn17;
        public string? ClockIn17
        {
            get => _clockIn17;
            set => SetProperty(ref _clockIn17, value);
        }

        private string? _clockOut17;
        public string? ClockOut17
        {
            get => _clockOut17;
            set => SetProperty(ref _clockOut17, value);
        }
        private string? _clockIn18;
        public string? ClockIn18
        {
            get => _clockIn18;
            set => SetProperty(ref _clockIn18, value);
        }

        private string? _clockOut18;
        public string? ClockOut18
        {
            get => _clockOut18;
            set => SetProperty(ref _clockOut18, value);
        }

        private string? _clockIn19;
        public string? ClockIn19
        {
            get => _clockIn19;
            set => SetProperty(ref _clockIn19, value);
        }

        private string? _clockOut19;
        public string? ClockOut19
        {
            get => _clockOut19;
            set => SetProperty(ref _clockOut19, value);
        }

        private string? _clockIn20;
        public string? ClockIn20
        {
            get => _clockIn20;
            set => SetProperty(ref _clockIn20, value);
        }

        private string? _clockOut20;
        public string? ClockOut20
        {
            get => _clockOut20;
            set => SetProperty(ref _clockOut20, value);
        }

        private string? _clockIn21;
        public string? ClockIn21
        {
            get => _clockIn21;
            set => SetProperty(ref _clockIn21, value);
        }

        private string? _clockOut21;
        public string? ClockOut21
        {
            get => _clockOut21;
            set => SetProperty(ref _clockOut21, value);
        }

        private string? _clockIn22;
        public string? ClockIn22
        {
            get => _clockIn22;
            set => SetProperty(ref _clockIn22, value);
        }

        private string? _clockOut22;
        public string? ClockOut22
        {
            get => _clockOut22;
            set => SetProperty(ref _clockOut22, value);
        }
        private string? _clockIn23;
        public string? ClockIn23
        {
            get => _clockIn23;
            set => SetProperty(ref _clockIn23, value);
        }

        private string? _clockOut23;
        public string? ClockOut23
        {
            get => _clockOut23;
            set => SetProperty(ref _clockOut23, value);
        }

        private string? _clockIn24;
        public string? ClockIn24
        {
            get => _clockIn24;
            set => SetProperty(ref _clockIn24, value);
        }

        private string? _clockOut24;
        public string? ClockOut24
        {
            get => _clockOut24;
            set => SetProperty(ref _clockOut24, value);
        }
        private string? _clockIn25;
        public string? ClockIn25
        {
            get => _clockIn25;
            set => SetProperty(ref _clockIn25, value);
        }

        private string? _clockOut25;
        public string? ClockOut25
        {
            get => _clockOut25;
            set => SetProperty(ref _clockOut25, value);
        }

        private string? _clockIn26;
        public string? ClockIn26
        {
            get => _clockIn26;
            set => SetProperty(ref _clockIn26, value);
        }

        private string? _clockOut26;
        public string? ClockOut26
        {
            get => _clockOut26;
            set => SetProperty(ref _clockOut26, value);
        }

        private string? _clockIn27;
        public string? ClockIn27
        {
            get => _clockIn27;
            set => SetProperty(ref _clockIn27, value);
        }

        private string? _clockOut27;
        public string? ClockOut27
        {
            get => _clockOut27;
            set => SetProperty(ref _clockOut27, value);
        }

        private string? _clockIn28;
        public string? ClockIn28
        {
            get => _clockIn28;
            set => SetProperty(ref _clockIn28, value);
        }

        private string? _clockOut28;
        public string? ClockOut28
        {
            get => _clockOut28;
            set => SetProperty(ref _clockOut28, value);
        }

        private string? _clockIn29;
        public string? ClockIn29
        {
            get => _clockIn29;
            set => SetProperty(ref _clockIn29, value);
        }

        private string? _clockOut29;
        public string? ClockOut29
        {
            get => _clockOut29;
            set => SetProperty(ref _clockOut29, value);
        }

        private string? _clockIn30;
        public string? ClockIn30
        {
            get => _clockIn30;
            set => SetProperty(ref _clockIn30, value);
        }

        private string? _clockOut30;
        public string? ClockOut30
        {
            get => _clockOut30;
            set => SetProperty(ref _clockOut30, value);
        }

        private string? _clockIn31;
        public string? ClockIn31
        {
            get => _clockIn31;
            set => SetProperty(ref _clockIn31, value);
        }

        private string? _clockOut31;
        public string? ClockOut31
        {
            get => _clockOut31;
            set => SetProperty(ref _clockOut31, value);
        }
    }
}
