using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectViewModels
{
    public class ProjectAttachmentVm : ObservableObject
    {
        private Guid? id;
        public Guid? Id
        {
            get => id;
            set => SetProperty(ref id, value);
        }

        private Guid? projectId;
        public Guid? ProjectId
        {
            get=> projectId;
            set => SetProperty(ref projectId, value);
        }
        private string fileName = null!;
        public string FileName
        {
            get => fileName;
            set => SetProperty(ref fileName, value);
        }
        private string fileType = null!;
        public string FileType
        {
            get => fileType; set => SetProperty(ref fileType, value);
        }

        private string fileAddress = null!;
        public string FileAddress
        {
            get => fileAddress; set => SetProperty(ref fileAddress, value);
        }
        private DateTime uploadDate;
        public DateTime UploadDate
        {
            get => uploadDate; set => SetProperty(ref uploadDate, value);
        }
        private ProjectVm? project;
        public ProjectVm? Project
        {
            get => project; set => SetProperty(ref project, value);
        }

    }
}
