using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectViewModels
{
    public class PlaceOnFileVm : ObservableObject
    {
        private Guid _projectId;
        public Guid ProjectId
        {
            get => _projectId;
            set => SetProperty(ref _projectId, value);
        }

        private ProjectVm? _project;
        public ProjectVm? Project
        {
            get => _project;
            set => SetProperty(ref _project, value);
        }

        private string? _applicationUserName;
        public string? ApplicationUserName
        {
            get => _applicationUserName;
            set => SetProperty(ref _applicationUserName, value);
        }
        private Guid? _applicationUserId;
        public Guid? ApplicationUserId
        {
            get => _applicationUserId;
            set => SetProperty(ref _applicationUserId, value);
        }

        private string? _reviewerName;
        public string? ReviewerName
        {
            get => _reviewerName;
            set => SetProperty(ref _reviewerName, value);
        }
        private Guid? _reviewerId;
        public Guid? ReviewerId
        {
            get => _reviewerId;
            set => SetProperty(ref _reviewerId, value);
        }

        private string? _reason;
        public string? Reason
        {
            get => _reason;
            set => SetProperty(ref _reason, value);
        }

        private bool? _isPass;
        public bool? IsPass
        {
            get => _isPass;
            set => SetProperty(ref _isPass, value);
        }

        private DateTime? _createTime;
        public DateTime? CreateTime
        {
            get => _createTime;
            set => SetProperty(ref _createTime, value);
        }
    }
}
