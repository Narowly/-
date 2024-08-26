using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using Project.Common;
using Project.Services;
using ProjectViewModels;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Autofac;
using Project.Views.UserControls;
using Project.Views.Windows;
using Project.Services.DataServices;
using Autofac.Core;

namespace Project.ViewModel
{
    public partial class ContractViewModel : ObservableRecipient
    {
        private readonly IContainer? _container;
        private readonly ProjectService _projectService;
        private readonly StaffService _staffService;


        private PaginatedModel? _paginated;
        public PaginatedModel? Paginated
        {
            get => _paginated;
            set => SetProperty(ref _paginated, value);
        }
        private ObservableCollection<ContractShowModel>? _contracts;
        public ObservableCollection<ContractShowModel>? Contracts
        {
            get => _contracts;
            set => SetProperty(ref _contracts, value);
        }

        private ContractShowModel? _selectedItem;
        public ContractShowModel? SelectedItem
        {
            get { return _selectedItem; }
            set => SetProperty(ref _selectedItem, value);
        }

        private int _currentIndex = 1;
        public int CurrentIndex
        {
            get { return _currentIndex; }
            set
            {
                SetProperty(ref _currentIndex, value);
                if (Req.Pagination != null)
                    Req.Pagination.Page = _currentIndex;
            }
        }

        private ProjectReqs _req = null!;
        public ProjectReqs Req
        {
            get => _req;
            set => SetProperty(ref _req, value);
        }

        private List<ContractVm> ContractNameList = null!;
        private List<ProjectAutoCompleteModel> ProjectNameList = null!;
        private string? _searchProjectNameText;
        public string? SearchProjectNameText
        {
            get => _searchProjectNameText;
            set
            {
                SetProperty(ref _searchProjectNameText, value);
                if (!string.IsNullOrWhiteSpace(value))
                {
                    List<ProjectAutoCompleteModel> sourceList;
                    if (SelectedStatus != null && SelectedStatus.Value == 1)
                    {
                        sourceList = ProjectNameList.Where(m => m.Name.Contains(value) || m.Number.Contains(value)).ToList();
                    }
                    else
                    {
                        sourceList = ContractNameList.Where(m => m.ContractName.Contains(value) || m.ContractNumber.Contains(value)).Select(m => new ProjectAutoCompleteModel { Id = m.ContractId, Name = string.Format("{0}|{1}", m.ContractNumber, m.ContractName), Number = m.ContractNumber }).ToList();
                    }
                    ProjectNamesSource = new ObservableCollection<ProjectAutoCompleteModel>(sourceList);
                }
                else
                {
                    ProjectNamesSource = null;
                }                
            }
        }
        private ObservableCollection<ProjectAutoCompleteModel>? _projectNamesSource;
        public ObservableCollection<ProjectAutoCompleteModel>? ProjectNamesSource
        {
            get => _projectNamesSource;
            set => SetProperty(ref _projectNamesSource, value);
        }
        
        private StatusComboBoxModel? _selectedStatus;
        public StatusComboBoxModel? SelectedStatus
        {
            get => _selectedStatus;
            set => SetProperty(ref _selectedStatus, value);
        }

        private ObservableCollection<StatusComboBoxModel> bindingStatus = null!;
        public ObservableCollection<StatusComboBoxModel> BindingStatus
        {
            get => bindingStatus;
            set => SetProperty(ref bindingStatus, value);
        }
        [ObservableProperty]
        public List<StaffVm>? managerList;
        [ObservableProperty]
        public ObservableCollection<StaffVm>? bindingManagerList;
        [ObservableProperty]
        public StaffVm? selectedManager;
        private string? _searchManagerText;
        public string? SearchManagerText
        {
            get => _searchManagerText;
            set
            {
                SetProperty(ref _searchManagerText, value);
                if (!string.IsNullOrWhiteSpace(_searchManagerText))
                {
                    var list = ManagerList?.Where(m => m.StaffName.Contains(_searchManagerText)).ToList();
                    BindingManagerList = new ObservableCollection<StaffVm>(list);
                }
                else
                {
                    BindingManagerList = null;
                }      
            }
        }

        private async Task LoadDataAsync()
        {
            await SearchContracts();
            await LoadContractName();
            await LoadProjectNames();
            await LoadManagerList();
        }

        private PopWindow? approvalWindow;
        public PopWindow? ApprovalWindow
        {
            get => approvalWindow;
            set => SetProperty(ref approvalWindow, value);
        }

        public ContractViewModel(ProjectService service, StaffService staffService)
        {
            _projectService = service;
            _staffService = staffService;
            _container = (IContainer?)App.Current.Properties[MessageToken.AppContainer];
            Req = new ProjectReqs { Pagination = new PaginationParams { Page = 1, PageSize = 10 } };
            BindingStatus = new ObservableCollection<StatusComboBoxModel>
            {
                new StatusComboBoxModel { Name = "未立项",Value=0 }, new StatusComboBoxModel { Name = "已立项",Value=1 }
            };
            Task.Run(LoadDataAsync);
            RegistMessages();            
        }

        private async Task LoadManagerList()
        {
            ManagerList = await _staffService.GetStaffListByDuty(ApiSettings.ProjectManagerDuty);            
        }
        private async Task LoadProjectNames()
        {
            ProjectNameList = await _projectService.LoadProjectNames();
        }

        [RelayCommand]
        private async Task Search()
        {
            Req.Content = SearchProjectNameText;
            if (SelectedStatus == null || SelectedStatus.Value == 0)
            {
                await SearchContracts();
            }
            else
            {
                await SearchProject();
            }
        }

        private async Task LoadContractName()
        {
            ContractNameList = await _projectService.GetContractNames();
        }

        [RelayCommand]
        private void OpenProjectApproval()
        {
            if (SelectedItem == null) return;
            ApprovalWindow = new PopWindow();
            var status = SelectedStatus;
            var parameterName = string.Empty;
            var parameterValue = Guid.Empty;
            if (SelectedItem.ProjectId != null)
            {
                parameterName = "projectId";
                parameterValue = SelectedItem.ProjectId.Value;
            }
            else
            {
                parameterName = "contractId";
                parameterValue = SelectedItem.ContractId.Value;
            }
            var viewParameter = new ResolvedParameter(
                (pi, ctx) => pi.Name == parameterName,
                (pi, ctx) => parameterValue);
            var paViewModel = _container.Resolve<ProjectApprovalViewModel>(viewParameter);
            var paWindow = new ProjectApproval(paViewModel);
            ApprovalWindow.controlHost.Content = paWindow;
            ApprovalWindow.Title = "立项";
            ApprovalWindow.ShowDialog();
        }
        private PopWindow? _settingsUcWindow;
        public PopWindow? SettingsUcWindow
        {
            get => _settingsUcWindow;
            set => SetProperty(ref _settingsUcWindow, value);
        }
        private void OpenSettingsUc()
        {
            if (SelectedItem == null) return;
            SettingsUcWindow = new PopWindow();
            var projectId = new ResolvedParameter(
                (pi, ctx) => pi.Name == "projectId",
                (pi, ctx) => SelectedItem.ProjectId);
            var vm = _container.Resolve<ProjectSettingsViewModel>(projectId);
            var settingsUc = new ProjectSettingsUc(vm);
            SettingsUcWindow.controlHost.Content = settingsUc;
            SettingsUcWindow.Title = SelectedItem.ProjectName;
            SettingsUcWindow.Show();
        }


        private void RegistMessages()
        {
            WeakReferenceMessenger.Default.Register(this, MessageToken.CloseProjectApproval, (MessageHandler<object, string>)(async (obj, m) =>
            {
                ApprovalWindow?.Close();
                await Search();
            }));
            WeakReferenceMessenger.Default.Register(this, MessageToken.CloseProjectApprovalAndOpenSettings, (MessageHandler<object, ProjectVm>)(async (obj, m) =>
            {
                ApprovalWindow?.Close();
             
                if (SelectedItem == null) return;
                SelectedItem.ProjectName = m.ProjectName;
                SelectedItem.ProjectId = m.ProjectId;
                OpenSettingsUc();
                
                await Search();
            }));
        }
        [RelayCommand]
        private async Task PageSearch()
        {
            await Application.Current.Dispatcher.Invoke(async () =>
            {
                await Search();
            });
        }
        [RelayCommand]
        private async Task SearchContracts()
        {
            await Application.Current.Dispatcher.Invoke(async () =>
            {
                var response = await _projectService.GetContractsAsync(Req);
                Contracts = new ObservableCollection<ContractShowModel>();
                Paginated = new PaginatedModel
                {
                    PageIndex = response.PageIndex,
                    PageSize = response.PageSize,
                    TotalCount = response.TotalCount,
                    TotalPages = response.TotalPages
                };
                if (response.Items != null)
                {
                    foreach (var item in response.Items)
                    {
                        Contracts.Add(new ContractShowModel
                        {
                            ContractId = item.ContractId,
                            ContractName = item.ContractName,
                            ContractNumber = item.ContractNumber
                        });
                    }
                }
            });   
        }

        private async Task SearchProject()
        {
            await Application.Current.Dispatcher.Invoke(async () =>
            {
                if (SelectedManager != null) Req.ProjectManagerId = SelectedManager.StaffId;
                var response = await _projectService.GetProjectsAsync(Req);
                Contracts = new ObservableCollection<ContractShowModel>();
                Paginated = new PaginatedModel
                {
                    PageIndex = response.PageIndex,
                    PageSize = response.PageSize,
                    TotalCount = response.TotalCount,
                    TotalPages = response.TotalPages
                };
                if (response.Items != null)
                {
                    foreach (var item in response.Items)
                    {
                        Contracts.Add(new ContractShowModel
                        {
                            ProjectId = item.ProjectId,
                            ContractId = item.ContractId,
                            ContractName = item.ProjectName,
                            ContractNumber = item.Contract.ContractNumber,
                            CreateTime = item.CreateTime
                        });
                    }
                }
            });
        }

        
    }

    public class PaginatedModel : ObservableObject
    {
        private int pageIndex;
        public int PageIndex
        {
            get => pageIndex;
            set => SetProperty(ref pageIndex, value);
        }
        private int totalPages;
        public int TotalPages
        {
            get => totalPages;
            set => SetProperty(ref totalPages, value);
        }
        private int totalCount;
        public int TotalCount
        {
            get => totalCount;
            set => SetProperty(ref totalCount, value);
        }
        private int pageSize;
        public int PageSize
        {
            get => pageSize;
            set => SetProperty(ref pageSize, value);
        }
    }

    public class ContractShowModel : ObservableObject
    {
        private Guid? _projectId;
        public Guid? ProjectId
        {
            get => _projectId;
            set => SetProperty(ref _projectId, value);
        }
        private string? _projectName;
        public string? ProjectName
        {
            get => _projectName;
            set => SetProperty(ref _projectName, value);
        }
        private Guid? _contractId;
        public Guid? ContractId
        {
            get => _contractId;
            set => SetProperty(ref _contractId, value);
        }

        private string _contractNumber = null!;
        public string ContractNumber
        {
            get => _contractNumber;
            set => SetProperty(ref _contractNumber, value);
        }

        private string _contractName = null!;
        public string ContractName
        {
            get => _contractName;
            set => SetProperty(ref _contractName, value);
        }
        private DateTime? _createTime;
        public DateTime? CreateTime
        {
            get => _createTime;
            set => SetProperty(ref _createTime, value);
        }
    }
    
    public class StatusComboBoxModel
    {
        public string Name { get; set; } = null!;
        public int Value { get; set; }
    }
}
