using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectViewModels
{
    public partial class ConsumableAskForItemVm : ObservableObject
    {
        private Guid? _consumableAskForItemId;
        public Guid? ConsumableAskForItemId
        {
            get => _consumableAskForItemId;
            set => SetProperty(ref _consumableAskForItemId, value);
        }

        private Guid? _consumableAskForId;
        public Guid? ConsumableAskForId
        {
            get => _consumableAskForId;
            set => SetProperty(ref _consumableAskForId, value);
        }

        private Guid? _consumableTypeId;
        public Guid? ConsumableTypeId
        {
            get => _consumableTypeId;
            set => SetProperty(ref _consumableTypeId, value);
        }

        private int _quantity;
        public int Quantity
        {
            get => _quantity;
            set => SetProperty(ref _quantity, value);
        }

        //private string? _consumableTypeName;
        //public string? ConsumableTypeName
        //{
        //    get => _consumableTypeName;
        //    set => SetProperty(ref _consumableTypeName, value);
        //}

        //private string? _consumableModel;
        //public string? ConsumableModel
        //{
        //    get => _consumableModel; 
        //    set => SetProperty(ref _consumableModel, value);
        //}
        private string? _consumableTypeShowName;
        public string? ConsumableTypeShowName
        {
            get => _consumableTypeShowName;
            set => SetProperty(ref _consumableTypeShowName, value);
        }
    }
}
