using CommunityToolkit.Mvvm.ComponentModel;
using Project.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using Project.Services.DataServices;
using ProjectViewModels;
using CommunityToolkit.Mvvm.Input;
using Project.Views.Windows;
using Autofac.Core;
using Project.Views.UserControls;

namespace Project.ViewModel
{
    public partial class ConsumableTypeListViewModel : ObservableObject
    {
        private readonly IContainer? _container = App.Current.Properties[MessageToken.AppContainer] as IContainer;
        private readonly ConsumableService _consumableService;
        private PaginatedList<ConsumableTypeVm> _paginatedList = null!;
        public PaginatedList<ConsumableTypeVm> PaginatedList
        {
            get => _paginatedList;
            set => SetProperty(ref _paginatedList, value);
        }
        private CommonReqs _req = new() { Pagination = new PaginationParams { Page = 1, PageSize = 10 } };
        public CommonReqs Req
        {
            get => _req;
            set => SetProperty(ref _req, value);
        }
        private ConsumableTypeVm? _selectedConsumableType;
        public ConsumableTypeVm? SelectedConsumableType
        {
            get => _selectedConsumableType;
            set => SetProperty(ref _selectedConsumableType, value);
        }
        public ConsumableTypeListViewModel(ConsumableService consumableService)
        {
            _consumableService = consumableService;
            Task.Run(LoadDataAsync);
        }
        private async Task LoadDataAsync()
        {
            await LoadConsumableTypeList();
        }
        [RelayCommand]
        private async Task LoadConsumableTypeList()
        {
            PaginatedList = await _consumableService.PaginatedConsumableType(Req);
        }
        private PopWindow? addWindow;
        [RelayCommand]
        private void OpenAddWindow(object type)
        {
            Guid? selectedTypeId = null;
            if (Convert.ToInt32(type) == 1)
            {
                if (SelectedConsumableType == null) return;
                else selectedTypeId = SelectedConsumableType.ConsumableTypeId;
            }
            ResolvedParameter consumableTypeId = new(
                (pi, ctx) => pi.Name == "consumableTypeId",
                (pi, ctx) => selectedTypeId);
            if (_container == null) return;
            var vm = _container.Resolve<AddConsumableTypeViewModel>(consumableTypeId);
            var view = _container.Resolve<AddConsumableType>();
            view.DataContext = vm;
            addWindow = new PopWindow();
            addWindow.controlHost.Content = view;
            addWindow.Title = "消耗品类型";
            addWindow.Width = 600;
            addWindow.Height = 600;
            addWindow.Closed += AddWindowClosed;
            addWindow.ShowDialog();            
        }
        private async void AddWindowClosed(object? sender, EventArgs e)
        {
            if (addWindow == null) return;
            addWindow.Closed -= AddWindowClosed;
            await LoadConsumableTypeList();
        }
    }
}
