using Microsoft.EntityFrameworkCore;
using ProjectService.Db;
using ProjectViewModels;

namespace ProjectService.Services
{
    public class ConsumableService : UserService
    {
        private readonly ProjectDbContext _context;
        public ConsumableService(ProjectDbContext context, IHttpContextAccessor httpContextAccessor) : base(httpContextAccessor)
        {
            _context = context;
        }
        public async Task<bool> SaveConsumableType(ConsumableTypeVm vm)
        {
            ConsumableType? consumableType;
            if (vm.ConsumableTypeId == null)
            {
                consumableType = new ConsumableType
                {
                    ConsumableTypeId = Guid.NewGuid(),
                    ConsumableModel = vm.ConsumableModel,
                    ConsumableTypeName = vm.ConsumableTypeName,
                    ConsumableUnit = vm.ConsumableUnit,
                    CreateBy = GetUserId(),
                    CreateTime = DateTime.Now,
                    Remarks = vm.Remarks
                };
                _context.ConsumableTypes.Add(consumableType);
            }
            else
            {
                consumableType = _context.ConsumableTypes.FirstOrDefault(m => m.ConsumableTypeId == vm.ConsumableTypeId);
                if (consumableType == null) return false;
                consumableType.ConsumableUnit = vm.ConsumableUnit;
                consumableType.ConsumableTypeName = vm.ConsumableTypeName;
                consumableType.ConsumableModel = vm.ConsumableModel;
                consumableType.Remarks = vm.Remarks;
                consumableType.UpdateBy = GetUserId();
                consumableType.UpdateTime = DateTime.Now;
            }
            await _context.SaveChangesAsync();
            return true;
        }
        public async Task<ConsumableType?> GetConsumableTypeById(Guid consumableTypeId)
        {
            return await _context.ConsumableTypes.FirstOrDefaultAsync(m => m.ConsumableTypeId == consumableTypeId);
        }
        public async Task<PaginatedList<ConsumableType>> PaginatedConsumableType(CommonReqs req)
        {
            var query = _context.ConsumableTypes.AsQueryable();
            if (!string.IsNullOrWhiteSpace(req.Content)) query = query.Where(m => m.ConsumableTypeName.Contains(req.Content) || (m.ConsumableModel != null && m.ConsumableModel.Contains(req.Content)));
            req.Pagination ??= new PaginationParams();
            return await query.OrderBy(m=>m.ConsumableTypeName).ToPaginatedListAsync(req.Pagination);
        }
        public async Task<List<ConsumableType>> GetConsumableTypeList(CommonReqs req)
        {
            var query = _context.ConsumableTypes.AsQueryable();
            if (!string.IsNullOrWhiteSpace(req.Content)) query = query.Where(m => m.ConsumableTypeName.Contains(req.Content) || (m.ConsumableModel != null && m.ConsumableModel.Contains(req.Content)));
            return await query.ToListAsync();
        }
        public async Task<TResult> GetConsumables<TResult>(ConsumableReqs req, Func<IQueryable<Consumable>, Task<TResult>> resultSelector)
        {
            IQueryable<Consumable> query = _context.Consumables;
            if (req.ConsumableTypeId != null)
                query = query.Where(m => m.ConsumableTypeId == req.ConsumableTypeId);

            if (!string.IsNullOrWhiteSpace(req.Content))
                query = query.Where(m => m.ConsumableNumber.Contains(req.Content) || m.ConsumableType.ConsumableTypeName.Contains(req.Content) || (m.ConsumableType.ConsumableModel != null && m.ConsumableType.ConsumableModel.Contains(req.Content)));
            if (req.Status != null)
                query = query.Where(m => m.ConsumableStatus == req.Status);
            query = query.OrderBy(o => o.ConsumableNumber);
            return await resultSelector(query);
        }
        public async Task<PaginatedList<Consumable>> PaginatedConsumable(ConsumableReqs req)
        {
            return await GetConsumables(req, async query =>
            {
                var list = await query.OrderBy(m=>m.ConsumableNumber).ToPaginatedListAsync(req.Pagination);
                return list;
            });
        }
        public async Task<List<Consumable>> GetConsumableList(ConsumableReqs req)
        {
            return await GetConsumables(req, async query =>
            {
                var list = await query.ToListAsync();
                return list;
            });
        }
        public async Task<Consumable?> GetConsumableById(Guid consumableId)
        {
            return await _context.Consumables.FirstOrDefaultAsync(m => m.ConsumableId == consumableId);
        }
        public async Task<bool> SaveConsumable(ConsumableVm vm)
        {
            if (vm.ConsumableTypeId == null) return false;
            Consumable? consumable;
            if(vm.ConsumableId == null)
            {
                consumable = new Consumable
                {
                    ConsumableId = Guid.NewGuid(),
                    ConsumableNumber = vm.ConsumableNumber,
                    ConsumableTypeId = vm.ConsumableTypeId.Value,
                    Quantity = vm.Quantity,
                    Price = vm.Price,
                    ConsumableStatus = vm.ConsumableStatus,
                    Remarks = vm.Remarks,
                    CreateBy = GetUserId(),
                    CreateTime = DateTime.Now
                };
                _context.Consumables.Add(consumable);
                if (consumable.Quantity > 0)
                {
                    _context.StockInBounds.Add(new StockInBound
                    {
                        ConsumableId = consumable.ConsumableId,
                        InBoundId = Guid.NewGuid(),
                        CreateBy = GetUserId(),
                        CreateTime = DateTime.Now,
                        InBoundDate = DateTime.Now,
                        Quantity = consumable.Quantity
                    });
                }
            }
            else
            {
                consumable = _context.Consumables.FirstOrDefault(m => m.ConsumableId == vm.ConsumableId);
                if (consumable == null) return false;
                consumable.ConsumableTypeId = vm.ConsumableTypeId.Value;
                consumable.ConsumableStatus = vm.ConsumableStatus;
                consumable.Remarks = vm.Remarks;
                consumable.Quantity = vm.Quantity;
                consumable.Price = vm.Price;
                consumable.ConsumableNumber = vm.ConsumableNumber;
                consumable.UpdateBy = GetUserId();
                consumable.UpdateTime = DateTime.Now;
            }
            await _context.SaveChangesAsync();
            return true;
        }
        public async Task<bool> SaveStockInBound(StockInBoundVm vm)
        {
            if (vm.ConsumableId == null) return false;
            var consumable = _context.Consumables.First(m => m.ConsumableId == vm.ConsumableId);
            StockInBound? stockInBound;
            if (vm.InBoundId == null)
            {
                stockInBound = new StockInBound
                {
                    InBoundId = Guid.NewGuid(),
                    ConsumableId = vm.ConsumableId.Value,
                    InBoundDate = vm.InBoundDate,
                    ProjectId = vm.ProjectId,
                    Quantity = vm.Quantity,
                    Remarks = vm.Remarks,
                    CreateBy = GetUserId(),
                    CreateTime = DateTime.Now,
                };
                _context.StockInBounds.Add(stockInBound);
                consumable.Quantity += vm.Quantity;
            }
            else
            {
                
                stockInBound = _context.StockInBounds.FirstOrDefault(m => m.InBoundId == vm.InBoundId);
                if (stockInBound == null) return false;
                consumable.Quantity += vm.Quantity - stockInBound.Quantity;
                stockInBound.InBoundDate = vm.InBoundDate;
                stockInBound.Remarks = vm.Remarks;
                stockInBound.Quantity = vm.Quantity;
                stockInBound.UpdateBy = GetUserId();
                stockInBound.UpdateTime = DateTime.Now;
                
            }
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> SaveStockOutBound(StockOutBoundVm vm)
        {
            if (vm.ConsumableId == null || vm.ProjectId == null) return false;
            var consumable = _context.Consumables.First(m => m.ConsumableId == vm.ConsumableId);
            StockOutBound? stockOutBound;
            if (vm.OutBoundId == null)
            {
                stockOutBound = new StockOutBound
                {
                    OutBoundId = Guid.NewGuid(),
                    ConsumableId = vm.ConsumableId.Value,
                    OutBoundDate = vm.OutBoundDate,
                    ProjectId = vm.ProjectId.Value,
                    Quantity = vm.Quantity,
                    Remarks = vm.Remarks,
                    CreateBy = GetUserId(),
                    CreateTime = DateTime.Now
                };
                _context.StockOutBounds.Add(stockOutBound);
                consumable.Quantity -= vm.Quantity;
            }
            else
            {
                stockOutBound = _context.StockOutBounds.FirstOrDefault(m => m.OutBoundId == vm.OutBoundId);
                if (stockOutBound == null) return false;
                consumable.Quantity -= vm.Quantity - stockOutBound.Quantity;
                stockOutBound.OutBoundDate = vm.OutBoundDate;
                stockOutBound.Remarks = vm.Remarks;
                stockOutBound.Quantity = vm.Quantity;
                stockOutBound.UpdateBy = GetUserId();
                stockOutBound.UpdateTime = DateTime.Now;
            }
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> SaveStockOutBoundList(List<StockOutBoundVm> list)
        {
            foreach (var outBound in list)
            {
                if (outBound.ConsumableId == null || outBound.ProjectId == null) continue;
                var consumable = _context.Consumables.FirstOrDefault(m => m.ConsumableId == outBound.ConsumableId);
                if (consumable == null) continue;
                
                _context.StockOutBounds.Add(new StockOutBound
                {
                    OutBoundId = Guid.NewGuid(),
                    ConsumableId = outBound.ConsumableId.Value,
                    OutBoundDate = DateTime.Now,
                    ProjectId = outBound.ProjectId.Value,
                    Quantity = outBound.Quantity,
                    Remarks = outBound.Remarks,
                    CreateBy = GetUserId(),
                    CreateTime = DateTime.Now
                });
                consumable.Quantity -= outBound.Quantity;
                await _context.SaveChangesAsync();
            }            
            return true;
        }

        public async Task<PaginatedList<ConsumableBoundDto>> PaginatedConsumableBound(ConsumableReqs req)
        {
            var inQuery = _context.StockInBounds.AsQueryable();
            var outQuery = _context.StockOutBounds.AsQueryable();
            if (req.ConsumableId != null)
            {
                inQuery =inQuery.Where(m=>m.ConsumableId== req.ConsumableId);
                outQuery = outQuery.Where(m => m.ConsumableId == req.ConsumableId);
            }
            if (req.ProjectId != null)
            {
                inQuery = inQuery.Where(m=>m.ProjectId== req.ProjectId);
                outQuery = outQuery.Where(m => m.ProjectId == req.ProjectId);
            }
            if (req.ProjectManagerId != null)
            {
                inQuery = inQuery.Where(m => m.Project != null && m.Project.ProjectManagerId == req.ProjectManagerId);
                outQuery = outQuery.Where(m=>m.Project!=null&& m.Project.ProjectManagerId == req.ProjectManagerId);
            }
            if (req.Content != null)
            {
                inQuery = inQuery.Where(m => m.Consumable.ConsumableNumber.Contains(req.Content) || m.Consumable.ConsumableType.ConsumableTypeName.Contains(req.Content) || (m.Consumable.ConsumableType.ConsumableModel != null && m.Consumable.ConsumableType.ConsumableModel.Contains(req.Content)));
                outQuery = outQuery.Where(m => m.Consumable.ConsumableNumber.Contains(req.Content) || m.Consumable.ConsumableType.ConsumableTypeName.Contains(req.Content) || (m.Consumable.ConsumableType.ConsumableModel != null && m.Consumable.ConsumableType.ConsumableModel.Contains(req.Content)));
            }
            if (req.StartDate != null)
            {
                inQuery = inQuery.Where(m=>m.InBoundDate>=req.StartDate);
                outQuery = outQuery.Where(m=>m.OutBoundDate>=req.StartDate);
            }
            if (req.EndDate != null)
            {
                inQuery = inQuery.Where(m => m.InBoundDate <= req.EndDate);
                outQuery = outQuery.Where(m => m.OutBoundDate <= req.EndDate);
            }
            var result = await inQuery.Select(m => new ConsumableBoundDto
            {
                BoundId = m.InBoundId,
                Type = "入库",
                BoundDate = m.InBoundDate,
                ConsumableNumber = m.Consumable.ConsumableNumber,
                ConsumableTypeName = m.Consumable.ConsumableType.ConsumableTypeName,
                ConsumableTypeModel = m.Consumable.ConsumableType.ConsumableModel,
                ConsumableId = m.ConsumableId,
                ProjectId = m.ProjectId,
                Quantity = m.Quantity,
                Remarks = m.Remarks,
                ProjectName = m.Project!=null?m.Project.ProjectName:null
            }).Concat(outQuery.Select(m => new ConsumableBoundDto
            {
                BoundId = m.OutBoundId,
                Remarks = m.Remarks,
                BoundDate = m.OutBoundDate,
                Type = "出库",
                ConsumableId = m.ConsumableId,
                ConsumableNumber = m.Consumable.ConsumableNumber,
                ConsumableTypeName = m.Consumable.ConsumableType.ConsumableTypeName,
                ConsumableTypeModel = m.Consumable.ConsumableType.ConsumableModel,
                ProjectId = m.ProjectId,
                Quantity = m.Quantity,
                ProjectName = m.Project.ProjectName
            })).OrderByDescending(m=>m.BoundDate).ToPaginatedListAsync(req.Pagination);
            return result;
        } 
        public async Task<StockInBound> GetStockInBoundById(Guid inBoundId)
        {
            return await _context.StockInBounds.FirstAsync(m => m.InBoundId == inBoundId);
        }
        public async Task<StockOutBound> GetStockOutBoundById(Guid outBoundId)
        {
            return await _context.StockOutBounds.FirstAsync(m=>m.OutBoundId == outBoundId);
        }
    }
}
