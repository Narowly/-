using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectViewModels;
using Project.Services.DataServices;
using Project.Model;
using CommunityToolkit.Mvvm.Input;
using Project.Views.UserControls;
using HandyControl.Controls;
using CommunityToolkit.Mvvm.Messaging;
using Project.Common;

namespace Project.ViewModel
{
    public partial class ProcessTemplateViewModel : ObservableObject
    {
        private ProcessService _service;
        private List<ProcessUnitVm> ProcessUnitList = new List<ProcessUnitVm>();
        private List<ProcessVm> ProcessList = new List<ProcessVm>();
        //private ObservableCollection<ProcessTemplateVm> _templateList = null!;
        //public ObservableCollection<ProcessTemplateVm> TemplateList
        //{
        //    get => _templateList;
        //    set => SetProperty(ref _templateList, value);
        //}
        [ObservableProperty]
        private bool canAddFlag;
        [ObservableProperty]
        private ProcessTemplateVm? _processTemplate;
        
        private ObservableCollection<ProcessUnitGroup> _processUnitGroups = new ObservableCollection<ProcessUnitGroup>();
        public ObservableCollection<ProcessUnitGroup> ProcessUnitGroups
        {
            get => _processUnitGroups;
            set => SetProperty(ref _processUnitGroups, value);
        }

        public ProcessTemplateViewModel(ProcessService service, ProcessTemplateVm? processTemplate)
        {
            _service = service;
            ProcessTemplate = processTemplate;
            if (ProcessTemplate == null) ProcessTemplate = new ProcessTemplateVm { ProcessTemplateDetails = new ObservableCollection<ProcessTemplateDetailVm>() };
            Task.Run(LoadDataAsync);
        }

        private async Task LoadDataAsync()
        {
            await LoadProcessUnitList();
        }

        private async Task LoadProcessUnitList()
        {
            ProcessUnitList = await _service.GetProcessUnitList();            
            ProcessList = ProcessUnitList.Select(m => new { pId = m.Process.ProcessId }).Distinct().Select(anon => ProcessUnitList.First(u => u.Process.ProcessId == anon.pId).Process).ToList();
            CanAddFlag = true;
            if (ProcessTemplate != null)
            {

            }
        }

        [RelayCommand]
        private void AddItem()
        {
            var item = new ProcessUnitGroup();
            item.ProcessUnitList = ProcessUnitList;
            item.ProcessList = ProcessList;
            item.ProcessSource = new ObservableCollection<ProcessVm>(ProcessList);
            ProcessUnitGroups.Add(item);
        }
        [RelayCommand]
        private void RemoveItem(ProcessUnitVm process)
        {
            //var item = ProcessUnitGroups.FirstOrDefault(m => m.ProcessUnitList.Any(w => w.ProcessId == process.ProcessId && w.UnitId == process.UnitId));
            //if (item != null) ProcessUnitGroups.Remove(item);
            var ppitem = ProcessTemplate.ProcessTemplateDetails.FirstOrDefault(m => m.ProcessUnit == process);
            ProcessTemplate.ProcessTemplateDetails.Remove(ppitem);
        }
        
            
        

        [RelayCommand]
        private async Task SaveTemplate()
        {
            foreach (var item in ProcessUnitGroups)
            {
                ProcessTemplate?.ProcessTemplateDetails?.Add(new ProcessTemplateDetailVm
                {
                    ProcessUnitId = item.ProcessUnitList.First(m => m.ProcessId == item.SelectedProcess?.ProcessId && m.UnitId == item.SelectedProUnit?.UnitId).Id,
                    Sequence = item.Sequence,
                    Weight = item.Weight
                });                
            }
            await _service.SaveProcessTemplate(ProcessTemplate);
            Growl.InfoGlobal("保存成功");
            WeakReferenceMessenger.Default.Send(string.Empty, MessageToken.CloseProcessTemplate);
        }

        [RelayCommand]
        private void WeightValueCheck()
        {

        }
    }
}
