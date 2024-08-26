using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using HandyControl.Controls;
using HandyControl.Tools.Extension;
using Project.Common;
using Project.Services;
using Project.Services.DataServices;
using ProjectViewModels;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using MessageBox = HandyControl.Controls.MessageBox;

namespace Project.ViewModel
{
    public partial class HandlingApplicationViewModel : ObservableObject
    {
        private readonly ProjectService _projectService;
        private readonly DeviceService _deviceService;
        private readonly DictService _dictService;
        private readonly ApplicationService _applicationService;
        private readonly StaffService _staffService;
        private readonly ConsumableService _consumableService;
        private readonly Guid _applicationId;
        //private readonly Guid _projectId;
        private string _applicationTypeValue = null!;
        private ProjectVm _project = null!;
        public ProjectVm Project
        {
            get => _project;
            set => SetProperty(ref _project, value);
        }
        private List<DeviceVm> DeviceList = null!;
        [ObservableProperty]
        private ObservableCollection<DeviceVm>? bindingDeviceList;
        //[ObservableProperty]
        //private DeviceVm? selectedDevice;
        [ObservableProperty]
        private ObservableCollection<DeviceVm>? selectedDeviceList = [];
        [ObservableProperty]
        private ObservableCollection<DeviceVm>? selectedProjectDeviceList = [];

        [ObservableProperty]
        private ObservableCollection<DeviceVm>? projectDeviceList;
        //[ObservableProperty]
        //private DeviceVm? selectedProjectDevice;
        [ObservableProperty]
        private DeviceReqs _deviceReq = new();
        [ObservableProperty]
        private ApplicationVm _application = null!;
        private bool _deviceVisibility;
        public bool DeviceVisibility
        {
            get => _deviceVisibility;
            set => SetProperty(ref _deviceVisibility, value);
        }
        private bool _consumableVisibility;
        public bool ConsumableVisibility
        {
            get => _consumableVisibility;
            set => SetProperty(ref _consumableVisibility, value);
        }
        private bool _peopleVisibility;
        public bool PeopleVisibility
        {
            get => _peopleVisibility;
            set => SetProperty(ref _peopleVisibility, value);
        }
        private List<StaffVm> IdleStaffs = null!;
        [ObservableProperty]
        private ObservableCollection<StaffVm> _bindingIdleStaffs = null!;
        [ObservableProperty]
        private ObservableCollection<StaffVm>? _projectStaffs;
        [ObservableProperty]
        private ObservableCollection<StaffVm>? _leftSelectdStaffList = [];
        [ObservableProperty]
        private ObservableCollection<StaffVm>? _rightSelectedStaffList = [];
        [ObservableProperty]
        private string? _searchStaffText;
        private List<ConsumableVm> ConsumableList = null!;
        [ObservableProperty]
        private ObservableCollection<ConsumableVm>? _bindingConsumableList = null!;
        [ObservableProperty]
        private ObservableCollection<StockOutBoundVm>? _selectedConsumableList = [];
        


        public HandlingApplicationViewModel(Guid applicationId, ProjectService projectService, DictService dictService,
            DeviceService deviceService, ApplicationService applicationService, StaffService staffService, ConsumableService consumableService)
        {
            //_projectId = projectId;
            //_applicationTypeValue = applicationTypeValue;
            _applicationId = applicationId;
            _applicationService = applicationService;
            _projectService = projectService;
            _dictService = dictService;
            _deviceService = deviceService;
            _staffService = staffService;
            _consumableService = consumableService;
            Task.Run(LoadDataAsync);
        }

        private async Task LoadDataAsync()
        {
            if (await LoadApplication())
            {
                switch (_applicationTypeValue)
                {
                    case "1":
                        await LoadDeviceList();
                        DeviceVisibility = true;
                        ConsumableVisibility = false;
                        PeopleVisibility = false;
                        break;
                    case "2":
                        await LoadConsumableList();
                        DeviceVisibility = false;
                        ConsumableVisibility = true;
                        PeopleVisibility = false;
                        break;
                    case "3":
                        await LoadStaffList();
                        DeviceVisibility = false;
                        ConsumableVisibility = false;
                        PeopleVisibility = true;
                        break;
                }
            }
        }
        private async Task<bool> LoadApplication()
        {
            var app = await _applicationService.GetApplicationById(_applicationId, true);
            if (app != null) Application = app;
            if (app == null)
            {
                MessageBox.Warning("申请不存在, 请关闭页面");
                return false;
            }
            var typeDictData = await _dictService.GetDictData(Application.ApplicationType);
            if (typeDictData != null)
            {
                _applicationTypeValue = typeDictData.DictValue;
            }
            ProjectDeviceList = Application.Project?.InProjectDevice ?? [];
            ProjectStaffs = Application.Project?.InProjectStaffs ?? [];
            return true;
        }

        private async Task LoadDeviceList()
        {
            DeviceReq.Status = await _dictService.GetDictDataId(DictSettings.DeviceStatusTypeName, DictSettings.DeviceStatus_Normal);
            DeviceList = await _deviceService.GetDeviceList(DeviceReq);
            foreach (var device in ProjectDeviceList)
            {
                var removeItem = DeviceList.FirstOrDefault(m => m.DeviceId == device.DeviceId);
                if (removeItem != null)
                    DeviceList.Remove(removeItem);
            }
            BindingDeviceList = new ObservableCollection<DeviceVm>(DeviceList);
        }
        private async Task LoadStaffList()
        {
            IdleStaffs = await _staffService.GetStaffListWithProjectName();
            if (ProjectStaffs != null)
            {
                var inProjectStaffIds = ProjectStaffs.Select(m => m.StaffId).ToList();
                IdleStaffs.RemoveAll(m => inProjectStaffIds.Contains(m.StaffId));
            }
            BindingIdleStaffs = new ObservableCollection<StaffVm>(IdleStaffs);
        }

        private async Task LoadConsumableList()
        {
            ConsumableList = await _consumableService.GetConsumableList(new ConsumableReqs());
            BindingConsumableList = new ObservableCollection<ConsumableVm>(ConsumableList);
        }
        [RelayCommand]
        private void UpdateLeftDeviceSelectedItems(object parameter)
        {
            var selectedLeftItems = parameter as IList;
            if (selectedLeftItems != null)
            {
                SelectedDeviceList.Clear();
                foreach (var item in selectedLeftItems)
                {
                    if (item is DeviceVm device)
                    {
                        SelectedDeviceList.Add(device);
                    }
                }
            }
        }
        [RelayCommand]
        private void UpdateRightDeviceSelectedItems(object parameter)
        {
            var selectedRightItems = parameter as IList;
            if (selectedRightItems != null)
            {
                SelectedProjectDeviceList.Clear();
                foreach (var item in selectedRightItems)
                {
                    if (item is DeviceVm device)
                    {
                        SelectedProjectDeviceList.Add(device);
                    }
                }
            }
        }
        [RelayCommand]
        private void AddDevice()
        {
            if (SelectedDeviceList != null && SelectedDeviceList.Count > 0)
            {
                var removeList = new List<DeviceVm>(SelectedDeviceList);
                foreach (var device in removeList)
                {
                    ProjectDeviceList.Add(device);
                    DeviceList.Remove(device);
                    BindingDeviceList.Remove(device);
                }
            }
        }

        [RelayCommand]
        private void RemoveDevice()
        {
            if (SelectedProjectDeviceList != null && SelectedProjectDeviceList.Count > 0)
            {
                var removeList = new List<DeviceVm>(SelectedProjectDeviceList);
                foreach (var device in removeList)
                {
                    DeviceList.Add(device);
                    BindingDeviceList.Add(device);
                    ProjectDeviceList.Remove(device);
                }
            }
        }
        [RelayCommand]
        private async Task SearchDevice()
        {
            if (ProjectDeviceList != null)
            {
                foreach (var device in ProjectDeviceList)
                {
                    var removeItem = DeviceList.FirstOrDefault(m => m.DeviceId == device.DeviceId);
                    if (removeItem != null)
                        DeviceList.Remove(removeItem);
                }
            }
            
            var searchResult = await _deviceService.GetDeviceListLocal(DeviceReq, DeviceList);
            if (ProjectDeviceList != null)
            {
                searchResult = searchResult.Except(ProjectDeviceList).ToList();
            }
            BindingDeviceList = new ObservableCollection<DeviceVm>(searchResult);
        }
        [RelayCommand]
        private void AddStaffIntoProject()
        {
            if (LeftSelectdStaffList != null && LeftSelectdStaffList.Count > 0)
            {
                var removeList = new List<StaffVm>(LeftSelectdStaffList);
                foreach (var staff in removeList)
                {
                    ProjectStaffs.Add(staff);
                    IdleStaffs.Remove(staff);
                    BindingIdleStaffs.Remove(staff);
                }
            }
        }
        [RelayCommand]
        private void RemoveStaffFromProject()
        {
            if (RightSelectedStaffList != null && RightSelectedStaffList.Count > 0)
            {
                var removeList = new List<StaffVm>(RightSelectedStaffList);
                foreach (var staff in removeList)
                {
                    IdleStaffs.Add(staff);
                    BindingIdleStaffs.Add(staff);
                    ProjectStaffs.Remove(staff);
                }
            }
        }
        [RelayCommand]
        private void UpdateLeftSelectedItems(object parameter)
        {
            var selectedLeftItems = parameter as IList;
            if (selectedLeftItems != null)
            {
                LeftSelectdStaffList.Clear();
                foreach (var item in selectedLeftItems)
                {
                    if (item is StaffVm staff)
                    {
                        LeftSelectdStaffList.Add(staff);
                    }
                }
            }
        }

        [RelayCommand]
        private void UpdateRightSelectedItems(object parameter)
        {
            var selectedRightItems = parameter as IList;
            if (selectedRightItems != null)
            {
                RightSelectedStaffList.Clear();
                foreach (var item in selectedRightItems)
                {
                    if (item is StaffVm staff)
                    {
                        RightSelectedStaffList.Add(staff);
                    }
                }
            }
        }

        [RelayCommand]
        private void UpdateLeftStaffSource()
        {
            if (!string.IsNullOrWhiteSpace(SearchStaffText))
            {
                BindingIdleStaffs = new ObservableCollection<StaffVm>(IdleStaffs.Where(m => m.StaffName.Contains(SearchStaffText)).ToList());
            }
            else
            {
                BindingIdleStaffs = new ObservableCollection<StaffVm>(IdleStaffs);
            }
        }

        [RelayCommand]
        private async Task ProcessStaffApplication()
        {
            Application.Project.InProjectStaffs = ProjectStaffs;
            var req = new ApplicationProcessdReq
            {
                ApplicationId = _applicationId,
                Project = Application.Project
            };
            var result = await _applicationService.ProcessStaffApplication(req);
            if (result)
            {
                Growl.Success("调动成功");
                SendCloseMessage();
            }
        }
        [RelayCommand]
        private async Task ProcessDeviceApplication()
        {
            Application.Project.InProjectDevice = ProjectDeviceList;
            var req = new ApplicationProcessdReq
            {
                ApplicationId = _applicationId,
                Project = Application.Project
            };
            var result = await _applicationService.ProcessDeviceApplication(req);
            if (result)
            {
                Growl.Success("调动成功");
                SendCloseMessage();
            }
        }

        [RelayCommand]
        private void UpdateLeftSelectedConsumable(object parameter)
        {
            var selectedLeftConsumable = parameter as IList;
            if (selectedLeftConsumable != null)
            {
                var selectedList = new List<ConsumableVm>();
                foreach (var item in selectedLeftConsumable)
                {
                    selectedList.Add(item as ConsumableVm);
                }
                
                var selectedListId = selectedList.Select(m => m.ConsumableId).ToList();
                var removeList = SelectedConsumableList.Where(m => !selectedListId.Contains(m.ConsumableId)).ToList();
                if(removeList!=null && removeList.Count>0)
                {
                    foreach (var item in removeList)
                    {
                        SelectedConsumableList.Remove(item);
                    }
                }
                var selectedIdList = SelectedConsumableList.Select(m => m.ConsumableId).ToList();
                var addList = selectedList.Where(m => !selectedIdList.Contains(m.ConsumableId)).ToList();
                foreach (var item in addList)
                {
                    SelectedConsumableList.Add(new StockOutBoundVm
                    {
                        Consumable = item,
                        ConsumableId = item.ConsumableId,
                        ProjectId = Application.ProjectId
                    });                    
                }
            }
        }

        [RelayCommand]
        private async Task ProcessConsumableApplication()
        {
            if (SelectedConsumableList == null || SelectedConsumableList.Count == 0) return;
            foreach (var selectedConsumable in SelectedConsumableList)
            {
                if(selectedConsumable.Quantity> selectedConsumable.Consumable.Quantity)
                {
                    MessageBox.Warning($"{selectedConsumable.Consumable.ConsumableNumber} 库存不足");
                    return;
                }
            }
            var req = new ApplicationProcessdReq
            {
                ApplicationId = _applicationId,
                OutConsumableList = SelectedConsumableList.ToList()
            };
            var result = await _applicationService.ProcessConsumableApplication(req);
            if (result)
            {
                Growl.Success("调动成功");
                SendCloseMessage();
            }            
        }
        private void SendCloseMessage()
        {
            WeakReferenceMessenger.Default.Send("close", MessageToken.CloseHandlingApplication);
        }
    }
}
