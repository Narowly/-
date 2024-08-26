using Castle.Components.DictionaryAdapter;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ProjectService.Db;
using ProjectService.ViewModels;
using ProjectViewModels;
using System.Diagnostics;
using static PaginationExtensions;
namespace ProjectService.Services;

public class ProjectAllService : UserService
{
    private readonly ProjectDbContext _context;
    private readonly StaffService _staffService;
    private readonly DeviceService _deviceService;
    private readonly DictService _dictService;
    private readonly ProjectPaymentTermService _paymentTermService;
    public ProjectAllService(IHttpContextAccessor httpContextAccessor, ProjectDbContext context, StaffService staffService, DeviceService deviceService, DictService dictService, ProjectPaymentTermService paymentTermService) : base(httpContextAccessor)
    {
        _context = context;
        _staffService = staffService;
        _deviceService = deviceService;
        _dictService = dictService;
        _paymentTermService = paymentTermService;
    }
    /// <summary>
    /// 获取未立项的合同列表
    /// </summary>
    /// <returns></returns>    
    public async Task<PaginatedList<Contract>> GetNotInitiatedContracts(ProjectReqs req)
    {

        var query = _context.Contracts.Where(m => (m.IsDeleted == null || !m.IsDeleted.Value) && (m.Projects == null || m.Projects.Count == 0));

        if (!string.IsNullOrWhiteSpace(req.Content))
        {
            query = query.Where(m => m.ContractName.Contains(req.Content)
                                    || m.ContractNumber.Contains(req.Content));
        }
        if (req.StartDate != null)
        {
            query = query.Where(m => m.ContractStartDate > req.StartDate.Value);
        }
        if (req.EndDate != null)
        {
            query = query.Where(m => m.ContractStartDate < req.EndDate.Value);
        }
        //return await query.ToPaginatedListAsync(_context, o => o.ContractNumber, false, req.Pagination, CancellationToken.None);
        return await query.OrderByDescending(o => o.ContractNumber).Select(m => new Contract
        {
            ContractId = m.ContractId,
            ContractName = m.ContractName,
            ContractNumber = m.ContractNumber
        }).AsNoTracking().ToPaginatedListAsync(req.Pagination);
    }

    public async Task<Contract?> GetContractById(Guid id)
    {
        return await _context.Contracts.Where(m => m.ContractId == id).FirstOrDefaultAsync();
    }

    public async Task<List<Contract>> GetContractNames()
    {
        return await _context.Contracts.Where(m => m.IsDeleted == null || !m.IsDeleted.Value).Select(m => new Contract
        {
            ContractName = m.ContractName,
            ContractNumber = m.ContractNumber,
            ContractId = m.ContractId
        }).AsNoTracking().ToListAsync();
    }

    public async Task<List<CustomerContact>> GetCustomerContactList(Guid customerId)
    {
        return await _context.CustomerContacts.Where(m => m.CustomerId == customerId).ToListAsync();
    }

    public async Task<Project?> SaveProject(ProjectVm vm, DictDatum updateStatusDict)
    {

        using (var transaction = _context.Database.BeginTransaction())
        {
            try
            {
                Project project;
                //创建项目并将状态设置为已立项
                if (vm.ProjectId == null)
                {
                    project = new Project
                    {
                        ProjectId = Guid.NewGuid(),
                        ProjectName = vm.ProjectName,
                        ContractId = vm.ContractId,
                        Status = updateStatusDict.DictCode,
                        SalesManagerId = vm.SalesManager.StaffId,
                        ProjectManagerId = vm.ProjectManager.StaffId,
                        Address = vm.Address,
                        RegionId = vm.RegionId,
                        CreateBy = GetUserId(),
                        CreateTime = DateTime.Now,
                        Remarks = vm.Remarks
                    };
                    _context.Projects.Add(project);
                    
                }
                else
                {
                    //更新项目
                    project = _context.Projects.First(m => m.ProjectId == vm.ProjectId);
                    project.ProjectName = vm.ProjectName;
                    project.SalesManagerId = vm.SalesManager.StaffId;
                    project.ProjectManagerId = vm.ProjectManager.StaffId;
                    project.RegionId = vm.RegionId;
                    project.UpdateBy = GetUserId();
                    project.UpdateTime = DateTime.Now;
                    project.Remarks = vm.Remarks;
                    project.Address = vm.Address;
                }
                _context.SaveChanges();
                vm.ProjectId = project.ProjectId;
                if (vm.Contract != null)
                {
                    var contract = _context.Contracts.FirstOrDefault(m => m.ContractId == vm.ContractId);
                    if (contract != null)
                    {
                        contract.ContractName = vm.Contract.ContractName;
                        contract.ContractStartDate = vm.Contract.ContractStartDate;
                        contract.ContractEndDate = vm.Contract.ContractEndDate;
                    }

                    if (vm.Contract.CustomerContact != null)
                    {
                        contract.CustomerContactId = vm.Contract.CustomerContact.CustomerContactId;
                        var customerContact = _context.CustomerContacts.FirstOrDefault(m => m.CustomerContactId == vm.Contract.CustomerContact.CustomerContactId);
                        if (customerContact != null)
                            customerContact.Mobile = vm.Contract.CustomerContact.Mobile;
                    }
                }
                if (vm.SalesManager != null)
                {
                    var salesStaff = _context.Staff.FirstOrDefault(m => m.StaffId == vm.SalesManager.StaffId);
                    if (salesStaff != null) salesStaff.StaffPhone = vm.SalesManager.StaffPhone;
                }
                if (vm.ProjectManager != null)
                {
                    var proStaff = _context.Staff.FirstOrDefault(m => m.StaffId == vm.ProjectManager.StaffId);
                    if (proStaff != null) proStaff.StaffPhone = vm.ProjectManager.StaffPhone;
                }
                //更新项目工序
                var projectProcesses = _context.ProjectProcesses.Where(m => m.ProjectId == project.ProjectId).ToList();
                var projectProcessVms = vm.ProjectProcesses?.ToList();

                List<int>? processUnitIds = null;
                if (projectProcessVms != null)
                {
                    processUnitIds = projectProcessVms.Select(m => m.ProcessUnitId).ToList();

                    //移除工序
                    var removeProjectProcesses = projectProcesses.Where(m => !processUnitIds.Contains(m.ProcessUnitId)).ToList();
                    if (removeProjectProcesses != null && removeProjectProcesses.Count > 0) _context.ProjectProcesses.RemoveRange(removeProjectProcesses);
                    projectProcesses = _context.ProjectProcesses.Where(m => m.ProjectId == project.ProjectId).ToList();
                    //更新工序
                    foreach (var vmItem in projectProcessVms)
                    {
                        var process = projectProcesses.FirstOrDefault(m => m.ProcessUnitId == vmItem.ProcessUnitId);
                        if (process != null)
                        {
                            process.ProjectId = project.ProjectId;
                            process.Workload = vmItem.Workload;
                            process.UpdateBy = GetUserId();
                            process.UpdateTime = DateTime.Now;
                            process.Remarks = vmItem.Remarks;
                        }
                        else
                        {
                            process = new ProjectProcess
                            {
                                Id = Guid.NewGuid(),
                                ProjectId = project.ProjectId,
                                ProcessUnitId = vmItem.ProcessUnitId,
                                Workload = vmItem.Workload,
                                CreateBy = GetUserId(),
                                CreateTime = DateTime.Now,
                                Remarks = vmItem.Remarks
                            };
                            _context.ProjectProcesses.Add(process);
                        }
                    }                    
                }
                else if (projectProcesses != null)
                {
                    _context.ProjectProcesses.RemoveRange(projectProcesses);
                }
                _context.SaveChanges();
                //更新项目人员
                var updateStaffResult = await _staffService.SaveProjectStaffs(vm);
                if (!updateStaffResult)
                {
                    transaction.Rollback();
                    return null;
                }

                //更新项目设备
                var updateDeviceResult = await _deviceService.SaveProjectDevice(vm);
                if (!updateDeviceResult)
                {
                    transaction.Rollback();
                    return null;
                }

                //更新付款条件
                var updatePaymentTermResult = await _paymentTermService.SaveProjectPaymentTerms(vm);
                if (!updatePaymentTermResult)
                {
                    transaction.Rollback();
                    return null;
                }

                _context.SaveChanges();
                transaction.Commit();
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                throw ex;
            }
        }
        var resultProject = _context.Projects.FirstOrDefault(m => m.ProjectId == vm.ProjectId);
        return resultProject;
    }

    public async Task<PaginatedList<Project>> PaginatedSearchProject(ProjectReqs req)
    {
        var query = _context.Projects.AsQueryable();
        if (req.ProjectId != null)
            query = query.Where(m => m.ProjectId == req.ProjectId);
        if (req.ProjectManagerId != null)
            query = query.Where(m => m.ProjectManagerId == req.ProjectManagerId);
        if (!string.IsNullOrWhiteSpace(req.Content))
        {
            query = query.Where(m => m.ProjectName.Contains(req.Content)
                                    || m.Contract.ContractNumber.Contains(req.Content));
        }
        if (req.StartDate != null)
            query = query.Where(m => m.StartDate > req.StartDate || m.CreateTime > req.StartDate);
        if (req.EndDate != null)
            query = query.Where(m => m.StartDate < req.StartDate || m.CreateTime < req.StartDate);
        if (req.Status != null)
            query = query.Where(m => m.Status == req.Status);
        return await query.OrderByDescending(m => m.Contract.ContractNumber).AsNoTracking().ToPaginatedListAsync(req.Pagination);
    }

    public async Task<List<ProjectAutoCompleteModel>> LoadProjectNames()
    {
        //return await _context.Projects.Select(m => new Project { ProjectId = m.ProjectId, ProjectName = m.ProjectName, Contract = new Contract { ContractNumber = m.Contract.ContractNumber } }).ToListAsync();
        return await _context.Projects.Select(m => new ProjectAutoCompleteModel { Id = m.ProjectId, Name = string.Format("{0}|{1}", m.Contract.ContractNumber, m.ProjectName), Number = m.Contract.ContractNumber, ProjectManagerId = m.ProjectManagerId  }).ToListAsync();
    }

    public async Task<Project?> GetProjectById(Guid id)
    {
        return await _context.Projects.FirstOrDefaultAsync(m=>m.ProjectId==id);
    }

    public async Task<bool> UpdateProjectStartDate(ProjectVm vm)
    {
        var project = await _context.Projects.FirstOrDefaultAsync(m => m.ProjectId == vm.ProjectId);
        if (project != null)
        {
            project.StartDate = vm.StartDate;
            await _context.SaveChangesAsync();
        }
        return true;
    }
    public async Task<bool> UpdateProjectProcess(ProjectVm vm)
    {
        var projectProcesses = await _context.ProjectProcesses.Where(m=>m.ProjectId==vm.ProjectId).ToListAsync();
        if (projectProcesses.Count > 0 && vm.ProjectProcesses != null)
        {
            foreach (var vmProcess in vm.ProjectProcesses)
            {
                var process = projectProcesses.FirstOrDefault(m=>m.Id == vmProcess.Id);
                if (process != null)
                {
                    process.Sequence = vmProcess.Sequence;
                    process.Remarks = vmProcess.Remarks;
                    process.ProcessUnitId = vmProcess.ProcessUnitId;
                    process.StartingWorkload = vmProcess.StartingWorkload;
                    process.Weight = vmProcess.Weight;
                    process.Workload = vmProcess.Workload;
                    process.UpdateBy = GetUserId();
                    process.UpdateTime = DateTime.Now;
                }
            }
            _context.SaveChanges();
        }
        return true;
    }
    public async Task<bool> UpdateProjectPlanData(ProjectVm vm)
    {
        var project = await _context.Projects.FirstOrDefaultAsync(m=>m.ProjectId == vm.ProjectId);
        if (project == null) return false;
        project.PlanEndDate = vm.PlanEndDate;
        project.PlanPersonDays = vm.PlanPersonDays;
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> AcceptanceProject(AcceptanceReq req)
    {
        var dictType = await _dictService.GetDictTypeByName("ProjectStatus");
        if (dictType == null) return false;
        var dictData = dictType.DictData.FirstOrDefault(m => m.DictLabel == "已验收");
        if (dictData == null) return false;
        var project = await _context.Projects.FirstOrDefaultAsync(m => m.ProjectId == req.ProjectId);
        if (project == null) return false;
        project.Status = dictData.DictCode;
        project.AcceptanceDate = req.AcceptanceDate;
        project.UpdateBy = GetUserId();
        project.UpdateTime = DateTime.Now;
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<PaginatedList<Project>> ApproachingPlanDateSearch(ProjectReqs req)
    {
        var projectStatusTypeId = await _dictService.GetDictTypeByName("ProjectStatus");
        var projectStatusDictDataList = await _dictService.GetDictDataByType(projectStatusTypeId.DictId);
        var projectStatusDict = projectStatusDictDataList.FirstOrDefault(m => m.DictLabel == "已开工");
        var currentDate = DateTime.Now.Date;
        return await _context.Projects.Where(m => m.Status == projectStatusDict.DictCode && m.PlanEndDate != null && EF.Functions.DateDiffDay(currentDate, m.PlanEndDate) >= 0
        && EF.Functions.DateDiffDay(currentDate, m.PlanEndDate) <= 7).OrderByDescending(m=>m.Contract.ContractNumber).ToPaginatedListAsync(req.Pagination);
    }

    public async Task<PaginatedList<Project>> DelayedPlanDateSearch(ProjectReqs req)
    {
        var projectStatusTypeId = await _dictService.GetDictTypeByName("ProjectStatus");
        var projectStatusDictDataList = await _dictService.GetDictDataByType(projectStatusTypeId.DictId);
        var projectStatusDict = projectStatusDictDataList.FirstOrDefault(m => m.DictLabel == "已开工");
        var currentDate = DateTime.Now.Date;
        return await _context.Projects.Where(m => m.Status == projectStatusDict.DictCode && m.PlanEndDate != null && m.PlanEndDate < currentDate).OrderByDescending(m=>m.Contract.ContractNumber).ToPaginatedListAsync(req.Pagination);
    }
    public async Task<PaginatedList<Project>> ProcessWarningDateSearch(ProjectReqs req)
    {
        var currentDate = DateTime.Now.Date;
        var projectStatusTypeId = await _dictService.GetDictTypeByName("ProjectStatus");
        var projectStatusDictDataList = await _dictService.GetDictDataByType(projectStatusTypeId.DictId);
        var projectStatusDict = projectStatusDictDataList.FirstOrDefault(m => m.DictLabel == "已开工");
        var warningTypeId = await _dictService.GetDictTypeByName("EarlyWarningType");
        var warningDictDataList = await _dictService.GetDictDataByType(warningTypeId.DictId);
        var warningDict = warningDictDataList.FirstOrDefault(m => m.DictLabel == "项目进度预警");
        return await _context.Projects.Where(m => m.Status == projectStatusDict.DictCode && m.ProjectEarlyWarnings.Count > 0
        && 0<= EF.Functions.DateDiffDay(currentDate, m.PlanEndDate) && EF.Functions.DateDiffDay(currentDate,m.PlanEndDate)
        <= m.ProjectEarlyWarnings.First(m => m.WarningType == warningDict.DictCode).WarningValue)
            .OrderByDescending(m=>m.Contract.ContractNumber).ToPaginatedListAsync(req.Pagination);
    }
}
