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
    public partial class ConsumableListViewModel : ObservableObject
    {
        private readonly IContainer? _container = App.Current.Properties[MessageToken.AppContainer] as IContainer;
        private readonly ConsumableService _consumableService;
        private PaginatedList<ConsumableVm> _paginatedList = null!;
        public PaginatedList<ConsumableVm> PaginatedList
        {
            get => _paginatedList;
            set => SetProperty(ref _paginatedList, value);
        }
        private ConsumableVm? _selectedConsumable;
        public ConsumableVm? SelectedConsumable
        {
            get => _selectedConsumable;
            set => SetProperty(ref _selectedConsumable, value);
        }
        private ConsumableReqs _req = new() { Pagination = new PaginationParams { Page = 1, PageSize = 10 } };
        public ConsumableReqs Req
        {
            get => _req;
            set => SetProperty(ref _req, value);
        }
        public ConsumableListViewModel(ConsumableService consumableService)
        {
            _consumableService = consumableService;
            Task.Run(LoadDataAsync);
        }
        private async Task LoadDataAsync()
        {
            await LoadConsumables();
        }
        [RelayCommand]
        private async Task LoadConsumables()
        {
            PaginatedList = await _consumableService.PaginatedConsumable(Req);
        }
        private PopWindow? addWindow;
        [RelayCommand]
        private void OpenAddWindow(object type)
        {
            Guid? selectedConsumableId = null;
            if (Convert.ToInt32(type) == 1)
            {
                if (SelectedConsumable == null) return;
                else selectedConsumableId = SelectedConsumable.ConsumableId;
            }
            ResolvedParameter consumableId = new(
                (pi, ctx) => pi.Name == "consumableId",
                (pi, ctx) => selectedConsumableId);
            if (_container == null) return;
            var vm = _container.Resolve<AddConsumableViewModel>(consumableId);
            var view = _container.Resolve<AddConsumableView>();
            view.DataContext = vm;
            addWindow = new PopWindow();
            addWindow.controlHost.Content = view;
            addWindow.Title = "消耗品";
            addWindow.Width = 600;
            addWindow.Height = 600;
            addWindow.Closed += addWindowClosed;
            addWindow.ShowDialog();
        }
        private async void addWindowClosed(object? sender, EventArgs e)
        {
            if(sender is PopWindow window)
            {
                window.Closed -= addWindowClosed;
                await LoadConsumables();
            }
        }
        private PopWindow? stockInBoundWindow;
        [RelayCommand]
        private void OpenStockInBoundWindow()
        {
            if (SelectedConsumable == null) return;
            ResolvedParameter consumableId = new(
                (pi, ctx) => pi.Name == "consumableId",
                (pi, ctx) => SelectedConsumable.ConsumableId);
            if (_container == null) return;
            var vm = _container.Resolve<StockInBoundViewModel>(consumableId);
            var view = _container.Resolve<StockInBoundView>();
            view.DataContext = vm;
            addWindow = new PopWindow();
            addWindow.controlHost.Content = view;
            addWindow.Title = "消耗品";
            addWindow.Width = 800;
            addWindow.Height = 800;
            addWindow.Closed += addWindowClosed;
            addWindow.ShowDialog();
        }
        private PopWindow? stockOutBoundWindow;
        [RelayCommand]
        private void OpenStockOutBoundWindow()
        {
            if (SelectedConsumable == null) return;
            ResolvedParameter consumableId = new(
                (pi, ctx) => pi.Name == "consumableId",
                (pi, ctx) => SelectedConsumable.ConsumableId);
            if (_container == null) return;
            var vm = _container.Resolve<StockOutBoundViewModel>(consumableId);
            var view = _container.Resolve<StockOutBoundView>();
            view.DataContext = vm;
            addWindow = new PopWindow();
            addWindow.controlHost.Content = view;
            addWindow.Title = "消耗品";
            addWindow.Width = 800;
            addWindow.Height = 800;
            addWindow.Closed += addWindowClosed;
            addWindow.ShowDialog();
        }
        [RelayCommand]
        private void OpenConsumableBoundHistory()
        {
            ResolvedParameter consumableId = new(
                (pi, ctx) => pi.Name == "consumableId",
                (pi, ctx) => SelectedConsumable?.ConsumableId);
            if (_container == null) return;
            var vm = _container.Resolve<ConsumableBoundHistoryViewModel>(consumableId);
            var view = _container.Resolve<ConsumableBoundHistoryView>();
            view.DataContext = vm;
            addWindow = new PopWindow();
            addWindow.controlHost.Content = view;
            addWindow.Title = "消耗品调动记录";
            addWindow.Closed += addWindowClosed;
            addWindow.ShowDialog();
        }

    }
}
