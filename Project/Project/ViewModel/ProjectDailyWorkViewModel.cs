using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Project.Common;
using Project.Services;
using Project.Services.DataServices;
using ProjectViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using Project.Views.UserControls;
using Project.Views.Windows;
using Autofac.Core;
using System.Windows;
using MessageBox = HandyControl.Controls.MessageBox;
using ClosedXML.Excel;
using Microsoft.Win32;
using HandyControl.Controls;

namespace Project.ViewModel
{
    public partial class ProjectDailyWorkViewModel : ObservableObject
    {
        private IContainer? _container = App.Current.Properties[MessageToken.AppContainer] as IContainer;
        private readonly ProjectDailyWorkService _dailyWorkService;
        private readonly ProjectService _projectService;
        private readonly StaffService _staffService;
        private ProjectWithStaffReq _req = null!;
        public ProjectWithStaffReq Req
        {
            get => _req;
            set => SetProperty(ref _req, value);
        }
        private PaginatedList<ProjectDailyWorkVm> paginatedList = null!;
        public PaginatedList<ProjectDailyWorkVm> PaginatedList
        {
            get => paginatedList;
            set => SetProperty(ref paginatedList, value);
        }
        private ProjectDailyWorkVm? _selectedDailyWork;
        public ProjectDailyWorkVm? SelectedDailyWork
        {
            get => _selectedDailyWork;
            set => SetProperty(ref _selectedDailyWork, value);
        }
        private List<ProjectAutoCompleteModel> ProjectNameList = null!;
        private ObservableCollection<ProjectAutoCompleteModel>? _projectNamesSource;
        public ObservableCollection<ProjectAutoCompleteModel>? ProjectNamesSource
        {
            get => _projectNamesSource;
            set => SetProperty(ref _projectNamesSource, value);
        }
        private string? _searchProjectNameText;
        public string? SearchProjectNameText
        {
            get => _searchProjectNameText;
            set
            {
                SetProperty(ref _searchProjectNameText, value);
                if (!string.IsNullOrWhiteSpace(_searchProjectNameText))
                {
                    var list = ProjectNameList?.Where(m => m.Name.Contains(_searchProjectNameText) || m.Number.Contains(_searchProjectNameText)).ToList();
                    if (list != null) ProjectNamesSource = new ObservableCollection<ProjectAutoCompleteModel>(list);
                }
                else
                {
                    ProjectNamesSource = null;
                }
                //if (!string.IsNullOrWhiteSpace(value))
                //{
                //    if (value.Equals(_searchProjectNameText)) return;
                //    if (SelectedProject != null) return;
                //    List<ProjectAutoCompleteModel> sourceList;
                //    sourceList = ProjectNameList.Where(m => m.ProjectName.Contains(value) || m.Contract.ContractNumber.Contains(value)).Select(m => new ProjectAutoCompleteModel { Id = m.ProjectId, Name = string.Format("{0}|{1}", m.Contract.ContractNumber, m.ProjectName), Number = m.Contract.ContractNumber }).ToList();
                //    ProjectNamesSource = new ObservableCollection<ProjectAutoCompleteModel>(sourceList);
                //}                
                //else
                //{
                //    ProjectNamesSource = null;
                //}
                //SetProperty(ref _searchProjectNameText, value);
            }
        }
        private ProjectAutoCompleteModel? _selectedProject;
        public ProjectAutoCompleteModel? SelectedProject
        {
            get => _selectedProject;
            set => SetProperty(ref _selectedProject, value);
        }
        private List<StaffVm>? ManagerList;
        [ObservableProperty]
        private ObservableCollection<StaffVm>? bindingManagerList;
        [ObservableProperty]
        private StaffVm? selectedManager;
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
                    if (list != null) BindingManagerList = new ObservableCollection<StaffVm>(list);
                }
                else
                {
                    BindingManagerList = null;
                }
            }
        }
        private List<StaffVm>? StaffList;
        [ObservableProperty]
        private ObservableCollection<StaffVm>? bindingStaffList;
        [ObservableProperty]
        private StaffVm? selectedStaff;
        private string? _searchStaffText;
        public string? SearchStaffText
        {
            get => _searchStaffText;
            set
            {
                SetProperty(ref _searchStaffText, value);
                if (!string.IsNullOrWhiteSpace(_searchStaffText))
                {
                    var list = StaffList?.Where(m => m.StaffName.Contains(_searchStaffText)).ToList();
                    if (list != null) BindingStaffList = new ObservableCollection<StaffVm>(list);
                }
                else
                {
                    BindingStaffList = null;
                }
            }
        }
        public ProjectDailyWorkViewModel(ProjectDailyWorkService dailyWorkService, ProjectService projectService, StaffService staffService)
        {
            _dailyWorkService = dailyWorkService;
            _projectService = projectService;
            _staffService = staffService;
            Task.Run(LoadDataAsync);
        }

        private async Task LoadDataAsync()
        {
            Req = new ProjectWithStaffReq {  };
            await LoadProjectDailyWork();
            await LoadProjectNames();
            await LoadManagerList();
            await LoadStaffList();
        }
        private async Task LoadProjectNames()
        {
            ProjectNameList = await _projectService.LoadProjectNames();
        }
        private async Task LoadManagerList()
        {
            ManagerList = await _staffService.GetStaffListByDuty(ApiSettings.ProjectManagerDuty);
        }
        private async Task LoadStaffList()
        {
            StaffList = await _staffService.GetStaffList();
        }
        
        [RelayCommand]
        private async Task LoadProjectDailyWork()
        {
            if (SelectedProject != null) Req.ProjectId = SelectedProject.Id;
            else Req.ProjectId = null;
            if (SelectedManager != null) Req.ProjectManagerId = SelectedManager.StaffId;
            else Req.ProjectManagerId = null;
            if (SelectedStaff != null) Req.Staff = SelectedStaff.StaffId;
            else Req.Staff = null;
            //if (PaginatedList != null && Req != null && Req.Pagination != null)
            //    Req.Pagination.Page = PaginatedList.PageIndex;
            PaginatedList = await _dailyWorkService.PaginatedSearchProjectDailyWork(Req);
        }
        private PopWindow? dailyWorkWindow; 
        [RelayCommand]
        private void OpenAddWindow(object type)
        {            
            Guid? selectedDailyWorkId = null;
            if (Convert.ToInt32(type) == 1)
            {
                if (SelectedDailyWork == null)
                {
                    return;
                }
                else
                {
                    selectedDailyWorkId = SelectedDailyWork.Id;                    
                }
            }
            ResolvedParameter dailyWorkId = new ResolvedParameter(
                                (pi, ctx) => pi.Name == "id",
                                (pi, ctx) => selectedDailyWorkId);
            if (_container == null) return;
            var vm = _container.Resolve<AddProjectDailyWorkViewModel>(dailyWorkId);            
            var view = _container.Resolve<AddProjectDailyWork>();
            view.DataContext = vm;
            dailyWorkWindow = new PopWindow();
            dailyWorkWindow.controlHost.Content = view;
            dailyWorkWindow.Title = "工作量补录";
            dailyWorkWindow.Width = 600;
            dailyWorkWindow.Height = 600;
            dailyWorkWindow.Closed += addWindowClosed;
            dailyWorkWindow.ShowDialog();
        }
        private PopWindow? batchWindow;
        [RelayCommand]
        private void OpenBatchWindow()
        {
            if (_container == null) return;
            var vm = _container.Resolve<AddBatchProjectDailyWorkViewModel>();
            var view = _container.Resolve<AddBatchProjectDailyWorkView>();
            view.DataContext = vm;
            batchWindow = new PopWindow { Title = "批量补录" };
            batchWindow.controlHost.Content = view;
            batchWindow.Closed += addWindowClosed;
            batchWindow.ShowDialog();
        }
        private async void addWindowClosed(object? sender, EventArgs e)
        {
            if (sender is PopWindow popWindow)
            {
                popWindow.Closed -= addWindowClosed;
            }
            //dailyWorkWindow.Closed -= addWindowClosed;
            await LoadProjectDailyWork();
        }
        [RelayCommand]
        private void OpenExcelFile()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Filter = "Excel files (*.xlsx)|*.xlsx|All files (*.*)|*.*",
                Title = "Select an Excel file"
            };

            if (openFileDialog.ShowDialog() == true)
            {
                string filePath = openFileDialog.FileName;
                ReadExcelFile(filePath);
            }
        }
        private async Task ReadExcelFile(string filePath)
        {
            using (var workbook = new XLWorkbook(filePath))
            {
                var worksheet = workbook.Worksheet(1);
                var range = worksheet.RangeUsed();
                var headers = range.Row(1).CellsUsed().Select(cell => cell.Value.ToString()).ToList();

                var list = new List<ProjectDailyWorkExcelVm>();
                foreach (var row in range.RowsUsed().Skip(1))
                {
                    var vm = new ProjectDailyWorkExcelVm();
                    foreach (var cell in row.CellsUsed())
                    {
                        var header = headers[cell.WorksheetColumn().ColumnNumber() - 1];
                        switch (header)
                        {
                            case "合同ID":
                                vm.ContractNumber = cell.Value.ToString().Trim();
                                break;
                            case "员工身份证号":
                                vm.StaffCard = cell.Value.ToString().Trim();
                                break;
                            case "工序":
                                vm.ProcessName = cell.Value.ToString().Trim();
                                break;
                            case "单位":
                                vm.UnitName = cell.Value.ToString().Trim();
                                break;
                            case "报量":
                                vm.Workload = Convert.ToDouble(cell.Value.ToString());
                                break;
                            case "报量日期":
                                vm.BillDate = DateOnly.FromDateTime(Convert.ToDateTime(cell.Value.ToString()));
                                break;
                            case "备注":
                                vm.Remarks = cell.Value.ToString().Trim();
                                break;
                        }
                    }
                    list.Add(vm);
                }
                var result = await _dailyWorkService.SaveExcelDailyWork(list);
                if (result)
                {
                    Growl.Success("保存成功");
                }
                else
                {
                    MessageBox.Warning("保存失败，请检查文件数据");
                }
            }
        }
    }
}
