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
using MessageBox = HandyControl.Controls.MessageBox;

namespace Project.ViewModel
{
    public partial class AddConsumableTypeViewModel : ObservableObject
    {
        private readonly ConsumableService _consumableService;
        private Guid? _consumableTypeId;
        private ConsumableTypeVm? _consumableType;
        public ConsumableTypeVm? ConsumableType
        {
            get => _consumableType;
            set => SetProperty(ref _consumableType, value);
        }
        public AddConsumableTypeViewModel(Guid? consumableTypeId, ConsumableService consumableService)
        {
            _consumableTypeId = consumableTypeId;
            _consumableService = consumableService;
            Task.Run(LoadConsumableType);
        }
        private async Task LoadConsumableType()
        {
            if (_consumableTypeId != null) ConsumableType = await _consumableService.GetConsumableTypeById(_consumableTypeId.Value);
            else ConsumableType = new ConsumableTypeVm();
        }
        [RelayCommand]
        private async Task Save()
        {
            if (ConsumableType == null) return;
            if (string.IsNullOrWhiteSpace(ConsumableType.ConsumableTypeName))
            {
                MessageBox.Warning("类型不能为空");
                return;
            }
            var result = await _consumableService.SaveConsumableType(ConsumableType);
            if (result)
            {
                Growl.Success("保存成功");
            }
        }
    }
}
