using Microsoft.Win32;
using Project.Common;
using ProjectViewModels;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Services.DataServices
{
    public class WorkAttendanceService
    {
        private readonly IRestClient restClient;
        public WorkAttendanceService(IRestClient restClient)
        {
            this.restClient = restClient;
        }
        public async Task<bool> InsertYearMonthWorkAttendanceExcel(List<WorkAttendanceVm> list)
        {
            return await RestClientHelper.ExecuteRequestAsync<bool>(restClient, Method.Post, ApiSettings.InsertYearMonthWorkAttendanceExcel, body: list);
        }
        public async Task<bool> InsertDelayClockExcel(List<WorkDelayClockVm> list)
        {
            return await RestClientHelper.ExecuteRequestAsync<bool>(restClient, Method.Post, ApiSettings.InsertDelayClockExcel, body: list);
        }

        public async Task<bool> InsertOutClockExcel(List<WorkOutClockVm> list)
        {
            return await RestClientHelper.ExecuteRequestAsync<bool>(restClient, Method.Post, ApiSettings.InsertOutClockExcel, body: list);
        }

        public async Task<bool> InsertApplyLeaveExcel(List<WorkApplyLeaveVm> list)
        {
            return await RestClientHelper.ExecuteRequestAsync<bool>(restClient, Method.Post, ApiSettings.InsertApplyLeaveExcel, body: list);
        }

        public async Task<List<WorkAttendanceVm>> GetYearMonthWorkAttendance(string yearMonth)
        {
            return await RestClientHelper.ExecuteRequestAsync<List<WorkAttendanceVm>>(restClient, Method.Get, ApiSettings.GetYearMonthWorkAttendance, queryParameters:
                new Dictionary<string, string>
                {
                    { nameof(yearMonth), yearMonth } });
        }

        public async Task<bool> ExportYearMonthExcel(DateTime yearMonth)
        {            
            var days = DateTime.DaysInMonth(yearMonth.Year,yearMonth.Month);
            var list = await GetYearMonthWorkAttendance(yearMonth.ToString("yyyyMM"));
            var headersList = new List<List<string>>();
            var headers1 = new List<string>() { "序号","日","项目"};
            for (int i = 1; i <= days; i++)
            {
                headers1.Add(i.ToString());
            }
            
            headers1.AddRange([ "工作天数","报量天数","异常","备注"]);
            var headers2 = new List<string>();

            return false;
            
            
        }
    }
}
