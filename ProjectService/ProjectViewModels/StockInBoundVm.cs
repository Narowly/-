using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectViewModels
{
    public class StockInBoundVm : ObservableObject
    {
        private Guid? _inBoundId;
        public Guid? InBoundId
        {
            get => _inBoundId;
            set => SetProperty(ref _inBoundId, value);
        }
        private Guid? _consumableId;
        public Guid? ConsumableId
        {
            get => _consumableId;
            set => SetProperty(ref _consumableId, value);
        }
        private int _quantity;
        public int Quantity
        {
            get => _quantity;
            set => SetProperty(ref _quantity, value);
        }
        private DateTime _inBoundDate;
        public DateTime InBoundDate
        {
            get => _inBoundDate;
            set => SetProperty(ref _inBoundDate, value);
        }
        private Guid? _projectId;
        public Guid? ProjectId
        {
            get => _projectId;
            set => SetProperty(ref _projectId, value);
        }
        private string? _remarks;
        public string? Remarks
        {
            get => _remarks;
            set => SetProperty(ref _remarks, value);
        }
        private ProjectVm? _project;
        public ProjectVm? Project
        {
            get => _project;
            set => SetProperty(ref _project, value);
        }
        private ConsumableVm? _consumable;
        public ConsumableVm? Consumable
        {
            get => _consumable;
            set => SetProperty(ref _consumable, value);
        }
    }
}
