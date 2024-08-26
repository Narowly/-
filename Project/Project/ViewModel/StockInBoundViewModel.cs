using CommunityToolkit.Mvvm.ComponentModel;
using Project.Services.DataServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectViewModels;
using Project.Services;
using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.Input;
using MessageBox = HandyControl.Controls.MessageBox;
using HandyControl.Controls;
using Newtonsoft.Json.Linq;

namespace Project.ViewModel
{
    public partial class StockInBoundViewModel : ObservableObject
    {
        private readonly ConsumableService _consumableService;
        private readonly ProjectService _projectService;
        private readonly Guid? _consumableId;
        private StockInBoundVm _stockInBound = new();
        public StockInBoundVm StockInBound
        {
            get => _stockInBound;
            set => SetProperty(ref _stockInBound, value);
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
        public StockInBoundViewModel(Guid? consumableId, ConsumableService consumableService, ProjectService projectService)
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
            if (SelectedConsumable == null || SelectedDate==null)
            {
                MessageBox.Warning("消耗品或入库日期不能为空");
                return;
            }
            if(StockInBound.Quantity==0)
            {
                MessageBox.Warning("数量不能为0");
                return;
            }
            StockInBound.ConsumableId = SelectedConsumable.ConsumableId;
            StockInBound.ProjectId = SelectedProject?.Id;
            StockInBound.InBoundDate = SelectedDate.Value;
            var result = await _consumableService.SaveStockInBound(StockInBound);
            if (result)
            {
                Growl.Success("保存成功");
            }
        }
    }
}
