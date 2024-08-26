using Microsoft.EntityFrameworkCore;
using ProjectService.Db;
using ProjectService.ViewModels;
using ProjectViewModels;

namespace ProjectService.Services
{
    public class ConsumableAskForService : UserService
    {
        private readonly ProjectDbContext _context;
        private readonly DictService _dictService;
        public ConsumableAskForService(ProjectDbContext context, IHttpContextAccessor httpContextAccessor, DictService dictService) : base(httpContextAccessor)
        {
            _context = context;
            _dictService = dictService;
        }

        public async Task<PaginatedList<ConsumableAskForVm>> PaginatedConsumableAskFor(ConsumableAskForReq req)
        {
            var query = _context.ConsumableAskFors.AsQueryable();
            if (req.ProjectId != null)
                query = query.Where(m => m.ProjectId == req.ProjectId);
            if (req.StartDate != null)
                query = query.Where(m => m.CreateTime >= req.StartDate);
            if (req.EndDate != null)
                query = query.Where(m => m.CreateTime <= req.EndDate);
            if (req.Staff != null)
                query = query.Where(m => m.StaffId == req.Staff);
            if (req.Status != null)
                query = query.Where(m => m.Status == req.Status);
            if (req.ConsumableTypeId != null)
                query = query.Where(m => m.ConsumableAskForItems.Any(m => m.ConsumableTypeId == req.ConsumableTypeId));
            if (!string.IsNullOrWhiteSpace(req.Content))
                query = query.Where(m => m.Title.Contains(req.Content) || (m.Content != null && m.Content.Contains(req.Content)));

            var list = await query.OrderByDescending(m => m.CreateTime).ToPaginatedListAsync(req.Pagination);
            var result = list.ToViewModelPaginatedList(m => m.ToViewModel());
            var statusDictData = await _dictService.GetDictDataByTypeName(DictSettings.ConsumableAskForStatusTypeName);
            foreach (var item in result.Items)
            {
                item.StatusName = statusDictData?.FirstOrDefault(m => m.DictCode == item.Status)?.DictLabel;
            }
            return result;
        }
    
        public async Task<ConsumableAskForVm?> GetConsumableAskForById(Guid id)
        {
            var askFor = await _context.ConsumableAskFors.FirstOrDefaultAsync(m=>m.ConsumableAskForId == id);
            if (askFor == null) return null;
            var vm = askFor.ToViewModel();
            //var dictData = await _dictService.GetDictData(askFor.Status);
            //vm.StatusName = dictData?.DictLabel;
            return vm;
        }
        public async Task<bool> SaveConsumableAskFor(ConsumableAskForVm vm)
        {
            using (var transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    var status = await _dictService.GetDictDataId(DictSettings.ConsumableAskForStatusTypeName, DictSettings.ConsumableAskForStatus_Unprocess);
                    ConsumableAskFor? askFor;
                    if (vm.ConsumableAskForId == null)
                    {
                        askFor = new ConsumableAskFor
                        {
                            ConsumableAskForId = Guid.NewGuid(),
                            Content = vm.Content,
                            CreateBy = GetUserId(),
                            CreateTime = DateTime.Now,
                            ProjectId = vm.ProjectId,
                            StaffId = vm.StaffId,
                            Remarks = vm.Remarks,
                            Status = status.Value,
                            Title = vm.Title
                        };
                        _context.ConsumableAskFors.Add(askFor);
                        await _context.SaveChangesAsync();
                    }
                    else
                    {
                        askFor = _context.ConsumableAskFors.FirstOrDefault(m => m.ConsumableAskForId == vm.ConsumableAskForId);
                        if (askFor == null) return false;
                        askFor.ProjectId = vm.ProjectId;
                        askFor.Content = vm.Content;
                        askFor.StaffId = vm.StaffId;
                        askFor.Status = vm.Status;
                        askFor.Title = vm.Title;
                        askFor.Remarks = vm.Remarks;
                        askFor.UpdateBy = GetUserId();
                        askFor.UpdateTime = DateTime.Now;
                    }
                    //更新消耗品申请详情表
                    var askForItems = _context.ConsumableAskForItems.Where(m => m.ConsumableAskForId == askFor.ConsumableAskForId).ToList();
                    foreach (var item in askForItems)
                    {
                        var i = vm.ConsumableAskForItemList.FirstOrDefault(m => m.ConsumableTypeId == item.ConsumableTypeId);
                        if (i == null) _context.ConsumableAskForItems.Remove(item);
                    }
                    if (vm.ConsumableAskForItemList != null)
                    {
                        foreach (var item in vm.ConsumableAskForItemList)
                        {
                            var i = askForItems.FirstOrDefault(m => m.ConsumableTypeId == item.ConsumableTypeId);
                            if (i == null)
                            {
                                _context.ConsumableAskForItems.Add(new ConsumableAskForItem
                                {
                                    ConsumableAskForItemId = Guid.NewGuid(),
                                    ConsumableAskForId = askFor.ConsumableAskForId,
                                    ConsumableTypeId = item.ConsumableTypeId.Value,
                                    Quantity = item.Quantity,
                                    CreateBy = GetUserId(),
                                    CreateTime = DateTime.Now
                                });
                            }
                            else
                            {
                                i.ConsumableTypeId = item.ConsumableTypeId.Value;
                                i.Quantity = item.Quantity;
                                i.UpdateTime = DateTime.Now;
                                i.UpdateBy = GetUserId();
                            }
                        }
                    }

                    await _context.SaveChangesAsync();
                    transaction.Commit();
                    return true;
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    return false;
                }
            }
        }
    }
}
