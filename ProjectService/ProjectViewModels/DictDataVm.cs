using CommunityToolkit.Mvvm.ComponentModel;
using System;

namespace ProjectViewModels
{
    public class DictDataVm : ObservableObject
    {
        private int _dictCode;
        public int DictCode
        {
            get => _dictCode;
            set => SetProperty(ref _dictCode, value);
        }

        private string? _dictLabel;
        public string? DictLabel
        {
            get => _dictLabel;
            set => SetProperty(ref _dictLabel, value);
        }

        private string? _dictValue;
        public string? DictValue
        {
            get => _dictValue;
            set => SetProperty(ref _dictValue, value);
        }

        private int? _dictTypeId;
        public int? DictTypeId
        {
            get => _dictTypeId;
            set => SetProperty(ref _dictTypeId, value);
        }

        private int? _parentCode;
        public int? ParentCode
        {
            get => _parentCode;
            set => SetProperty(ref _parentCode, value);
        }

        private bool? _status;
        public bool? Status
        {
            get => _status;
            set => SetProperty(ref _status, value);
        }

        private DictDataVm? _parentDataVm;
        public DictDataVm? ParentDataVm
        {
            get => _parentDataVm;
            set => SetProperty(ref _parentDataVm, value);
        }
    }
}