using CommunityToolkit.Mvvm.ComponentModel;
using System;

namespace ProjectViewModels
{
    public class DictTypeVm : ObservableObject
    {
        private int _dictId;
        public int DictId
        {
            get => _dictId;
            set => SetProperty(ref _dictId, value);
        }

        private string? _dictName;
        public string? DictName
        {
            get => _dictName;
            set => SetProperty(ref _dictName, value);
        }

        private string? _typeName;
        public string? TypeName
        {
            get => _typeName;
            set => SetProperty(ref _typeName, value);
        }

        private bool _status;
        public bool Status
        {
            get => _status;
            set => SetProperty(ref _status, value);
        }

        private string? _remarks;
        public string? Remarks
        {
            get => _remarks;
            set => SetProperty(ref _remarks, value);
        }
    }
}