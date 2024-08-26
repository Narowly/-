using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using HandyControl.Controls;
using Project.Services.DataServices;
using ProjectViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MessageBox = HandyControl.Controls.MessageBox;

namespace Project.ViewModel
{
    public partial class AddDeviceViewModel : ObservableObject
    {
        private readonly DeviceService _deviceService;
        private readonly Guid? _deviceId;
        private readonly DictService _dictService;
        private readonly StaffService _staffService;
        private DeviceVm _device = new();
        public DeviceVm Device
        {
            get => _device;
            set => SetProperty(ref _device, value);
        }
        private List<DeviceTypeVm> _deviceTypeList = [];
        public List<DeviceTypeVm> DeviceTypeList
        {
            get => _deviceTypeList;
            set => SetProperty(ref _deviceTypeList, value);
        }
        private DeviceTypeVm? _selectedType;
        public DeviceTypeVm? SelectedType
        {
            get => _selectedType;
            set
            {
                SetProperty(ref _selectedType, value);
            }
        }
        private List<DictDataVm> _statusDictList = [];
        public List<DictDataVm> StatusDictList
        {
            get => _statusDictList;
            set => SetProperty(ref _statusDictList, value);
        }
        private DictDataVm? _selectedStatus;
        public DictDataVm? SelectedStatus
        {
            get => _selectedStatus;
            set => SetProperty(ref _selectedStatus, value);
        }        
        
        public AddDeviceViewModel(Guid? deviceId, DeviceService deviceService, DictService dictService, StaffService staffService)
        {
            _deviceId = deviceId;
            _deviceService = deviceService;
            _dictService = dictService;
            _staffService = staffService;
            Task.Run(LoadDataAsync);
        }
        [RelayCommand]
        private async Task LoadDataAsync()
        {
            await Task.WhenAll(GetDeviceTypeList(), LoadDeviceStatusDict()).ContinueWith(c=> LoadDevice());            
        }

        private async Task LoadDevice()
        {
            if (_deviceId != null)
            {
                Device = await _deviceService.GetDeviceById(_deviceId.Value);
                SelectedStatus = StatusDictList.FirstOrDefault(m => m.DictCode == Device.DeviceStatus);
                SelectedType = DeviceTypeList.FirstOrDefault(m => m.DeviceTypeId == Device.DeviceTypeId);
            }
                
        }

        private async Task LoadDeviceStatusDict()
        {
            StatusDictList = await _dictService.GetDictDataByTypeName("DeviceStatus");
        }
        private async Task GetDeviceTypeList()
        {
            var list = await _deviceService.GetDeviceTypeList();
            if (list != null)
                DeviceTypeList = new List<DeviceTypeVm>(list);
        }

        [RelayCommand]
        private async Task AddDevice()
        {
            if (string.IsNullOrWhiteSpace(Device.DeviceNumber))
            {
                MessageBox.Warning("设备编号不能为空");
                return;
            }
            if (SelectedStatus == null || SelectedType == null || SelectedType.DeviceTypeId == null)
            {
                MessageBox.Warning("类型及状态不能为空");
                return;
            }
            Device.DeviceStatus = SelectedStatus.DictCode;
            Device.DeviceTypeId = SelectedType.DeviceTypeId.Value;
            var result = await _deviceService.SaveDevice(Device);
            if (result) Growl.Success("保存成功");
        }
    }
}
