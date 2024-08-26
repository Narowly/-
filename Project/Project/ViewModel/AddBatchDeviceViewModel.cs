using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using HandyControl.Controls;
using Project.Services.DataServices;
using ProjectViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.ViewModel
{
    public partial class AddBatchDeviceViewModel : ObservableObject
    {
        private readonly DeviceService _deviceService;
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
        private int? _count;
        public int? Count
        {
            get => _count;
            set => SetProperty(ref _count, value);
        }
        public AddBatchDeviceViewModel(DeviceService deviceService)
        {
            _deviceService = deviceService;
            Task.Run(LoadDataAsync);
        }
        private async Task LoadDataAsync()
        {
            await GetDeviceTypeList();    
        }

        private async Task GetDeviceTypeList()
        {
            var list = await _deviceService.GetDeviceTypeList();
            if (list != null)
                DeviceTypeList = new List<DeviceTypeVm>(list);
        }
        [RelayCommand]
        private async Task Save()
        {
            if (Count != null && SelectedType != null)
            {
                var req = new BatchSaveDeviceReq { Count = Count.Value, DeviceTypeId = SelectedType.DeviceTypeId.Value };
                var result = await _deviceService.BatchSaveDevice(req);
                if (result)
                {
                    Growl.Success("保存成功");
                }
            }
            
        }
    }
}
