using CommunityToolkit.Mvvm.ComponentModel;
using Project.Common;
using Project.Services.DataServices;
using ProjectViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using System.Collections.ObjectModel;
using Project.Services;
using CommunityToolkit.Mvvm.Input;

namespace Project.ViewModel
{
    public partial class PlaceOnFileViewModel : ObservableObject
    {
        private readonly IContainer? _container = App.Current.Properties[MessageToken.AppContainer] as IContainer;
        private readonly AttachmentsService _attachmentsService;
        private readonly ProjectService _projectService;
        private PaginatedList<PlaceOnFileVm> _paginatedList = null!;
        public PaginatedList<PlaceOnFileVm> PaginatedList
        {
            get => _paginatedList;
            set => SetProperty(ref _paginatedList, value);
        }
        private PlaceOnFileVm? _selectedDto;
        public PlaceOnFileVm? SelectedDto
        {
            get => _selectedDto;
            set => SetProperty(ref _selectedDto, value);
        }
        private ProjectReqs _req = new ProjectReqs();
        public ProjectReqs Req
        {
            get => _req;
            set => SetProperty(ref _req, value);
        }

        private List<ProjectAutoCompleteModel> ProjectNameList = null!;
        [ObservableProperty]
        private ObservableCollection<ProjectAutoCompleteModel>? _projectNamesSource;
        private string? _searchProjectNameText;
        public string? SearchProjectNameText
        {
            get => _searchProjectNameText;
            set
            {
                SetProperty(ref _searchProjectNameText, value);
                if (!string.IsNullOrWhiteSpace(_searchProjectNameText))
                {
                    var list = ProjectNameList?.Where(m => m.Name.Contains(_searchProjectNameText) || m.Number.Contains(_searchProjectNameText)).ToList();
                    if (list != null) ProjectNamesSource = new ObservableCollection<ProjectAutoCompleteModel>(list);
                }
                else
                {
                    ProjectNamesSource = null;
                }

            }
        }
        [ObservableProperty]
        private ProjectAutoCompleteModel? _selectedProject;

        public PlaceOnFileViewModel(AttachmentsService attachmentsService, ProjectService projectService)
        {
            _attachmentsService = attachmentsService;
            _projectService = projectService;
            Task.Run(LoadDataAsync);
        }
        async Task LoadDataAsync()
        {
            await LoadApplyPlaceOnFileList();
            await LoadProjectNames();
        }
        [RelayCommand]
        async Task LoadApplyPlaceOnFileList()
        {
            Req.ProjectId = SelectedProject?.Id;
            PaginatedList = await _attachmentsService.PaginatedApplyPlaceOnFileProject(Req);
        }

        async Task LoadProjectNames()
        {
            ProjectNameList = await _projectService.LoadProjectNames();
        }
    }
}
