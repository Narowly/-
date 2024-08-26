using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ProjectService.Db;
using ProjectService.UserDb;
using ProjectService.ViewModels;
using ProjectViewModels;
using System.Linq;
using System.Text.RegularExpressions;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace ProjectService.Services
{
    public class DeviceService : UserService
    {
        private readonly ProjectDbContext _context;
        private readonly DictService _dictService;
        public DeviceService(ProjectDbContext context, IHttpContextAccessor httpContextAccessor, UserManager<ApplicationUser> userManager, DictService dictService) : base(httpContextAccessor, userManager)
        {
            _context = context;
            _dictService = dictService;
        }
        
        public async Task<List<DeviceType>> GetDeviceTypeList()
        {
            return await _context.DeviceTypes.ToListAsync();
        }

        public async Task<bool> SaveDeviceType(DeviceTypeVm vm)
        {
            DeviceType? deviceType;
            if (vm.DeviceTypeId == null)
            {
                deviceType = new DeviceType
                {
                    DeviceTypeId = Guid.NewGuid(),
                    DeviceModel = vm.DeviceModel,
                    DeviceTypeName = vm.DeviceTypeName,
                    DeviceUnit = vm.DeviceUnit,
                    CreateBy = GetUserId(),
                    CreateTime = DateTime.Now,
                    Remarks = vm.Remarks
                };
                _context.DeviceTypes.Add(deviceType);
            }
            else
            {
                deviceType = _context.DeviceTypes.FirstOrDefault(m => m.DeviceTypeId == vm.DeviceTypeId);
                if (deviceType == null) return false;
                deviceType.DeviceUnit = vm.DeviceUnit;
                deviceType.UpdateTime = DateTime.Now;
                deviceType.UpdateBy = GetUserId();
                deviceType.DeviceTypeName = vm.DeviceTypeName;
                deviceType.DeviceModel = vm.DeviceModel;
                deviceType.Remarks = vm.Remarks;
            }
            await _context.SaveChangesAsync();
            return true;
        }
        public async Task<DeviceType?> GetDeviceTypeById(Guid deviceTypeId)
        {
            return await _context.DeviceTypes.FirstOrDefaultAsync(m => m.DeviceTypeId == deviceTypeId);
        }

        public async Task<DeviceType> UpdateDeviceType(DeviceTypeVm vm)
        {
            var deviceType = _context.DeviceTypes.First(m => m.DeviceTypeId == vm.DeviceTypeId);           
            deviceType.DeviceUnit = vm.DeviceUnit;
            deviceType.UpdateTime = DateTime.Now;
            deviceType.UpdateBy = GetUserId();
            deviceType.Remarks = vm.Remarks;
            deviceType.DeviceModel = vm.DeviceModel;
            deviceType.DeviceTypeName = vm.DeviceTypeName;
            await _context.SaveChangesAsync();
            return deviceType;           
        }
        public async Task<TResult> GetDevices<TResult>(DeviceReqs req, Func<IQueryable<Device>, Task<TResult>> resultSelector)
        {
            IQueryable<Device> query = _context.Devices;
            
            if (req.ProjectId != null || req.ProjectManagerId != null)
            {
                
                var subQuery = _context.ProjectDevices.Where(m => m.TransferOutDate == null);
                if(req.ProjectId != null)
                {
                    subQuery = subQuery.Where(m=>m.ProjectId == req.ProjectId);
                }
                if(req.ProjectManagerId != null)
                {
                    subQuery = subQuery.Where(m=>m.Project.ProjectManagerId == req.ProjectManagerId);
                }
                query = subQuery.Select(m => m.Device);
                
            }
           
            if (req.DeviceTypeId != null)
                query = query.Where(m => m.DeviceTypeId == req.DeviceTypeId);            
                
            if (!string.IsNullOrWhiteSpace(req.Content))
                query = query.Where(m => m.DeviceType.DeviceTypeName.Contains(req.Content) || (m.DeviceType.DeviceModel != null && m.DeviceType.DeviceModel.Contains(req.Content)) || m.DeviceNumber.Contains(req.Content));
            if (req.Status != null)
                query = query.Where(m => m.DeviceStatus == req.Status);

            query = query.OrderBy(o => o.DeviceNumber);
            
            return await resultSelector(query);
        }

        public async Task<PaginatedList<Device>> GetDevicePaginatedList(DeviceReqs req)
        {
            return await GetDevices(req, async query =>
            {
                var list = await query.OrderBy(m=>m.DeviceNumber).ToPaginatedListAsync(req.Pagination);
                return list;
            });
        }

        public async Task<List<DeviceVm>> GetDeviceList(DeviceReqs req)
        {
            return await GetDevices(req, async query =>
            {
                var list = await query.ToListAsync();
                var result = list.Select(m => m.ToViewModel()).ToList();
                var projectDeviceList = await _context.ProjectDevices.Where(m=>m.TransferOutDate==null).ToListAsync();
                var projectDeviceIdList = projectDeviceList.Select(m=>m.DeviceId).ToList();
                var resultDeviceList = result.Where(m => projectDeviceIdList.Contains(m.DeviceId.Value)).ToList();
                foreach (var device in resultDeviceList)
                {
                    var pd = projectDeviceList.FirstOrDefault(m=>m.DeviceId==device.DeviceId);
                    device.DeviceWithProjectName += $" ({pd.Project.ProjectName})";
                }
                return result;
            });
        }

        public async Task<bool> SaveDevice(DeviceVm vm)
        {
            Device? device;
            if (vm.DeviceId == null)
            {
                device = new Device
                {
                    CreateBy = GetUserId(),
                    CreateTime = DateTime.UtcNow,
                    DeviceId = Guid.NewGuid(),
                    DeviceNumber = vm.DeviceNumber,
                    DeviceStatus = vm.DeviceStatus,
                    DeviceTypeId = vm.DeviceTypeId,
                    Remarks = vm.Remarks
                };
                _context.Devices.Add(device);
            }
            else
            {
                device = _context.Devices.FirstOrDefault(m=>m.DeviceId == vm.DeviceId);
                if (device == null) return false;
                device.DeviceTypeId = vm.DeviceTypeId;
                device.DeviceStatus = vm.DeviceStatus;
                device.Remarks = vm.Remarks;
                device.DeviceNumber = vm.DeviceNumber;
                device.UpdateBy = GetUserId();
                device.UpdateTime = DateTime.Now;
            }            
            await _context.SaveChangesAsync();
            return true;
        }
        public async Task<bool> RemoveDevice(Guid deviceId)
        {
            var device = _context.Devices.FirstOrDefault(m => m.DeviceId == deviceId);
            if (device != null)
            {
                _context.Devices.Remove(device);
                await _context.SaveChangesAsync();
            }            
            return true;
        }

        public async Task<Device?> GetDeviceById(Guid deviceId)
        {
            return await _context.Devices.FirstOrDefaultAsync(m => m.DeviceId == deviceId);
        }

        public async Task<Device> UpdateDevice(DeviceVm vm)
        {
            var device = _context.Devices.First(m => m.DeviceId == vm.DeviceId);
            device.DeviceStatus = vm.DeviceStatus;
            device.DeviceNumber = vm.DeviceNumber;
            device.DeviceTypeId = vm.DeviceTypeId;
            device.Remarks = vm.Remarks;
            device.UpdateBy = GetUserId();
            device.UpdateTime = DateTime.Now;
            await _context.SaveChangesAsync();
            return device;
        }

        public async Task<List<Device>> GetProjectDeviceList(Guid projectId)
        {
            return await _context.ProjectDevices.Where(m => m.ProjectId == projectId).Select(m => m.Device).ToListAsync();
        }

        public async Task<bool> SaveProjectDevice(ProjectVm vm)
        {
            var projectDevices = _context.ProjectDevices.Where(m => m.ProjectId == vm.ProjectId && m.TransferOutDate == null).ToList();
            var inProjectDevices = vm.InProjectDevice?.ToList();
            List<Guid?>? projectDeviceVmIds = null;
            if (inProjectDevices != null)
            {
                projectDeviceVmIds = inProjectDevices.Select(m => m.DeviceId).ToList();
                var removeProjectDevices = projectDevices.Where(m => !projectDeviceVmIds.Contains(m.DeviceId)).ToList();
                if (removeProjectDevices != null && removeProjectDevices.Count > 0)
                {
                    foreach (var removeItem in removeProjectDevices)
                    {
                        removeItem.TransferOutDate = DateTime.Now;
                        removeItem.TransferOutOperator = GetUserId();
                        removeItem.UpdateBy = GetUserId();
                        removeItem.UpdateTime = DateTime.Now;
                        var typeList = _context.DeviceTypes.Where(m => m.DeviceTypeName == "主机").Select(m=>m.DeviceTypeId).ToList();
                        if (typeList.Contains(removeItem.Device.DeviceTypeId))
                        {
                            var device = removeItem.Device;
                            device.DeviceStatus = await _dictService.GetDictDataId("DeviceStatus", "未清理") ?? device.DeviceStatus;
                        }
                    }
                }
                projectDevices = _context.ProjectDevices.Where(m => m.ProjectId == vm.ProjectId && m.TransferOutDate == null).ToList();
                foreach (var vmItem in inProjectDevices)
                {
                    var device = projectDevices.FirstOrDefault(m => m.DeviceId == vmItem.DeviceId);
                    if (device == null)
                    {
                        device = new ProjectDevice
                        {
                            AssociationId = Guid.NewGuid(),
                            DeviceId = vmItem.DeviceId.Value,
                            ProjectId = (Guid)vm.ProjectId,
                            TransferInDate = DateTime.Now,
                            TransferInOperator = (Guid)GetUserId(),
                            CreateBy = GetUserId(),
                            CreateTime = DateTime.Now
                        };
                        _context.ProjectDevices.Add(device);
                    }
                }
            }
            else if (projectDevices != null)
            {
                foreach (var removeItem in projectDevices)
                {
                    removeItem.TransferOutDate = DateTime.Now;
                    removeItem.TransferOutOperator = GetUserId();
                    removeItem.UpdateBy = GetUserId();
                    removeItem.UpdateTime = DateTime.Now;
                    var typeList = _context.DeviceTypes.Where(m => m.DeviceTypeName == "主机").Select(m => m.DeviceTypeId).ToList();
                    if (typeList.Contains(removeItem.Device.DeviceTypeId))
                    {
                        var device = removeItem.Device;
                        device.DeviceStatus = await _dictService.GetDictDataId("DeviceStatus", "未清理") ?? device.DeviceStatus;
                    }
                }
            }
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<PaginatedList<ProjectDeviceVm>> PaginatedProjectDeviceHistory(DeviceReqs req)
        {
            var query = _context.ProjectDevices.AsQueryable();
            if (req.DeviceId != null) query = query.Where(m => m.DeviceId == req.DeviceId);
            if (req.ProjectId!=null) query = query.Where(m=>m.ProjectId == req.ProjectId);
            if (req.ProjectManagerId!=null) query = query.Where(m=>m.Project.ProjectManagerId == req.ProjectManagerId);
            if (req.StartDate != null) query = query.Where(m => m.TransferInDate >= req.StartDate);
            if (req.EndDate != null) query = query.Where(m => m.TransferInDate <= req.EndDate);
            if (!string.IsNullOrWhiteSpace(req.Content)) query = query.Where(m => m.Device.DeviceNumber.Contains(req.Content) || m.Device.DeviceType.DeviceTypeName.Contains(req.Content) || m.Device.DeviceType.DeviceModel.Contains(req.Content));

            var projectDeviceList = await query.OrderByDescending(o => o.TransferOutDate).ToPaginatedListAsync(req.Pagination);
            var histories = projectDeviceList.ToViewModelPaginatedList(m => m.ToViewModel());
            if (histories.Items != null)
            {
                var inUserIdList = histories.Items.Select(m => m.TransferInOperator).Distinct().ToList();
                var outUserIdList = histories.Items.Where(m => m.TransferOutOperator != null).Select(m => m.TransferOutOperator.Value).Distinct().ToList();
                var userIdList = inUserIdList.Union(outUserIdList).Distinct().Select(m=>m.ToString()).ToList();
                var users = await GetUsersByIds(userIdList);
                if(users != null)
                {
                    foreach (var item in histories.Items)
                    {
                        var inuser = users.FirstOrDefault(m => m.Id == item.TransferInOperator.ToString());
                        if (inuser != null) item.TransferInStaffName = inuser.NormalizedUserName;
                        if (item.TransferOutDate != null && item.TransferOutOperator != null)
                        {
                            var outuser = users.FirstOrDefault(m => m.Id == item.TransferOutOperator.ToString());
                            if (outuser != null) item.TransferOutStaffName = outuser.NormalizedUserName;
                        }
                    }
                }                
            }
            return histories;
        }

        public async Task<PaginatedList<DeviceType>> PaginatedDeviceType(CommonReqs req)
        {
            var query = _context.DeviceTypes.AsQueryable();
            if (!string.IsNullOrWhiteSpace(req.Content)) query = _context.DeviceTypes.Where(m => m.DeviceTypeName.Contains(req.Content) || (m.DeviceModel != null && m.DeviceModel.Contains(req.Content)));
            req.Pagination ??= new PaginationParams();
            return await query.OrderBy(m=>m.DeviceTypeName).ToPaginatedListAsync(req.Pagination);
        }
        public async Task<string> ReturnNextNumber(Guid deviceTypeId)
        {
            var deviceList = await _context.Devices.Where(m => m.DeviceTypeId == deviceTypeId && m.DeviceNumber.Contains("-")).ToListAsync();
            if (deviceList != null && deviceList.Count() > 0)
            {
                var number = deviceList.First().DeviceNumber;
                var prefix = number.Substring(0, number.LastIndexOf('-') + 1);
                var max = deviceList.Select(m => Regex.Replace(m.DeviceNumber, "[^0-9]", ""))
                    .Select(m => Convert.ToInt32(m)).DefaultIfEmpty(0).Max();
                return $"{prefix}{max + 1}";
            }
            else
            {
                return string.Empty;
            }
        }

        public async Task<bool> SetHandleBy(Guid deviceId, Guid staffId)
        {
            var computerTypeList = _context.DeviceTypes.Where(m => m.DeviceTypeName == DictSettings.ComputerDevice).Select(m => m.DeviceTypeId).ToList();

            var projectDevice = _context.ProjectDevices.Where(m => m.DeviceId == deviceId && m.TransferOutDate != null && computerTypeList.Contains(m.Device.DeviceTypeId)).OrderByDescending(m => m.TransferOutDate).FirstOrDefault();
            if (projectDevice == null) return false;
            projectDevice.HandleBy = staffId;
            var normalStatus = await _dictService.GetDictDataId(DictSettings.DeviceStatusTypeName, DictSettings.DeviceStatus_Normal);
            if (normalStatus == null) return false;
            projectDevice.Device.DeviceStatus = normalStatus.Value;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> BatchSaveDevice(BatchSaveDeviceReq req)
        {
            var normalStatus = await _dictService.GetDictDataId(DictSettings.DeviceStatusTypeName, DictSettings.DeviceStatus_Normal);
            if(normalStatus ==  null) return false;
            for (var i = 0; i < req.Count; i++)
            {
                var number = await ReturnNextNumber(req.DeviceTypeId);
                var device = new Device
                {
                    DeviceNumber = number,
                    DeviceId = Guid.NewGuid(),
                    DeviceStatus = normalStatus.Value,
                    DeviceTypeId = req.DeviceTypeId,
                    CreateBy = GetUserId(),
                    CreateTime = DateTime.Now
                };
                _context.Devices.Add(device);
                await _context.SaveChangesAsync();
            }
            
            return true;
        }
    }
}
