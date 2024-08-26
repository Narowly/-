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
    public partial class UpdateConsumableInBoundViewModel : ObservableObject
    {
        private readonly ConsumableService _consumableService;
        private readonly Guid _boundId;
        private StockInBoundVm _boundData = null!;
        public StockInBoundVm BoundData
        {
            get => _boundData;
            set => SetProperty(ref _boundData, value);
        }
        private string _dateName = null!;
        public string DateName
        {
            get => _dateName;
            set => SetProperty(ref _dateName, value);
        }
        private DateTime _selectedDate;
        public DateTime SelectedDate
        {
            get => _selectedDate;
            set => SetProperty(ref _selectedDate, value);
        }
        public UpdateConsumableInBoundViewModel(Guid boundId,  ConsumableService consumableService)
        {
            _boundId = boundId;
            _consumableService = consumableService;
            Task.Run(LoadDataAsync);
        }
        private async Task LoadDataAsync()
        {
            await LoadBoundByType();
        }
        private async Task LoadBoundByType()
        {
            BoundData = await _consumableService.GetStockInBoundById(_boundId);
            SelectedDate = BoundData.InBoundDate;
            DateName = "入库日期：";
        }
        [RelayCommand]
        private async Task Save()
        {
            BoundData.InBoundDate = SelectedDate;
            var result = await _consumableService.SaveStockInBound(BoundData);
            if (result)
            {
                Growl.Success("保存成功");
            }
        }
    }

    public partial class UpdateConsumableOutBoundViewModel : ObservableObject
    {
        private readonly ConsumableService _consumableService;
        private readonly Guid _boundId;
        private StockOutBoundVm _boundData = null!;
        public StockOutBoundVm BoundData
        {
            get => _boundData;
            set => SetProperty(ref _boundData, value);
        }
        private string _dateName = null!;
        public string DateName
        {
            get => _dateName;
            set => SetProperty(ref _dateName, value);
        }
        private DateTime _selectedDate;
        public DateTime SelectedDate
        {
            get => _selectedDate;
            set => SetProperty(ref _selectedDate, value);
        }
        public UpdateConsumableOutBoundViewModel(Guid boundId, ConsumableService consumableService)
        {
            _boundId = boundId;
            _consumableService = consumableService;
            Task.Run(LoadDataAsync);
        }
        private async Task LoadDataAsync()
        {
            await LoadBoundByType();
        }
        private async Task LoadBoundByType()
        {
            BoundData = await _consumableService.GetStockOutBoundById(_boundId);
            SelectedDate = BoundData.OutBoundDate;
            DateName = "出库日期：";
        }
        [RelayCommand]
        private async Task Save()
        {
            BoundData.OutBoundDate = SelectedDate;
            var result = await _consumableService.SaveStockOutBound(BoundData);
            if (result)
            {
                Growl.Success("保存成功");
            }
        }
    }
}
