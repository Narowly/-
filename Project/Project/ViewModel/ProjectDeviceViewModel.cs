using CommunityToolkit.Mvvm.ComponentModel;
using Project.Services.DataServices;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectViewModels;
using Project.Common;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using Project.Views.UserControls;
using System.Collections;
using System.Security.Cryptography.Xml;

namespace Project.ViewModel
{
    public partial class ProjectDeviceViewModel:ObservableRecipient
    {
        private readonly IContainer? _container;
        private readonly DeviceService _deviceService;
        private readonly DictService _dictService;
        
        private Guid? projectId;
        private List<DeviceVm> DeviceList = null!;
        [ObservableProperty]
        private ObservableCollection<DeviceVm>? bindingDeviceList;
        [ObservableProperty]
        private DeviceVm? selectedDevice;
        [ObservableProperty]
        private ObservableCollection<DeviceVm>? selectedDeviceList = new ObservableCollection<DeviceVm>();
        [ObservableProperty]
        private ObservableCollection<DeviceVm>? selectedProjectDeviceList = new ObservableCollection<DeviceVm>();

        [ObservableProperty]
        private ObservableCollection<DeviceVm>? projectDeviceList;
        [ObservableProperty]
        private DeviceVm? selectedProjectDevice;
        [ObservableProperty]
        private DeviceReqs deviceReq;
        public IAsyncRelayCommand SearchDeviceCmd { get; }
        public ProjectDeviceViewModel(DeviceService deviceService, Guid? projectId, ObservableCollection<DeviceVm>? inProjectDevices, DictService dictService)
        {
            this.projectId = projectId;
            ProjectDeviceList = inProjectDevices;
            ProjectDeviceList ??= [];
            DeviceReq = new DeviceReqs();
            _container = App.Current.Properties[MessageToken.AppContainer] as IContainer;
            _deviceService = deviceService;
            _dictService = dictService;
            SearchDeviceCmd = new AsyncRelayCommand(SearchDeviceAsync);
            Task.Run(LoadDataAsync);
            
        }
        private async Task LoadDataAsync()
        {
            await LoadDeviceList();
        }

        private async Task LoadDeviceList()
        {
            DeviceReq.Status = await _dictService.GetDictDataId(DictSettings.DeviceStatusTypeName, DictSettings.DeviceStatus_Normal);
            DeviceList = await _deviceService.GetDeviceList(DeviceReq);
            foreach (var device in ProjectDeviceList)
            {
                var removeItem = DeviceList.FirstOrDefault(m => m.DeviceId == device.DeviceId);
                DeviceList.Remove(removeItem);
            }
            BindingDeviceList = new ObservableCollection<DeviceVm>(DeviceList);
        }
        
        private async Task SearchDeviceAsync()
        {
            foreach (var device in ProjectDeviceList)
            {
                var removeItem = DeviceList.FirstOrDefault(m => m.DeviceId == device.DeviceId);
                DeviceList.Remove(removeItem);
            }
            var searchResult = await _deviceService.GetDeviceListLocal(DeviceReq, DeviceList);
            if (ProjectDeviceList != null)
            {
                searchResult = searchResult.Except(ProjectDeviceList).ToList();
            }            
            BindingDeviceList = new ObservableCollection<DeviceVm>(searchResult);
        }
        [RelayCommand]
        private void UpdateLeftSelectedItems(object parameter)
        {
            var selectedLeftItems = parameter as IList;
            if (selectedLeftItems != null)
            {
                SelectedDeviceList.Clear();
                foreach (var item in selectedLeftItems)
                {
                    if (item is DeviceVm device)
                    {
                        SelectedDeviceList.Add(device);
                    }
                }
            }
        }

        [RelayCommand]
        private void UpdateRightSelectedItems(object parameter)
        {
            var selectedRightItems = parameter as IList;
            if (selectedRightItems != null)
            {
                SelectedProjectDeviceList.Clear();
                foreach (var item in selectedRightItems)
                {
                    if (item is DeviceVm device)
                    {
                        SelectedProjectDeviceList.Add(device);
                    }
                }
            }
        }
        [RelayCommand]
        private void Add()
        {
            if (SelectedDeviceList != null && SelectedDeviceList.Count > 0)
            {
                var removeList = new List<DeviceVm>(SelectedDeviceList);
                foreach (var device in removeList)
                {                    
                    ProjectDeviceList.Add(device);
                    DeviceList.Remove(device);
                    BindingDeviceList.Remove(device);
                }                
            }
            //if (SelectedDevice == null) return;
        }

        [RelayCommand]
        private void Remove()
        {
            if(SelectedProjectDeviceList!=null && SelectedProjectDeviceList.Count > 0)
            {
                var removeList = new List<DeviceVm>(SelectedProjectDeviceList);
                foreach (var device in removeList)
                {
                    DeviceList.Add(device);
                    BindingDeviceList.Add(device);
                    ProjectDeviceList.Remove(device);
                }
            }
            //if (SelectedProjectDevice == null) return;
            //BindingDeviceList.Add(SelectedProjectDevice);
            //ProjectDeviceList.Remove(selectedProjectDevice);
        }
        [RelayCommand]
        private void ReturnProjectDevice()
        {
            if (ProjectDeviceList != null)
                WeakReferenceMessenger.Default.Send(ProjectDeviceList, MessageToken.ReturnProjectDevice);            
        }
    }
}
