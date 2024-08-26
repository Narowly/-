using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ProjectService.Db;
using ProjectService.ViewModels;
using ProjectViewModels;

namespace ProjectService.Services
{
    public class ProjectBonusService : UserService
    {
        private readonly ProjectDbContext _context;
        public ProjectBonusService(IHttpContextAccessor httpContextAccessor, ProjectDbContext context) : base(httpContextAccessor)
        {
            _context = context;
        }

        public async Task<bool> SaveBonus(List<ProjectBonusVm> list)
        {
            if (list.Count > 0)
            {
                var projectProcessIds = list.Select(m=>m.ProjectProcessId).ToList();
                var RemoveBonus = _context.ProjectBonus.Where(m => projectProcessIds.Contains(m.ProjectProcessId)).ToList();
                _context.ProjectBonus.RemoveRange(RemoveBonus);
                foreach (var vm in list)
                {
                    var bonus = new ProjectBonu();
                    bonus.CreateBy = GetUserId();
                    bonus.CreateTime = DateTime.Now;
                    bonus.Bonus = vm.Bonus;
                    bonus.Remarks = vm.Remarks;
                    bonus.ProjectProcessId = vm.ProjectProcessId.Value;
                    bonus.Workload = vm.Workload;
                    _context.ProjectBonus.Add(bonus);
                }
            }
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<List<ProjectBonu>?> GetBonusList(Guid projectId)
        {
            var project = await _context.Projects.FirstOrDefaultAsync(m=>m.ProjectId == projectId);           
            var bonuslist = project.ProjectProcesses.SelectMany(m => m.ProjectBonus).ToList();
            return bonuslist;            
        }

        public async Task<ProjectBonusEx?> GetBonusEx(Guid projectId)
        {
            return await _context.ProjectBonusexes.FirstOrDefaultAsync(m => m.ProjectId == projectId);
        }

        public async Task<bool> SaveBonusEx(ProjectBonusExVm vm)
        {
            ProjectBonusEx? bonus;
            if (vm.Id == null)
            {
                bonus = new ProjectBonusEx
                {
                    ProjectId = vm.ProjectId,
                    Bonus = vm.Bonus,
                    Penalty = vm.Penalty,
                    Rewards = vm.Rewards,
                    PlanPersonDays = vm.PlanPersonDays,
                    CreateBy = GetUserId(),
                    CreateTime = DateTime.Now
                };
                _context.ProjectBonusexes.Add(bonus);
            }
            else
            {
                bonus = await _context.ProjectBonusexes.FirstOrDefaultAsync(m => m.Id == vm.Id);
                if (bonus == null)
                {
                    return false;
                }
                bonus.PlanPersonDays = vm.PlanPersonDays;
                bonus.Bonus = vm.Bonus;
                bonus.Penalty = vm.Penalty;
                bonus.Rewards = vm.Rewards;
                bonus.UpdateBy = GetUserId();
                bonus.UpdateTime = DateTime.Now;
            }
            await _context.SaveChangesAsync();
            return true;
        }

        public double? CalculateBonusEx(ProjectBonusExVm vm)
        {
            double bonus = vm.Bonus.Value;
            if (vm.VerifyPersonDays != null)
            {
                if (vm.PlanPersonDays < vm.VerifyPersonDays)
                {
                    bonus -= (vm.VerifyPersonDays.Value - vm.PlanPersonDays.Value) * vm.Penalty.Value;
                }
                else if (vm.PlanPersonDays > vm.VerifyPersonDays)
                {
                    bonus += (vm.PlanPersonDays.Value - vm.VerifyPersonDays.Value) * vm.Rewards.Value;
                }
            }
            else
            {
                return null;
            }
            
            return bonus;
        }

        public async Task<List<StaffAchievementBonusVm>> AchievementBonusCalculate(AchievementBonusCalculateReq req)
        {

            var query = _context.ProjectDailyWorks.AsQueryable();

            if (req.ProjectId != null)
            {
                query = query.Where(m => m.ProjectProcess.ProjectId == req.ProjectId);
            }
            if (req.Staff != null)
            {
                query = query.Where(m => m.StaffId == req.Staff);
            }
            if (req.StartDate != null)
            {
                query = query.Where(m => m.BillDate >= DateOnly.FromDateTime(req.StartDate.Value));
            }
            if (req.EndDate != null)
            {
                query = query.Where(m => m.BillDate < DateOnly.FromDateTime(req.EndDate.Value));
            }
            if (req.YearMonth != null)
            {
                //var currentMonth = new DateTime(req.YearMonth.Value.Year, req.YearMonth.Value.Month, 1);
                //var addOneMonth = req.YearMonth.Value.AddMonths(1);
                //var nextMonth = new DateTime(addOneMonth.Year, addOneMonth.Month, 1);
                query = query.Where(m => m.BillDate >= DateOnly.FromDateTime(req.YearMonth.Value) && m.BillDate < DateOnly.FromDateTime(req.YearMonth.Value.AddMonths(1)));
            }
            var queryList = await query.ToListAsync();
            var dailyProcessList = queryList.SelectMany(m => m.ProjectProcess.ProjectDailyProcesses).Distinct().ToList();
            var projectBonusList = queryList.SelectMany(m => m.ProjectProcess.ProjectBonus).Distinct().ToList();
            var result = new List<StaffAchievementBonusVm>();
            foreach (var dailyWork in queryList)
            {
                var vm = result.FirstOrDefault(m => m.ProjectProcessId == dailyWork.ProjectProcessId && m.StaffId == dailyWork.StaffId);
                if (vm != null)
                {
                    vm.SumWorkload += dailyWork.Workload;
                    vm.UsedDays++;
                }
                else
                {
                    vm = new StaffAchievementBonusVm
                    {
                        StaffId = dailyWork.StaffId,
                        ProjectProcessId = dailyWork.ProjectProcessId,
                        SumWorkload = dailyWork.Workload,
                        UsedDays = 1,
                        ProjectName = dailyWork.ProjectProcess.Project.ProjectName,
                        StaffName = dailyWork.Staff.StaffName,
                        ProcessShowName = $"{dailyWork.ProjectProcess.ProcessUnit.Process.ProcessName}({dailyWork.ProjectProcess.ProcessUnit.Unit.UnitName})"
                    };
                    result.Add(vm);
                }
            }
            foreach (var vm in result)
            {
                var processBonusList = projectBonusList.Where(m => m.ProjectProcessId == vm.ProjectProcessId).OrderBy(m => m.Workload).ToList();
                if (processBonusList.Count > 0)
                {
                    vm.BonusList = processBonusList.Select(m => m.ToViewModel()).ToList();
                    for (int i = 0; i < processBonusList.Count; i++)
                    {
                        double exceed = 0;
                        if (processBonusList[i].Workload < vm.AverageWorkloadPerDay)
                        {
                            exceed = vm.AverageWorkloadPerDay - processBonusList[i].Workload;
                            if (i + 1 < processBonusList.Count)
                            {
                                if (vm.AverageWorkloadPerDay >= processBonusList[i + 1].Workload)
                                {
                                    exceed = processBonusList[i + 1].Workload - processBonusList[i].Workload;
                                    vm.SumBonus += (decimal)exceed * processBonusList[i].Bonus * (decimal)vm.UsedDays;
                                }
                                else
                                {
                                    vm.SumBonus += (decimal)exceed * processBonusList[i].Bonus * (decimal)vm.UsedDays;
                                }
                            }
                            else
                            {
                                vm.SumBonus += (decimal)exceed * processBonusList[i].Bonus * (decimal)vm.UsedDays;
                            }
                        }
                    }
                }
            }
            return result.Where(m => m.SumBonus > 0).OrderBy(m => m.ProcessShowName).ThenByDescending(m => m.SumBonus).ToList();
        }

        public async Task<ProjectAchievementBonusVm?> ProjectBonusCalculate(Guid projectId)
        {
            var result = new ProjectAchievementBonusVm();
            var project = await _context.Projects.FirstOrDefaultAsync(m => m.ProjectId == projectId);
            if (project == null) return null;

            var bonusEx = project.ProjectBonusexes.FirstOrDefault();
            if (bonusEx != null && bonusEx.Rewards != null && bonusEx.Penalty != null && bonusEx.PlanPersonDays != null && bonusEx.Bonus!=null)
            {
                result.PlanPersonDays = project.PlanPersonDays ?? 0;
                result.StaffName = project.ProjectManager.StaffName;
                result.ProjectName = project.ProjectName;
                result.ProjectId = projectId;
                result.StaffId = project.ProjectManager.StaffId;
                result.Bonus = bonusEx.Bonus.Value;
                var dailyWorkList = project.ProjectProcesses.Where(m => m.ProjectDailyWorks != null).SelectMany(m => m.ProjectDailyWorks).ToList();
                result.VerifyPersonDays = dailyWorkList.GroupBy(m => new { m.BillDate, m.StaffId }).Count();
                var days = bonusEx.PlanPersonDays - result.VerifyPersonDays;
                if (days >= 0)
                {
                    result.Bonus += (double)days * bonusEx.Rewards.Value;
                }
                else
                {
                    result.Bonus -= (double)days * bonusEx.Penalty.Value;
                }
                return result;
            }
            else
            {
                return null;
            }
        }
    }
}
