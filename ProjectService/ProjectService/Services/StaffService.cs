using Castle.Core.Logging;
using Microsoft.EntityFrameworkCore;
using ProjectService.Db;
using ProjectService.ViewModels;
using ProjectViewModels;

namespace ProjectService.Services
{
    public class StaffService : UserService
    {
        private readonly ProjectDbContext _context;
        public StaffService(ProjectDbContext ctx, IHttpContextAccessor httpContextAccessor) : base(httpContextAccessor)
        {
            _context = ctx;
        }

        public async Task<Staff?> GetStaffById(Guid id)
        {
            return await _context.Staff.FirstOrDefaultAsync(m=>m.StaffId == id);
        }

        public async Task<List<Staff>> GetStaffAllList()
        {
            return await _context.Staff.Where(m => m.StaffStatus == 2).ToListAsync();
        }
        public async Task<List<StaffVm>> GetStaffListWithProjectName()
        {
            var ps = await _context.ProjectStaffs.Where(m => m.TransferOutDate == null).ToListAsync();
            var psIds = ps.Select(m => m.StaffId).ToList();
            var staffs = await _context.Staff.Where(m => m.StaffStatus == 2).ToListAsync();
            var result = staffs.Select(m=>m.ToViewModel()).ToList();
            var inProjectStaffs = result.Where(m => psIds.Contains(m.StaffId)).ToList();
            foreach (var staff in inProjectStaffs)
            {
                staff.InProjectName = ps.FirstOrDefault(m => m.StaffId == staff.StaffId)?.Project.ProjectName;
            }
            return result;
        }

        public async Task<List<Staff>> GetStaffByDuty(List<int> dutyList)
        {
            return await _context.Staff.Where(m => dutyList.Contains(m.StaffDuty)).ToListAsync();
        }

        public async Task<List<Guid>> GetStaffGuidByDuty(List<int> dutyList)
        {
            return await _context.Staff.Where(m=>dutyList.Contains(m.StaffDuty)).Select(m=>m.StaffId).ToListAsync();
        }
        /// <summary>
        /// 获取超额完成项目中的人员
        /// </summary>
        /// <returns></returns>
        public async Task<List<StaffVm>> GetSpeedUpProjectStaff()
        {
            var projects = (await _context.Projects.Where(m => m.ProjectStaffs.Count > 0).ToListAsync()).Select(m => m.ToViewModel(includeProjectProcess:true)).ToList();
            projects = projects.Where(m => m.WorkloadPercentage - m.TimePercentage > 20).ToList();
            var speedupStaffs = new List<StaffVm>();
            foreach (var p in projects)
            {
                var projectStaffs = p.InProjectStaffs;
                foreach (var staff in projectStaffs)
                {
                    staff.InProjectName = p.ProjectName;
                }
                speedupStaffs.AddRange(projectStaffs);
            }
            return speedupStaffs;
        }

        public async Task<List<Staff>> GetIdleStaffId(List<int> dutyList)
        {
            var staffIds = await _context.Staff.Where(m => dutyList.Contains(m.StaffDuty)).Select(m => m.StaffId).ToListAsync();
            var projectStaffIds =  await _context.ProjectStaffs.Select(m => m.StaffId).Distinct().ToListAsync();
            var idleStaffIds = staffIds.Except(projectStaffIds);
            var staffList = await _context.Staff.Where(m => idleStaffIds.Contains(m.StaffId)).ToListAsync();
            return staffList;
        }        

        public async Task<List<Staff>> GetProjectStaff(Guid projectId)
        {
            var ps = await _context.ProjectStaffs.Where(m => m.ProjectId == projectId && m.TransferOutDate == null).Select(m => m.Staff).ToListAsync();
            return ps;
        }

        public async Task<bool> SaveProjectStaffs(ProjectVm vm)
        {
            var projectStaffs = _context.ProjectStaffs.Where(m => m.ProjectId == vm.ProjectId && m.TransferOutDate == null).ToList();
            var inProjectStaffs = vm.InProjectStaffs?.ToList();
            List<Guid>? projectStaffVmIds = null;
            if (inProjectStaffs != null)
            {
                var staffIdList = inProjectStaffs.Select(m => m.StaffId).ToList();
                var otherProjectHistories = _context.ProjectStaffs.Where(m => staffIdList.Contains(m.StaffId) && vm.ProjectId != m.ProjectId && m.TransferOutDate == null).ToList();
                foreach (var h in otherProjectHistories)
                {
                    h.TransferOutDate = DateTime.Now;
                    h.TransferOutOperator = GetUserId();
                    h.UpdateBy = GetUserId();
                    h.UpdateTime = DateTime.Now;
                }
                projectStaffVmIds = inProjectStaffs.Select(m => m.StaffId).ToList();
                var removeProjectStaffs = projectStaffs.Where(m => !projectStaffVmIds.Contains(m.StaffId)).ToList();
                if (removeProjectStaffs != null && removeProjectStaffs.Count > 0)
                {
                    foreach (var removeItem in removeProjectStaffs)
                    {
                        removeItem.TransferOutDate = DateTime.Now;
                        removeItem.TransferOutOperator = GetUserId();
                        removeItem.UpdateBy = GetUserId();
                        removeItem.UpdateTime = DateTime.Now;
                    }
                }
                projectStaffs = _context.ProjectStaffs.Where(m => m.ProjectId == vm.ProjectId && m.TransferOutDate == null).ToList();
                foreach (var vmItem in inProjectStaffs)
                {
                    var staff = projectStaffs.FirstOrDefault(m => m.StaffId == vmItem.StaffId);
                    if (staff == null)
                    {
                        staff = new ProjectStaff
                        {
                            AssociationId = Guid.NewGuid(),
                            StaffId = vmItem.StaffId,
                            ProjectId = vm.ProjectId.Value,
                            TransferInDate = DateTime.Now,
                            TransferInOperator = (Guid)GetUserId(),
                            CreateBy = GetUserId(),
                            CreateTime = DateTime.Now
                        };
                        _context.ProjectStaffs.Add(staff);
                    }
                }
            }
            else if (projectStaffs != null)
            {
                foreach (var removeItem in projectStaffs)
                {
                    removeItem.TransferOutDate = DateTime.Now;
                    removeItem.TransferOutOperator = GetUserId();
                    removeItem.UpdateBy = GetUserId();
                    removeItem.UpdateTime = DateTime.Now;
                }
            }
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
