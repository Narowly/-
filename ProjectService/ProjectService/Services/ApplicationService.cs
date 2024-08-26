using Microsoft.AspNetCore.Identity;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using ProjectService.Db;
using ProjectService.UserDb;
using ProjectService.ViewModels;
using ProjectViewModels;

namespace ProjectService.Services
{
    public class ApplicationService : UserService
    {
        private readonly ProjectDbContext _context;
        private readonly DictService _dictService;
        public ApplicationService(ProjectDbContext context, IHttpContextAccessor httpContextAccessor, UserManager<ApplicationUser> userManager, DictService dictService) : base(httpContextAccessor, userManager)
        {
            _context = context;
            _dictService = dictService;
        }
        public async Task<PaginatedList<ApplicationVm>> PaginatedApplication(ApplicationReq req)
        {
            var query = _context.Applications.AsQueryable();
            if (req.ProjectId != null)
                query = query.Where(m => m.ProjectId == req.ProjectId);
            if (req.StartDate != null)
                query = query.Where(m => m.ApplicationTime >= req.StartDate);
            if (req.EndDate != null)
                query = query.Where(m => m.ApplicationTime <= req.EndDate);
            if (req.ApplicationStaff != null)
                query = query.Where(m => m.ApplicationUser == req.ApplicationStaff);
            if(req.Status!=null)
                query = query.Where(m=>m.ApplicationStatus == req.Status);
            if (req.ApplicationType != null)
                query = query.Where(m => m.ApplicationType == req.ApplicationType);
            if (!string.IsNullOrWhiteSpace(req.Content))
            query = query.Where(m => m.ApplicationTitle.Contains(req.Content) || m.ApplicationContent.Contains(req.Content) || (m.ApplicationResContent != null && m.ApplicationResContent.Contains(req.Content)));
            
            var list = await query.OrderByDescending(m=>m.ApplicationTime).ToPaginatedListAsync(req.Pagination);
            var result = list.ToViewModelPaginatedList(m => m.ToViewModel());
            var statusDictData = await _dictService.GetDictDataByTypeName(DictSettings.ApplicationStatusTypeName);
            var userIds = result.Items.Where(m => m.ApplicationResUserId != null).Select(m => m.ApplicationResUserId.Value.ToString()).ToList();
            var typeDictData = await _dictService.GetDictDataByTypeName(DictSettings.ApplicationTypeName);
            foreach (var item in result.Items)
            {
                item.ApplicationStatusName = statusDictData?.FirstOrDefault(m => m.DictCode == item.ApplicationStatus)?.DictLabel;
                item.ApplicationTypeName = typeDictData?.FirstOrDefault(m=>m.DictCode==item.ApplicationType)?.DictLabel;
            }            
            
            if (userIds != null && userIds.Count > 0)
            {
                var users = await GetUsersByIds(userIds);
                if (users != null)
                {
                    foreach (var item in result.Items)
                    {
                        if (item.ApplicationResUserId != null)
                        {
                            var resUser = users.FirstOrDefault(m => m.Id == item.ApplicationResUserId.ToString());
                            if (resUser != null) item.ApplicationResUserName = resUser.NormalizedUserName;
                        }                        
                    }
                }
            }
            return result;
        }

        public async Task<ApplicationVm?> GetApplicationById(Guid applicationId, bool includeProject = false)
        {
            var app = await _context.Applications.FirstOrDefaultAsync(m => m.ApplicationId == applicationId);
            if (app == null) return null;
            var vm = app.ToViewModel(includeProject);
            var dictData = await _dictService.GetDictData(vm.ApplicationStatus);
            vm.ApplicationStatusName = dictData?.DictLabel;
            if (vm.ApplicationResUserId != null)
            {
                vm.ApplicationResUserName = (await GetUserById(vm.ApplicationResUserId.Value.ToString()))?.NormalizedUserName;
            }
            return vm;
        }
        public async Task<bool> SaveApplication(ApplicationVm vm)
        {
            using (var transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    var status = await _dictService.GetDictDataId(DictSettings.ApplicationStatusTypeName, DictSettings.ApplicationStatus_ApplyFor);
                    Application? app;
                    if (vm.ApplicationId == null)
                    {
                        app = new Application
                        {
                            ApplicationId = Guid.NewGuid(),
                            ProjectId = vm.ProjectId,
                            ApplicationType = vm.ApplicationType,
                            ApplicationUser = vm.ApplicationUser,
                            ApplicationTitle = vm.ApplicationTitle,
                            ApplicationContent = vm.ApplicationContent,
                            ApplicationItemCount = vm.ApplicationItemCount,
                            ApplicationDelivery = vm.ApplicationDelivery,
                            ApplicationTime = vm.ApplicationTime,
                            IsDeleted = false,
                            ApplicationStatus = status.Value,
                            //ApplicationResContent = vm.ApplicationResContent,
                            //ApplicationResTime = vm.ApplicationResTime,
                            Remarks = vm.Remarks,
                            CreateBy = GetUserId(),
                            CreateTime = DateTime.Now
                        };
                        _context.Applications.Add(app);
                        await _context.SaveChangesAsync();
                    }
                    else
                    {
                        app = _context.Applications.FirstOrDefault(m => m.ApplicationId == vm.ApplicationId);
                        if (app == null) return false;
                        app.ProjectId = vm.ProjectId;
                        app.ApplicationType = vm.ApplicationType;
                        app.ApplicationUser = vm.ApplicationUser;
                        app.ApplicationTitle = vm.ApplicationTitle;
                        app.ApplicationContent = vm.ApplicationContent;
                        app.ApplicationItemCount = vm.ApplicationItemCount;
                        app.ApplicationDelivery = vm.ApplicationDelivery;
                        app.ApplicationTime = vm.ApplicationTime;
                        app.IsDeleted = vm.IsDeleted;
                        app.ApplicationStatus = vm.ApplicationStatus;
                        app.ApplicationResContent = vm.ApplicationResContent;
                        app.ApplicationResTime = vm.ApplicationResTime;
                        app.ApplicationResUserId = vm.ApplicationResUserId;
                        app.Remarks = vm.Remarks;
                        app.UpdateBy = GetUserId();
                        app.UpdateTime = DateTime.Now;
                    }
                    //更新申请设备表
                    var devices = _context.ApplicationDevices.Where(m => m.ApplicationId == app.ApplicationId).ToList();
                    foreach (var device in devices)
                    {
                        var d = vm.DeviceList?.FirstOrDefault(m=>m.ApplicationDeviceId == device.ApplicationDeviceId);
                        if (d == null) _context.ApplicationDevices.Remove(device);
                    }
                    if (vm.DeviceList != null)
                    {
                        foreach (var device in vm.DeviceList)
                        {
                            var d = devices.FirstOrDefault(m => m.ApplicationDeviceId == device.ApplicationDeviceId);
                            if (d == null)
                            {
                                _context.ApplicationDevices.Add(new ApplicationDevice
                                {
                                    ApplicationDeviceId = Guid.NewGuid(),
                                    ApplicationId = app.ApplicationId,                                    
                                    DeviceTypeId = device.DeviceTypeId,
                                    Quantity = device.Quantity,
                                    Remarks = device.Remarks,
                                    CreateBy = GetUserId(),
                                    CreateTime = DateTime.Now
                                });
                            }
                            else
                            {
                                d.DeviceTypeId = device.DeviceTypeId;
                                d.Quantity = device.Quantity;
                                d.Remarks = device.Remarks;
                                d.UpdateBy = GetUserId();
                                d.UpdateTime = DateTime.Now;
                            }
                        }
                    }

                    //更新申请消耗品表
                    var consumables = _context.ApplicationConsumables.Where(m=>m.ApplicationId == app.ApplicationId).ToList();
                    foreach (var con in consumables)
                    {
                        var c = vm.ConsumableList?.FirstOrDefault(m=>m.ApplicationConsumableId == con.ApplicationConsumableId);
                        if (c == null) _context.ApplicationConsumables.Remove(con);
                    }
                    if (vm.ConsumableList != null)
                    {
                        foreach(var con in vm.ConsumableList)
                        {
                            var c = consumables.FirstOrDefault(m=>m.ApplicationConsumableId == con.ApplicationConsumableId);
                            if (c == null)
                            {
                                _context.ApplicationConsumables.Add(new ApplicationConsumable
                                {
                                    ApplicationConsumableId = Guid.NewGuid(),
                                    ApplicationId = app.ApplicationId,
                                    ConsumableTypeId = con.ConsumableTypeId,
                                    Quantity = con.Quantity,
                                    Remarks = con.Remarks,
                                    CreateBy = GetUserId(),
                                    CreateTime = DateTime.Now
                                });

                            }
                            else
                            {
                                c.ConsumableTypeId = con.ConsumableTypeId;
                                c.Quantity = con.Quantity;
                                c.Remarks = con.Remarks;
                                c.UpdateBy = GetUserId();
                                c.UpdateTime = DateTime.Now;
                            }
                        }
                    }

                    //更新申请人员表
                    var persons = _context.ApplicationPeople.Where(m=>m.ApplicationId == app.ApplicationId).ToList();
                    foreach (var person in persons)
                    {
                        var p = vm.PersonList?.FirstOrDefault(m => m.ApplicationPersonId == person.ApplicationPersonId);
                        if (p == null) _context.ApplicationPeople.Remove(person);
                    }
                    if (vm.PersonList != null)
                    {
                        foreach (var person in vm.PersonList)
                        {
                            var p = persons.FirstOrDefault(m => m.ApplicationPersonId == person.ApplicationPersonId);
                            if(p == null)
                            {
                                _context.ApplicationPeople.Add(new ApplicationPerson
                                {
                                    ApplicationPersonId = Guid.NewGuid(),
                                    ApplicationId = app.ApplicationId,
                                    ProcessId = person.ProcessId,
                                    Count = person.Count,
                                    Remarks = person.Remarks,
                                    CreateBy = GetUserId(),
                                    CreateTime = DateTime.Now
                                });
                            }
                            else
                            {
                                p.ProcessId = person.ProcessId;
                                p.Count = person.Count;
                                p.Remarks  = person.Remarks;
                                p.UpdateBy = GetUserId();
                                p.UpdateTime = DateTime.Now;
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

        public async Task<bool> Approve(ApplicationVm vm)
        {
            var application = await _context.Applications.FirstOrDefaultAsync(m => m.ApplicationId == vm.ApplicationId);
            if (application == null) return false;
            application.ApplicationResContent = vm.ApplicationResContent;
            application.ApplicationStatus = vm.ApplicationStatus;
            application.ApplicationResTime = DateTime.Now;
            application.ApplicationResUserId = GetUserId();
            await _context.SaveChangesAsync();
            return true;
        }
        public async Task<bool> Processd(Guid applicationId)
        {
            var application = _context.Applications.FirstOrDefault(m=> m.ApplicationId == applicationId);
            if (application == null) return false;
            var status = await _dictService.GetDictDataId(DictSettings.ApplicationStatusTypeName, DictSettings.ApplicationStatus_Processed);
            if (status == null) return false;
            application.ApplicationStatus = status.Value;
            await _context.SaveChangesAsync();
            return true;
        }
        
    }
}
