using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using HandyControl.Controls;
using Project.Services.DataServices;
using ProjectViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;

namespace Project.ViewModel
{
    public partial class HandlingWarningViewModel : ObservableObject
    {
        private readonly int historyId;
        private readonly EarlyWarningService _warningService;
        private readonly DictService _dictService;
        private EarlyWarningHistoryVm history = null!;
        public EarlyWarningHistoryVm History
        {
            get => history;
            set => SetProperty(ref  history, value);
        }
        private ObservableCollection<DictDataVm> handlingStatusList = null!;
        public ObservableCollection<DictDataVm> HandlingStatusList
        {
            get => handlingStatusList;
            set => SetProperty(ref  handlingStatusList, value);
        }
        private DictDataVm? selectedHandlingStatus;
        public DictDataVm? SelectedHandlingStatus
        {
            get => selectedHandlingStatus;
            set => SetProperty(ref  selectedHandlingStatus, value);
        }
        public HandlingWarningViewModel(int id, EarlyWarningService service, DictService dictService)
        {
            historyId = id;
            _warningService = service;
            _dictService = dictService;
            Task.Run(LoadDataAsync);
        }
        private async Task LoadDataAsync()
        {
            await Task.WhenAll(GetWarningHistory(), LoadHandlingStatus()).ContinueWith(t =>
            {
                SelectedHandlingStatus = HandlingStatusList.FirstOrDefault(m => m.DictCode == History.Status);
            });
        }
        private async Task GetWarningHistory()
        {
            History = await _warningService.GetEarlyWarningHistoryById(historyId);
        }
        private async Task LoadHandlingStatus()
        {
            var statusList = await _dictService.GetDictDataByTypeName("EarlyWarningHandlingStatus");
            HandlingStatusList = new ObservableCollection<DictDataVm>(statusList);
        }
        [RelayCommand]
        private async Task SaveEarlyWarningHistory()
        {
            if (SelectedHandlingStatus != null) History.Status = SelectedHandlingStatus.DictCode;
            var result = await _warningService.SaveEarlyWarningHistory(History);
            if (result)
            {
                Growl.Success("保存成功");
            }
        }
    }
}
