using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectViewModels
{
    public class ConsumableVm : ObservableObject
    {
        private Guid? _consumableId;
        public Guid? ConsumableId
        {
            get => _consumableId;
            set => SetProperty(ref _consumableId, value);
        }
        private string _consumableNumber = null!;
        public string ConsumableNumber
        {
            get => _consumableNumber;
            set => SetProperty(ref _consumableNumber, value);
        }
        private Guid? _consumableTypeId;
        public Guid? ConsumableTypeId
        {
            get => _consumableTypeId;
            set => SetProperty(ref _consumableTypeId, value);
        }

        private int _consumableStatus;
        public int ConsumableStatus
        {
            get => _consumableStatus;
            set => SetProperty(ref _consumableStatus, value);
        }
        private int _quantity;
        public int Quantity
        {
            get => _quantity;
            set => SetProperty(ref _quantity, value);
        }

        private decimal _price;
        public decimal Price
        {
            get => _price;
            set => SetProperty(ref _price, value);
        }
        private string? _remarks;
        public string? Remarks
        {
            get => _remarks;
            set => SetProperty(ref _remarks, value);
        }

        private ConsumableTypeVm? _consumableType;
        public ConsumableTypeVm? ConsumableType
        {
            get => _consumableType;
            set => SetProperty(ref _consumableType, value);
        }

    }
}
