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

namespace Project.ViewModel
{
    public partial class StockOutBoundViewModel : ObservableObject
    {
        private readonly ConsumableService _consumableService;
        private readonly ProjectService _projectService;
        private readonly Guid? _consumableId;
        private StockOutBoundVm _stockOutBound = new();
        public StockOutBoundVm StockOutBound
        {
            get => _stockOutBound;
            set => SetProperty(ref _stockOutBound, value);
        }
        private ConsumableVm? _selectedConsumable;
        public ConsumableVm? SelectedConsumable
        {
            get => _selectedConsumable;
            set => SetProperty(ref _selectedConsumable, value);
        }
        private List<ConsumableVm> _consumableList = null!;
        public List<ConsumableVm> ConsumableList
        {
            get => _consumableList;
            set => SetProperty(ref _consumableList, value);
        }
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
        private List<ProjectAutoCompleteModel> ProjectNameList = null!;
        private ObservableCollection<ProjectAutoCompleteModel>? _projectNamesSource;
        public ObservableCollection<ProjectAutoCompleteModel>? ProjectNamesSource
        {
            get => _projectNamesSource;
            set => SetProperty(ref _projectNamesSource, value);
        }
        private ProjectAutoCompleteModel? _selectedProject;
        public ProjectAutoCompleteModel? SelectedProject
        {
            get => _selectedProject;
            set
            {
                SetProperty(ref _selectedProject, value);
            }
        }
        private DateTime? _selectedDate = DateTime.Now;
        public DateTime? SelectedDate
        {
            get => _selectedDate;
            set => SetProperty(ref _selectedDate, value);
        }
        public StockOutBoundViewModel(Guid? consumableId, ConsumableService consumableService, ProjectService projectService)
        {
            _consumableService = consumableService;
            _projectService = projectService;
            _consumableId = consumableId;
            Task.Run(LoadDataAsync);
        }
        private async Task LoadDataAsync()
        {
            await Task.WhenAll(LoadConsumableList(), LoadProjectNames()).ContinueWith(c => LoadConsumableById());
        }
        private async Task LoadConsumableList()
        {
            ConsumableList = await _consumableService.GetConsumableList(new ConsumableReqs());
        }
        private async Task LoadProjectNames()
        {
            ProjectNameList = await _projectService.LoadProjectNames();
            //ProjectNameList = list.Select(m => new ProjectAutoCompleteModel { Id = m.ProjectId, Name = string.Format("{0}|{1}", m.Contract.ContractNumber, m.ProjectName), Number = m.Contract.ContractNumber }).ToList();
        }
        private async Task LoadConsumableById()
        {
            if (_consumableId == null) return;
            var consumable = await _consumableService.GetConsumableById(_consumableId.Value);
            SelectedConsumable = ConsumableList.FirstOrDefault(m => m.ConsumableId == consumable.ConsumableId);
        }
        [RelayCommand]
        private async Task Save()
        {
            if (SelectedConsumable == null || SelectedDate == null || SelectedProject ==null)
            {
                MessageBox.Warning("消耗品、出库日期或项目不能为空");
                return;
            }
            if (StockOutBound.Quantity == 0)
            {
                MessageBox.Warning("数量不能为0");
                return;
            }
            StockOutBound.ConsumableId = SelectedConsumable.ConsumableId;
            StockOutBound.ProjectId = SelectedProject?.Id;
            StockOutBound.OutBoundDate = SelectedDate.Value;
            var result = await _consumableService.SaveStockOutBound(StockOutBound);
            if (result)
            {
                Growl.Success("保存成功");
            }
        }
    }
}
