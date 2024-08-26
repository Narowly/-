using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectViewModels
{
    public partial class DeviceTypeVm : ObservableObject
    {
        private Guid? _deviceTypeId;
        public Guid? DeviceTypeId
        {
            get => _deviceTypeId;
            set=>SetProperty(ref _deviceTypeId, value);
        }
        
        private string _deviceTypeName = null!;
        public string DeviceTypeName
        {
            get => _deviceTypeName;
            set=> SetProperty(ref _deviceTypeName, value);
        }
        
        private string? _deviceModel;
        public string? DeviceModel
        {
            get => _deviceModel;
            set => SetProperty(ref _deviceModel, value);
        }
        
        private string? _deviceUnit;
        public string? DeviceUnit
        {
            get=> _deviceUnit; 
            set => SetProperty(ref _deviceUnit, value);
        }
        private string? _remarks;
        public string? Remarks
        {
            get => _remarks; 
            set => SetProperty(ref _remarks, value);
        }

        public string ShowName
        {
            get
            {
                return $"{DeviceTypeName} {DeviceModel}";
            }
        }
    }
}
