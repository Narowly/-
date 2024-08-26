using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using Project.Common;
using Project.Services.DataServices;
using ProjectViewModels;
using System.Collections.ObjectModel;
using Project.Services;
using CommunityToolkit.Mvvm.Input;
using Autofac.Core;
using Project.Views.UserControls;
using Project.Views.Windows;
using Microsoft.Win32;
using ClosedXML.Excel;
using MessageBox = HandyControl.Controls.MessageBox;
using HandyControl.Controls;
using DocumentFormat.OpenXml.Drawing;
namespace Project.ViewModel
{
    public partial class PatrolViewModel : ObservableObject
    {
        private readonly IContainer? _container = App.Current.Properties[MessageToken.AppContainer] as IContainer;
        private readonly PatrolService _patrolService;
        private readonly DictService _dictService;
        private readonly ProjectService _projectService;
        private readonly StaffService _staffService;

        private PaginatedList<ProjectPatrolVm> _paginatedList = null!;
        public PaginatedList<ProjectPatrolVm> PaginatedList
        {
            get => _paginatedList;
            set => SetProperty(ref _paginatedList, value);
        }

        private ProjectPatrolVm? _selectedDto;
        public ProjectPatrolVm? SelectedDto
        {
            get => _selectedDto;
            set => SetProperty(ref _selectedDto, value);
        }
        private List<ProjectAutoCompleteModel> ProjectNameList = null!;
        [ObservableProperty]
        private ObservableCollection<ProjectAutoCompleteModel>? _projectNamesSource;
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

            }
        }
        [ObservableProperty]
        private ProjectAutoCompleteModel? _selectedProject;

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
        private List<DictDataVm> _patrolStatusList = null!;
        public List<DictDataVm> PatrolStatusList
        {
            get => _patrolStatusList;
            set => SetProperty(ref _patrolStatusList, value);
        }

        private DictDataVm? _selectedStatus;
        public DictDataVm? SelectedStatus
        {
            get => _selectedStatus;
            set => SetProperty(ref _selectedStatus, value);
        }

        private ProjectWithStaffReq _req = new ();
        public ProjectWithStaffReq Req
        {
            get => _req;
            set => SetProperty(ref _req, value);
        }

        public PatrolViewModel(PatrolService patrolService, DictService dictService, ProjectService projectService, StaffService staffService)
        {
            _patrolService = patrolService;
            _dictService = dictService;
            _projectService = projectService;
            _staffService = staffService;
            Task.Run(LoadDataAsync);
        }

        private async Task LoadDataAsync()
        {
            await LoadProjectNames();
            await LoadManagerList();
            await LoadStaff();
            await LoadPatrolStatusList();
            await LoadPaginatedList();
        }

        private async Task LoadProjectNames()
        {
            ProjectNameList = await _projectService.LoadProjectNames();
        }
        private async Task LoadManagerList()
        {
            ManagerList = await _staffService.GetStaffListByDuty(ApiSettings.ProjectManagerDuty);
        }
        private async Task LoadStaff()
        {
            StaffList = await _staffService.GetStaffList();
        }
        private async Task LoadPatrolStatusList()
        {
            PatrolStatusList = await _dictService.GetDictDataByTypeName(DictSettings.PatrolStatusTypeName);
        }
        [RelayCommand]
        private async Task LoadPaginatedList()
        {
            if (SelectedProject != null) Req.ProjectId = SelectedProject.Id;
            else Req.ProjectId = null;
            if (SelectedManager != null) Req.ProjectManagerId = SelectedManager.StaffId;
            else Req.ProjectManagerId = null;
            if (SelectedStaff != null) Req.Staff = SelectedStaff.StaffId;
            else Req.Staff = null;
            if (SelectedStatus != null) Req.Status = SelectedStatus.DictCode;
            else Req.Status = null;
            
            PaginatedList = await _patrolService.PaginatedPatrol(Req);
        }

        private PopWindow? addWindow;
        [RelayCommand]
        private void AddPatrol()
        {
            ResolvedParameter id = new ResolvedParameter(
                (pi, ctx) => pi.Name == "id",
                (pi, ctx) => SelectedDto?.Id);
            if (_container == null) return;
            var vm = _container.Resolve<AddPatrolViewModel>(id);
            var view = _container.Resolve<AddPatrolView>();
            view.DataContext = vm;
            addWindow = new PopWindow();
            addWindow.controlHost.Content = view;
            addWindow.Title = $"巡查";
            addWindow.Closed += SubWindowClosed;
            addWindow.ShowDialog();
        }
        private async void SubWindowClosed(object? sender, EventArgs e)
        {
            if (sender is PopWindow window)
            {
                window.Closed -= SubWindowClosed;
                await LoadPaginatedList();
            }
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
             
                var list = new List<ProjectPatrolExcelVm>();
                foreach (var row in range.RowsUsed().Skip(1))
                {
                    var patrolVm = new ProjectPatrolExcelVm();
                    foreach (var cell in row.CellsUsed())
                    {
                        var header = headers[cell.WorksheetColumn().ColumnNumber() - 1];
                        switch (header)
                        {
                            case "合同ID":
                                patrolVm.Ht = cell.Value.ToString().Trim();
                                break;
                            case "巡查人身份证号":
                                patrolVm.Card = cell.Value.ToString().Trim();
                                break;
                            case "巡查日期":
                                patrolVm.PatrolDateTime = Convert.ToDateTime(cell.Value.ToString());
                                break;
                            case "整改状态":
                                patrolVm.PatrolStatus = cell.Value.ToString().Trim();
                                break;
                            case "备注":
                                patrolVm.Remarks = cell.Value.ToString().Trim();
                                break;
                        }
                    }
                    list.Add(patrolVm);
                }
                var result = await _patrolService.SavePatrolByExcel(list);
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
