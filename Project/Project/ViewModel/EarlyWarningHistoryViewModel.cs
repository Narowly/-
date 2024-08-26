using CommunityToolkit.Mvvm.ComponentModel;
using Project.Services.DataServices;
using ProjectViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Project.Common;
using CommunityToolkit.Mvvm.Input;
using Project.Views.Windows;
using Autofac;
using Autofac.Core;
using Project.Views.UserControls;

namespace Project.ViewModel
{
    public partial class EarlyWarningHistoryViewModel : ObservableObject
    {
        private readonly IContainer? _container = (IContainer?)App.Current.Properties[MessageToken.AppContainer];
        private readonly EarlyWarningService _earlyWarningService;
        private readonly StaffService _staffService;
        private DictService _dictService;
        private ProjectReqs? projectReqs;
        
        public ProjectReqs? ProjectReqs
        {
            get => projectReqs;
            set => SetProperty(ref projectReqs, value);
        } 
        private PaginatedList<EarlyWarningHistoryVm>? _paginatedEarlyWarnings;
        public PaginatedList<EarlyWarningHistoryVm>? PaginatedEarlyWarnings
        {
            get => _paginatedEarlyWarnings;
            set => SetProperty(ref _paginatedEarlyWarnings, value);
        }
        private ObservableCollection<EarlyWarningHistoryVm>? _earlyWarnings;
        public ObservableCollection<EarlyWarningHistoryVm>? EarlyWarnings
        {
            get => _earlyWarnings;
            set => SetProperty(ref _earlyWarnings, value);
        }
        private List<StaffVm>? ManagerList;
        public ObservableCollection<StaffVm>? _bindingManagerList;
        public ObservableCollection<StaffVm>? BindingManagerList
        {
            get => _bindingManagerList;
            set => SetProperty(ref _bindingManagerList, value);
        }

        private string? _searchManagerText;
        public string? SearchManagerText
        {
            get => _searchManagerText;
            set
            {
                SetProperty(ref _searchManagerText, value);
                if (_searchManagerText != null)
                {
                    var list = ManagerList?.Where(m => m.StaffName.Contains(_searchManagerText)).ToList();
                    if (list != null) BindingManagerList = new ObservableCollection<StaffVm>(list);
                }
            }
        }

        private ObservableCollection<DictDataVm>? warningTypeList;
        public ObservableCollection<DictDataVm>? WarningTypeList
        {
            get => warningTypeList;
            set => SetProperty(ref  warningTypeList, value);
        }
        private DictDataVm? selectedWarningType;
        public DictDataVm? SelectedWarningType
        {
            get => selectedWarningType;
            set => SetProperty(ref  selectedWarningType, value);
        }
        private EarlyWarningHistoryVm? selectedHistory;
        public EarlyWarningHistoryVm? SelectedHistory
        {
            get => selectedHistory;
            set => SetProperty(ref selectedHistory, value);
        }
        public EarlyWarningHistoryViewModel(EarlyWarningService earlyWarningService, StaffService staffService, DictService dictService)
        {
            _earlyWarningService = earlyWarningService;
            _staffService = staffService;
            _dictService = dictService;
            ProjectReqs = new ProjectReqs
            {
                Pagination = new PaginationParams
                {
                    Page = 1,
                    PageSize = 10
                }
            };
            
            Task.Run(LoadDataAsync);
        }

        private async Task LoadDataAsync()
        {
            await LoadHistory();
            await LoadManagerList();
            await LoadWarningType();
        }
        [RelayCommand]
        private async Task LoadHistory()
        {
            if (SelectedWarningType != null) ProjectReqs.Status = SelectedWarningType.DictCode;
            PaginatedEarlyWarnings = await _earlyWarningService.PaginatedWarningHistory(ProjectReqs);
            EarlyWarnings = new ObservableCollection<EarlyWarningHistoryVm>(PaginatedEarlyWarnings.Items);
        }
        private async Task LoadManagerList()
        {
            await Application.Current.Dispatcher.Invoke(async () =>
            {
                ManagerList = await _staffService.GetStaffListByDuty(ApiSettings.ProjectManagerDuty);
            });
        }

        private async Task LoadWarningType()
        {
            var typeList = await _dictService.GetDictDataByTypeName("EarlyWarningType");
            WarningTypeList = new ObservableCollection<DictDataVm>(typeList);
        }
        //private PopWindow handlingWarningWindow;
        public PopWindow? HandlingWarningWindow;
        //{
        //    get => handlingWarningWindow;
        //    set => SetProperty(ref handlingWarningWindow, value);
        //}
        [RelayCommand]
        private void OpenHandlingWarning()
        {
            if (SelectedHistory == null) return;
            HandlingWarningWindow = new PopWindow();
            var viewParameter = new ResolvedParameter(
                (pi, ctx) => pi.Name == "id",
                (pi, ctx) => SelectedHistory.Id
                );
            if (_container == null) return;
            var viewModel = _container.Resolve<HandlingWarningViewModel>(viewParameter);
            var handlingWarning = _container.Resolve<HandlingWarning>();
            handlingWarning.DataContext = viewModel;
            HandlingWarningWindow.controlHost.Content = handlingWarning;
            HandlingWarningWindow.Title = "预警处理";
            HandlingWarningWindow.ShowDialog();
        }
    }
}
