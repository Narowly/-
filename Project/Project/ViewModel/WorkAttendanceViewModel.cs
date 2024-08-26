using ClosedXML.Excel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using DocumentFormat.OpenXml.Spreadsheet;
using HandyControl.Controls;
using HandyControl.Tools.Extension;
using Microsoft.Win32;
using Project.Services.DataServices;
using ProjectViewModels;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Project.ViewModel
{
    public partial class WorkAttendanceViewModel : ObservableObject
    {
        private readonly WorkAttendanceService _workAttendanceService;
        private string? _workYearMonth;
        public string? WorkYearMonth
        {
            get => _workYearMonth;
            set => SetProperty(ref _workYearMonth, value);
        }
        public WorkAttendanceViewModel(WorkAttendanceService workAttendanceService)
        {
            _workAttendanceService = workAttendanceService;
        }

        [RelayCommand]
        private async Task OpenYearMonthExcelFile()
        {
            if (string.IsNullOrWhiteSpace(WorkYearMonth))
            {
                MessageBox.Warning("请输入年月");
                return;
            }
            string formattedDateString = WorkYearMonth.Insert(6, "/01"); // 插入日期部分，设为月份的第一天  
            DateTime dateTime;

            if (!DateTime.TryParseExact(formattedDateString, "yyyyMM/dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out dateTime))
            {
                MessageBox.Warning("日期格式不正确");
                return;
            }
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Filter = "Excel files (*.xlsx)|*.xlsx|All files (*.*)|*.*",
                Title = "Select an Excel file"
            };

            if (openFileDialog.ShowDialog() == true)
            {
                string filePath = openFileDialog.FileName;
                await ReadYearMonthExcelFile(filePath);
            }
        }

        private async Task ReadYearMonthExcelFile(string filePath)
        {
            using (var workbook = new XLWorkbook(filePath))
            {
                var worksheet = workbook.Worksheet(1);
                var range = worksheet.RangeUsed();
                //var workYearMonth = range.Row(1).CellsUsed().FirstOrDefault()?.Value.ToString();
                //if (workYearMonth == null)
                //{
                //    MessageBox.Warning("没有填写年月，重新编辑");
                //    return;
                //}
                //WorkYearMonth = workYearMonth;
                var headers = range.Row(2).CellsUsed().Select(cell => cell.Value.ToString()).ToList();
                headers.Remove("打卡明细");
                headers.AddRange(range.Row(3).CellsUsed().Select(cell => cell.Value.ToString()).ToList());

                var list = new List<WorkAttendanceVm>();
                foreach (var row in range.RowsUsed().Skip(3))
                {
                    var workAttendanceVm = new WorkAttendanceVm();
                    workAttendanceVm.WorkYearMonth = WorkYearMonth;
                    foreach (var cell in row.CellsUsed())
                    {
                        var header = headers[cell.WorksheetColumn().ColumnNumber() - 1];
                        
                        var cellIndex = cell.WorksheetColumn().ColumnNumber() - 1;
                        switch (cellIndex)
                        {
                            case 0:
                                workAttendanceVm.StaffName = cell.Value.ToString();
                                break;
                            case 1:
                                workAttendanceVm.StaffAccount = cell.Value.ToString();
                                break;
                            case 2:
                                break;
                            case 3:
                                break;
                            case 4:
                                workAttendanceVm.ProjectName = cell.Value.ToString();
                                break;
                            case 5:
                                workAttendanceVm.ClockIn01 = cell.Value.ToString();
                                break;
                            case 6:
                                workAttendanceVm.ClockIn02 = cell.Value.ToString();
                                break;
                            case 7:
                                workAttendanceVm.ClockIn03 = cell.Value.ToString();
                                break;
                            case 8:
                                workAttendanceVm.ClockIn04 = cell.Value.ToString();
                                break;
                            case 9:
                                workAttendanceVm.ClockIn05 = cell.Value.ToString();
                                break;
                            case 10:
                                workAttendanceVm.ClockIn06 = cell.Value.ToString();
                                break;
                            case 11:
                                workAttendanceVm.ClockIn07 = cell.Value.ToString();
                                break;
                            case 12:
                                workAttendanceVm.ClockIn08 = cell.Value.ToString();
                                break;
                            case 13:
                                workAttendanceVm.ClockIn09 = cell.Value.ToString();
                                break;
                            case 14:
                                workAttendanceVm.ClockIn10 = cell.Value.ToString();
                                break;
                            case 15:
                                workAttendanceVm.ClockIn11 = cell.Value.ToString();
                                break;
                            case 16:
                                workAttendanceVm.ClockIn12 = cell.Value.ToString();
                                break;
                            case 17:
                                workAttendanceVm.ClockIn13 = cell.Value.ToString();
                                break;
                            case 18:
                                workAttendanceVm.ClockIn14 = cell.Value.ToString();
                                break;
                            case 19:
                                workAttendanceVm.ClockIn15 = cell.Value.ToString();
                                break;
                            case 20:
                                workAttendanceVm.ClockIn16 = cell.Value.ToString();
                                break;
                            case 21:
                                workAttendanceVm.ClockIn17 = cell.Value.ToString();
                                break;
                            case 22:
                                workAttendanceVm.ClockIn18 = cell.Value.ToString();
                                break;
                            case 23:
                                workAttendanceVm.ClockIn19 = cell.Value.ToString();
                                break;
                            case 24:
                                workAttendanceVm.ClockIn20 = cell.Value.ToString();
                                break;
                            case 25:
                                workAttendanceVm.ClockIn21 = cell.Value.ToString();
                                break;
                            case 26:
                                workAttendanceVm.ClockIn22 = cell.Value.ToString();
                                break;
                            case 27:
                                workAttendanceVm.ClockIn23 = cell.Value.ToString();
                                break;
                            case 28:
                                workAttendanceVm.ClockIn24 = cell.Value.ToString();
                                break;
                            case 29:
                                workAttendanceVm.ClockIn25 = cell.Value.ToString();
                                break;
                            case 30:
                                workAttendanceVm.ClockIn26 = cell.Value.ToString();
                                break;
                            case 31:
                                workAttendanceVm.ClockIn27 = cell.Value.ToString();
                                break;
                            case 32:
                                workAttendanceVm.ClockIn28 = cell.Value.ToString();
                                break;
                            case 33:
                                workAttendanceVm.ClockIn29 = cell.Value.ToString();
                                break;
                            case 34:
                                workAttendanceVm.ClockIn30 = cell.Value.ToString();
                                break;
                            case 35:
                                workAttendanceVm.ClockIn31 = cell.Value.ToString();
                                break;
                        }                     
                    }
                    list.Add(workAttendanceVm);
                }
                var result = await _workAttendanceService.InsertYearMonthWorkAttendanceExcel(list);
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

        [RelayCommand]
        private async Task OpenDelayClockExcelFile()
        {
            if (WorkYearMonth == null)
            {
                MessageBox.Warning("先上传打卡月报");
                return;
            }
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Filter = "Excel files (*.xlsx)|*.xlsx|All files (*.*)|*.*",
                Title = "Select an Excel file"
            };

            if (openFileDialog.ShowDialog() == true)
            {
                string filePath = openFileDialog.FileName;
                await ReadDelayClockExcelFile(filePath);
            }
        }

        private async Task ReadDelayClockExcelFile(string filePath)
        {
            using (var workbook = new XLWorkbook(filePath))
            {
                var worksheet = workbook.Worksheet(1);
                var range = worksheet.RangeUsed();
                var headers = range.Row(1).CellsUsed().Select(cell => cell.Value.ToString()).ToList();
                if (headers.Count != 15)
                {
                    MessageBox.Warning("文件格式不正确");
                    return;
                }

                var list = new List<WorkDelayClockVm>();
                foreach (var row in range.RowsUsed().Skip(1))
                {
                    var delayClockVm = new WorkDelayClockVm();
                    var passFlag = true;
                    foreach (var cell in row.CellsUsed())
                    {
                        var header = headers[cell.WorksheetColumn().ColumnNumber() - 1];
                        
                        switch (header)
                        {
                            case "审批编号":
                                delayClockVm.DelayClockId = cell.Value.ToString();
                                break;
                            case "申请人账号":
                                delayClockVm.StaffAccount = cell.Value.ToString();
                                break;
                            case "补卡事由":
                                delayClockVm.Reason = cell.Value.ToString();
                                break;
                            case "补卡班次":
                                delayClockVm.ApplyWorkTime = cell.Value.ToString();
                                break;
                            case "补卡时间":
                                delayClockVm.DelayClockTime = Convert.ToDateTime(cell.Value.ToString());
                                break;
                            case "当前审批状态":
                                if (cell.Value.ToString() != "已通过") passFlag = false;
                                break;
                        }
                        delayClockVm.WorkYearMonth = WorkYearMonth;
                    }
                    if (!passFlag) continue;
                    list.Add(delayClockVm);
                }
                var result = await _workAttendanceService.InsertDelayClockExcel(list);
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

        [RelayCommand]
        private async Task OpenOutClockExcelFile()
        {
            if (WorkYearMonth == null)
            {
                MessageBox.Warning("先上传打卡月报");
                return;
            }
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Filter = "Excel files (*.xlsx)|*.xlsx|All files (*.*)|*.*",
                Title = "Select an Excel file"
            };

            if (openFileDialog.ShowDialog() == true)
            {
                string filePath = openFileDialog.FileName;
                await ReadOutClockExcelFile(filePath);
            }
        }

        private async Task ReadOutClockExcelFile(string filePath)
        {
            using (var workbook = new XLWorkbook(filePath))
            {
                var worksheet = workbook.Worksheet(2);
                var range = worksheet.RangeUsed();
                var headers = range.Row(3).CellsUsed().Select(cell => cell.Value.ToString()).ToList();
                if (headers.Count != 10)
                {
                    MessageBox.Warning("文件格式不正确");
                    return;
                }

                var list = new List<WorkOutClockVm>();
                foreach (var row in range.RowsUsed().Skip(3))
                {
                    var outClockVm = new WorkOutClockVm();

                    DateTime? applyDate = null;
                    string? time = null;
                    foreach (var cell in row.CellsUsed())
                    {
                        var header = headers[cell.WorksheetColumn().ColumnNumber() - 1];

                        switch (header)
                        {
                            case "日期":
                                applyDate = Convert.ToDateTime(cell.Value.ToString());
                                break;
                            case "账号":
                                outClockVm.StaffAccount = cell.Value.ToString();
                                break;
                            case "打卡时间":
                                time = cell.Value.ToString();
                                break;
                        }
                    }
                    if (applyDate == null || time == null) continue;
                    var timelist = time.Split(':').Select(m => Convert.ToInt32(m)).ToList();
                    outClockVm.OutClockDateTime = applyDate.Value.AddHours(timelist[0]).AddMinutes(timelist[1]);
                    outClockVm.WorkYearMonth = WorkYearMonth;
                    list.Add(outClockVm);
                }
                var result = await _workAttendanceService.InsertOutClockExcel(list);
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

        [RelayCommand]
        private async Task OpenApplyLeaveExcelFile()
        {
            if (WorkYearMonth == null)
            {
                MessageBox.Warning("先上传打卡月报");
                return;
            }
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Filter = "Excel files (*.xlsx)|*.xlsx|All files (*.*)|*.*",
                Title = "Select an Excel file"
            };

            if (openFileDialog.ShowDialog() == true)
            {
                string filePath = openFileDialog.FileName;
                await ReadApplyLeaveExcelFile(filePath);
            }
        }

        private async Task ReadApplyLeaveExcelFile(string filePath)
        {
            using (var workbook = new XLWorkbook(filePath))
            {
                var worksheet = workbook.Worksheet(1);
                var range = worksheet.RangeUsed();
                var headers = range.Row(1).CellsUsed().Select(cell => cell.Value.ToString()).ToList();
                if (headers.Count != 17)
                {
                    MessageBox.Warning("文件格式不正确");
                    return;
                }

                var list = new List<WorkApplyLeaveVm>();
                foreach (var row in range.RowsUsed().Skip(1))
                {
                    var applyLeaveVm = new WorkApplyLeaveVm();

                    var passFlag = true;
                    foreach (var cell in row.CellsUsed())
                    {
                        var header = headers[cell.WorksheetColumn().ColumnNumber() - 1];

                        switch (header)
                        {
                            case "审批编号":
                                applyLeaveVm.WorkApplyLeaveId = cell.Value.ToString();
                                break;
                            case "申请人账号":
                                applyLeaveVm.StaffAccount = cell.Value.ToString();
                                break;
                            case "请假类型":
                                applyLeaveVm.LeaveType = cell.Value.ToString();
                                break;
                            case "开始时间":
                                var startValue = cell.Value.ToString();
                                var formatStartTime = startValue.Replace("上午", "08:30:00").Replace("下午", "17:30:00");
                                applyLeaveVm.StartTime = Convert.ToDateTime(formatStartTime);
                                break;
                            case "结束时间":
                                var endValue = cell.Value.ToString();
                                var formatEndTime = endValue.Replace("上午", "08:30:00").Replace("下午", "17:30:00");
                                applyLeaveVm.EndTime = Convert.ToDateTime(formatEndTime);
                                break;
                            case "当前审批状态":
                                if (cell.Value.ToString() != "已通过") passFlag = false;
                                break;
                        }
                    }
                    if (!passFlag) continue;
                    applyLeaveVm.WorkYearMonth = WorkYearMonth;
                    list.Add(applyLeaveVm);
                }
                var result = await _workAttendanceService.InsertApplyLeaveExcel(list);
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

        private List<string> workFlag = new List<string> { "√", "a", "b", "缺卡", "m" };
        [RelayCommand]
        private async Task ExportYearMonthExcel(string yearMonth)
        {
            string formattedDateString = yearMonth.Insert(6, "/01"); // 插入日期部分，设为月份的第一天  
            DateTime dateTime;

            if (!DateTime.TryParseExact(formattedDateString, "yyyyMM/dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out dateTime))
            {
                MessageBox.Warning("日期格式不正确");
                return;
            }
            // 创建一个保存文件对话框  
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Excel files (*.xlsx)|*.xlsx"; // 设置文件类型过滤器  
            saveFileDialog.Title = "选择保存Excel文件的路径"; // 设置对话框标题  

            // 如果用户点击了保存按钮  
            if (saveFileDialog.ShowDialog() == true)
            {
                string filePath = saveFileDialog.FileName; // 获取用户选择的文件路径  

                // 创建一个新的 Excel 工作簿  
                using (var workbook = new XLWorkbook())
                {
                    var worksheet = workbook.Worksheets.Add("sheet1");
                    var a1 = worksheet.Cell("A1");
                    a1.Value = $"员  工  考  勤  统  计  表\n{dateTime.ToString("yyyy年MM月")}";
                    var font = a1.Style.Font;
                    font.FontSize = 24;
                    font.FontName = "宋体";
                    a1.Style.Alignment.WrapText = true;
                    a1.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                    a1.Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;
                    
                    worksheet.Columns("A:BN").Width = 2.2;
                    worksheet.Row(1).Height = 100;

                    var a4 = worksheet.Cell("A4");
                    a4.Value = "序号";
                    a4.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                    a4.Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;
                    a4.WorksheetColumn().Width = 5;
                    worksheet.Range("A4:A9").Merge();
                    var b4 = worksheet.Cell("B4");
                    b4.Value = "日期";
                    b4.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                    b4.Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;
                    worksheet.Range("B4:B5").Merge();
                    var b6 = worksheet.Cell("B6");
                    b6.Value = "姓名";
                    b6.WorksheetColumn().Width = 15;
                    b6.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                    b6.Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;
                    worksheet.Range("B6:B9").Merge();
                    var c4 = worksheet.Cell("C4");
                    c4.Value = "项目";
                    c4.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                    c4.Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;
                    c4.WorksheetColumn().Width = 35;
                    worksheet.Range("C4:C9").Merge();
                    
                    var days = DateTime.DaysInMonth(dateTime.Year, dateTime.Month);

                    var startRowIndex = 4;
                    var startColIndex = 4;
                    for(int i = 0; i < days; i++)
                    {
                        var weekDay = string.Empty;
                        switch (new DateTime(dateTime.Year, dateTime.Month, i + 1).DayOfWeek)
                        {
                           case DayOfWeek.Sunday:
                                weekDay = "日";
                                break;
                            case DayOfWeek.Monday:
                                weekDay = "一";
                                break;
                            case DayOfWeek.Tuesday:
                                weekDay = "二";
                                break;
                            case DayOfWeek.Wednesday:
                                weekDay = "三";
                                break;
                            case DayOfWeek.Thursday:
                                weekDay = "四";
                                break;
                            case DayOfWeek.Friday:
                                weekDay = "五";
                                break;
                            case DayOfWeek.Saturday:
                                weekDay = "六";
                                break;
                        };
                        var cell = worksheet.Cell(startRowIndex, startColIndex + i * 2);
                        var rightcell = worksheet.Cell(startRowIndex, startColIndex + i * 2 + 1);
                        cell.Value = (i+1).ToString();
                        cell.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                        cell.Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;
                        worksheet.Range(cell.Address, rightcell.Address).Merge();

                        var weekDayCell = worksheet.Cell(startRowIndex + 1, startColIndex + i * 2);
                        var weekDayRigthCell = worksheet.Cell(startRowIndex + 1, startColIndex + i * 2 + 1);
                        weekDayCell.Value = weekDay;
                        weekDayCell.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                        weekDayCell.Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;
                        if (weekDay == "六" || weekDay == "日")
                        {
                            worksheet.Column(weekDayCell.Address.ColumnNumber).Style.Fill.BackgroundColor = XLColor.LightBlue;
                            worksheet.Column(weekDayCell.Address.ColumnNumber+1).Style.Fill.BackgroundColor = XLColor.LightBlue;
                        }
                        worksheet.Range(weekDayCell.Address, weekDayRigthCell.Address).Merge();

                        var morningCell = worksheet.Cell(startRowIndex + 2, startColIndex + i * 2);
                        morningCell.Value = "上\n午";
                        morningCell.Style.Alignment.WrapText = true;
                        worksheet.Range(morningCell.Address.RowNumber, morningCell.Address.ColumnNumber, morningCell.Address.RowNumber + 1, morningCell.Address.ColumnNumber).Merge();
                        var afternoonCell = worksheet.Cell(startRowIndex + 4, startColIndex + i * 2 + 1);
                        afternoonCell.Value = "下\n午";
                        afternoonCell.Style.Alignment.WrapText = true;
                        worksheet.Range(afternoonCell.Address.RowNumber, afternoonCell.Address.ColumnNumber, afternoonCell.Address.RowNumber + 1, afternoonCell.Address.ColumnNumber).Merge();
                    }


                    var workDays = worksheet.Cell(4, days * 2 + 4);
                    var workBottom = worksheet.Cell(9, days * 2 + 4);
                    workDays.Value = "工作天数";
                    workDays.WorksheetColumn().Width = 10;
                    workDays.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                    workDays.Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;
                    worksheet.Range(workDays.Address, workBottom.Address).Merge();

                    var workProcessDays = worksheet.Cell(4, days * 2 + 5);
                    var workProcessDaysBottom = worksheet.Cell(9, days * 2 + 5);
                    workProcessDays.Value = "报量天数";
                    workProcessDays.WorksheetColumn().Width = 10;
                    workProcessDays.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                    workProcessDays.Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;
                    worksheet.Range(workProcessDays.Address, workProcessDaysBottom.Address).Merge();

                    var abnormal = worksheet.Cell(4, days * 2 + 6);
                    var abnormalBottom = worksheet.Cell(9, days * 2 + 6);
                    abnormal.Value = "异常";
                    abnormal.WorksheetColumn().Width = 20;
                    abnormal.Style.Alignment.WrapText = true;
                    abnormal.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                    abnormal.Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;
                    worksheet.Range(abnormal.Address, abnormalBottom.Address).Merge();

                    var remarks = worksheet.Cell(4, days * 2 + 7);
                    var remarksBottom = worksheet.Cell(9, days * 2 + 7);
                    remarks.Value = "备注";
                    remarks.WorksheetColumn().Width = 20;
                    remarks.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                    remarks.Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;
                    worksheet.Range(remarks.Address, remarksBottom.Address).Merge();

                    var index = 1;
                    var rowIndex = 10;
                    var list = await _workAttendanceService.GetYearMonthWorkAttendance(dateTime.ToString("yyyyMM"));
                    foreach (var item in list)
                    {
                        worksheet.Cell(rowIndex, 1).Value = index.ToString();
                        worksheet.Cell(rowIndex, 2).Value = item.StaffName;
                        worksheet.Cell(rowIndex, 3).Value = item.ProjectName;
                        worksheet.Cell(rowIndex, 4).Value = item.ClockIn01;
                        worksheet.Cell(rowIndex, 5).Value = item.ClockOut01;
                        worksheet.Cell(rowIndex, 6).Value = item.ClockIn02;
                        worksheet.Cell(rowIndex, 7).Value = item.ClockOut02;
                        worksheet.Cell(rowIndex, 8).Value = item.ClockIn03;
                        worksheet.Cell(rowIndex, 9).Value = item.ClockOut03;
                        worksheet.Cell(rowIndex, 10).Value = item.ClockIn04;
                        worksheet.Cell(rowIndex, 11).Value = item.ClockOut04;
                        worksheet.Cell(rowIndex, 12).Value = item.ClockIn05;
                        worksheet.Cell(rowIndex, 13).Value = item.ClockOut05;
                        worksheet.Cell(rowIndex, 14).Value = item.ClockIn06;
                        worksheet.Cell(rowIndex, 15).Value = item.ClockOut06;
                        worksheet.Cell(rowIndex, 16).Value = item.ClockIn07;
                        worksheet.Cell(rowIndex, 17).Value = item.ClockOut07;
                        worksheet.Cell(rowIndex, 18).Value = item.ClockIn08;
                        worksheet.Cell(rowIndex, 19).Value = item.ClockOut08;
                        worksheet.Cell(rowIndex, 20).Value = item.ClockIn09;
                        worksheet.Cell(rowIndex, 21).Value = item.ClockOut09;
                        worksheet.Cell(rowIndex, 22).Value = item.ClockIn10;
                        worksheet.Cell(rowIndex, 23).Value = item.ClockOut10;
                        worksheet.Cell(rowIndex, 24).Value = item.ClockIn11;
                        worksheet.Cell(rowIndex, 25).Value = item.ClockOut11;
                        worksheet.Cell(rowIndex, 26).Value = item.ClockIn12;
                        worksheet.Cell(rowIndex, 27).Value = item.ClockOut12;
                        worksheet.Cell(rowIndex, 28).Value = item.ClockIn13;
                        worksheet.Cell(rowIndex, 29).Value = item.ClockOut13;
                        worksheet.Cell(rowIndex, 30).Value = item.ClockIn14;
                        worksheet.Cell(rowIndex, 31).Value = item.ClockOut14;
                        worksheet.Cell(rowIndex, 32).Value = item.ClockIn15;
                        worksheet.Cell(rowIndex, 33).Value = item.ClockOut15;
                        worksheet.Cell(rowIndex, 34).Value = item.ClockIn16;
                        worksheet.Cell(rowIndex, 35).Value = item.ClockOut16;
                        worksheet.Cell(rowIndex, 36).Value = item.ClockIn17;
                        worksheet.Cell(rowIndex, 37).Value = item.ClockOut17;
                        worksheet.Cell(rowIndex, 38).Value = item.ClockIn18;
                        worksheet.Cell(rowIndex, 39).Value = item.ClockOut18;
                        worksheet.Cell(rowIndex, 40).Value = item.ClockIn19;
                        worksheet.Cell(rowIndex, 41).Value = item.ClockOut19;
                        worksheet.Cell(rowIndex, 42).Value = item.ClockIn20;
                        worksheet.Cell(rowIndex, 43).Value = item.ClockOut20;
                        worksheet.Cell(rowIndex, 44).Value = item.ClockIn21;
                        worksheet.Cell(rowIndex, 45).Value = item.ClockOut21;
                        worksheet.Cell(rowIndex, 46).Value = item.ClockIn22;
                        worksheet.Cell(rowIndex, 47).Value = item.ClockOut22;
                        worksheet.Cell(rowIndex, 48).Value = item.ClockIn23;
                        worksheet.Cell(rowIndex, 49).Value = item.ClockOut23;
                        worksheet.Cell(rowIndex, 50).Value = item.ClockIn24;
                        worksheet.Cell(rowIndex, 51).Value = item.ClockOut24;
                        worksheet.Cell(rowIndex, 52).Value = item.ClockIn25;
                        worksheet.Cell(rowIndex, 53).Value = item.ClockOut25;
                        worksheet.Cell(rowIndex, 54).Value = item.ClockIn26;
                        worksheet.Cell(rowIndex, 55).Value = item.ClockOut26;
                        worksheet.Cell(rowIndex, 56).Value = item.ClockIn27;
                        worksheet.Cell(rowIndex, 57).Value = item.ClockOut27;
                        worksheet.Cell(rowIndex, 58).Value = item.ClockIn28;
                        worksheet.Cell(rowIndex, 59).Value = item.ClockOut28;
                        if (days > 28)
                        {
                            worksheet.Cell(rowIndex, 60).Value = item.ClockIn29;
                            worksheet.Cell(rowIndex, 61).Value = item.ClockOut29;
                        }
                        if(days > 29)
                        {
                            worksheet.Cell(rowIndex, 62).Value = item.ClockIn30;
                            worksheet.Cell(rowIndex, 63).Value = item.ClockOut30;
                        }
                        if(days > 30)
                        {
                            worksheet.Cell(rowIndex, 64).Value = item.ClockIn31;
                            worksheet.Cell(rowIndex, 65).Value = item.ClockOut31;
                        }

                        List<string> workDayList = new List<string>();
                        Type type = item.GetType();
                        foreach (PropertyInfo property in type.GetProperties())
                        {
                            if (property.Name.Contains("ClockIn") || property.Name.Contains("ClockOut"))
                            {
                                string? value = (string?)property.GetValue(item);
                                if (value != null && workFlag.Any(m=>value.Contains(m)))
                                {
                                    item.TotalWorkDays += 0.5;
                                    workDayList.Add(Regex.Match(property.Name, @"\d+").Value);
                                }                                
                            }                            
                        }
                        workDayList = workDayList.Distinct().ToList();
                        var onlyProcessDays  = item.ProcessDays.Except(workDayList).ToList();
                        var onlyWorkDays = workDayList.Except(item.ProcessDays).ToList();

                        worksheet.Cell(rowIndex, workDays.Address.ColumnNumber).Value = item.TotalWorkDays;
                        worksheet.Cell(rowIndex, workProcessDays.Address.ColumnNumber).Value = item.ProcessDays.Count;
                        var abnormalList = new StringBuilder();
                        if (onlyWorkDays.Count > 0)
                        {
                            foreach (var onlyWork in onlyWorkDays)
                            {
                                abnormalList.Append($"{new DateTime(dateTime.Year, dateTime.Month, int.Parse(onlyWork)).ToString("yyyy-MM-dd")} 有打卡，没报量\n");
                            }                            
                        }
                        if (onlyProcessDays.Count > 0)
                        {
                            foreach (var onlyProcess in onlyProcessDays)
                            {
                                abnormalList.Append($"{new DateTime(dateTime.Year, dateTime.Month, int.Parse(onlyProcess)).ToString("yyyy-MM-dd")} 有报量，没打卡\n");
                            }
                        }

                        worksheet.Cell(rowIndex, abnormal.Address.ColumnNumber).Value = abnormalList.ToString();

                        index++;
                        rowIndex++;
                    }
                    worksheet.Range(1,1,3, remarks.Address.ColumnNumber).Merge();
                    var range = worksheet.RangeUsed();
                    var border = range.Style.Border;
                    border.InsideBorder = XLBorderStyleValues.Thin;
                    border.OutsideBorder = XLBorderStyleValues.Thin;
                    workbook.SaveAs(filePath);
                }                
            }
        }
    }
}
