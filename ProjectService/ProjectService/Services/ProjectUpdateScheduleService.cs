using ProjectService.Db;
using ProjectViewModels;

namespace ProjectService.Services
{
    public class ProjectUpdateScheduleService : UserService
    {
        private readonly ProjectDbContext _context;
        public ProjectUpdateScheduleService(IHttpContextAccessor httpContextAccessor, ProjectDbContext context) : base(httpContextAccessor)
        {
            _context = context;
        }

        public async Task<bool> AddProjectUpdateSchedule(ProjectUpdateScheduleVm vm)
        {
            var schedule = new ProjectUpdateSchedule
            {
                ProjectId = vm.ProjectId,
                PlanEndDate = vm.PlanEndDate,
                UpdatedEndDate = vm.UpdateEndDate,
                ReasonType = vm.ReasonType,
                Remarks = vm.Remarks,
                CreateBy = GetUserId(),
                CreateTime = DateTime.Now
            };
            _context.ProjectUpdateSchedules.Add(schedule);
            await _context.SaveChangesAsync();
            var project = _context.Projects.FirstOrDefault(m=>m.ProjectId == vm.ProjectId);
            if (project != null)
            {
                project.PlanEndDate = vm.UpdateEndDate;
                await _context.SaveChangesAsync();
            }
            return true;
        }
        public async Task<PaginatedList<ProjectUpdateSchedule>> PaginatedProjectUpdateSchedule(ProjectReqs req)
        {
            var query = _context.ProjectUpdateSchedules.AsQueryable();
            if (req.ProjectId != null)
                query = query.Where(m => m.ProjectId == req.ProjectId);
            if(req.ProjectManagerId!=null)
                query = query.Where(m=>m.Project.ProjectManagerId == req.ProjectManagerId);

            var list = await query.OrderByDescending(m=>m.CreateTime).ToPaginatedListAsync(req.Pagination);

            return list;
        }
        
    }
}
