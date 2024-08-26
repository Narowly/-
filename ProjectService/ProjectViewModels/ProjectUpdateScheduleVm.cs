using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectViewModels
{
    public class ProjectUpdateScheduleVm : ObservableObject
    {
        private int? _id;
        public int? Id
        {
            get => _id;
            set => SetProperty(ref _id, value);
        }
        private Guid _projectId;
        public Guid ProjectId
        {
            get => _projectId;
            set => SetProperty(ref _projectId, value);
        }
        private string? _projectName = null!;
        public string? ProjectName
        {
            get => _projectName;
            set => SetProperty(ref _projectName, value);
        }
        private DateTime _planEndDate;
        public DateTime PlanEndDate
        {
            get => _planEndDate;
            set => SetProperty(ref _planEndDate, value);
        }
        private DateTime _updatedEndDate;
        public DateTime UpdateEndDate
        {
            get => _updatedEndDate;
            set => SetProperty(ref _updatedEndDate, value);
        }
        private int _reasonType;
        public int ReasonType
        {
            get => _reasonType;
            set => SetProperty(ref _reasonType, value);
        }
        private DictDataVm? _reasonTypeDict;
        public DictDataVm? ReasonTypeDict
        {
            get => _reasonTypeDict;
            set => SetProperty(ref _reasonTypeDict, value);
        }
        private string? _remarks;
        public string? Remarks
        {
            get => _remarks;
            set => SetProperty(ref _remarks, value);
        }

    }
}
