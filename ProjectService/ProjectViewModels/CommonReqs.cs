using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectViewModels
{
    
    public partial class CommonReqs : DateReq
    {
        private string? _content;
        public string? Content
        {
            get => _content;
            set => SetProperty(ref _content, value);
        }
        private Guid? _projectId;
        public Guid? ProjectId
        {
            get => _projectId;
            set => SetProperty(ref _projectId, value);
        }
        private int? _status;
        public int? Status
        {
            get => _status;
            set => SetProperty(ref _status, value);
        }
        private PaginationParams _pagination = null!;
        public PaginationParams Pagination
        {
            get => _pagination;
            set => SetProperty(ref _pagination, value);
        }
        public CommonReqs()
        {
            Pagination = new PaginationParams();
        }
    }
    
    public partial class DateReq : ObservableObject
    {
        private DateTime? _startDate;
        public DateTime? StartDate
        {
            get => _startDate;
            set => SetProperty(ref _startDate, value);
        }
        private DateTime? _endDate;
        public DateTime? EndDate
        {
            get
            {
                // 在返回EndDate之前检查它是否小于StartDate  
                return _endDate.HasValue && !(_endDate.Value < StartDate) ? _endDate : null;
            }
            set
            {
                // 设置后备字段的值，而不是直接设置EndDate属性  
                _endDate = value?.Date > (StartDate?.Date ?? DateTime.MinValue) ? value : null;
                SetProperty(ref _endDate, _endDate);
            }
        }
    }

    public class PaginationParams : ObservableObject
    {
        private int _page;
        public int Page
        {
            get
            {
                if (_page == 0)
                    _page = 1;
                return _page;
            }
            set => SetProperty(ref _page, value);
        }
        private int _pageSize;
        public int PageSize
        {
            get
            {
                if (_pageSize == 0)
                    _pageSize = 10;
                return _pageSize;
            }
            set => SetProperty(ref _pageSize, value);
        }
        public PaginationParams()
        {
            _pageSize = 10;
            _page = 1;
        }
    }

    public class ProjectReqs : CommonReqs
    {
        private Guid? _projectManagerId;
        public Guid? ProjectManagerId
        {
            get => _projectManagerId;
            set => SetProperty(ref _projectManagerId, value);
        }
        public ProjectReqs() : base() { }
    }

    public partial class DeviceReqs : ProjectReqs
    {
        private Guid? _deviceTypeId;
        public Guid? DeviceTypeId
        {
            get => _deviceTypeId;
            set => SetProperty(ref _deviceTypeId, value);
        }
        private Guid? _deviceId;
        public Guid? DeviceId
        {
            get => _deviceId;
            set => SetProperty(ref _deviceId, value);
        }
        public DeviceReqs() : base() { }
    }
    public partial class ConsumableReqs : ProjectReqs
    {
        private Guid? _consumableTypeId;
        public Guid? ConsumableTypeId
        {
            get => _consumableTypeId;
            set => SetProperty(ref _consumableTypeId, value);
        }
        private Guid? _consumableId;
        public Guid? ConsumableId
        {
            get => _consumableId;
            set => SetProperty(ref _consumableId, value);
        }
        public ConsumableReqs() : base() { }
    }


    public partial class AcceptanceReq : ObservableObject
    {
        private Guid? _projectId;
        public Guid? ProjectId
        {
            get => _projectId;
            set => SetProperty(ref _projectId, value);
        }
        private DateTime _acceptanceDate = DateTime.Now;
        public DateTime AcceptanceDate
        {
            get => _acceptanceDate;
            set => SetProperty(ref _acceptanceDate, value);
        }
        public AcceptanceReq() : base() { }
    }
    public partial class ProjectWithStaffReq : ProjectReqs
    {
        private Guid? _staff;
        public Guid? Staff
        {
            get => _staff;
            set => SetProperty(ref _staff, value);
        }
        public ProjectWithStaffReq(): base() { }
    }

    public class BatchSaveDeviceReq
    {
        public int Count { get; set; }
        public Guid DeviceTypeId { get; set; }
    }

    public class ApplicationReq :CommonReqs
    {
        private Guid? _applicationStaff;
        public Guid? ApplicationStaff
        {
            get => _applicationStaff;
            set => SetProperty(ref _applicationStaff, value);
        }
        private int? _applicationType;
        public int? ApplicationType
        {
            get => _applicationType;
            set => SetProperty(ref _applicationType, value);
        }
        public ApplicationReq(): base() { }
    }

    public class ApplicationProcessdReq
    {
        public ProjectVm Project { get; set; }
        public Guid ApplicationId { get; set; }
        public List<StockOutBoundVm> OutConsumableList { get; set; }
    }

    public class AchievementBonusCalculateReq : ProjectWithStaffReq
    {
        private DateTime? _yearMonth;
        public DateTime? YearMonth
        {
            get => _yearMonth;
            set => SetProperty(ref _yearMonth, value);
        }
    }

    public class ConsumableAskForReq : ProjectWithStaffReq
    {
        private Guid? _consumableTypeId;
        public Guid? ConsumableTypeId
        {
            get => _consumableTypeId;
            set => SetProperty(ref _consumableTypeId, value);
        }
        public ConsumableAskForReq() : base() { }
    }
}
