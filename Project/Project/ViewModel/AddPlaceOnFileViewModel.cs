using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using HandyControl.Controls;
using Microsoft.Win32;
using Project.Services.DataServices;
using ProjectViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Project.ViewModel
{
    public partial class AddPlaceOnFileViewModel : ObservableObject
    {
        private readonly AttachmentsService _attachmentsService;

        private List<AttachmentRequirementVm> _requirementList = [];
        public List<AttachmentRequirementVm> RequirementList
        {
            get => _requirementList;
            set => SetProperty(ref _requirementList, value);
        }
        private Guid _projectId;
        public Guid ProjectId
        {
            get => _projectId;
            set => SetProperty(ref _projectId, value);
        }

        private List<ProjectAttachmentVm> _attachmentList = [];
        public List<ProjectAttachmentVm> AttachmentList
        {
            get => _attachmentList;
            set => SetProperty(ref _attachmentList, value);
        }

        private ObservableCollection<string> _needList = [];
        public ObservableCollection<string> NeedList
        {
            get => _needList;
            set => SetProperty(ref _needList, value);
        }

        private ObservableCollection<string>? selectedFiles;
        public ObservableCollection<string>? SelectedFiles
        {
            get => selectedFiles;
            set => SetProperty(ref selectedFiles, value);
        }

        private ProjectAttachmentVm? selectAttachment;
        public ProjectAttachmentVm? SelectAttachment
        {
            get => selectAttachment;
            set => SetProperty(ref selectAttachment, value);
        }

        public AddPlaceOnFileViewModel(Guid projectId, AttachmentsService attachmentsService)
        {
            ProjectId = projectId;
            _attachmentsService = attachmentsService;

            Task.Run(LoadDataAsync);
        }

        async Task LoadDataAsync()
        {
            await Task.WhenAll(LoadAttachmentRequirementList(), LoadProjectAttachmentList())
                .ContinueWith(m => CheckNeedList());
        }

        async Task LoadAttachmentRequirementList()
        {
            RequirementList = await _attachmentsService.GetAttachmentRequirementList();
        }

        async Task LoadProjectAttachmentList()
        {
            AttachmentList = await _attachmentsService.GetProjectAttachments(ProjectId);
        }

        void CheckNeedList()
        {
            NeedList = [];
            var fileNames = RequirementList.Select(m => m.AttachmentName).ToList();
            System.Windows.Application.Current.Dispatcher.Invoke(() =>
            {
                foreach (var file in fileNames)
                {
                    if (!AttachmentList.Any(m => m.FileName.Contains(file)))
                    {
                        NeedList.Add(file);
                    }
                }
            });       
        }
        [RelayCommand]
        private void SelectFiles()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Multiselect = true,
                Filter = "All files (*.*)|*.*"
            };

            if (openFileDialog.ShowDialog() == true)
            {
                if (SelectedFiles == null) SelectedFiles = new ObservableCollection<string>();
                SelectedFiles.Clear();
                foreach (var item in openFileDialog.FileNames)
                {
                    SelectedFiles.Add(item);
                }
            }
        }
        [RelayCommand]
        private async Task UploadMultipleFiles()
        {
            if (selectedFiles == null) return;
            var result = await _attachmentsService.UploadMultipleFilesAsync(SelectedFiles, _projectId);
            if (result)
            {
                await LoadProjectAttachmentList().ContinueWith(m => {
                    Application.Current.Dispatcher.Invoke(() =>
                    {
                        SelectedFiles.Clear();
                        CheckNeedList();
                    });
                    Growl.Success("上传成功");
                });
            }
        }
        [RelayCommand]
        async Task SavePlaceOnFile()
        {
            if (NeedList.Count == 0)
            {
                var result = await _attachmentsService.SavePlaceOnFile(new PlaceOnFileVm
                {
                    ProjectId = ProjectId
                });
                if (result)
                {
                    Growl.Success("申请成功");
                }
            }
            
        }
    }
}
