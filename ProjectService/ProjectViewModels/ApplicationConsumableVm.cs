using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectViewModels
{
    public class ApplicationConsumableVm : ObservableObject
    {
        private Guid? _applicationConsumableId;
        public Guid? ApplicationConsumableId
        {
            get => _applicationConsumableId;
            set => SetProperty(ref _applicationConsumableId, value);
        }
        private Guid _applicationId;
        public Guid ApplicationId
        {
            get => _applicationId;
            set => SetProperty(ref _applicationId, value);
        }
        private Guid? _consumableTypeId;
        public Guid? ConsumableTypeId
        {
            get => _consumableTypeId;
            set => SetProperty(ref _consumableTypeId, value);
        }
        private int? _quantity;
        public int? Quantity
        {
            get => _quantity;
            set => SetProperty(ref _quantity, value);
        }
        private string? _remarks;
        public string? Remarks
        {
            get => _remarks;
            set => SetProperty(ref _remarks, value);
        }
        private string? _consumableTypeName;
        public string? ConsumableTypeName
        {
            get => _consumableTypeName;
            set => SetProperty(ref _consumableTypeName, value);
        }
        private string? _consumableModel;
        public string? ConsumableModel
        {
            get => _consumableModel;
            set => SetProperty(ref _consumableModel,value);
        }
        public string ShowName
        {
            get
            {
                return $"{ConsumableTypeName} {ConsumableModel}";
            }
        }
    }
}
