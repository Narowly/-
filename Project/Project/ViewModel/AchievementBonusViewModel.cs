using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using HandyControl.Controls;
using Project.Services;
using Project.Services.DataServices;
using ProjectViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MessageBox = HandyControl.Controls.MessageBox;

namespace Project.ViewModel
{
    public partial class AchievementBonusViewModel : ObservableObject
    {
        private readonly ProjectService _projectService;
        private readonly StaffService _staffService;
        private readonly ProjectBonusService _bonusService;
        
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
        private List<StaffVm>? StaffList;
        [ObservableProperty]
        private ObservableCollection<StaffVm>? bindingStaffList;
        [ObservableProperty]
        private StaffVm? selectedStaff;
        private string? _searchStaffText;
        public string? SearchStaffText
        {
            get => _searchStaffText;
            set
            {
                SetProperty(ref _searchStaffText, value);
                if (!string.IsNullOrWhiteSpace(_searchStaffText))
                {
                    var list = StaffList?.Where(m => m.StaffName.Contains(_searchStaffText)).ToList();
                    if (list != null) BindingStaffList = new ObservableCollection<StaffVm>(list);
                }
                else
                {
                    BindingStaffList = null;
                }
            }
        }
        private AchievementBonusCalculateReq _req = new ();
        public AchievementBonusCalculateReq Req
        {
            get => _req;
            set => SetProperty(ref _req, value);
        }
        private string? _yearMonth;
        public string? YearMonth
        {
            get => _yearMonth;
            set => SetProperty(ref _yearMonth, value);
        }

        private List<StaffAchievementBonusVm>? _bonusList;
        public List<StaffAchievementBonusVm>? BonusList
        {
            get => _bonusList;
            set => SetProperty(ref _bonusList, value);
        }

        public AchievementBonusViewModel(ProjectService projectService, StaffService staffService, ProjectBonusService projectBonusService)
        {
            _projectService = projectService;
            _staffService = staffService;
            _bonusService = projectBonusService;

            Task.Run(LoadDataAsync);
        }

        private async Task LoadDataAsync()
        {
            await LoadProjectNames();
            await LoadStaff();
        }

        private async Task LoadProjectNames()
        {
            ProjectNameList = await _projectService.LoadProjectNames();
        }
        private async Task LoadStaff()
        {
            StaffList = await _staffService.GetStaffList();
        }
        [RelayCommand]
        private async Task AchievementBonusCalculate()
        {
            DateTime? yearMonth = null;
            if (!string.IsNullOrWhiteSpace(YearMonth))
            {
                if (YearMonth.Length == 7)
                {
                    if (DateTime.TryParse($"{YearMonth}-01", out DateTime parseDate))
                    {
                        yearMonth = parseDate;
                    }
                    else
                    {
                        MessageBox.Warning("年月格式不正确");
                        return;
                    }
                }
                else
                {
                    MessageBox.Warning("年月格式不正确");
                    return;
                }
            }
            if (SelectedProject == null && yearMonth == null && SelectedStaff == null && Req.StartDate == null && Req.EndDate == null)
            {
                MessageBox.Warning("请添加筛选条件！");
                return;
            }
            Req.ProjectId = SelectedProject?.Id;
            Req.Staff = SelectedStaff?.StaffId;
            Req.YearMonth = yearMonth;
            BonusList = await _bonusService.AchievementBonusCalculate(Req);            
        }
    }
}
