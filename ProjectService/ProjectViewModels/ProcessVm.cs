using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectViewModels
{
    public class ProcessVm : ObservableObject
    {
        private int _processId;
        public int ProcessId
        {
            get => _processId;
            set => SetProperty(ref _processId, value);
        }
        private string _processName = null!;
        public string ProcessName
        {
            get => _processName;
            set => SetProperty(ref _processName, value);
        }
        private string? _remarks;
        public string? Remarks
        {
            get => _remarks;
            set => SetProperty(ref _remarks, value);
        }

        private List<ProcessUnitVm>? _processUnits;
        public List<ProcessUnitVm>? ProcessUnits
        {
            get => _processUnits;
            set => SetProperty(ref _processUnits, value);
        }

    }
}
