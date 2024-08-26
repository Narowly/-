using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectViewModels
{
    public class ProcessUnitVm : ObservableObject
    {
        private int _id;
        public int Id
        {
            get => _id;
            set => SetProperty(ref _id, value);
        }

        private int _processId;
        public int ProcessId
        {
            get => _processId;
            set => SetProperty(ref _processId, value);
        }

        private int _unitId;
        public int UnitId
        {
            get => _unitId;
            set => SetProperty(ref _unitId, value);
        }

        private string? _remarks;
        public string? Remarks
        {
            get => _remarks; 
            set => SetProperty(ref _remarks, value);
        }
        private ProcessVm _process = null!;
        public ProcessVm Process
        {
            get => _process; 
            set => SetProperty(ref _process, value);
        }
        private ProUnitVm _proUnit = null!;
        public ProUnitVm ProUnit
        {
            get => _proUnit; 
            set => SetProperty(ref _proUnit, value);
        }
        public string ShowName
        {
            get
            {
                return $"{Process.ProcessName} {ProUnit.UnitName}";
            }
        }
    }
}
