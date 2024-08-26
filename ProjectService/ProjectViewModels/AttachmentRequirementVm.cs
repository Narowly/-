using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectViewModels
{
    public class AttachmentRequirementVm : ObservableObject
    {
        private int _attachmentRequirementId;
        public int AttachmentRequirementId
        {
            get => _attachmentRequirementId;
            set => SetProperty(ref _attachmentRequirementId, value);
        }

        private string _attachmentName = null!;
        public string AttachmentName
        {
            get => _attachmentName;
            set => SetProperty(ref _attachmentName, value);
        }
    }
}
