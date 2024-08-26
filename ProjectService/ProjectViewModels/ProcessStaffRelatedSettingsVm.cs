using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectViewModels
{   
    public partial class ProcessStaffRelatedSettingsVm : ObservableObject
    {
        public Guid ProjectId { get; set; }
        private ObservableCollection<StaffVm>? _availableStaffOptions;
        public ObservableCollection<StaffVm>? AvailableStaffOptions
        {
            get => _availableStaffOptions;
            set => SetProperty(ref _availableStaffOptions, value);
        }
        private StaffVm? _selectedStaff;
        public StaffVm? SelectedStaff
        {
            get => _selectedStaff;
            set => SetProperty(ref _selectedStaff, value);
        }
        private ObservableCollection<ProcessUnitVm>? _availableProcessUnitOptions;
        public ObservableCollection<ProcessUnitVm>? AvailableProcessUnitOptions
        {
            get => _availableProcessUnitOptions;
            set => SetProperty(ref _availableProcessUnitOptions, value);
        }
        private ProcessUnitVm? _selectedProcessUnit;
        public ProcessUnitVm? SelectedProcessUnit
        {
            get => _selectedProcessUnit;
            set => SetProperty(ref _selectedProcessUnit, value);
        }
    }
    public partial class ProcessStaffRelatedVm : ObservableObject
    {
        private Guid? _relatedId;
        public Guid? RelatedId
        {
            get => _relatedId;
            set => SetProperty(ref _relatedId, value);
        }
        private Guid _projectId;
        public Guid ProjectId
        {
            get => _projectId;
            set => SetProperty(ref _projectId, value);
        }
        private Guid _staffId;
        public Guid StaffId
        {
            get => _staffId;
            set => SetProperty(ref _staffId, value);
        }
        private int _processUnitId;
        public int ProcessUnitId
        {
            get => _processUnitId; 
            set => SetProperty(ref _processUnitId, value); 
        }
    }
}
