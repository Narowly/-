using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using HandyControl.Data;
using Project.Services;
using Project.Services.DataServices;
using ProjectViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Project.Views;
using Autofac;
using Project.Views.UserControls;
using Project.Common;
using Project.Views.Windows;
using CommunityToolkit.Mvvm.Messaging;
using Autofac.Core;
using System.Collections.ObjectModel;
using HandyControl.Controls;
using System.Diagnostics.Contracts;
using MessageBox = HandyControl.Controls.MessageBox;
using System.Windows.Media.Animation;

namespace Project.ViewModel
{
    /// <summary>
    /// 立项
    /// </summary>
    public partial class ProjectApprovalViewModel : ObservableRecipient
    {
        private readonly IContainer? _container;
        private ProjectApprovalService _service;
        private StaffService _staffService;
        private DictService _dictService;
        private ProjectService _projectService;
        [ObservableProperty]
        private Guid? projectId;
        private Guid? _contractId;
        public Guid? ContractId
        {
            get => _contractId;
            set
            {
                SetProperty(ref _contractId, value);                
            }
        }
        private ProjectVm _project;
        
        public ProjectVm Project
        {
            get=>_project;
            set
            {
                SetProperty(ref _project, value);
            }
        }
        
        public List<StaffVm>? ManagerList = null!;
        [ObservableProperty]
        private ObservableCollection<StaffVm>? _bindingManagerStaffs;
        private StaffVm? _selectedSalesManager;
        public StaffVm? SelectedSalesManager
        {
            get => _selectedSalesManager;
            set
            {
                SetProperty(ref _selectedSalesManager, value);
            }
        }
        //主管
        private StaffVm? _selectedManager;
        public StaffVm? SelectedManager
        {
            get => _selectedManager;
            set => SetProperty(ref _selectedManager, value);
        }
        

        private ObservableCollection<CustomerContactVm>? _customerContacts;
        public ObservableCollection<CustomerContactVm>? CustomerContacts
        {
            get => _customerContacts;
            set => SetProperty(ref _customerContacts, value);
        }
        private CustomerContactVm _selectedCustomerContact;
        public CustomerContactVm SelectedCustomerContact
        {
            get => _selectedCustomerContact;
            set => SetProperty(ref _selectedCustomerContact, value);
        }
        private string _searchContactText;
        public string SearchContactText
        {
            get => _searchContactText;
            set => SetProperty(ref _searchContactText, value);
        }
        private string _searchManagerText;
        public string SearchManagerText
        {
            get => _searchManagerText;
            set 
            {
                SetProperty(ref _searchManagerText, value);
                if (!string.IsNullOrWhiteSpace(_searchManagerText))
                {
                    var list = SearchProjectManager(_searchManagerText);                    
                    BindingManagerStaffs = new ObservableCollection<StaffVm>(list);
                }
                else
                {
                    BindingManagerStaffs = null;
                }
            }
        }
        private string? _searchSalesText;
        public string? SearchSalesText
        {
            get => _searchSalesText;
            set
            {
                SetProperty(ref _searchSalesText, value);
            }
        }
        private ObservableCollection<StaffVm>? _salesList;
        public ObservableCollection<StaffVm>? SalesList
        {
            get => _salesList;
            set
            {
                SetProperty(ref _salesList, value);
            }
        }

        private List<DictDataVm> RegionList;
        private ObservableCollection<DictDataVm> _bindingRegionList;
        public ObservableCollection<DictDataVm> BindingRegionList
        {
            get => _bindingRegionList;
            set => SetProperty(ref _bindingRegionList, value);
        }
        private ObservableCollection<DictDataVm> _subBindingRegionList;
        public ObservableCollection<DictDataVm> SubBindingRegionList
        {
            get => _subBindingRegionList;
            set => SetProperty(ref _subBindingRegionList, value);
        }

        private DictDataVm _selectedRegion;
        public DictDataVm SelectedRegion
        {
            get => _selectedRegion;
            set => SetProperty(ref _selectedRegion, value);
        }
        private DictDataVm _selectedSubRegion;
        public DictDataVm SelectedSubRegion
        {
            get => _selectedSubRegion;
            set => SetProperty(ref _selectedSubRegion, value);
        }
        private bool _subRegionVisiable;
        public bool SubRegionVisiable
        {
            get => _subRegionVisiable;
            set => SetProperty(ref _subRegionVisiable, value);
        }
        private ContractVm? _contract;
        public ContractVm? Contract
        {
            get => _contract;
            set => SetProperty(ref _contract, value);
        }


        public ICommand OpenProcessUnitCmd { get; }
        public ICommand OpenProjectStaffCmd { get; }
        //public IAsyncRelayCommand SaveProjectCommand{ get; }
        
        //public RelayCommand<SelectionChangedEventArgs> CcComboxChangedCmd => new(CcComboxChanged);
        //public RelayCommand<SelectionChangedEventArgs> ScomboxChangedCmd => new(SComboxChanged);
        public RelayCommand<SelectionChangedEventArgs> RcomboxChangedCmd => new(RcomboBoxChanged);
        public RelayCommand<SelectionChangedEventArgs> SrComboxChangedCmd => new(SrComboboxChanged);

        private Task initTask;
        [ObservableProperty]
        private PopWindow? processUnitPopWindow;
        [ObservableProperty]
        private PopWindow? processStaffPopWindow;
        [ObservableProperty]
        private PopWindow? projectDevicePopWindow;
        public ProjectApprovalViewModel(ProjectApprovalService service, StaffService staffService, DictService dictService,ProjectService projectService, Guid? projectId=null, Guid? contractId=null)
        {
            ProjectId = projectId;
            ContractId = contractId;
            _service = service;
            _staffService = staffService;
            _dictService = dictService;
            _projectService = projectService;
            _container = (IContainer?)App.Current.Properties[MessageToken.AppContainer];
            initTask = Task.Run(() => LoadDataAsync());
            OpenProcessUnitCmd = new RelayCommand(OpenProcessUnit);
            OpenProjectStaffCmd = new RelayCommand(OpenProjectStaff);
            RegistMessager();
        }
        private void RegistMessager()
        {
            WeakReferenceMessenger.Default.Register(this, MessageToken.ReturnProjectProcess, (MessageHandler<ProjectApprovalViewModel, ObservableCollection<ProjectProcessVm>>)((r, m) =>
            {
                Project.ProjectProcesses = m;
                WeakReferenceMessenger.Default.Send(string.Empty, MessageToken.CloseProjectProcess);
            }));
            WeakReferenceMessenger.Default.Register(this, MessageToken.CloseProjectProcess, (MessageHandler<ProjectApprovalViewModel, string>)((r, m) =>
            {
                ProcessUnitPopWindow?.Close();
            }));
            WeakReferenceMessenger.Default.Register(this, MessageToken.ReturnProjectStaff, (MessageHandler<ProjectApprovalViewModel, ObservableCollection<StaffVm>>)((r, m) =>
            {
                Project.InProjectStaffs = m;
                WeakReferenceMessenger.Default.Send(string.Empty, MessageToken.CloseProjectStaff);
            }));
            WeakReferenceMessenger.Default.Register(this, MessageToken.CloseProjectStaff, (MessageHandler<ProjectApprovalViewModel, string>)((r, m) =>
            {
                ProcessStaffPopWindow?.Close();
            }));
            WeakReferenceMessenger.Default.Register(this, MessageToken.ReturnProjectDevice, (MessageHandler<ProjectApprovalViewModel, ObservableCollection<DeviceVm>>)((r, m) =>
            {
                Project.InProjectDevice = m;
                WeakReferenceMessenger.Default.Send(string.Empty, MessageToken.CloseProjectDevice);
            }));
            WeakReferenceMessenger.Default.Register(this, MessageToken.CloseProjectDevice, (MessageHandler<ProjectApprovalViewModel, string>)((r, m) =>
            {
                ProjectDevicePopWindow?.Close();
            }));
            WeakReferenceMessenger.Default.Register(this, MessageToken.ReturnProjectPaymentTerms, (MessageHandler<ProjectApprovalViewModel, ObservableCollection<ProjectPaymentTermVm>>)((r, m) =>
            {
                Project.ProjectPyamentTerms = m;
                WeakReferenceMessenger.Default.Send(string.Empty, MessageToken.CloseProjectPaymentTerms);
            }));
            WeakReferenceMessenger.Default.Register(this, MessageToken.CloseProjectPaymentTerms, (MessageHandler<ProjectApprovalViewModel, string>)((r, m) =>
            {
                paymentTermWindow?.Close();
            }));
        }
        private async Task LoadDataAsync()
        {
            await Task.WhenAll(LoadRegion(), LoadManagerList(), 
                ProjectId == null ? LoadContractAsync() : LoadProjectAsync(), LoadSalesManagerList())
                .ContinueWith(t => GetCustomerContactList()
                .ContinueWith(t => InitResponseData()));
        }

        private async Task LoadManagerList()
        {
            ManagerList = await _staffService.GetStaffListByDuty(ApiSettings.ProjectManagerDuty);
            BindingManagerStaffs = new ObservableCollection<StaffVm>(ManagerList);
        }
        private void InitResponseData()
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                if (ProjectId == null)
                {
                    Project.Address = Contract.Customer.CustAddress;
                    Project.ProjectName = Contract.Customer.CustomerName;
                    if (Contract != null && Contract.SalesManager != null && SalesList != null)
                        Project.SalesManager = SalesList.FirstOrDefault(m => m.StaffId == Contract.SalesManager.StaffId);
                }
                else
                {
                    if (SalesList != null)
                    {
                        if (Project.SalesManager != null)
                        {
                            Project.SalesManager = SalesList.FirstOrDefault(m => m.StaffId == Project.SalesManager.StaffId);
                        }
                        else
                        {
                            Project.SalesManager = SalesList.FirstOrDefault(m => m.StaffId == Project.Contract.SalesManagerId);
                        }
                    }
                    if (ManagerList != null)
                    {
                        if (Project.ProjectManager != null)
                        {
                            Project.ProjectManager = BindingManagerStaffs.FirstOrDefault(m => m.StaffId == Project.ProjectManager.StaffId);
                        }
                    }
                    if (BindingRegionList != null)
                    {
                        var projectRegion = RegionList.FirstOrDefault(m => m.DictCode == Project.RegionId);
                        Project.Region = projectRegion;
                        if (projectRegion.ParentCode != null)
                        {
                            SelectedRegion = BindingRegionList.FirstOrDefault(m => m.DictCode == projectRegion.ParentCode);

                            SubBindingRegionList = new ObservableCollection<DictDataVm>(RegionList.Where(m => m.ParentCode == SelectedRegion.DictCode));
                            SelectedSubRegion = SubBindingRegionList.FirstOrDefault(m => m.DictCode == projectRegion.DictCode);
                            SubRegionVisiable = true;
                        }
                        else
                        {
                            SelectedRegion = BindingRegionList.FirstOrDefault(m => m.DictCode == projectRegion.DictCode);
                            SubRegionVisiable = false;
                        }
                    }
                }
                if (CustomerContacts != null && Project.Contract.CustomerContact != null)
                    Project.Contract.CustomerContact = CustomerContacts.FirstOrDefault(m => m.CustomerContactId == Project.Contract.CustomerContact.CustomerContactId);

            });
            
        }

        private async Task LoadProjectAsync()
        {
            if (ProjectId == null) return;
            await Application.Current.Dispatcher.Invoke(async () =>
            {
                Project = await _projectService.GetProjectById(ProjectId.Value);
            });
        }
        //private void SComboxChanged(SelectionChangedEventArgs? e)
        //{
        //    if (e == null) return;
        //    System.Windows.Controls.ComboBox combox = e.Source as System.Windows.Controls.ComboBox;
        //    StaffVm selectedItem = combox?.SelectedItem as StaffVm;
        //    SelectedManager = selectedItem;
        //}

        //private void CcComboxChanged(SelectionChangedEventArgs? e)
        //{
        //    if (e == null) return;
        //    System.Windows.Controls.ComboBox comboBox = e.Source as System.Windows.Controls.ComboBox;
        //    CustomerContactVm selectedItem = comboBox?.SelectedItem as CustomerContactVm;
        //    SelectedCustomerContact = selectedItem;            
        //}

        private void RcomboBoxChanged(SelectionChangedEventArgs? e)
        {
            if (e == null) return;
            System.Windows.Controls.ComboBox comboBox = e.Source as System.Windows.Controls.ComboBox;
            DictDataVm selectedItem = comboBox?.SelectedItem as DictDataVm;
            SelectedRegion = selectedItem;
            if (selectedItem != null)
            {
                SubBindingRegionList = new ObservableCollection<DictDataVm>(RegionList.Where(m => m.ParentCode == selectedItem.DictCode));
                if (SubBindingRegionList == null || SubBindingRegionList.Count == 0) SubRegionVisiable = false;
                else SubRegionVisiable = true;
            }
        }
        private void SrComboboxChanged(SelectionChangedEventArgs? e)
        {
            if (e == null) return;
            System.Windows.Controls.ComboBox comboBox = e.Source as System.Windows.Controls.ComboBox;
            DictDataVm selectedItem = comboBox?.SelectedItem as DictDataVm;
            SelectedSubRegion = selectedItem;
        }
        private void OpenProcessUnit()
        {
            ProcessUnitPopWindow = new PopWindow();
            var projectId = new ResolvedParameter(
                (pi, ctx) => pi.Name == "projectId",
                (pi, ctx) => ProjectId);
            var projectProcessList = new ResolvedParameter(
                (pi, ctx) => pi.Name == "pList",
                (pi, ctx) => Project.ProjectProcesses);
            var vm = _container.Resolve<ProcessViewModel>(projectId, projectProcessList);
            var view = new ProjectProcessView(vm);
            ProcessUnitPopWindow.controlHost.Content = view;
            ProcessUnitPopWindow.ShowDialog();            
        }

        private void OpenProjectStaff()
        {
            ProcessStaffPopWindow = new PopWindow();
            var projectId = new ResolvedParameter(
                (pi, ctx) => pi.Name == "projectId",
                (pi, ctx) => ProjectId);
            var projectStaffList = new ResolvedParameter(
                (pi,ctx)=>pi.Name== "inProjectStaffs",
                (pi,ctx)=>Project.InProjectStaffs);
            var vm = _container.Resolve<ProjectStaffSetViewModel>(projectId, projectStaffList);
            var view = new ProjectStaffUc();
            view.DataContext = vm;
            ProcessStaffPopWindow.controlHost.Content = view;
            ProcessStaffPopWindow.ShowDialog();
            
        }
        private PopWindow paymentTermWindow;
        [RelayCommand]
        private void OpenProjectPaymentTerm()
        {
            paymentTermWindow = new PopWindow();
            var projectId = new ResolvedParameter(
                (pi, ctx) => pi.Name == "projectId",
                (pi, ctx) => ProjectId);
            var projectPyamentTerms = new ResolvedParameter(
                (pi, ctx) => pi.Name == "projectPyamentTerms",
                (pi, ctx) => Project.ProjectPyamentTerms);
            if (_container == null) return;
            var vm = _container.Resolve<ProjectPaymentTermViewModel>(projectId, projectPyamentTerms);
            var view = new ProjectPaymentTermView();
            view.DataContext = vm;
            paymentTermWindow.controlHost.Content = view;
            paymentTermWindow.ShowDialog();
        }
        [RelayCommand]
        private void OpenProjectDevice()
        {
            ProjectDevicePopWindow = new PopWindow();
            var projectId = new ResolvedParameter(
                (pi,ctx)=>pi.Name =="projectId",
                (pi, ctx) => ProjectId);
            var projectDeviceList = new ResolvedParameter(
                (pi, ctx) => pi.Name == "inProjectDevices",
                (pi, ctx) => Project.InProjectDevice);
            var vm = _container.Resolve<ProjectDeviceViewModel>(projectId, projectDeviceList);
            var view = new ProjectDeviceUc();
            view.DataContext = vm;
            ProjectDevicePopWindow.controlHost.Content = view;
            ProjectDevicePopWindow.ShowDialog();
            
        }
        private List<StaffVm> SearchProjectManager(string search)
        {
            return ManagerList?.Where(m => m.StaffName.Contains(search)).ToList();
        }

        public async Task LoadContractAsync()
        {
            if (ContractId == null) return;

            await Application.Current.Dispatcher.Invoke(async () =>
            {
                Contract = await _service.GetContractById(ContractId.Value);
                Project = new ProjectVm();
                Project.Contract = Contract;
            });
        }

        private async Task GetCustomerContactList()
        {
            if (Project.Contract.CustomerId == null) return;
            await Application.Current.Dispatcher.Invoke(async () =>
            {
                var response = await _projectService.GetCustomerContactList(Project.Contract.CustomerId.Value);
                CustomerContacts = new ObservableCollection<CustomerContactVm>(response);
            });            
        }

        private async Task LoadRegion()
        {
            await Application.Current.Dispatcher.Invoke(async () =>
            {
                RegionList = await _dictService.GetDictDataByTypeName("Region");
                BindingRegionList = new ObservableCollection<DictDataVm>(RegionList.Where(m => m.ParentCode == null));
            });
        }
        [RelayCommand]
        private async Task SaveProject()
        {
            var result = await Save();
            if (result != null)
            {
                MessageBox.Success("保存成功");
                WeakReferenceMessenger.Default.Send(string.Empty, MessageToken.CloseProjectApproval);
            }
            else
            {
                MessageBox.Warning("保存失败");
            }
        }
        private async Task<ProjectVm?> Save()
        {
            Check();
            if (SelectedSubRegion != null)
            {
                Project.RegionId = SelectedSubRegion.DictCode;
            }
            else if (SelectedRegion != null)
            {
                Project.RegionId = SelectedRegion.DictCode;
            }
            Project.ContractId = Project.Contract.ContractId;
            return await _service.SaveProject(Project);
        }
        [RelayCommand]
        private async Task SaveProjectAndOpenSettings()
        {
            Check();
            var result = await Save();
            if (result != null)
            {
                MessageBox.Success("保存成功");
                WeakReferenceMessenger.Default.Send(result, MessageToken.CloseProjectApprovalAndOpenSettings);
            }
            else
            {
                MessageBox.Warning("保存失败");
            }
        }
        private bool Check()
        {
            var result = false;
            if (Project.Contract.CustomerContact == null)
            {
                MessageBox.Warning("联系人不能为空");
                return result;
            }
            if (Project.SalesManager == null)
            {
                MessageBox.Warning("销售主管不能为空");
                return result;
            }
            if (Project.ProjectManager == null)
            {
                MessageBox.Warning("项目主管不能为空");
                return result;
            }
            result = true;
            return result;
        }
        private async Task LoadSalesManagerList()
        {
            var list = await _staffService.GetStaffListByDuty(ApiSettings.SalesManagerDuty);
            SalesList = new ObservableCollection<StaffVm>(list);
        }
    }
}


