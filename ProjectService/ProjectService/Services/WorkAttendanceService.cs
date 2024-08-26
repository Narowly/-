using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjectService.Db;
using ProjectService.ViewModels;
using ProjectViewModels;
using System.Globalization;
using System.Reflection;
using System.Reflection.Metadata.Ecma335;

namespace ProjectService.Services
{
    public class WorkAttendanceService : UserService
    {
        private readonly ProjectDbContext _context;
        private readonly DictService _dictService;
        public WorkAttendanceService(ProjectDbContext context, IHttpContextAccessor httpContextAccessor, DictService dictService) : base(httpContextAccessor)
        {
            _context = context;
            _dictService = dictService;
        }

        public async Task<bool> InsertYearMonthWorkAttendanceExcel(List<WorkAttendanceVm> list)
        {
            foreach (var item in list)
            {
                var workAttandance = _context.WorkAttendances.FirstOrDefault(m => m.WorkYearMonth == item.WorkYearMonth && m.StaffAccount == item.StaffAccount);
                bool newFlag = false;
                if (workAttandance == null)
                {
                    newFlag = true;
                    workAttandance = new WorkAttendance();
                    workAttandance.WorkAttendanceId = Guid.NewGuid();       
                    workAttandance.WorkYearMonth = item.WorkYearMonth;
                    workAttandance.StaffAccount = item.StaffAccount;
                }
                workAttandance.StaffName = item.StaffName;
                var sname = item.StaffName;
                if (item.StaffName.Contains("（"))
                {
                    sname = item.StaffName.Substring(0, item.StaffName.IndexOf('（'));
                }
                var staffList = _context.Staff.Where(m => m.StaffName == sname).ToList();
                if (staffList.Count == 0) 
                    continue;
                if (staffList.Count > 1)
                {
                    var staffIdList = staffList.Select(m => m.StaffId).ToList();
                    var dailywork = _context.ProjectDailyWorks.Where(m => staffIdList.Contains(m.StaffId) && m.BillDate > DateOnly.FromDateTime(DateTime.Now.AddMonths(-1))).OrderByDescending(m => m.BillDate).FirstOrDefault();
                    if (dailywork != null)
                    {
                        workAttandance.StaffId = dailywork.StaffId;
                    }
                    else
                    {
                        workAttandance.StaffId = staffList.OrderByDescending(m => m.StaffCode).First().StaffId;
                    }
                }
                else
                {
                    workAttandance.StaffId = staffList.FirstOrDefault().StaffId;
                }

                workAttandance.StaffAccount = item.StaffAccount;
                workAttandance.ProjectName = item.ProjectName;
                workAttandance.ClockIn01 = item.ClockIn01;
                workAttandance.ClockIn02 = item.ClockIn02;
                workAttandance.ClockIn03 = item.ClockIn03;
                workAttandance.ClockIn04 = item.ClockIn04;
                workAttandance.ClockIn05 = item.ClockIn05;
                workAttandance.ClockIn06 = item.ClockIn06;
                workAttandance.ClockIn07 = item.ClockIn07;
                workAttandance.ClockIn08 = item.ClockIn08;
                workAttandance.ClockIn09 = item.ClockIn09;
                workAttandance.ClockIn10 = item.ClockIn10;
                workAttandance.ClockIn11 = item.ClockIn11;
                workAttandance.ClockIn12 = item.ClockIn12;
                workAttandance.ClockIn13 = item.ClockIn13;
                workAttandance.ClockIn14 = item.ClockIn14;
                workAttandance.ClockIn15 = item.ClockIn15;
                workAttandance.ClockIn16 = item.ClockIn16;
                workAttandance.ClockIn17 = item.ClockIn17;
                workAttandance.ClockIn18 = item.ClockIn18;
                workAttandance.ClockIn19 = item.ClockIn19;
                workAttandance.ClockIn20 = item.ClockIn20;
                workAttandance.ClockIn21 = item.ClockIn21;
                workAttandance.ClockIn22 = item.ClockIn22;
                workAttandance.ClockIn23 = item.ClockIn23;
                workAttandance.ClockIn24 = item.ClockIn24;
                workAttandance.ClockIn25 = item.ClockIn25;
                workAttandance.ClockIn26 = item.ClockIn26;
                workAttandance.ClockIn27 = item.ClockIn27;
                workAttandance.ClockIn28 = item.ClockIn28;
                workAttandance.ClockIn29 = item.ClockIn29;
                workAttandance.ClockIn30 = item.ClockIn30;
                workAttandance.ClockIn31 = item.ClockIn31;

                workAttandance.ClockOut01 = item.ClockOut01;
                workAttandance.ClockOut02 = item.ClockOut02;
                workAttandance.ClockOut03 = item.ClockOut03;
                workAttandance.ClockOut04 = item.ClockOut04;
                workAttandance.ClockOut05 = item.ClockOut05;
                workAttandance.ClockOut06 = item.ClockOut06;
                workAttandance.ClockOut07 = item.ClockOut07;
                workAttandance.ClockOut08 = item.ClockOut08;
                workAttandance.ClockOut09 = item.ClockOut09;
                workAttandance.ClockOut10 = item.ClockOut10;
                workAttandance.ClockOut11 = item.ClockOut11;
                workAttandance.ClockOut12 = item.ClockOut12;
                workAttandance.ClockOut13 = item.ClockOut13;
                workAttandance.ClockOut14 = item.ClockOut14;
                workAttandance.ClockOut15 = item.ClockOut15;
                workAttandance.ClockOut16 = item.ClockOut16;
                workAttandance.ClockOut17 = item.ClockOut17;
                workAttandance.ClockOut18 = item.ClockOut18;
                workAttandance.ClockOut19 = item.ClockOut19;
                workAttandance.ClockOut20 = item.ClockOut20;
                workAttandance.ClockOut21 = item.ClockOut21;
                workAttandance.ClockOut22 = item.ClockOut22;
                workAttandance.ClockOut23 = item.ClockOut23;
                workAttandance.ClockOut24 = item.ClockOut24;
                workAttandance.ClockOut25 = item.ClockOut25;
                workAttandance.ClockOut26 = item.ClockOut26;
                workAttandance.ClockOut27 = item.ClockOut27;
                workAttandance.ClockOut28 = item.ClockOut28;
                workAttandance.ClockOut29 = item.ClockOut29;
                workAttandance.ClockOut30 = item.ClockOut30;
                workAttandance.ClockOut31 = item.ClockOut31;

                if (newFlag) _context.WorkAttendances.Add(workAttandance);
            }

            await _context.SaveChangesAsync();
            return true;
        }
    
        public async Task<bool> InsertDelayClockExcel(List<WorkDelayClockVm> list)
        {
            foreach (var item in list)
            {
                var searchYearMonth = $"{item.DelayClockTime.ToString("yyyyMM")}";
                var workAttendance = _context.WorkAttendances.FirstOrDefault(m => m.WorkYearMonth == searchYearMonth && m.StaffAccount == item.StaffAccount);
                if (workAttendance == null) continue;

                var delayClock = _context.WorkDelayClocks.FirstOrDefault(m => m.DelayClockId == item.DelayClockId);
                bool newFlag = false;
                if (delayClock == null)
                {
                    newFlag = true;
                    delayClock = new WorkDelayClock();
                    delayClock.DelayClockId = item.DelayClockId;
                    delayClock.WorkYearMonth = item.WorkYearMonth;
                }
                delayClock.StaffId = workAttendance.StaffId;
                delayClock.Reason = item.Reason;
                delayClock.ApplyWorkTime = item.ApplyWorkTime;
                delayClock.DelayClockTime = item.DelayClockTime;
                if (newFlag) _context.WorkDelayClocks.Add(delayClock);
            }
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> InsertOutClockExcel(List<WorkOutClockVm> list)
        {
            foreach (var item in list)
            {
                var searchYearMonth = $"{item.OutClockDateTime.ToString("yyyyMM")}";
                var workAttendance = _context.WorkAttendances.FirstOrDefault(m => m.WorkYearMonth == searchYearMonth && m.StaffAccount == item.StaffAccount);
                if (workAttendance == null) continue;

                var outClock = _context.WorkOutClocks.FirstOrDefault(m => m.StaffId == workAttendance.StaffId && m.OutClockDateTime == item.OutClockDateTime);
                bool newFlag = false;
                if(outClock == null)
                {
                    newFlag = true;
                    outClock = new WorkOutClock();
                    outClock.WorkOutClockId = Guid.NewGuid();
                    outClock.StaffId = workAttendance.StaffId;
                    outClock.OutClockDateTime = item.OutClockDateTime;
                    outClock.WorkYearMonth = item.WorkYearMonth;
                }                
                if(newFlag) _context.WorkOutClocks.Add(outClock);
            }
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> InsertApplyLeaveExcel(List<WorkApplyLeaveVm> list)
        {
            foreach (var item in list)
            {
                var searchYearMonth = $"{item.StartTime.ToString("yyyyMM")}";
                var workAttendance = _context.WorkAttendances.FirstOrDefault(m => m.WorkYearMonth == searchYearMonth && m.StaffAccount == item.StaffAccount);
                if (workAttendance == null) continue;

                var applyLeave = _context.WorkApplyLeaves.FirstOrDefault(m => m.WorkApplyLeaveId == item.WorkApplyLeaveId);

                bool newFlag = false;
                if (applyLeave == null)
                {
                    newFlag = true;
                    applyLeave = new WorkApplyLeave();
                    applyLeave.WorkApplyLeaveId = item.WorkApplyLeaveId;
                    applyLeave.WorkYearMonth = item.WorkYearMonth;
                }
                applyLeave.StaffId = workAttendance.StaffId;
                applyLeave.LeaveType = item.LeaveType;
                applyLeave.StartTime = item.StartTime;
                applyLeave.EndTime = item.EndTime;
                if (newFlag) _context.WorkApplyLeaves.Add(applyLeave);
            }
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<List<WorkAttendanceVm>> GetYearMonthWorkAttendance(string yearMonth)
        {
            var workAttendancelist = await _context.WorkAttendances.Where(m => m.WorkYearMonth == yearMonth).OrderBy(m => m.ProjectName).ThenBy(m => m.StaffName).ToListAsync();

            var applyLeaveList = await _context.WorkApplyLeaves.Where(m => m.WorkYearMonth == yearMonth).ToListAsync();
            var delayClockList = await _context.WorkDelayClocks.Where(m => m.WorkYearMonth == yearMonth).ToListAsync();
            var outClockList = await _context.WorkOutClocks.Where(m => m.WorkYearMonth == yearMonth).ToListAsync();
            if (workAttendancelist.Count > 0)
            {
                for (int i = 0; i < workAttendancelist.Count; i++)
                {
                    var workAttendance = workAttendancelist[i];
                    var alList = applyLeaveList.Where(m => m.StaffId == workAttendance.StaffId).ToList();
                    var dcList = delayClockList.Where(m => m.StaffId == workAttendance.StaffId).ToList();
                    var ocList = outClockList.Where(m => m.StaffId == workAttendance.StaffId).ToList();
                    CheckPropertyValue(ref workAttendance, workAttendance.ClockIn01, 1);
                    CheckPropertyValue(ref workAttendance, workAttendance.ClockIn02, 2);
                    CheckPropertyValue(ref workAttendance, workAttendance.ClockIn03, 3);
                    CheckPropertyValue(ref workAttendance, workAttendance.ClockIn04, 4);
                    CheckPropertyValue(ref workAttendance, workAttendance.ClockIn05, 5);
                    CheckPropertyValue(ref workAttendance, workAttendance.ClockIn06, 6);
                    CheckPropertyValue(ref workAttendance, workAttendance.ClockIn07, 7);
                    CheckPropertyValue(ref workAttendance, workAttendance.ClockIn08, 8);
                    CheckPropertyValue(ref workAttendance, workAttendance.ClockIn09, 9);
                    CheckPropertyValue(ref workAttendance, workAttendance.ClockIn10, 10);
                    CheckPropertyValue(ref workAttendance, workAttendance.ClockIn11, 11);
                    CheckPropertyValue(ref workAttendance, workAttendance.ClockIn12, 12);
                    CheckPropertyValue(ref workAttendance, workAttendance.ClockIn13, 13);
                    CheckPropertyValue(ref workAttendance, workAttendance.ClockIn14, 14);
                    CheckPropertyValue(ref workAttendance, workAttendance.ClockIn15, 15);
                    CheckPropertyValue(ref workAttendance, workAttendance.ClockIn16, 16);
                    CheckPropertyValue(ref workAttendance, workAttendance.ClockIn17, 17);
                    CheckPropertyValue(ref workAttendance, workAttendance.ClockIn18, 18);
                    CheckPropertyValue(ref workAttendance, workAttendance.ClockIn19, 19);
                    CheckPropertyValue(ref workAttendance, workAttendance.ClockIn20, 20);
                    CheckPropertyValue(ref workAttendance, workAttendance.ClockIn21, 21);
                    CheckPropertyValue(ref workAttendance, workAttendance.ClockIn22, 22);
                    CheckPropertyValue(ref workAttendance, workAttendance.ClockIn23, 23);
                    CheckPropertyValue(ref workAttendance, workAttendance.ClockIn24, 24);
                    CheckPropertyValue(ref workAttendance, workAttendance.ClockIn25, 25);
                    CheckPropertyValue(ref workAttendance, workAttendance.ClockIn26, 26);
                    CheckPropertyValue(ref workAttendance, workAttendance.ClockIn27, 27);
                    CheckPropertyValue(ref workAttendance, workAttendance.ClockIn28, 28);
                    CheckPropertyValue(ref workAttendance, workAttendance.ClockIn29, 29);
                    CheckPropertyValue(ref workAttendance, workAttendance.ClockIn30, 30);
                    CheckPropertyValue(ref workAttendance, workAttendance.ClockIn31, 31);

                    if (alList.Count > 0)
                    {
                        foreach (var al in alList)
                        {
                            var setValue = string.Empty;
                            List<string>? valueList = null;
                            var duration = al.EndTime - al.StartTime;
                            var length = ((duration.Days + (duration.Hours == 0 ? 0.5 : 0)) * 9 + duration.Hours) / 9;
                            switch (al.LeaveType)
                            {
                                case "事假":
                                    setValue = "c";
                                    break;
                                case "病假":
                                    setValue = "d";
                                    break;
                                case "调休假":
                                    setValue = "h";
                                    break;
                                case "年假":
                                    setValue = "i";
                                    break;
                            }
                            valueList = AddValue(length, setValue);

                            if (valueList != null && valueList.Count > 0)
                            {
                                var index = 0;
                                var day = al.StartTime.Day + (al.StartTime.Hour > 12 ? 0.5 : 0);
                                var endday = day + length;
                                for (double d = day; d < endday; d += 0.5)
                                {
                                    UpdateProperty(ref workAttendance, d, valueList[index], true);
                                    index++;
                                }
                            }
                        }
                    }

                    if (dcList.Count > 0)
                    {
                        var valueList = new List<string> { "√" };
                        foreach (var dl in dcList)
                        {
                            double day = dl.DelayClockTime.Day;
                            if (dl.ApplyWorkTime.Contains("下班"))
                            {
                                day += 0.5;
                            }
                            var index = 0;
                            for (double d = day; d <= day; d += 0.5)
                            {
                                UpdateProperty(ref workAttendance, d, valueList[index]);
                                index++;
                            }

                        }
                    }
                    if (ocList.Count > 0)
                    {
                        var valueList = new List<string> { "√" };
                        foreach (var ol in ocList)
                        {
                            double day = ol.OutClockDateTime.Day + (ol.OutClockDateTime.Hour > 14 ? 0.5 : 0);
                            var index = 0;
                            for (double d = day; d <= day; d += 0.5)
                            {
                                UpdateProperty(ref workAttendance, d, valueList[index]);
                                index++;
                            }
                        }
                    }
                }
            }

            var result = workAttendancelist.Select(m => m.ToViewModel()).ToList();
            var startBillDate = DateTime.ParseExact(yearMonth + "01", "yyyyMMdd", CultureInfo.InvariantCulture, DateTimeStyles.None);
            var endBillDate = startBillDate.AddMonths(1);
            var dailyWorkList = _context.ProjectDailyWorks.Where(m => m.BillDate >= DateOnly.FromDateTime(startBillDate) && m.BillDate < DateOnly.FromDateTime(endBillDate)).ToList();
            foreach (var item in result)
            {
                var dailyWorks = dailyWorkList.Where(m => m.StaffId == item.StaffId).ToList();
                if (dailyWorks.Count > 0)
                    item.ProcessDays.AddRange(dailyWorks.Select(m => $"{m.BillDate.Day:00}").OrderBy(m => m).ToList());
            }
            return result;
        }

        private List<string> AddValue(double length,string value)
        {
            var list = new List<string>();
            for (double i = 0; i < length * 2; i++)
            {
                list.Add(value);
            }
            return list;
        }
        private void CheckPropertyValue(ref WorkAttendance wa, string value, int day)
        {
            double endday = 0;
            var setValue = string.Empty;
            var valueList = new List<string>();
            if (value.Contains("休息"))
            {
                endday = day + 1;
                valueList.Add(string.Empty);
                valueList.Add(string.Empty);
            }
            else if (value.Contains("正常"))
            {
                endday = day + 1;
                valueList.Add("√");
                valueList.Add("√");
            }            
            else if (value.Contains("旷工"))
            {
                endday = day + 1;
                valueList.Add("e");
                valueList.Add("e");
            }           
            else if (value.Contains("--"))
            {
                endday = day + 1;
                valueList.Add("");
                valueList.Add("");
            }
            else
            {
                if (value.Split(";").ToList().Count() > 1)
                {
                    endday = day + 1;
                    valueList.Add("");
                    valueList.Add("");
                    if (value.Contains("迟到"))
                    {                                               
                        valueList[0] = "a";                        
                    }
                    if (value.Contains("早退"))
                    {                    
                        valueList[1] = "b";                        
                    }
                    if (value.Contains("缺卡"))
                    {
                        if (valueList[0] == "")
                            valueList[0] = "缺卡";
                        else if (valueList[1] == "")
                            valueList[1] = "缺卡";
                    }
                    if (value.Contains("异常"))
                    {
                        if (valueList[0] == "") valueList[0] = "m";
                        else valueList[0] += "; m";
                    }
                    if (valueList[0] == "")
                        valueList[0] = "√";
                    if (valueList[1] == "")
                        valueList[1] = "√";
                }
                else
                {
                    if (value.Contains("迟到"))
                    {
                        endday = day + 1;
                        valueList.Add("a");
                        valueList.Add("√");
                    }
                    else if (value.Contains("早退"))
                    {
                        endday = day + 1;
                        valueList.Add("√");
                        valueList.Add("b");
                    }
                    else if (value.Contains("缺卡"))
                    {
                        endday = day + 1;
                        valueList.Add("缺卡");
                        valueList.Add("√");

                    }
                    else if (value.Contains("异常"))
                    {
                        endday = day + 1;
                        valueList.Add("m");
                        valueList.Add("m");
                    }
                }                
            }
            

            if (valueList.Count > 0)
            {
                var index = 0;
                for (double i = day; i < endday; i += 0.5)
                {
                    UpdateProperty(ref wa, i, valueList[index]);
                    index++;
                }
            }
                      
        }

        private void UpdateProperty(ref WorkAttendance wa, double day, string value, bool missingCheck = false)
        {
            var missingFix = "√";
            switch (day)
            {
                case 1:
                    wa.ClockIn01 = value;
                    break;                
                case 2:
                    wa.ClockIn02 = value;
                    break;
                case 3:
                    wa.ClockIn03 = value;
                    break;
                case 4:
                    wa.ClockIn04 = value;
                    break;
                case 5:
                    wa.ClockIn05 = value;
                    break;
                case 6:
                    wa.ClockIn06 = value;
                    break;
                case 7:
                    wa.ClockIn07 = value;
                    break;
                case 8:
                    wa.ClockIn08 = value;
                    break;
                case 9:
                    wa.ClockIn09 = value;
                    break;
                case 10:
                    wa.ClockIn10 = value;
                    break;
                case 11:
                    wa.ClockIn11 = value;
                    break;
                case 12:
                    wa.ClockIn12 = value;
                    break;
                case 13:
                    wa.ClockIn13 = value;
                    break;
                case 14:
                    wa.ClockIn14 = value;
                    break;
                case 15:
                    wa.ClockIn15 = value;
                    break;
                case 16:
                    wa.ClockIn16 = value;
                    break;
                case 17:
                    wa.ClockIn17 = value;
                    break;
                case 18:
                    wa.ClockIn18 = value;
                    break;
                case 19:
                    wa.ClockIn19 = value;
                    break;
                case 20:
                    wa.ClockIn20 = value;
                    break;
                case 21:
                    wa.ClockIn21 = value;
                    break;
                case 22:
                    wa.ClockIn22 = value;
                    break;
                case 23:
                    wa.ClockIn23 = value;
                    break;
                case 24:
                    wa.ClockIn24 = value;
                    break;
                case 25:
                    wa.ClockIn25 = value;
                    break;
                case 26:
                    wa.ClockIn26 = value;
                    break;
                case 27:
                    wa.ClockIn27 = value;
                    break;
                case 28:
                    wa.ClockIn28 = value;
                    break;
                case 29:
                    wa.ClockIn29 = value;
                    break;
                case 30:
                    wa.ClockIn30 = value;
                    break;
                case 31:
                    wa.ClockIn31 = value;
                    break;
                case 1.5:
                    if (missingCheck && wa.ClockIn01.Contains("缺卡")) wa.ClockIn01 = missingFix;
                    wa.ClockOut01 = value;
                    break;
                case 2.5:
                    if (missingCheck && wa.ClockIn02.Contains("缺卡")) wa.ClockIn02 = missingFix;
                    wa.ClockOut02 = value;
                    break;
                case 3.5:
                    if (missingCheck && wa.ClockIn03.Contains("缺卡")) wa.ClockIn03 = missingFix;
                    wa.ClockOut03 = value;
                    break;
                case 4.5:
                    if (missingCheck && wa.ClockIn04.Contains("缺卡")) wa.ClockIn04 = missingFix;
                    wa.ClockOut04 = value;
                    break;
                case 5.5:
                    if (missingCheck && wa.ClockIn05.Contains("缺卡")) wa.ClockIn05 = missingFix;
                    wa.ClockOut05 = value;
                    break;
                case 6.5:
                    if (missingCheck && wa.ClockIn06.Contains("缺卡")) wa.ClockIn06 = missingFix;
                    wa.ClockOut06 = value;
                    break;
                case 7.5:
                    if (missingCheck && wa.ClockIn07.Contains("缺卡")) wa.ClockIn07 = missingFix;
                    wa.ClockOut07 = value;
                    break;
                case 8.5:
                    if (missingCheck && wa.ClockIn08.Contains("缺卡")) wa.ClockIn08 = missingFix;
                    wa.ClockOut08 = value;
                    break;
                case 9.5:
                    if (missingCheck && wa.ClockIn09.Contains("缺卡")) wa.ClockIn09 = missingFix;
                    wa.ClockOut09 = value;
                    break;
                case 10.5:
                    if (missingCheck && wa.ClockIn10.Contains("缺卡")) wa.ClockIn10 = missingFix;
                    wa.ClockOut10 = value;
                    break;
                case 11.5:
                    if (missingCheck && wa.ClockIn11.Contains("缺卡")) wa.ClockIn11 = missingFix;
                    wa.ClockOut11 = value;
                    break;
                case 12.5:
                    if (missingCheck && wa.ClockIn12.Contains("缺卡")) wa.ClockIn12 = missingFix;
                    wa.ClockOut12 = value;
                    break;
                case 13.5:
                    if (missingCheck && wa.ClockIn13.Contains("缺卡")) wa.ClockIn13 = missingFix;
                    wa.ClockOut13 = value;
                    break;     
                case 14.5:
                    if (missingCheck && wa.ClockIn14.Contains("缺卡")) wa.ClockIn14 = missingFix;
                    wa.ClockOut14 = value;
                    break;     
                case 15.5:
                    if (missingCheck && wa.ClockIn15.Contains("缺卡")) wa.ClockIn15 = missingFix;
                    wa.ClockOut15 = value;
                    break;     
                case 16.5:
                    if (missingCheck && wa.ClockIn16.Contains("缺卡")) wa.ClockIn16 = missingFix;
                    wa.ClockOut16 = value;
                    break;     
                case 17.5:
                    if (missingCheck && wa.ClockIn17.Contains("缺卡")) wa.ClockIn17 = missingFix;
                    wa.ClockOut17 = value;
                    break;     
                case 18.5:
                    if (missingCheck && wa.ClockIn18.Contains("缺卡")) wa.ClockIn18 = missingFix;
                    wa.ClockOut18 = value;
                    break;     
                case 19.5:
                    if (missingCheck && wa.ClockIn19.Contains("缺卡")) wa.ClockIn19 = missingFix;
                    wa.ClockOut19 = value;
                    break;     
                case 20.5:
                    if (missingCheck && wa.ClockIn20.Contains("缺卡")) wa.ClockIn20 = missingFix;
                    wa.ClockOut20 = value;
                    break;
                case 21.5:
                    if (missingCheck && wa.ClockIn21.Contains("缺卡")) wa.ClockIn21 = missingFix;
                    wa.ClockOut21 = value;
                    break;
                case 22.5:
                    if (missingCheck && wa.ClockIn22.Contains("缺卡")) wa.ClockIn22 = missingFix;
                    wa.ClockOut22 = value;
                    break;
                case 23.5:
                    if (missingCheck && wa.ClockIn23.Contains("缺卡")) wa.ClockIn23 = missingFix;
                    wa.ClockOut23 = value;
                    break;
                case 24.5:
                    if (missingCheck && wa.ClockIn24.Contains("缺卡")) wa.ClockIn24 = missingFix;
                    wa.ClockOut24 = value;
                    break;
                case 25.5:
                    if (missingCheck && wa.ClockIn25.Contains("缺卡")) wa.ClockIn25 = missingFix;
                    wa.ClockOut25 = value;
                    break;
                case 26.5:
                    if (missingCheck && wa.ClockIn26.Contains("缺卡")) wa.ClockIn26 = missingFix;
                    wa.ClockOut26 = value;
                    break;
                case 27.5:
                    if (missingCheck && wa.ClockIn27.Contains("缺卡")) wa.ClockIn27 = missingFix;
                    wa.ClockOut27 = value;
                    break;
                case 28.5:
                    if (missingCheck && wa.ClockIn28.Contains("缺卡")) wa.ClockIn28 = missingFix;
                    wa.ClockOut28 = value;
                    break;
                case 29.5:
                    if (missingCheck && wa.ClockIn29.Contains("缺卡")) wa.ClockIn29 = missingFix;
                    wa.ClockOut29 = value;
                    break;
                case 30.5:
                    if (missingCheck && wa.ClockIn30.Contains("缺卡")) wa.ClockIn30 = missingFix;
                    wa.ClockOut30 = value;
                    break;
                case 31.5:
                    if (missingCheck && wa.ClockIn31.Contains("缺卡")) wa.ClockIn31 = missingFix;
                    wa.ClockOut31 = value;
                    break;
            }
        }

    }
}
