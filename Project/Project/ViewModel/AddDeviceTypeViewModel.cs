using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using HandyControl.Controls;
using Project.Services.DataServices;
using ProjectViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Printing;
using System.Text;
using System.Threading.Tasks;
using MessageBox = HandyControl.Controls.MessageBox;

namespace Project.ViewModel
{
    public partial class AddDeviceTypeViewModel : ObservableObject
    {
        private readonly DeviceService _deviceService;
        private Guid? _deviceTypeId;
        private DeviceTypeVm? _deviceType;
        public DeviceTypeVm? DeviceType
        {
            get => _deviceType;
            set => SetProperty(ref _deviceType, value);
        }
        public AddDeviceTypeViewModel(Guid? deviceTypeId, DeviceService deviceService)
        {
            _deviceTypeId = deviceTypeId;
            _deviceService = deviceService;
            Task.Run(LoadDeviceType);
        }
        private async Task LoadDeviceType()
        {
            if (_deviceTypeId != null) DeviceType = await _deviceService.GetDeviceTypeById(_deviceTypeId.Value);
            else DeviceType = new DeviceTypeVm();
        }
        [RelayCommand]
        private async Task Save()
        {
            if (DeviceType == null) return;
            if (string.IsNullOrWhiteSpace(DeviceType.DeviceTypeName))
            {
                MessageBox.Warning("设备类型不能为空");
                return;
            }
            var result = await _deviceService.SaveDeviceType(DeviceType);
            if (result)
            {
                Growl.Success("保存成功");
            }
        }
    }
}
