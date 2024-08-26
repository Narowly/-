using Microsoft.EntityFrameworkCore;
using ProjectService.Db;
using ProjectService.Helper;
using ProjectService.ViewModels;
using ProjectViewModels;

namespace ProjectService.Services
{
    public class ProjectDailyWorkService : UserService
    {
        private readonly ProjectDbContext _context;
        public ProjectDailyWorkService(ProjectDbContext context, IHttpContextAccessor httpContextAccessor) : base(httpContextAccessor)
        {
            _context = context;
        }

        public async Task<bool> SaveProjectDailyWork(ProjectDailyWorkVm vm)
        {
            ProjectDailyWork? dailyWork;
            if (vm.ProjectProcessId == null || vm.StaffId == null) return false;
            if (vm.Id == null)
            {
                dailyWork = _context.ProjectDailyWorks.FirstOrDefault(m => m.ProjectProcessId == vm.ProjectProcessId && m.StaffId == vm.StaffId && m.BillDate == vm.BillDate);
                if (dailyWork == null)
                {
                    dailyWork = new ProjectDailyWork
                    {
                        Id = Guid.NewGuid(),
                        ProjectProcessId = vm.ProjectProcessId.Value,
                        StaffId = vm.StaffId.Value,
                        Workload = vm.Workload,
                        BillDate = vm.BillDate,
                        CreateBy = GetUserId(),
                        CreateTime = DateTime.Now,
                        Remarks = vm.Remarks
                    };
                    _context.ProjectDailyWorks.Add(dailyWork);
                }
                else
                {
                    UpdateDto(ref dailyWork, vm);
                }
            }
            else
            {
                dailyWork = _context.ProjectDailyWorks.FirstOrDefault(m => m.Id == vm.Id);
                if (dailyWork == null) return false;
                UpdateDto(ref dailyWork, vm);
            }
            await _context.SaveChangesAsync();
            return true;
        }
        private void UpdateDto(ref ProjectDailyWork dailyWork, ProjectDailyWorkVm vm)
        {
            dailyWork.Workload = vm.Workload;
            dailyWork.BillDate = vm.BillDate;
            dailyWork.UpdateBy = GetUserId();
            dailyWork.UpdateTime = DateTime.Now;
            dailyWork.Remarks = vm.Remarks;
        }

        public async Task<PaginatedList<ProjectDailyWork>> PaginatedSearchProjectDailyWork(ProjectWithStaffReq req)
        {
            var query = _context.ProjectDailyWorks.AsQueryable();
            if (req.ProjectId != null)
            {
                query = query.Where(m => m.ProjectProcess.ProjectId == req.ProjectId);
            }
            else
            {
                if(req.ProjectManagerId != null)
                {
                    query = query.Where(m=>m.ProjectProcess.Project.ProjectManagerId== req.ProjectManagerId);
                }
                if (req.Staff != null)
                {
                    query = query.Where(m => m.StaffId == req.Staff);
                }
                if (req.Content != null)
                {
                    query = query.Where(m => m.ProjectProcess.Project.ProjectName.Contains(req.Content) || m.ProjectProcess.Project.Contract.ContractNumber.Contains(req.Content));
                }
                
                
            }
            if (req.StartDate != null)
            {
                query = query.Where(m => m.BillDate >= DateOnly.FromDateTime(req.StartDate.Value.Date));
            }
            if (req.EndDate != null)
            {
                query = query.Where(m => m.BillDate <= DateOnly.FromDateTime(req.EndDate.Value.Date));
            }
            return await query.OrderByDescending(m => m.BillDate).ThenBy(m=>m.ProjectProcessId).ThenByDescending(m=>m.Workload).AsNoTracking().ToPaginatedListAsync(req.Pagination);
        }

        public async Task<ProjectDailyWork?> GetDailyWorkById(Guid id)
        {
            return await _context.ProjectDailyWorks.FirstOrDefaultAsync(m => m.Id == id);
        }

        public async Task<PaginatedList<DailyWorkSummaryVm>> GetDailyWorkSummary(ProjectWithStaffReq req)
        {
            var summary = new List<DailyWorkSummaryVm>();
            var projectQuery = _context.Projects.AsQueryable();
            if (req.ProjectManagerId != null)
            {
                projectQuery = projectQuery.Where(m => m.ProjectManagerId == req.ProjectManagerId);
            }
            if (req.ProjectId != null)
            {
                projectQuery = projectQuery.Where(m => m.ProjectId == req.ProjectId);
            }

            var projects = await projectQuery.ToListAsync();
            foreach (var project in projects)
            {
                foreach (var process in project.ProjectProcesses)
                {
                    var dailyWorkQuery = process.ProjectDailyWorks.AsQueryable();
                    if (req.StartDate != null) { dailyWorkQuery = dailyWorkQuery.Where(m => m.BillDate >= DateOnly.FromDateTime(req.StartDate.Value)); }
                    if (req.EndDate != null) { dailyWorkQuery = dailyWorkQuery.Where(m => m.BillDate <= DateOnly.FromDateTime(req.EndDate.Value)); }
                    if (req.Staff != null) { dailyWorkQuery = dailyWorkQuery.Where(m => m.StaffId == req.Staff); }
                    var dailyWorkList = dailyWorkQuery.GroupBy(g => g.StaffId).ToList();
                    foreach (var staffDailyWorkList in dailyWorkList)
                    {
                        summary.Add(new DailyWorkSummaryVm
                        {
                            ProcessName = process.ProcessUnit.Process.ProcessName,
                            ProjectName = project.ProjectName,
                            StaffCard = staffDailyWorkList.Select(m => m.Staff.StaffCard).FirstOrDefault(),
                            StaffName = staffDailyWorkList.Select(m => m.Staff.StaffName).FirstOrDefault(),
                            SumWorkload = staffDailyWorkList.Sum(m => m.Workload),
                            UnitName = process.ProcessUnit.Unit.UnitName
                        });
                    }
                }
            }
            return summary.OrderBy(m=>m.ProcessName).ThenByDescending(m=>m.SumWorkload).AsQueryable().ToPaginatedList(req.Pagination);

        }

        public async Task<bool> SaveBatchDailyWork(List<BatchProjectDailyWorkVm> list)
        {
            var projectId = list.FirstOrDefault()?.ProjectId;
            if (projectId == null) { return false; }
            var projectProcessList = _context.ProjectProcesses.Where(m=>m.ProjectId == projectId).ToList();
            foreach (var vm in list)
            {
                if (vm.Workload == 0) continue;
                var projectProcess = projectProcessList.FirstOrDefault(m => m.ProcessUnitId == vm.ProcessUnit.Id);
                var dailyWork = _context.ProjectDailyWorks.FirstOrDefault(m => m.StaffId == vm.Staff.StaffId && m.BillDate == vm.BillDate && m.ProjectProcessId == projectProcess.Id);
                if (dailyWork == null)
                {
                    dailyWork = new ProjectDailyWork
                    {
                        Id = Guid.NewGuid(),
                        ProjectProcessId = projectProcess.Id,
                        StaffId = vm.Staff.StaffId,
                        Workload = vm.Workload,
                        BillDate = vm.BillDate,
                        CreateBy = GetUserId(),
                        CreateTime = DateTime.Now,
                        Remarks = vm.Remarks
                    };
                    _context.ProjectDailyWorks.Add(dailyWork);
                }
                else
                {
                    dailyWork.Workload = vm.Workload;
                    dailyWork.UpdateBy = GetUserId();
                    dailyWork.UpdateTime = DateTime.Now;
                    dailyWork.Remarks = vm.Remarks;
                }
            }
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> SaveExcelDailyWork(List<ProjectDailyWorkExcelVm> list)
        {
            var projectList = new List<Project>();
            var staffList = new List<Staff>();
            var projectProcessList = new List<ProjectProcess>();
            foreach (var vm in list)
            {
                var project = projectList.FirstOrDefault(m => m.Contract.ContractNumber == vm.ContractNumber);
                if (project == null)
                {
                    project = await _context.Projects.FirstOrDefaultAsync(m => m.Contract.ContractNumber == vm.ContractNumber);
                    if (project == null)
                    {
                        LogManager.Log.Error($"excel导入报量：{vm.ContractNumber}没有找到对应项目");
                        continue;
                    }
                    projectList.Add(project);
                    var projectProcesses = await _context.ProjectProcesses.Where(m => m.ProjectId == project.ProjectId).ToListAsync();
                    if (projectProcesses != null && projectProcesses.Count > 0)
                    {
                        projectProcessList.AddRange(projectProcesses);
                    }                    
                }
                var staff = staffList.FirstOrDefault(m => m.StaffCard == vm.StaffCard);
                if (staff == null)
                {
                    staff = await _context.Staff.FirstOrDefaultAsync(m => m.StaffCard == vm.StaffCard);
                    if(staff == null)
                    {
                        LogManager.Log.Error($"excel导入报量：{vm.StaffCard}员工没有找到");
                        continue;
                    }
                    staffList.Add(staff);
                }
                var projectProcess = projectProcessList.FirstOrDefault(m => m.ProcessUnit.Process.ProcessName == vm.ProcessName && m.ProcessUnit.Unit.UnitName == vm.UnitName);
                if (projectProcess == null)
                {
                    LogManager.Log.Error($"excel导入报量：{vm.ContractNumber}:{vm.ProcessName}{vm.UnitName}未找到");
                    continue;
                }
                var dailyWork = await _context.ProjectDailyWorks.FirstOrDefaultAsync(m => m.ProjectProcessId == projectProcess.Id && m.BillDate == vm.BillDate && m.StaffId == staff.StaffId);
                if (dailyWork == null)
                {
                    _context.Add(new ProjectDailyWork
                    {
                        Id = Guid.NewGuid(),
                        ProjectProcessId = projectProcess.Id,
                        StaffId = staff.StaffId,
                        Workload = vm.Workload,
                        BillDate = vm.BillDate,
                        CreateBy = GetUserId(),
                        CreateTime = DateTime.Now,
                        Remarks = vm.Remarks
                    });
                }
                else
                {
                    dailyWork.Workload = vm.Workload;
                    dailyWork.Remarks = vm.Remarks;
                    dailyWork.UpdateBy = GetUserId();
                    dailyWork.UpdateTime = DateTime.Now;
                }
            }
            await _context.SaveChangesAsync();
            return true;
        }

        
    }
}
