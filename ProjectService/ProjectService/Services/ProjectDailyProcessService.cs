using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ProjectService.Db;
using ProjectViewModels;

namespace ProjectService.Services
{
    public class ProjectDailyProcessService : UserService
    {
        private readonly ProjectDbContext _context;
        public ProjectDailyProcessService(ProjectDbContext context, IHttpContextAccessor httpContextAccessor) : base(httpContextAccessor)
        {
            _context = context;
        }

        public async Task<bool> SaveProjectDailyProcess(List<ProjectDailyProcessVm> list)
        {
            foreach (var vm in list)
            {
                ProjectDailyProcess dailyProcess;
                if (vm.Id != null)
                {
                    dailyProcess = await _context.ProjectDailyProcesses.FirstAsync(m => m.Id == vm.Id);
                    dailyProcess.StartDate = vm.StartDate;
                    dailyProcess.DailyWorkload = vm.DailyWorkload;
                    dailyProcess.Remarks = vm.Remarks;
                    dailyProcess.UpdateBy = GetUserId();
                    dailyProcess.UpdateTime = DateTime.Now;
                }
                else
                {
                    dailyProcess = new ProjectDailyProcess
                    {
                        DailyWorkload = vm.DailyWorkload,
                        ProjectProcessId = vm.ProjectProcessId,
                        Remarks = vm.Remarks,
                        StartDate = vm.StartDate,
                        CreateBy = GetUserId(),
                        CreateTime = DateTime.Now
                    };
                    _context.ProjectDailyProcesses.Add(dailyProcess);
                }   
            }
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<Dictionary<DateTime, List<ProjectDailyProcess>>?> GetProjectDailyProcessHistory(Guid projectId)
        {
            Dictionary<DateTime, List<ProjectDailyProcess>>? result = null;
            var processIds = await _context.ProjectProcesses.Where(m => m.ProjectId == projectId).Select(m => m.Id).ToListAsync();
            if (processIds != null)
            {
                Dictionary<DateTime, List<ProjectDailyProcess>> history = (_context.ProjectDailyProcesses.Where(m => processIds.Contains(m.ProjectProcessId)).GroupBy(m => m.StartDate)).ToDictionary(m => m.Key, m => m.ToList());
                if (history.Count == 0)
                {
                    var processes = _context.ProjectProcesses.Where(m => processIds.Contains(m.Id)).ToList();
                    var dailyProcesses = new List<ProjectDailyProcess>();
                    foreach (var p in processes)
                    {
                        dailyProcesses.Add(new ProjectDailyProcess
                        {
                            ProjectProcessId = p.Id,
                            ProjectProcess = p,
                            StartDate = DateTime.Now.Date,
                            Remarks = string.Empty
                        });
                    }
                    history.Add(DateTime.Now.Date, dailyProcesses);                    
                }
                return history;
            }
            return result;
        }

        public async Task<bool> UpdateProjectDailyProcess(List<ProjectDailyProcessVm> list)
        {
            foreach (var vm in list)
            {
                ProjectDailyProcess process;
                if (vm.Id != null)
                {
                    process = _context.ProjectDailyProcesses.First(m => m.Id == vm.Id);
                    process.StartDate = vm.StartDate;
                    process.Remarks = vm.Remarks;
                    process.DailyWorkload = vm.DailyWorkload;
                    process.UpdateBy = GetUserId();
                    process.UpdateTime = DateTime.Now;
                }
                else
                {
                    process = new ProjectDailyProcess();
                    process.StartDate = vm.StartDate;
                    process.Remarks = vm.Remarks;
                    process.DailyWorkload = vm.DailyWorkload;
                    process.CreateBy = GetUserId();
                    process.CreateTime = DateTime.Now;
                    process.ProjectProcessId = vm.ProjectProcessId;
                    _context.ProjectDailyProcesses.Add(process);
                }                
            }
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> RemoveProjectDailyProcess(List<ProjectDailyProcessVm> list)
        {
            foreach (var vm in list)
            {
                if (vm.Id != null)
                {
                    var process = _context.ProjectDailyProcesses.First(m => m.Id == vm.Id);
                    _context.ProjectDailyProcesses.Remove(process);
                }
                else
                {
                    return true;
                }                
            }
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
