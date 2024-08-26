using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectViewModels
{
    public class ProjectDeviceVm : ObservableObject
    {
        private Guid _associationId;
        public Guid AssociationId
        {
            get => _associationId;
            set => SetProperty(ref _associationId, value);
        }
        private Guid _deviceId;
        public Guid DeviceId
        {
            get => _deviceId;
            set => SetProperty(ref _deviceId, value);
        }
        private DeviceVm? _device;
        public DeviceVm? Device
        {
            get => _device;
            set => SetProperty(ref _device, value);
        }
        private Guid _projectId;
        public Guid ProjectId
        {
            get => _projectId;
            set => SetProperty(ref _projectId, value);
        }
        private ProjectVm? _project;
        public ProjectVm? Project
        {
            get => _project;
            set => SetProperty(ref _project, value);
        }
        private DateTime _transferInDate;
        public DateTime TransferInDate
        {
            get => _transferInDate;
            set => SetProperty(ref _transferInDate, value);
        }
        private Guid _transferInOperator;
        public Guid TransferInOperator
        {
            get => _transferInOperator;
            set => SetProperty(ref _transferInOperator, value);
        }

        private string? _transferInStaffName;
        public string? TransferInStaffName
        {
            get => _transferInStaffName;
            set => SetProperty(ref _transferInStaffName, value);
        }

        private DateTime? _transferOutDate;
        public DateTime? TransferOutDate
        {
            get => _transferOutDate;
            set => SetProperty(ref _transferOutDate, value);
        }
        private string? _transferOutStaffName;
        public string? TransferOutStaffName
        {
            get => _transferOutStaffName;
            set => SetProperty(ref _transferOutStaffName, value);
        }
        private Guid? _transferOutOperator;
        public Guid? TransferOutOperator
        {
            get => _transferOutOperator;
            set => SetProperty(ref _transferOutOperator, value);
        }
        private string? _remarks;
        public string? Remarks
        {
            get => _remarks;
            set => SetProperty(ref _remarks, value);
        }
        private string? _handleByName;
        public string? HandleByName
        {
            get => _handleByName;
            set => SetProperty(ref _handleByName, value);
        }        
    }
}
