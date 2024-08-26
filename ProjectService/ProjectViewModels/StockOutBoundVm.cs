using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectViewModels
{
    public class StockOutBoundVm : ObservableObject
    {
        private Guid? _outBoundId;
        public Guid? OutBoundId
        {
            get => _outBoundId;
            set => SetProperty(ref _outBoundId, value);
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
        private DateTime _outBoundDate;
        public DateTime OutBoundDate
        {
            get => _outBoundDate;
            set => SetProperty(ref _outBoundDate, value);
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
