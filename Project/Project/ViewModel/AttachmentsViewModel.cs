using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Win32;
using Project.Common;
using Project.Services;
using Project.Services.DataServices;
using ProjectViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using MessageBox = HandyControl.Controls.MessageBox;

namespace Project.ViewModel
{
    public partial class AttachmentsViewModel : ObservableObject
    {
        private readonly AttachmentsService _attachmentsService;
        private readonly ProjectService _projectService;
        private PaginatedList<ProjectAttachmentVm>? _paginatedList;
        public PaginatedList<ProjectAttachmentVm>? PaginatedList
        {
            get => _paginatedList;
            set => SetProperty(ref _paginatedList, value);
        }

        private ObservableCollection<ProjectAttachmentVm>? _attachments;
        public ObservableCollection<ProjectAttachmentVm>? Attachments
        {
            get => _attachments;
            set => SetProperty(ref _attachments, value);
        }
        private ProjectAttachmentVm? _selectedAttachment;
        public ProjectAttachmentVm? SelectedAttachment
        {
            get => _selectedAttachment;
            set => SetProperty(ref _selectedAttachment, value);
        }
        private ProjectReqs? _projectReqs = null!;
        public ProjectReqs? ProjectReqs
        {
            get => _projectReqs;
            set => SetProperty(ref _projectReqs, value);
        }

        private ObservableCollection<ProjectVm>? _projects;
        public ObservableCollection<ProjectVm>? Projects
        {
            get => _projects;
            set => SetProperty(ref _projects, value);
        }

        private List<ProjectAutoCompleteModel> ProjectNameList;
        [ObservableProperty]
        private ObservableCollection<ProjectAutoCompleteModel>? projectSource;
        private string searchProjectText = string.Empty;
        public string SearchProjectText
        {
            get => searchProjectText;
            set
            {
                SetProperty(ref searchProjectText, value);
                //ProjectReqs.Content = searchProjectText;
                //if (!string.IsNullOrWhiteSpace(searchProjectText) && searchProjectText.Length > 2)
                //{
                //    var result = ProjectNameList.Where(m => m.ProjectName.Contains(searchProjectText) || m.Contract.ContractNumber.Contains(searchProjectText)).ToList();
                //    ProjectSource = new ObservableCollection<ProjectVm>(result);
                //}
                if (!string.IsNullOrWhiteSpace(searchProjectText))
                {
                    var list = ProjectNameList?.Where(m => m.Name.Contains(searchProjectText) || m.Number.Contains(searchProjectText)).ToList();
                    if (list != null) ProjectSource = new ObservableCollection<ProjectAutoCompleteModel>(list);
                }
                else
                {
                    ProjectSource = null;
                }
            }
        }

        public AttachmentsViewModel(AttachmentsService attachmentsService, ProjectService projectService)
        {
            _attachmentsService = attachmentsService;
            _projectService = projectService;
            ProjectReqs = new ProjectReqs()
            {
                Pagination = new PaginationParams { Page = 1, PageSize = 10 }
            };
            Task.Run(LoadDataAsync);
        }

        private async Task LoadDataAsync()
        {
            await LoadAttachments();
            await LoadProjectNames();
        }
        [RelayCommand]
        private async Task LoadAttachments()
        {
            if (PaginatedList != null && ProjectReqs != null && ProjectReqs.Pagination != null)
                ProjectReqs.Pagination.Page = PaginatedList.PageIndex;
            PaginatedList = await _attachmentsService.PaginatedProjectAttachments(ProjectReqs);
            //var baseUrl = App.Current.Properties[MessageToken.RestClientBaseUrl] as string;
            //foreach (var attachment in PaginatedList.Items)
            //{
            //    attachment.FileAddress = $"{baseUrl}{attachment.FileAddress}";
            //}

            //Attachments = new ObservableCollection<ProjectAttachmentVm>(PaginatedList.Items);
        }

        private async Task LoadProjectNames()
        {
            await Application.Current.Dispatcher.Invoke(async () =>
            {
                ProjectNameList = await _projectService.LoadProjectNames();
            });
        }

        [RelayCommand]
        private async Task DownloadAttachment(ProjectAttachmentVm vm)
        {
            var result = await _attachmentsService.DownloadAttachment(vm);
            if (result)
            {
                MessageBox.Success("下载完成！");
            }
        }
    }
}
