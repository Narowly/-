using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectViewModels
{
    public class ApplicationVm : ObservableObject
    {
        private Guid? _applicationId;
        public Guid? ApplicationId
        {
            get => _applicationId;
            set => SetProperty(ref _applicationId, value);
        }
        private Guid _projectId;
        public Guid ProjectId
        {
            get => _projectId;
            set => SetProperty(ref _projectId, value);
        }
        private int _applicationType;
        public int ApplicationType
        {
            get => _applicationType;
            set => SetProperty(ref _applicationType, value);
        }
        private Guid _applicationUser;
        public Guid ApplicationUser
        {
            get => _applicationUser;
            set => SetProperty(ref _applicationUser, value);
        }
        private string _applicationTitle = null!;
        public string ApplicationTitle
        {
            get => _applicationTitle;
            set => SetProperty(ref _applicationTitle, value);
        }
        private string _applicationContent = null!;
        public string ApplicationContent
        {
            get => _applicationContent;
            set => SetProperty(ref _applicationContent, value);
        }
        private int? _applicationItemCount;
        public int? ApplicationItemCount
        {
            get => _applicationItemCount;
            set => SetProperty(ref _applicationItemCount, value);
        }
        private string? _applicationDelivery;
        public string? ApplicationDelivery
        {
            get => _applicationDelivery;
            set => SetProperty(ref _applicationDelivery, value);
        }
        private DateTime _applicationTime;
        public DateTime ApplicationTime
        {
            get => _applicationTime;
            set => SetProperty(ref _applicationTime, value);
        }

        private bool _isDeleted;
        public bool IsDeleted
        {
            get => _isDeleted;
            set => SetProperty(ref _isDeleted, value);
        }

        private int _applicationStatus;
        public int ApplicationStatus
        {
            get => _applicationStatus;
            set => SetProperty(ref _applicationStatus, value);
        }
        private string? _applicationResContent;
        public string? ApplicationResContent
        {
            get => _applicationResContent;
            set => SetProperty(ref _applicationResContent, value);
        }
        private DateTime? _applicationResTime;
        public DateTime? ApplicationResTime
        {
            get => _applicationResTime;
            set => SetProperty(ref _applicationResTime,value);
        }
        private Guid? _applicationResUserId;
        public Guid? ApplicationResUserId
        {
            get => _applicationResUserId;
            set => SetProperty(ref _applicationResUserId, value);
        }
        private string? _remarks;
        public string? Remarks
        {
            get => _remarks;
            set => SetProperty(ref _remarks, value);
        }
        private string? _applicationUserName;
        public string? ApplicationUserName
        {
            get => _applicationUserName;
            set => SetProperty(ref _applicationUserName, value);
        }
        private string? _projectName;
        public string? ProjectName
        {
            get => _projectName;
            set => SetProperty(ref _projectName, value);
        }
        private string? _applicationResUserName;
        public string? ApplicationResUserName
        {
            get => _applicationResUserName;
            set => SetProperty(ref _applicationResUserName, value);
        }
        private List<ApplicationConsumableVm>? _consumableList;
        public List<ApplicationConsumableVm>? ConsumableList
        {
            get => _consumableList;
            set => SetProperty(ref _consumableList, value);
        }
        private List<ApplicationDeviceVm>? _deviceList;
        public List<ApplicationDeviceVm>? DeviceList
        {
            get => _deviceList;
            set => SetProperty(ref _deviceList, value);
        }
        private List<ApplicationPersonVm>? _personList;
        public List<ApplicationPersonVm>? PersonList
        {
            get => _personList;
            set => SetProperty(ref _personList, value);
        }
        private string? _applicationStatusName;
        public string? ApplicationStatusName
        {
            get => _applicationStatusName;
            set => SetProperty(ref _applicationStatusName, value);
        }
        private string? _applicationTypeName;
        public string? ApplicationTypeName
        {
            get => _applicationTypeName;
            set => SetProperty(ref _applicationTypeName, value);
        }
        private ProjectVm? _project;
        public ProjectVm? Project
        {
            get => _project;
            set => SetProperty(ref _project, value);
        }
    }
}
