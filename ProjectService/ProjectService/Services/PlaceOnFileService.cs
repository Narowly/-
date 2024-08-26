using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ProjectService.Db;
using ProjectService.UserDb;
using ProjectService.ViewModels;
using ProjectViewModels;

namespace ProjectService.Services
{
    public class PlaceOnFileService : UserService
    {
        private readonly ProjectDbContext _context;
        private readonly DictService _dictService;
        public PlaceOnFileService(ProjectDbContext context, IHttpContextAccessor httpContextAccessor, DictService dictService, UserManager<ApplicationUser> userManager) :base(httpContextAccessor, userManager)
        {
            _context = context;
            _dictService = dictService;
        }

        public async Task<bool> SavePlaceOnFile(PlaceOnFileVm vm)
        {
            var placeOnFile = await _context.PlaceOnFiles.FirstOrDefaultAsync(m=>m.ProjectId == vm.ProjectId);
            if (placeOnFile == null)
            {
                _context.PlaceOnFiles.Add(new PlaceOnFile
                {
                    ProjectId = vm.ProjectId,
                    ApplicationUserId = GetUserId().Value,
                    PlaceOnFileId = Guid.NewGuid(),
                    CreateBy = GetUserId().Value,
                    CreateTime = DateTime.Now
                });
            }
            else
            {
                placeOnFile.ReviewerId = GetUserId().Value;
                placeOnFile.Reason = vm.Reason;
                placeOnFile.UpdateBy = GetUserId().Value;
                placeOnFile.UpdateTime = DateTime.Now;
                if (vm.IsPass.HasValue && vm.IsPass.Value)
                {
                    var project = _context.Projects.FirstOrDefault(m => m.ProjectId == vm.ProjectId);
                    var placeOnFileStatus = await _dictService.GetDictDataId(DictSettings.ProjectStatusTypeName, DictSettings.ProjectStatus_PlaceOnFile);
                    project.Status = placeOnFileStatus.Value;
                }
            }
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<PaginatedList<PlaceOnFileVm>> PaginatedApplyPlaceOnFileProject(ProjectReqs req)
        {
            var placeOnFileStatus = await _dictService.GetDictDataId(DictSettings.ProjectStatusTypeName, DictSettings.ProjectStatus_PlaceOnFile);
            var query = _context.PlaceOnFiles.Where(m => m.Project.Status != placeOnFileStatus).AsQueryable();
            if(req.ProjectId!=null)
                query = query.Where(m=>m.ProjectId == req.ProjectId);
            var list= await query.OrderByDescending(m=>m.CreateTime).ToPaginatedListAsync(req.Pagination);
            var result = list.ToViewModelPaginatedList(m => m.ToViewModel());
            var userIds = list.Items.Select(m => m.ApplicationUserId).Union(list.Items.Where(m => m.ReviewerId != null).Select(m => m.ReviewerId.Value)).Distinct().Select(m => m.ToString()).ToList();
            var users = await GetUsersByIds(userIds);
            foreach (var item in result.Items)
            {
                var appUser = users.FirstOrDefault(m => m.Id == item.ApplicationUserId.ToString());
                if (appUser != null) item.ApplicationUserName = appUser.NormalizedUserName;
                if (item.ReviewerId != null)
                {
                    var reviewer = users.FirstOrDefault(m => m.Id == item.ReviewerId.Value.ToString());
                    if (reviewer != null) item.ReviewerName = reviewer.NormalizedUserName;
                }                
            }
            return result;
        }
    }
}
