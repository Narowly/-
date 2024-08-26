using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectViewModels
{
    public class ApplicationPersonVm : ObservableObject
    {
        private Guid? _applicationPersonId;
        public Guid? ApplicationPersonId
        {
            get => _applicationPersonId;
            set => SetProperty(ref _applicationPersonId, value);
        }
        private Guid _applicationId;
        public Guid ApplicationId
        {
            get => _applicationId;
            set => SetProperty(ref _applicationId, value);
        }
        private int? _processId;
        public int? ProcessId
        {
            get => _processId;
            set => SetProperty(ref _processId, value);
        }
        private int? _count;
        public int? Count
        {
            get => _count;
            set => SetProperty(ref _count, value);
        }
        private string? _remarks;
        public string? Remarks
        {
            get => _remarks;
            set => SetProperty(ref _remarks, value);
        }
        private string? _processName;
        public string? ProcessName
        {
            get => _processName;
            set => SetProperty(ref _processName, value);
        }
        //private string? _unitName;
        //public string? UnitName
        //{
        //    get => _unitName;
        //    set => SetProperty(ref _unitName, value);
        //}
        //public string? ShowName
        //{
        //    get
        //    {
        //        if (!string.IsNullOrWhiteSpace(ProcessName) && !string.IsNullOrWhiteSpace(UnitName))
        //        {
        //            return $"{ProcessName}{UnitName}";
        //        }
        //        return string.Empty;
        //    }
        //}
    }
}
