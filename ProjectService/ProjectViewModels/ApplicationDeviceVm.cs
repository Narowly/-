using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectViewModels
{
    public partial class ApplicationDeviceVm : ObservableObject
    {
        private Guid? _applicationDeviceId;
        public Guid? ApplicationDeviceId
        {
            get => _applicationDeviceId;
            set => SetProperty(ref _applicationDeviceId, value);
        }
        private Guid _applicationId;
        public Guid ApplicationId
        {
            get => _applicationId;
            set => SetProperty(ref _applicationId, value);
        }
        private Guid? _deviceTypeId;
        public Guid? DeviceTypeId
        {
            get => _deviceTypeId;
            set => SetProperty(ref _deviceTypeId, value);
        }
        private int? _quantity;
        public int? Quantity
        {
            get => _quantity;
            set => SetProperty(ref _quantity, value);
        }
        private string? _remarks;
        public string? Remarks
        {
            get => _remarks;
            set => SetProperty(ref _remarks,value);
        }
        private string? _deviceTypeName;
        public string? DeviceTypeName
        {
            get => _deviceTypeName;
            set => SetProperty(ref _deviceTypeName, value);
        }
        private string? _deviceModel;
        public string? DeviceModel
        {
            get => _deviceModel;
            set => SetProperty(ref _deviceModel, value);
        }
        public string? ShowName
        {
            get
            {
                return $"{DeviceTypeName} {DeviceModel}";
            }
        }
    }
}
