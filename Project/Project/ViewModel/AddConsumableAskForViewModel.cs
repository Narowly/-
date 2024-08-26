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
    public partial class AddConsumableAskForViewModel : ObservableObject
    {
        private readonly ConsumableAskForService _consumableAskForService;
        private readonly ProjectService _projectService;
        private readonly StaffService _staffService;
        private readonly DictService _dictService;
        private readonly ConsumableService _consumableService;

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
        private ProjectAutoCompleteModel? _selectedProject;
        public ProjectAutoCompleteModel? SelectedProject
        {
            get => _selectedProject;
            set
            {
                SetProperty(ref _selectedProject, value);
                if (value != null)
                {
                    SelectedStaff = StaffList?.FirstOrDefault(m => m.StaffId == value.ProjectManagerId);
                    SearchStaffText = SelectedStaff?.StaffName;
                }
            }
        }
        private List<StaffVm> StaffList = [];
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

        private List<DictDataVm>? _askForStatusList = null!;
        public List<DictDataVm>? AskForStatusList
        {
            get => _askForStatusList;
            set => SetProperty(ref _askForStatusList, value);
        }

        private DictDataVm? _selectedStatus;
        public DictDataVm? SelectedStatus
        {
            get => _selectedStatus;
            set => SetProperty(ref _selectedStatus, value);
        }

        private List<ConsumableTypeVm>? _consumableTypeList;
        public List<ConsumableTypeVm>? ConsumableTypeList
        {
            get => _consumableTypeList;
            set => SetProperty(ref _consumableTypeList, value);
        }

        private ConsumableAskForVm _consumableAskFor = new();
        public ConsumableAskForVm ConsumableAskFor
        {
            get => _consumableAskFor;
            set => SetProperty(ref _consumableAskFor, value);
        }

        private ObservableCollection<SelectedTypeVm<ConsumableTypeVm>> _selectedConsumableTypeList = [];
        public ObservableCollection<SelectedTypeVm<ConsumableTypeVm>> SelectedConsumableTypeList
        {
            get => _selectedConsumableTypeList;
            set => SetProperty(ref _selectedConsumableTypeList, value);
        }

        public AddConsumableAskForViewModel(Guid? consumableAskForId, ConsumableAskForService consumableAskForService, ProjectService projectService, StaffService staffService, DictService dictService, ConsumableService consumableService)
        {
            _consumableAskForService = consumableAskForService;
            _projectService = projectService;
            _staffService = staffService;
            _dictService = dictService;
            _consumableService = consumableService;
            ConsumableAskFor.ConsumableAskForId = consumableAskForId;
            Task.Run(LoadDataAsync);
        }
        private async Task LoadDataAsync()
        {
            await Task.WhenAll(LoadProjectNames(), LoadStaff(), LoadAskForStatusList(), LoadConsumableTypeList())
                .ContinueWith(c => LoadConsumableAskFor());
        }

        private async Task LoadProjectNames()
        {
            ProjectNameList = await _projectService.LoadProjectNames();
        }
        private async Task LoadStaff()
        {
            StaffList = await _staffService.GetStaffList();
        }
        private async Task LoadAskForStatusList()
        {
            AskForStatusList = await _dictService.GetDictDataByTypeName(DictSettings.ConsumableAskForStatusTypeName);
        }
        private async Task LoadConsumableTypeList()
        {
            ConsumableTypeList = await _consumableService.GetConsumableTypeList(new CommonReqs());
        }

        private async Task LoadConsumableAskFor()
        {
            await System.Windows.Application.Current.Dispatcher.Invoke(async () => {
                if (!ConsumableAskFor.ConsumableAskForId.HasValue) return;

                var result = await _consumableAskForService.GetConsumableAskForById(ConsumableAskFor.ConsumableAskForId.Value);
                if (result != null)
                {
                    ConsumableAskFor = result;
                    SelectedProject = ProjectNameList.FirstOrDefault(m => m.Id == ConsumableAskFor.ProjectId);
                    SearchProjectNameText = SelectedProject?.Name;
                    SelectedStaff = StaffList.FirstOrDefault(m => m.StaffId == ConsumableAskFor.StaffId);
                    SelectedStatus = AskForStatusList?.FirstOrDefault(m => m.DictCode == ConsumableAskFor.Status);
                    if (ConsumableAskFor.ConsumableAskForItemList != null)
                    {
                        foreach (var item in ConsumableAskFor.ConsumableAskForItemList)
                        {
                            SelectedConsumableTypeList.Add(new SelectedTypeVm<ConsumableTypeVm>
                            {
                                Quantity = item.Quantity,
                                TypeList = ConsumableTypeList,
                                SelectedType = ConsumableTypeList.FirstOrDefault(m => m.ConsumableTypeId == item.ConsumableTypeId)
                            });
                        }
                    }
                }
            });            
        }

        [RelayCommand]
        private void AddConsumableType()
        {
            SelectedConsumableTypeList.Add(new SelectedTypeVm<ConsumableTypeVm> { TypeList = ConsumableTypeList });
        }
        [RelayCommand]
        private void RemoveConsumableType(SelectedTypeVm<ConsumableTypeVm> del)
        {
            SelectedConsumableTypeList.Remove(del);
        }

        [RelayCommand]
        private async Task Save()
        {
            if(SelectedStaff == null)
            {
                MessageBox.Warning("申请人不能为空");
                return;
            }
            if (SelectedStatus == null)
            {
                MessageBox.Warning("状态不能为空");
                return;
            }
            if (string.IsNullOrWhiteSpace(ConsumableAskFor.Title))
            {
                MessageBox.Warning("标题不能为空");
                return;
            }
            ConsumableAskFor.ProjectId = SelectedProject?.Id;
            ConsumableAskFor.StaffId = SelectedStaff.StaffId;
            ConsumableAskFor.Status = SelectedStatus.DictCode;
            ConsumableAskFor.ConsumableAskForItemList = [];
            foreach (var consumable in SelectedConsumableTypeList)
            {
                if (consumable.SelectedType == null || consumable.Quantity == 0) continue;
                ConsumableAskFor.ConsumableAskForItemList.Add(new ConsumableAskForItemVm
                {
                    ConsumableTypeId = consumable.SelectedType.ConsumableTypeId,
                    Quantity = consumable.Quantity
                });
            }
            var result = await _consumableAskForService.SaveConsumableAskFor(ConsumableAskFor);
            if (result)
            {
                Growl.Success("保存成功");
            }
        }
    }
}
