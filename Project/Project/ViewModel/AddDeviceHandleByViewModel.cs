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
    public partial class AddDeviceHandleByViewModel : ObservableObject
    {
        private readonly DeviceService _deviceService;
        private readonly StaffService _staffService;
        private Guid _deviceId;

        private List<StaffVm>? StaffList;
        [ObservableProperty]
        private ObservableCollection<StaffVm>? bindingStaffList;
        [ObservableProperty]
        private StaffVm? selectedStaff;
        private string? _searchStaffText;
        public string? SearchStaffText
        {
            get => _searchStaffText;
            set
            {
                SetProperty(ref _searchStaffText, value);
                if (!string.IsNullOrWhiteSpace(_searchStaffText))
                {
                    var list = StaffList?.Where(m => m.StaffName.Contains(_searchStaffText)).ToList();
                    if (list != null) BindingStaffList = new ObservableCollection<StaffVm>(list);
                }
                else
                {
                    BindingStaffList = null;
                }
            }
        }
        public AddDeviceHandleByViewModel(Guid deviceId, DeviceService deviceService, StaffService staffService)
        {
            _deviceId = deviceId;
            _deviceService = deviceService;
            _staffService = staffService;
            Task.Run(LoadStaffList);
        }
        private async Task LoadStaffList()
        {
            StaffList = await _staffService.GetStaffList();
        }
        [RelayCommand]
        private async Task Save()
        {
            if (SelectedStaff == null)
            {
                MessageBox.Warning("处理人不能为空");
                return;
            }
            var result = await _deviceService.SetHandleBy(_deviceId, SelectedStaff.StaffId);
            if (result)
            {
                Growl.Success("保存成功");
            }
        }
    }
}
