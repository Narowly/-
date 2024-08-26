using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ProjectService.Db;
using ProjectService.ViewModels;
using ProjectViewModels;
using System.Security.Claims;
using static PaginationExtensions;

namespace ProjectService.Services
{
    public class DictService : UserService
    {
        private readonly ProjectDbContext _context;
        //private readonly IHttpContextAccessor _httpContextAccessor;
        public DictService(ProjectDbContext context, IHttpContextAccessor httpContextAccessor) : base(httpContextAccessor)
        {
            _context = context;
        }

        public async Task<bool> AddDictType(DictTypeVm model)
        {
            var userId = GetUserId();
            var dictType = new DictType
            {
                DictName = string.IsNullOrWhiteSpace(model.DictName) ? null : model.DictName,
                TypeName = string.IsNullOrWhiteSpace(model.TypeName) ? null : model.TypeName,
                Status = true,
                Remarks = model.Remarks,
                CreateBy = userId != null ? userId : null,
                CreateTime = DateTime.Now
            };
            _context.DictTypes.Add(dictType);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<DictType> UpdateDictType(DictTypeVm model)
        {
            var userId = GetUserId();
            var entity = _context.DictTypes.FirstOrDefault(m => m.DictId == model.DictId);
            if (entity != null)
            {
                entity.DictName = model.DictName;
                entity.TypeName = model.TypeName;
                entity.Status = model.Status;
                entity.UpdateTime = DateTime.Now;
                entity.UpdateBy = userId != null ? userId : null;
                entity.Remarks = model.Remarks;
                await _context.SaveChangesAsync();
            }
            else
            {
                throw new Exception("没有找到DictType");
            }
            return entity;
        }

        public async Task<PaginatedList<DictType>> GetDictTypeList(PaginationParams req)
        {
            return await _context.DictTypes.ToPaginatedListAsync(req);
        }

        public async Task<DictType?> GetDictTypeByName(string name)
        {
            return await _context.DictTypes.FirstOrDefaultAsync(m => m.TypeName == name);
        }

        public async Task<List<DictDatum>?> GetDictDataByType(int typeId)
        {
            return await _context.DictData.Where(m=>m.DictTypeId == typeId).ToListAsync();
        }
        public async Task<List<DictDatum>?> GetDictDataByTypeName(string typeName)
        {
            var type = await _context.DictTypes.FirstOrDefaultAsync(m => m.TypeName == typeName);
            return type?.DictData.ToList();
        }
        public async Task<int?> GetDictDataId(string typeName, string label)
        {
            var type = await _context.DictTypes.FirstOrDefaultAsync(m => m.TypeName == typeName);
            if (type == null) return null;
            var data = type.DictData.FirstOrDefault(m => m.DictLabel == label);
            return data?.DictCode;
        }
        public async Task<DictDatum?> GetDictData(string typeName, string label)
        {
            var type = await _context.DictTypes.FirstOrDefaultAsync(m => m.TypeName == typeName);
            if (type == null) return null;
            var data = type.DictData.FirstOrDefault(m => m.DictLabel == label);
            return data;
        }
        public async Task<DictDatum?> GetDictData(int dictCode)
        {
            return await _context.DictData.FirstOrDefaultAsync(m => m.DictCode == dictCode);
        }
    }
}
