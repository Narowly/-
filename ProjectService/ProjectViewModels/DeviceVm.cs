using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectViewModels
{
    public partial class DeviceVm : ObservableObject
    {
        private Guid? _deviceId;
        public Guid? DeviceId
        {
            get => _deviceId;
            set => SetProperty(ref _deviceId, value);
        }
        private string _deviceNumber = null!;
        public string DeviceNumber
        {
            get => _deviceNumber;
            set => SetProperty(ref _deviceNumber, value);
        } 
        
        private Guid _deviceTypeId;
        public Guid DeviceTypeId
        {
            get=> _deviceTypeId;
            set=> SetProperty(ref _deviceTypeId, value);
        }
        
        private int _deviceStatus;
        public int DeviceStatus
        {
            get => _deviceStatus;
            set => SetProperty(ref _deviceStatus, value);
        }

        private string? _remarks;
        public string? Remarks
        {
            get => _remarks;
            set => SetProperty(ref _remarks, value);
        }
        
        private DeviceTypeVm? _deviceType = null!;        
        public DeviceTypeVm? DeviceType
        {
            get => _deviceType;
            set => SetProperty(ref _deviceType, value);
        }
        private string? _deviceWithProjectName;
        public string? DeviceWithProjectName
        {
            get => _deviceWithProjectName;
            set => SetProperty(ref _deviceWithProjectName, value);
        }
    }

    public class DeviceStockVm : ObservableObject
    {
        private DeviceVm? _device;
        public DeviceVm? Device
        {
            get => _device;
            set => SetProperty(ref _device, value);
        }
        private ProjectVm? _project;
        public ProjectVm? Project
        {
            get => _project;
            set => SetProperty(ref _project, value);
        }
    }

}
