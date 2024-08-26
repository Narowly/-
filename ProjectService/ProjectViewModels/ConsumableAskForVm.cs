using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectViewModels
{
    public partial class ConsumableAskForVm : ObservableObject
    {
        private Guid? _consumableAskForId;
        public Guid? ConsumableAskForId
        {
            get => _consumableAskForId;
            set => SetProperty(ref  _consumableAskForId, value);
        }

        private string _title = null!;
        public string Title
        {
            get => _title;
            set => SetProperty(ref _title, value);
        }

        private string? _content;
        public string? Content
        {
            get => _content;
            set => SetProperty(ref _content, value);
        }

        private Guid _staffId;
        public Guid StaffId
        {
            get => _staffId;
            set => SetProperty(ref _staffId, value);
        }

        private Guid? _projectId;
        public Guid? ProjectId
        {
            get => _projectId;
            set => SetProperty(ref _projectId, value);
        }
        private int _status;
        public int Status
        {
            get => _status;
            set => SetProperty(ref _status, value);
        }
        private string? _statusName;
        public string? StatusName
        {
            get => _statusName;
            set => SetProperty(ref _statusName, value);
        }
        private string? _projectName;
        public string? ProjectName
        {
            get => _projectName;
            set => SetProperty(ref _projectName, value);
        }
        private string? _staffName;
        public string? StaffName
        {
            get => _staffName;
            set => SetProperty(ref _staffName, value);
        }

        private List<ConsumableAskForItemVm> _consumableAskForItemList = [];
        public List<ConsumableAskForItemVm> ConsumableAskForItemList
        {
            get => _consumableAskForItemList;
            set => SetProperty(ref _consumableAskForItemList, value);
        }

        private string? _remarks;
        public string? Remarks
        {
            get => _remarks;
            set => SetProperty(ref _remarks, value);
        }
        private DateTime? _createTime;
        public DateTime? CreateTime
        {
            get => _createTime;
            set => SetProperty(ref _createTime, value);
        }
    }
}
