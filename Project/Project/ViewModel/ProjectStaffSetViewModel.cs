using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using Project.Common;
using Project.Services.DataServices;
using Project.Views.UserControls;
using ProjectViewModels;
using System.Collections;
using System.Collections.ObjectModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace Project.ViewModel
{
    internal partial class ProjectStaffSetViewModel : ObservableObject
    {
        private readonly StaffService _staffService;

        private List<StaffVm> IdleStaffs = null!;
        [ObservableProperty]
        private ObservableCollection<StaffVm> bindingIdleStaffs = null!;
        
        private Guid? _projectId;
        public Guid? ProjectId
        {
            get => _projectId;
            set
            {
                SetProperty(ref _projectId, value);

            }
        }
        [ObservableProperty]
        private ObservableCollection<StaffVm>? projectStaffs;
        [ObservableProperty]
        private ObservableCollection<StaffVm>? leftSelectdStaffList = new ObservableCollection<StaffVm>();
        [ObservableProperty]
        private ObservableCollection<StaffVm>? rightSelectedStaffList = new ObservableCollection<StaffVm>();
        [ObservableProperty]
        private string? searchText;

        public ProjectStaffSetViewModel(StaffService staffService, Guid? projectId, ObservableCollection<StaffVm>? inProjectStaffs)
        {
            ProjectId = projectId;
            _staffService = staffService;
            ProjectStaffs = inProjectStaffs;
            if(ProjectStaffs == null) ProjectStaffs = new ObservableCollection<StaffVm>();
            Task.Run(LoadDataAsync);
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

        private async Task LoadDataAsync()
        {
            await GetIdleStaffList().ContinueWith(t=> GetSpeedupStaffs());
        }

        private async Task GetIdleStaffList()
        {
            IdleStaffs = await _staffService.GetStaffList();
            //IdleStaffs = await _staffService.GetIdleStaffs(ApiSettings.ProjectStaffDuty);
            if (ProjectStaffs != null)
            {
                var inProjectStaffIds = ProjectStaffs.Select(m => m.StaffId).ToList();
                IdleStaffs.RemoveAll(m => inProjectStaffIds.Contains(m.StaffId));
            }
            BindingIdleStaffs = new ObservableCollection<StaffVm>(IdleStaffs);
            
        }
        private async Task GetSpeedupStaffs()
        {
            var speedupStaffs = await _staffService.SpeedupProjectStaff();
            if (speedupStaffs != null && speedupStaffs.Count > 0)
            {
                IdleStaffs.AddRange(speedupStaffs);
                foreach (var s in speedupStaffs)
                {
                    BindingIdleStaffs.Add(s);
                }
            }
        }
        [RelayCommand]
        private void AddIntoProject()
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
        private void RemoveFromProject()
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
        private void UpdateLeftSource()
        {
            if (!string.IsNullOrWhiteSpace(SearchText))
            {
                BindingIdleStaffs = new ObservableCollection<StaffVm>(IdleStaffs.Where(m => m.StaffName.Contains(SearchText)).ToList());
            }
            else
            {
                BindingIdleStaffs = new ObservableCollection<StaffVm>(IdleStaffs);
            }
        }

        [RelayCommand]
        private void ReturnProjectStaff()
        {
            if (ProjectStaffs != null)
                WeakReferenceMessenger.Default.Send(ProjectStaffs, MessageToken.ReturnProjectStaff);
        }

    }
}
