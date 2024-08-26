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
    public partial class AddConsumableViewModel : ObservableObject
    {
        private readonly ConsumableService _consumableService;
        private readonly Guid? _consumableId;
        private readonly DictService _dictService;
        private ConsumableVm? _consumable = new();
        public ConsumableVm? Consumable
        {
            get => _consumable;
            set => SetProperty(ref _consumable, value);
        }
        private List<ConsumableTypeVm>? _typeList;
        public List<ConsumableTypeVm>? TypeList
        {
            get => _typeList;
            set => SetProperty(ref _typeList, value);
        }
        private ConsumableTypeVm? _selectedType;
        public ConsumableTypeVm? SelectedType
        {
            get => _selectedType;
            set => SetProperty(ref _selectedType, value);
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

        public AddConsumableViewModel(Guid? consumableId, ConsumableService consumableService, DictService dictService)
        {
            _consumableId = consumableId;
            _consumableService = consumableService;
            _dictService = dictService;
            Task.Run(LoadDataAsync);
        }

        private async Task LoadDataAsync()
        {
            await Task.WhenAll(LoadConsumableTypeList(), LoadConsumableStatusDict()).ContinueWith(w => LoadConsumable());
        }

        private async Task LoadConsumable()
        {
            if (_consumableId == null) return;
            Consumable = await _consumableService.GetConsumableById(_consumableId.Value);
            Consumable ??= new ConsumableVm();
            SelectedStatus = StatusDictList.FirstOrDefault(m => m.DictCode == Consumable.ConsumableStatus);
            SelectedType = TypeList?.FirstOrDefault(m => m.ConsumableTypeId == Consumable.ConsumableTypeId);
        }
        private async Task LoadConsumableStatusDict()
        {
            StatusDictList = await _dictService.GetDictDataByTypeName("ConsumableStatus");
        }

        private async Task LoadConsumableTypeList()
        {
            TypeList = await _consumableService.GetConsumableTypeList(new CommonReqs());
            //TypeList = new ObservableCollection<ConsumableTypeVm>(list);
        }

        [RelayCommand]
        private async Task SaveComsumable()
        {
            if (Consumable == null) return;
            if(Consumable.ConsumableNumber == null || SelectedType?.ConsumableTypeId==null||SelectedStatus==null)
            {
                MessageBox.Warning("编码、类型或状态不能为空");
                return;
            }
            Consumable.ConsumableStatus = SelectedStatus.DictCode;
            Consumable.ConsumableTypeId = SelectedType?.ConsumableTypeId;
            var result = await _consumableService.SaveConsumable(Consumable);
            if (result)
            {
                Growl.Success("保存成功");
            }
        }
    }
}
