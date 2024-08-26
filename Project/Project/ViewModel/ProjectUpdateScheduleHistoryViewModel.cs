using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Project.Common;
using Project.Services;
using Project.Services.DataServices;
using ProjectViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.ViewModel
{
    public partial class ProjectUpdateScheduleHistoryViewModel : ObservableObject
    {
        private readonly ProjectUpdateScheduleService _updateScheduleService;
        private readonly ProjectService _projectService;
        private readonly StaffService _staffService;
        private readonly DictService _dictService;
        
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
        private List<StaffVm>? ManagerList;
        [ObservableProperty]
        private ObservableCollection<StaffVm>? bindingManagerList;
        [ObservableProperty]
        private StaffVm? selectedManager;
        private string? _searchManagerText;
        public string? SearchManagerText
        {
            get => _searchManagerText;
            set
            {
                SetProperty(ref _searchManagerText, value);
                if (!string.IsNullOrWhiteSpace(_searchManagerText))
                {
                    var list = ManagerList?.Where(m => m.StaffName.Contains(_searchManagerText)).ToList();
                    if (list != null) BindingManagerList = new ObservableCollection<StaffVm>(list);
                }
                else
                {
                    BindingManagerList = null;
                }
            }
        }
        private ProjectReqs _req = new();
        public ProjectReqs Req
        {
            get => _req;
            set => SetProperty(ref _req, value);
        }
        private PaginatedList<ProjectUpdateScheduleVm> _paginatedList = null!;
        public PaginatedList<ProjectUpdateScheduleVm> PaginatedList
        {
            get => _paginatedList;
            set => SetProperty(ref _paginatedList, value);
        }
        private ProjectUpdateScheduleVm? _projectUpdateSchedule;
        public ProjectUpdateScheduleVm? ProjectUpdateSchedule
        {
            get => _projectUpdateSchedule;
            set => SetProperty(ref _projectUpdateSchedule, value);
        }
        private List<DictDataVm> _reasonTypeList = null!;
        public List<DictDataVm> ReasonTypeList
        {
            get => _reasonTypeList;
            set => SetProperty(ref _reasonTypeList, value);
        }
        private Guid? _projectId;
        public ProjectUpdateScheduleHistoryViewModel(Guid? projectId, ProjectUpdateScheduleService updateScheduleService, ProjectService projectService, StaffService staffService, DictService dictService)
        {
            _projectId = projectId;
            _updateScheduleService = updateScheduleService;
            _projectService = projectService;
            _staffService = staffService;
            _dictService = dictService;
            Task.Run(LoadDataAsync);
        }

        private async Task LoadDataAsync()
        {
            await LoadProjectNames();
            await LoadManagerList();
            await Task.WhenAll(LoadReasonDictData(), LoadUpdateScheduleList()).ContinueWith(c => SetReasonTypeDict());
        }
        
        private async Task LoadProjectNames()
        {
            ProjectNameList = await _projectService.LoadProjectNames();
            //ProjectNameList = list.Select(m => new ProjectAutoCompleteModel { Id = m.ProjectId, Name = string.Format("{0}|{1}", m.Contract.ContractNumber, m.ProjectName), Number = m.Contract.ContractNumber }).ToList();
        }
        private async Task LoadManagerList()
        {
            ManagerList = await _staffService.GetStaffListByDuty(ApiSettings.ProjectManagerDuty);
        }
        private async Task LoadReasonDictData()
        {
            ReasonTypeList = await _dictService.GetDictDataByTypeName("UpdateScheduleReasonType");
        }
        private void SetReasonTypeDict()
        {
            if (PaginatedList != null && PaginatedList.Items != null)
            {
                foreach (var item in PaginatedList.Items)
                {
                    item.ReasonTypeDict = ReasonTypeList.FirstOrDefault(m => m.DictCode == item.ReasonType);
                }
            }
        }
        [RelayCommand]
        private async Task LoadUpdateScheduleList()
        {
            if (SelectedManager != null)
                Req.ProjectManagerId = SelectedManager.StaffId;
            else
                Req.ProjectManagerId = null;
            if (SelectedProject != null)
                Req.ProjectId = SelectedProject.Id;
            else 
                Req.ProjectId = null;
            if (_projectId != null)
            {
                Req.ProjectId = _projectId;
                _projectId = null;
            }
                
            
            PaginatedList = await _updateScheduleService.PaginatedProjectUpdateSchedule(Req);
        }
    }
}
