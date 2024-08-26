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
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using MessageBox = HandyControl.Controls.MessageBox;

namespace Project.ViewModel
{
    public partial class AddApplicationViewModel : ObservableObject
    {
        private readonly ApplicationService _applicationService;
        private readonly ProjectService _projectService;
        private readonly StaffService _staffService;
        private readonly DictService _dictService;
        private readonly DeviceService _deviceService;
        private readonly ConsumableService _consumableService;
        private readonly ProcessService _processService;

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

        private DateTime? _selectedApplicationDate = DateTime.Now;
        public DateTime? SelectedApplicationDate
        {
            get => _selectedApplicationDate;
            set => SetProperty(ref _selectedApplicationDate, value);
        }

        private ApplicationVm _application = null!;
        public ApplicationVm Application
        {
            get => _application;
            set => SetProperty(ref _application, value);
        }
        private List<DictDataVm> _applicationTypeSource = null!;
        public List<DictDataVm> ApplicationTypeSource
        {
            get => _applicationTypeSource;
            set => SetProperty(ref _applicationTypeSource, value);
        }
        private DictDataVm? _selectedApplicationType;
        public DictDataVm? SelectedApplicationType
        {
            get => _selectedApplicationType;
            set
            {
                SetProperty(ref _selectedApplicationType, value);
                if (value != null)
                {
                    DeviceApplicationVisible = value.DictLabel == "设备申请";
                    ConsumableApplicationVisible = value.DictLabel == "消耗品申请";
                    PeopleApplicationVisible = value.DictLabel == "人员申请";
                }
                
            }
        }

        private ObservableCollection<SelectedTypeVm<DeviceTypeVm>> _selectedDeviceTypeList = [];
        public ObservableCollection<SelectedTypeVm<DeviceTypeVm>> SelectedDeviceTypeList
        {
            get => _selectedDeviceTypeList;
            set => SetProperty(ref _selectedDeviceTypeList, value);
        }
        private ObservableCollection<SelectedTypeVm<ConsumableTypeVm>> _selectedConsumableTypeList = [];
        public ObservableCollection<SelectedTypeVm<ConsumableTypeVm>> SelectedConsumableTypeList
        {
            get => _selectedConsumableTypeList;
            set => SetProperty(ref _selectedConsumableTypeList, value);
        }
        private ObservableCollection<SelectedProcessVm> _selectedProcessList = [];
        public ObservableCollection<SelectedProcessVm> SelectedProcessList
        {
            get => _selectedProcessList;
            set => SetProperty(ref _selectedProcessList, value);
        }
        
        private bool _deviceApplicationVisible;
        public bool DeviceApplicationVisible
        {
            get => _deviceApplicationVisible;
            set => SetProperty(ref _deviceApplicationVisible, value);
        }
        private bool _consumableApplicationVisible;
        public bool ConsumableApplicationVisible
        {
            get => _consumableApplicationVisible;
            set => SetProperty(ref _consumableApplicationVisible, value);
        }
        private bool _peopleApplicationVisible;
        public bool PeopleApplicationVisible
        {
            get => _peopleApplicationVisible;
            set => SetProperty(ref _peopleApplicationVisible, value);
        }
        private List<DeviceTypeVm> DeviceTypeList = null!;
        private List<ConsumableTypeVm> ConsumableTypeList = null!;
        private List<ProcessVm> ProcessList = null!;
        private Guid? _applicationId;
        private bool _isUpdateEnabled;
        public bool IsUpdateVisibility
        {
            get => _isUpdateEnabled;
            set => SetProperty(ref _isUpdateEnabled, value);
        }
        private bool _resVisibility;
        public bool ResVisibility
        {
            get => _resVisibility;
            set => SetProperty(ref _resVisibility, value);
        }
        private bool _resUpdateVisibility;
        public bool ResUpdateVisibility
        {
            get => _resUpdateVisibility;
            set => SetProperty(ref _resUpdateVisibility, value);
        }
        private bool _resTimeVisibility;
        public bool ResTimeVisibility
        {
            get => _resTimeVisibility;
            set => SetProperty(ref _resTimeVisibility, value);
        }
        private bool _resTimeEnabled;
        public bool ResTimeEnabled
        {
            get => _resTimeEnabled;
            set => SetProperty(ref _resTimeEnabled, value);
        }
        public AddApplicationViewModel(Guid? applicationId, ApplicationService applicationService, StaffService staffService, ProjectService projectService, DictService dictService, DeviceService deviceService, ConsumableService consumableService, ProcessService processService, bool? showRes = null)
        {
            _applicationService = applicationService;
            _staffService = staffService;
            _projectService = projectService;
            _dictService = dictService;
            _deviceService = deviceService;
            _consumableService = consumableService;
            _processService = processService;
            _applicationId = applicationId;
            if (showRes != null && showRes == true)
            {
                ResVisibility = true;
            }
            Task.Run(LoadDataAsync);
        }

        private async Task LoadDataAsync()
        {
            await Task.WhenAll(LoadProjectNames(), LoadStaff(), LoadApplicationTypeDict(), LoadDeviceTypeList(), LoadConsumableTypeList(), LoadProcessList())
                .ContinueWith(c => LoadApplication());
        }
        private async Task LoadProjectNames()
        {
            ProjectNameList = await _projectService.LoadProjectNames();
        }
        private async Task LoadStaff()
        {
            StaffList = await _staffService.GetStaffList();
        }
        private async Task LoadApplicationTypeDict()
        {
            ApplicationTypeSource = await _dictService.GetDictDataByTypeName(DictSettings.ApplicationTypeName);
            
        }
        private async Task LoadDeviceTypeList()
        {
            DeviceTypeList = await _deviceService.GetDeviceTypeList();
        }
        private async Task LoadConsumableTypeList()
        {
            ConsumableTypeList = await _consumableService.GetConsumableTypeList(new CommonReqs());
        }
        private async Task LoadProcessList()
        {
            ProcessList = await _processService.GetProcessList();
        }

        private async Task LoadApplication()
        {
            await System.Windows.Application.Current.Dispatcher.Invoke(async () => {
                if (_applicationId != null)
                {
                    Application = await _applicationService.GetApplicationById(_applicationId.Value);
                    SelectedProject = ProjectNameList.FirstOrDefault(m => m.Id == Application?.ProjectId);
                    SearchProjectNameText = SelectedProject?.Name;
                    SelectedStaff = StaffList?.FirstOrDefault(m => m.StaffId == Application?.ApplicationUser);
                    SearchStaffText = SelectedStaff?.StaffName;
                    SelectedApplicationType = ApplicationTypeSource.FirstOrDefault(m => m.DictCode == Application?.ApplicationType);
                    if (Application?.ApplicationTime != null)
                    {
                        SelectedApplicationDate = Application.ApplicationTime;
                    }                    
                    if (Application?.DeviceList != null)
                    {
                        foreach (var device in Application.DeviceList)
                        {
                            SelectedDeviceTypeList.Add(new SelectedTypeVm<DeviceTypeVm>
                            {
                                Quantity = device.Quantity ?? 0,
                                TypeList = DeviceTypeList,
                                SelectedType = DeviceTypeList.FirstOrDefault(m => m.DeviceTypeId == device.DeviceTypeId)
                            });
                        }
                    }
                    if (Application?.ConsumableList != null)
                    {
                        foreach (var consumable in Application.ConsumableList)
                        {
                            SelectedConsumableTypeList.Add(new SelectedTypeVm<ConsumableTypeVm>
                            {
                                Quantity = consumable.Quantity ?? 0,
                                TypeList = ConsumableTypeList,
                                SelectedType = ConsumableTypeList.FirstOrDefault(m => m.ConsumableTypeId == consumable.ConsumableTypeId)
                            });
                        }
                    }
                    if (Application?.PersonList != null)
                    {
                        foreach (var people in Application.PersonList)
                        {
                            var vm = new SelectedProcessVm
                            {
                                Quantity = people.Count ?? 0,
                                ProcessList = ProcessList
                            };
                            vm.SearchProcessText = people.ProcessName;
                            vm.SelectedProcess = ProcessList.FirstOrDefault(m => m.ProcessId == people.ProcessId);
                            SelectedProcessList.Add(vm);
                            
                        }
                    }                                      
                }
                if (Application == null) Application = new ApplicationVm();
                
                if (ResVisibility)
                {
                    IsUpdateVisibility = false;
                    ResUpdateVisibility = true;
                }
                else
                {
                    switch (Application?.ApplicationStatusName)
                    {
                        //case DictSettings.ApplicationStatus_Approved:
                        //    IsUpdateVisibility = false;
                        //    ResVisibility = true;
                        //    ResUpdateVisibility = false;
                        //    break;
                        //case DictSettings.ApplicationStatus_Unapproved:
                        //    IsUpdateVisibility = false;
                        //    ResVisibility = true;
                        //    ResUpdateVisibility = false;
                        //    break;
                        case DictSettings.ApplicationStatus_Approved:
                        case DictSettings.ApplicationStatus_Unapproved:
                        case DictSettings.ApplicationStatus_Processed:
                            IsUpdateVisibility = false;
                            ResVisibility = true;
                            ResUpdateVisibility = false;
                            break;
                        case DictSettings.ApplicationStatus_ApplyFor:
                        default:
                            IsUpdateVisibility = true;
                            break;
                    }
                }
            });
            
        }
        [RelayCommand]
        private async Task Save()
        {
            if (SelectedProject == null || SelectedProject.Id == null)
            {
                MessageBox.Warning("项目不能为空");
                return;
            }
            if (SelectedStaff == null)
            {
                MessageBox.Warning("申请人不能为空");
                return;
            }
            if(SelectedApplicationType ==null)
            {
                MessageBox.Warning("申请类型不能为空");
                return;
            }
            if (string.IsNullOrWhiteSpace(Application.ApplicationTitle) || string.IsNullOrWhiteSpace(Application.ApplicationContent))
            {
                MessageBox.Warning("标题和内容不能为空");
                return;
            }
            if (SelectedApplicationDate == null)
            {
                MessageBox.Warning("申请日期不能为空");
                return;
            }
            Application.ProjectId = SelectedProject.Id.Value;
            Application.ApplicationUser = SelectedStaff.StaffId;
            Application.ApplicationType = SelectedApplicationType.DictCode;
            Application.ApplicationTime = SelectedApplicationDate.Value;
            switch (SelectedApplicationType.DictLabel)
            {
                default:
                case "设备申请":
                    Application.DeviceList = [];
                    foreach (var device in SelectedDeviceTypeList)
                    {
                        if (device.SelectedType == null || device.Quantity == 0) continue;
                        
                        Application.DeviceList.Add(new ApplicationDeviceVm
                        {
                            DeviceTypeId = device.SelectedType.DeviceTypeId,
                            Quantity = device.Quantity
                        });                        
                    }
                    Application.ConsumableList = null;
                    Application.PersonList = null;
                    break;
                case "消耗品申请":
                    Application.ConsumableList = [];
                    foreach (var consumable in SelectedConsumableTypeList)
                    {
                        if (consumable.SelectedType == null || consumable.Quantity == 0) continue;
                        
                        Application.ConsumableList.Add(new ApplicationConsumableVm
                        {
                            ConsumableTypeId = consumable.SelectedType.ConsumableTypeId,
                            Quantity = consumable.Quantity
                        });
                    }
                    Application.DeviceList = null;
                    Application.PersonList = null;
                    break;
                case "人员申请":
                    Application.PersonList = [];
                    foreach (var process in SelectedProcessList)
                    {
                        if (process.SelectedProcess == null || process.Quantity == 0) continue;
                        
                        Application.PersonList.Add(new ApplicationPersonVm
                        {
                            ProcessId = process.SelectedProcess.ProcessId,
                            Count = process.Quantity
                        });
                    }
                    Application.DeviceList = null;
                    Application.ConsumableList = null;                    
                    break;
            }
            var result = await _applicationService.SaveApplication(Application);
            if (result)
            {
                Growl.Success("保存成功");
            }
        }
        [RelayCommand]
        private void AddDeviceType()
        {
            SelectedDeviceTypeList.Add(new SelectedTypeVm<DeviceTypeVm> { TypeList = DeviceTypeList });
        }
        [RelayCommand]
        private void RemoveDeviceType(SelectedTypeVm<DeviceTypeVm> del)
        {
            SelectedDeviceTypeList.Remove(del);
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
        private void AddProcessType()
        {
            SelectedProcessList.Add(new SelectedProcessVm { ProcessList = ProcessList });
        }
        [RelayCommand]
        private void RemoveProcessType(SelectedProcessVm del)
        {
            SelectedProcessList.Remove(del);
        }
        [RelayCommand]
        private async Task Approve(object p)
        {
            if (Convert.ToInt32(p) == 1)
            {
                Application.ApplicationStatus = (await _dictService.GetDictDataId(DictSettings.ApplicationStatusTypeName, DictSettings.ApplicationStatus_Approved)).Value;
            }
            else
            {
                Application.ApplicationStatus = (await _dictService.GetDictDataId(DictSettings.ApplicationStatusTypeName, DictSettings.ApplicationStatus_Unapproved)).Value;
            }
            var result = await _applicationService.Approve(Application);
            if (result)
            {
                Growl.Success("已审批");
            }
        }
    }
    public class SelectedTypeVm<T> : ObservableObject
    {
        private List<T>? _typeList;
        public List<T>? TypeList
        {
            get => _typeList;
            set => SetProperty(ref _typeList, value);
        }

        private T? _selectedType;
        public T? SelectedType
        {
            get => _selectedType;
            set => SetProperty(ref _selectedType, value);
        }
        private int _quantity;
        public int Quantity
        {
            get => _quantity;
            set => SetProperty(ref _quantity, value);
        }
    }

    public partial class SelectedProcessVm : ObservableObject
    {
        public List<ProcessVm> ProcessList = null!;
        [ObservableProperty]
        private ObservableCollection<ProcessVm>? _processSource;
        private string? _searchProcessText;
        public string? SearchProcessText
        {
            get => _searchProcessText;
            set
            {
                SetProperty(ref _searchProcessText, value);
                if (!string.IsNullOrWhiteSpace(_searchProcessText))
                {
                    //var searchText = _searchProcessUnitText;
                    //var splitList = searchText.Split(' ').ToList();
                    //if (splitList.Count > 1) searchText = splitList[0];
                    var list = ProcessList?.Where(m => m.ProcessName.Contains(_searchProcessText)).ToList();
                    if (list != null) ProcessSource = new ObservableCollection<ProcessVm>(list);
                }
                else
                {
                    ProcessSource = null;
                }

            }
        }
        
        private ProcessVm? _selectedProcess;
        public ProcessVm? SelectedProcess
        {
            get => _selectedProcess;
            set => SetProperty(ref _selectedProcess, value);
        }
        private int _quantity;
        public int Quantity
        {
            get => _quantity;
            set => SetProperty(ref _quantity, value);
        }
    }
}
