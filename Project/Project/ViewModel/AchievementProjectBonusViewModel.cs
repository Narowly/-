using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
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
    public partial class AchievementProjectBonusViewModel : ObservableObject
    {
        private readonly ProjectBonusService _bonusService;
        private readonly ProjectService _projectService;

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
        private ProjectAchievementBonusVm? _bonus;
        public ProjectAchievementBonusVm? Bonus
        {
            get => _bonus;
            set => SetProperty(ref _bonus, value);
        }

        public AchievementProjectBonusViewModel(ProjectBonusService bonusService, ProjectService projectService)
        {
            _bonusService = bonusService;
            _projectService = projectService;
            Task.Run(LoadDataAsync);
        }

        private async Task LoadDataAsync()
        {
            await LoadProjectNames();
        }

        private async Task LoadProjectNames()
        {
            ProjectNameList = await _projectService.LoadProjectNames();
        }
        [RelayCommand]
        private async Task ProjectBonusCalculate()
        {
            if (SelectedProject != null && SelectedProject.Id != null)
                Bonus = await _bonusService.ProjectBonusCalculate(SelectedProject.Id.Value);
        }
    }
}
