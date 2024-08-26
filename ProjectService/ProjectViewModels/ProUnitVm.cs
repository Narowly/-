using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectViewModels
{
    public class ProUnitVm : ObservableObject
    {
        private int _unitId;
        public int UnitId
        {
            get => _unitId;
            set => SetProperty(ref _unitId, value);
        }
        private string _unitName = null!;
        public string UnitName
        {
            get => _unitName;
            set => SetProperty(ref _unitName, value);
        }
        private string? _remarks;
        public string? Remarks
        {
            get => _remarks;
            set => SetProperty(ref _remarks, value);
        }
    }
}
