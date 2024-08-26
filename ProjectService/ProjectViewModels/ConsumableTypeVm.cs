using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectViewModels
{
    public class ConsumableTypeVm : ObservableObject
    {
        private Guid? _consumableTypeId;
        public Guid? ConsumableTypeId
        {
            get => _consumableTypeId;
            set => SetProperty(ref _consumableTypeId, value);
        }
        private string _consumableTypeName = null!;
        public string ConsumableTypeName
        {
            get => _consumableTypeName;
            set => SetProperty(ref _consumableTypeName, value);
        }

        private string? _consumableModel;
        public string? ConsumableModel
        {
            get => _consumableModel;
            set => SetProperty(ref _consumableModel, value);
        }
        private string? _consumableUnit;
        public string? ConsumableUnit
        {
            get => _consumableUnit;
            set => SetProperty(ref _consumableUnit, value);
        }
        private string? _remarks;
        public string? Remarks
        {
            get => _remarks;
            set => SetProperty(ref _remarks, value);
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
