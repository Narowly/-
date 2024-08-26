using Autofac.Core;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Project.Common;
using Project.Services.DataServices;
using Project.Views.Windows;
using ProjectViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using Project.Views.UserControls;

namespace Project.ViewModel
{
    public partial class DeviceTypeListViewModel : ObservableObject
    {
        private readonly IContainer? _container = App.Current.Properties[MessageToken.AppContainer] as IContainer;
        private readonly DeviceService _deviceService;
        private PaginatedList<DeviceTypeVm> _paginatedList = null!;
        public PaginatedList<DeviceTypeVm> PaginatedList
        {
            get => _paginatedList;
            set => SetProperty(ref _paginatedList, value);
        }

        private DeviceReqs _req = new() { Pagination = new PaginationParams { Page = 1, PageSize = 10 } };
        public DeviceReqs Req
        {
            get => _req;
            set => SetProperty(ref _req, value);
        }
        private DeviceTypeVm? _selectedDeviceType;
        public DeviceTypeVm? SelectedDeviceType
        {
            get => _selectedDeviceType;
            set => SetProperty(ref _selectedDeviceType, value);
        }
        public DeviceTypeListViewModel(DeviceService deviceService)
        {
            _deviceService = deviceService;
            Task.Run(LoadDataAsync);
        }

        private async Task LoadDataAsync()
        {
            await LoadDeviceTypeList();
        }
        [RelayCommand]
        private async Task LoadDeviceTypeList()
        {
            PaginatedList = await _deviceService.PaginatedDeviceType(Req);
        }
        private PopWindow? addWindow;
        [RelayCommand]
        private void OpenAddWindow(object type)
        {
            Guid? selectedTypeId = null;
            if (Convert.ToInt32(type) == 1)
            {
                if (SelectedDeviceType == null) return;
                else selectedTypeId = SelectedDeviceType.DeviceTypeId;
            }
            ResolvedParameter deviceTypeId = new(
                (pi, ctx) => pi.Name == "deviceTypeId",
                (pi, ctx) => selectedTypeId);
            if (_container == null) return;
            var vm = _container.Resolve<AddDeviceTypeViewModel>(deviceTypeId);
            var view = _container.Resolve<AddDeviceType>();
            view.DataContext = vm;
            addWindow = new PopWindow();
            addWindow.controlHost.Content = view;
            addWindow.Title = "设备类型";
            addWindow.Width = 600;
            addWindow.Height = 600;
            addWindow.Closed += AddWindowClosed;
            addWindow.ShowDialog();
        }
        private async void AddWindowClosed(object? sender, EventArgs e)
        {
            if (addWindow == null) return;
            addWindow.Closed -= AddWindowClosed;
            await LoadDeviceTypeList();
        }
    }
}
