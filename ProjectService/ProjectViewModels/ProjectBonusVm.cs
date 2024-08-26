using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectViewModels
{
    public class ProjectBonusVm : ObservableObject
    {
        private long? _id;
        public long? Id
        {
            get => _id;
            set => SetProperty(ref _id, value);
        }

        private Guid? _projectProcessId;
        public Guid? ProjectProcessId
        {
            get => _projectProcessId;
            set => SetProperty(ref _projectProcessId, value);
        }

        private decimal _bonus;
        public decimal Bonus
        {
            get => _bonus;
            set => SetProperty(ref _bonus, value);
        }

        private double _workload;
        public double Workload
        {
            get => _workload; set => SetProperty(ref _workload, value);
        }

        private ProjectProcessVm? _projectProcess;
        public ProjectProcessVm? ProjectProcess
        {
            get => _projectProcess; set => SetProperty(ref _projectProcess, value);
        }

        private string? _remarks;
        public string? Remarks
        {
            get => _remarks;
            set => SetProperty(ref _remarks, value);
        }
    }

    public class ProjectBonusSettingsVm : ObservableObject
    {
        private ProjectProcessVm projectProcess = null!;
        public ProjectProcessVm ProjectProcess
        {
            get => projectProcess;
            set => SetProperty(ref projectProcess, value);
        }

        private ObservableCollection<ProjectBonusVm>? _projectBonusList;
        public ObservableCollection<ProjectBonusVm>? ProjectBonusList
        {
            get => _projectBonusList;
            set => SetProperty(ref _projectBonusList, value);
        }
    }

}
