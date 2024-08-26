using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ProjectService.Db;
using ProjectViewModels;

namespace ProjectService.Services
{
    public class EarlyWarningService : UserService
    {
        private readonly ProjectDbContext _context;
        public EarlyWarningService(ProjectDbContext context, IHttpContextAccessor httpContextAccessor) : base(httpContextAccessor)
        {
            _context = context;
        }

        public async Task<List<ProjectEarlyWarning>> GetProjectEarlyWarnings(Guid projectId)
        {
            return await _context.ProjectEarlyWarnings.Where(m => m.ProjectId == projectId).ToListAsync();
        }
        public async Task<bool> SaveProjectEarlyWarnings(List<ProjectEarlyWarningVm> list)
        {
            foreach (var vm in list)
            {
                var warning = _context.ProjectEarlyWarnings.FirstOrDefault(m => m.ProjectId == vm.ProjectId && m.WarningType == vm.WarningType);
                if (warning != null)
                {
                    warning.WarningValue = vm.WarningValue ?? 0;
                    warning.UpdateBy = GetUserId();
                    warning.UpdateTime = DateTime.Now;
                }
                else
                {
                    var newWarning = new ProjectEarlyWarning
                    {
                        ProjectId = vm.ProjectId,
                        WarningType = vm.WarningType,
                        WarningValue = vm.WarningValue ?? 0,
                        CreateBy = GetUserId(),
                        CreateTime = DateTime.Now
                    };
                    _context.ProjectEarlyWarnings.Add(newWarning);
                }
            }
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<PaginatedList<EarlyWarningHistory>> PaginatedWarningHistory(ProjectReqs req)
        {
            var query = _context.EarlyWarningHistories.AsQueryable();
            if (req.ProjectManagerId != null)
            {
                query = query.Where(m=>m.Project.ProjectManagerId == req.ProjectManagerId);
            }
            if (!string.IsNullOrWhiteSpace(req.Content))
            {
                query = query.Where(m => m.Project.ProjectName.Contains(req.Content) || m.Project.Contract.ContractNumber.Contains(req.Content));
            }
            if (req.Status != null)
            {
                query = query.Where(m=>m.WarningType ==  req.Status);
            }
            if (req.StartDate != null)
            {
                query = query.Where(m => m.CreateTime > req.StartDate);
            }
            if (req.EndDate != null)
            {
                query = query.Where(m => m.CreateTime < req.EndDate);
            }
            return await query.OrderByDescending(m=>m.CreateTime).AsNoTracking().ToPaginatedListAsync(req.Pagination);
        }

        public async Task<EarlyWarningHistory?> GetEarlyWarningHistoryById(int id)
        {
            return await _context.EarlyWarningHistories.FirstOrDefaultAsync(m => m.Id == id);
        }
        
        public async Task<bool> SaveEarlyWarningHistory(EarlyWarningHistoryVm vm)
        {
            var history = _context.EarlyWarningHistories.FirstOrDefault(m=>m.Id==vm.Id);
            if (history == null) return false;
            history.StaffReason = vm.StaffReason;
            history.ManagerReason = vm.ManagerReason;
            history.Suggestions = vm.Suggestions;
            history.Status = vm.Status;
            history.UpdateBy = GetUserId();
            history.UpdateTime = DateTime.Now;
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
