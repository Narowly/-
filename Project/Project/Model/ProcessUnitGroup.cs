using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectViewModels;
using System.Collections.ObjectModel;

namespace Project.Model
{

    public partial class ProcessUnitGroup : ObservableObject
    {
        public List<ProcessUnitVm> ProcessUnitList = null!;
        public List<ProcessVm> ProcessList = null!;
        [ObservableProperty]
        private ObservableCollection<ProcessVm>? processSource;        
        [ObservableProperty]
        private ObservableCollection<ProUnitVm>? proUnitSource;
        
        private ProcessVm? selectedProcess;
        public ProcessVm? SelectedProcess
        {
            get => selectedProcess;
            set
            {
                SetProperty(ref selectedProcess, value);
                UpdateUnitSource();
            }
        }
        [ObservableProperty]
        private ProUnitVm? selectedProUnit;
        
        private string? selectedProcessName;
        public string? SelectedProcessName
        {
            get => selectedProcessName;
            set
            {
                SetProperty(ref selectedProcessName, value);
                if (!string.IsNullOrWhiteSpace(selectedProcessName))
                {
                    if (ProcessList != null)
                    {
                        var list = ProcessList.Where(m => m.ProcessName.Contains(selectedProcessName)).ToList();
                        ProcessSource = new ObservableCollection<ProcessVm>(list);
                    }
                    else
                    {
                        var list = ProcessUnitList.Where(m => m.Process.ProcessName.Contains(selectedProcessName)).ToList();

                        var sourceList = list.Select(m => new { pId = m.Process.ProcessId }).Distinct().Select(anon => ProcessUnitList.First(u => u.Process.ProcessId == anon.pId).Process).ToList();
                        ProcessSource = new ObservableCollection<ProcessVm>(sourceList);
                    }
                    
                }
                else
                {
                    var list = ProcessUnitList.Select(m => new { pId = m.Process.ProcessId }).Distinct().Select(anon => ProcessUnitList.First(u => u.Process.ProcessId == anon.pId).Process).ToList();
                    ProcessSource = new ObservableCollection<ProcessVm>(list);
                }
            }
        }
        [ObservableProperty]
        private string? selectedProUnitName;
        [ObservableProperty]
        private int workload;
        
        private decimal weight;
        public decimal Weight
        {
            get => weight;
            set => SetProperty(ref weight, value);
        }
        [ObservableProperty]
        private int sequence;

        private void UpdateUnitSource()
        {
            if (SelectedProcess == null)
            {
                return;
            }
            var sourceList = ProcessUnitList.Where(m => m.ProcessId == SelectedProcess.ProcessId).Select(m => m.ProUnit).ToList();
            ProUnitSource = new ObservableCollection<ProUnitVm>(sourceList);
        }
    }
}
