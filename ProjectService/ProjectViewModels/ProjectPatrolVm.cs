using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectViewModels
{
    public class ProjectPatrolVm : ObservableObject
    {
        private long? _id;
        public long? Id
        {
            get => _id;
            set => SetProperty(ref _id, value);
        }
        private Guid? _projectId;
        public Guid? ProjectId
        {
            get => _projectId;
            set => SetProperty(ref _projectId, value);
        }
        private Guid? _staffId;
        public Guid? StaffId
        {
            get => _staffId;
            set => SetProperty(ref _staffId, value);
        }
        private DateTime? _patrolDate;
        public DateTime? PatrolDate
        {
            get => _patrolDate;
            set => SetProperty(ref _patrolDate, value);
        }
        private int? _status;
        public int? Status
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

        private StaffVm? _staff;
        public StaffVm? Staff
        {
            get => _staff;
            set => SetProperty(ref _staff, value);
        }
    }

    public class ProjectPatrolExcelVm
    {
        public string Ht { get; set; }
        public string Card { get; set; }
        public DateTime PatrolDateTime { get; set; }
        public string PatrolStatus { get; set; }
        public string Remarks { get; set; }
    }
}
