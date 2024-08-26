using Microsoft.EntityFrameworkCore;
using ProjectService.Db;
using ProjectService.ViewModels;
using ProjectViewModels;

namespace ProjectService.Services
{
    public class PatrolService : UserService
    {
        private readonly ProjectDbContext _context;
        private DictService _dictService;
        public PatrolService(ProjectDbContext context, IHttpContextAccessor httpContextAccessor, DictService dictService):base(httpContextAccessor)
        {
            _context = context;
            _dictService = dictService;
        }
        public async Task<TResult> GetPatrols<TResult>(ProjectWithStaffReq req, Func<IQueryable<ProjectPatrol>, Task<TResult>> resultSelector)
        {
            IQueryable<ProjectPatrol> query = _context.ProjectPatrols;
            if (req.ProjectId != null)
                query = query.Where(m => m.ProjectId == req.ProjectId);
            if(req.ProjectManagerId!=null)
                query = query.Where(m=>m.Project.ProjectManagerId == req.ProjectManagerId);
            if (req.Staff != null)
                query = query.Where(m => m.StaffId == req.Staff);
            if (req.StartDate != null)
                query = query.Where(m => m.PatrolDate >= req.StartDate);
            if(req.EndDate != null) 
                query = query.Where(m=>m.PatrolDate<=req.EndDate);            
            if (req.Status != null)
                query = query.Where(m => m.Status == req.Status);
            query = query.OrderByDescending(o => o.PatrolDate);
            return await resultSelector(query);
        }
        public async Task<PaginatedList<ProjectPatrolVm>> PaginatedPatrol(ProjectWithStaffReq req)
        {
            var list = await GetPatrols(req, async query =>
            {
                var list = await query.ToPaginatedListAsync(req.Pagination);
                return list;
            });
            var result = list.ToViewModelPaginatedList(m => m.ToViewModel());
            var statusList = await _dictService.GetDictDataByTypeName(DictSettings.PatrolStatusTypeName);
            foreach (var item in result.Items)
            {
                item.StatusName = statusList?.FirstOrDefault(m => m.DictCode == item.Status)?.DictLabel;
            }
            return result;

        }

        public async Task<List<ProjectPatrolVm>> GetPatrolList(ProjectWithStaffReq req)
        {
            var list = await GetPatrols(req, async query =>
            {
                var list = await query.ToListAsync();
                return list;
            });
            var result = list.Select(m => m.ToViewModel()).ToList();
            var statusList = await _dictService.GetDictDataByTypeName(DictSettings.PatrolStatusTypeName);
            foreach (var item in result)
            {
                item.StatusName = statusList?.FirstOrDefault(m => m.DictCode == item.Status)?.DictValue;
            }
            return result;
        }
        public async Task<bool> SavePatrol(ProjectPatrolVm vm)
        {
            if (vm.ProjectId == null || vm.PatrolDate == null || vm.Status == null || vm.StaffId == null) return false;
            ProjectPatrol? patrol;
            if (vm.Id == null)
            {
                patrol = new ProjectPatrol
                {
                    PatrolDate = vm.PatrolDate.Value,
                    ProjectId = vm.ProjectId.Value,
                    StaffId = vm.StaffId.Value,
                    Status = vm.Status.Value,
                    Remarks = vm.Remarks,
                    CreateBy = GetUserId(),
                    CreateTime = DateTime.Now
                };
                _context.ProjectPatrols.Add(patrol);
                await _context.SaveChangesAsync();
            }
            else
            {
                patrol = _context.ProjectPatrols.FirstOrDefault(m => m.Id == vm.Id);
                if (patrol == null) return false;
                patrol.PatrolDate = vm.PatrolDate.Value;
                patrol.ProjectId = vm.ProjectId.Value;
                patrol.StaffId = vm.StaffId.Value;
                patrol.Status = vm.Status.Value;
                patrol.Remarks = vm.Remarks;
                patrol.UpdateBy = GetUserId();
                patrol.UpdateTime = DateTime.Now;
            }
            await _context.SaveChangesAsync();
            return true;
        }
        public async Task<ProjectPatrol?> GetPatrolById(long id)
        {
            return await _context.ProjectPatrols.FirstOrDefaultAsync(m => m.Id == id);
        }

        public async Task<bool> SavePatrolByExcelData(List<ProjectPatrolExcelVm> list)
        {
            var htlist = list.Select(m => m.Ht).Distinct().ToList();
            var projectList = _context.Projects.Where(m => htlist.Contains(m.Contract.ContractNumber)).ToList();
            var cardlist = list.Select(m=>m.Card).Distinct().ToList();
            var staffList = _context.Staff.Where(m => m.StaffCard != null && cardlist.Contains(m.StaffCard)).ToList();
            var statusList = await _dictService.GetDictDataByTypeName(DictSettings.PatrolStatusTypeName);
            foreach (var item in list)
            {
                var patrol = new ProjectPatrol();
                var projectid = projectList.FirstOrDefault(m => m.Contract.ContractNumber == item.Ht)?.ProjectId;
                if (projectid == null) continue;
                patrol.ProjectId = projectid.Value;
                var staffid = staffList.FirstOrDefault(m => m.StaffCard == item.Card)?.StaffId;
                if (staffid == null) continue;
                patrol.StaffId = staffid.Value;
                patrol.PatrolDate = item.PatrolDateTime;
                var status = statusList?.FirstOrDefault(m => m.DictLabel == item.PatrolStatus)?.DictCode;
                if(status == null) continue;
                patrol.Status = status.Value;
                patrol.Remarks = item.Remarks;
                var checkPatrol = _context.ProjectPatrols.FirstOrDefault(m => m.ProjectId == patrol.ProjectId && m.PatrolDate == patrol.PatrolDate);
                if (checkPatrol == null)
                {
                    _context.ProjectPatrols.Add(patrol);
                }
                else
                {
                    checkPatrol.StaffId = patrol.StaffId;
                    checkPatrol.Status = patrol.Status;
                    checkPatrol.Remarks = patrol.Remarks;
                }
            }
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
