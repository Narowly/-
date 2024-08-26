using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ProjectService.Db;
using ProjectService.ViewModels;
using ProjectViewModels;
using System.Collections.ObjectModel;

namespace ProjectService.Services
{
    public class ProcessService:UserService
    {
        private readonly ProjectDbContext _context;
        public ProcessService(ProjectDbContext context, IHttpContextAccessor httpContextAccessor) : base(httpContextAccessor)
        {
            _context = context;
        }

        public async Task<List<Process>> GetProcessList()
        {
            return await _context.Processes.ToListAsync();
        }

        public async Task<List<ProcessUnit>> GetProcessUnitList()
        {
            return await _context.ProcessUnits.ToListAsync();
        }

        public async Task<List<ProcessTemplate>> GetProcessTemplateList()
        {
            return await _context.ProcessTemplates.ToListAsync();
        }
        public async Task<ProcessTemplate?> GetProcessTemplate(int templateId)
        {
            return await _context.ProcessTemplates.FirstOrDefaultAsync(m => m.Id == templateId);
        }
        public async Task<bool> SaveProcessTemplate(ProcessTemplateVm vm)
        {
            using (var transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    var template = _context.ProcessTemplates.FirstOrDefault(m => m.Id == vm.Id);
                    if (template == null)
                    {
                        template = new ProcessTemplate
                        {
                            Name = vm.Name,
                            CreateBy = GetUserId(),
                            CreateTime = DateTime.Now,
                            Remarks = vm.Remarks
                        };
                        _context.ProcessTemplates.Add(template);
                        _context.SaveChanges();
                    }
                    else
                    {
                        template.Name = vm.Name;
                        template.UpdateBy = GetUserId();
                        template.UpdateTime = DateTime.Now;
                        template.Remarks = vm.Remarks;
                    }
                    var details = _context.ProcessTemplateDetails.Where(m => m.TemplateId == template.Id).ToList();
                    _context.ProcessTemplateDetails.RemoveRange(details);
                    if (vm.ProcessTemplateDetails != null)
                    {
                        foreach (var detail in vm.ProcessTemplateDetails)
                        {
                            _context.ProcessTemplateDetails.Add(new ProcessTemplateDetail
                            {
                                CreateBy = GetUserId(),
                                CreateTime = DateTime.Now,
                                ProcessUnitId = detail.ProcessUnitId,
                                Remarks = detail.Remarks,
                                Sequence = detail.Sequence,
                                TemplateId = template.Id,
                                Weight = detail.Weight
                            });
                        }
                    }
                    await _context.SaveChangesAsync();
                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw ex;
                }
            }
            return true;        
            
        }

        public async Task<List<ProjectProcess>> GetProjectProcesses(Guid projectId) 
        {
            return await _context.ProjectProcesses.Where(m=>m.ProjectId == projectId).ToListAsync();
        }
        public async Task<List<ProcessStaffRelatedSettingsVm>> GetProjectProcessStaffRelatedSettings(Guid projectId)
        {
            var staffList = await _context.ProjectStaffs.Where(m => m.ProjectId == projectId && m.TransferOutDate == null).Select(m => m.Staff).ToListAsync();
            var processUnitList = await _context.ProjectProcesses.Where(m => m.ProjectId == projectId).Select(m => m.ProcessUnit).ToListAsync();
            var relatedList = await _context.ProjectProcessStaffRelateds.Where(m => m.ProjectId == projectId).OrderBy(m => m.ProcessUnitId).ToListAsync();
            var resultList = new List<ProcessStaffRelatedSettingsVm>();
            foreach (var related in relatedList)
            {
                if (staffList.Any(m => m.StaffId == related.StaffId) && processUnitList.Any(m => m.Id == related.ProcessUnitId))
                {
                    var relatedVm = resultList.FirstOrDefault(m => m.SelectedProcessUnit?.Id == related.ProcessUnitId && m.SelectedStaff?.StaffId == related.StaffId);
                    if (relatedVm == null)
                    {
                        relatedVm = new ProcessStaffRelatedSettingsVm();
                        relatedVm.ProjectId = projectId;
                        relatedVm.SelectedProcessUnit = related.ProcessUnit.ToViewModel();
                        relatedVm.AvailableProcessUnitOptions = new ObservableCollection<ProcessUnitVm>(processUnitList.Select(m => m.ToViewModel()));
                        relatedVm.SelectedStaff = related.Staff.ToViewModel();
                        relatedVm.AvailableStaffOptions = new ObservableCollection<StaffVm>(staffList.Select(m=>m.ToViewModel()));
                        resultList.Add(relatedVm);
                    }
                }
            }
            if (relatedList.Count == 0)
            {
                resultList.Add(new ProcessStaffRelatedSettingsVm
                {
                    ProjectId = projectId,
                    AvailableProcessUnitOptions = new ObservableCollection<ProcessUnitVm>(processUnitList.Select(m => m.ToViewModel())),
                    AvailableStaffOptions = new ObservableCollection<StaffVm>(staffList.Select(m => m.ToViewModel()))
                });
            }
            return resultList;
        }

        public async Task<List<ProcessStaffRelatedVm>> GetProjectProcessStaffRelatedList(Guid projectId)
        {
            return await _context.ProjectProcessStaffRelateds.Where(m => m.ProjectId == projectId).Select(m => m.ToViewModel()).ToListAsync();
        }
        public async Task<bool> SaveProjectProcessStaffRelated(List<ProcessStaffRelatedVm> list)
        {
            Guid projectId;
            if (list != null && list.Count > 0)
            {
                projectId = list[0].ProjectId;
            }
            else return false;
            var relateds = await _context.ProjectProcessStaffRelateds.Where(m => m.ProjectId == projectId).ToListAsync();
            _context.ProjectProcessStaffRelateds.RemoveRange(relateds);
            await _context.SaveChangesAsync();

            foreach (var vm in list)
            {
                _context.ProjectProcessStaffRelateds.Add(new ProjectProcessStaffRelated
                {
                    ProcessUnitId = vm.ProcessUnitId,
                    ProjectId = vm.ProjectId,
                    RelatedId = Guid.NewGuid(),
                    StaffId = vm.StaffId
                });
            }
            await _context.SaveChangesAsync();
            return true;
        }
        //public async Task<bool> SaveProjectProcessStaffRelated(List<ProcessStaffRelatedSettingsVm> list)
        //{
        //    Guid projectId;
        //    if (list != null && list.Count > 0)
        //    {
        //        projectId = list[0].ProjectId;
        //    } else return false;
        //    var relateds = _context.ProjectProcessStaffRelateds.Where(m => m.ProjectId == projectId).ToList();
        //    _context.ProjectProcessStaffRelateds.RemoveRange(relateds);
        //    _context.SaveChanges();
        //    foreach(var vm in list)
        //    {
        //        if (vm.SelectedStaff == null || vm.SelectedProcessUnit == null) continue;                
        //        _context.ProjectProcessStaffRelateds.Add(new ProjectProcessStaffRelated
        //        {
        //            ProjectId = vm.ProjectId,
        //            ProcessUnitId = vm.SelectedProcessUnit.Id,
        //            RelatedId = Guid.NewGuid(),
        //            StaffId = vm.SelectedStaff.StaffId
        //        });
        //    }
        //    await _context.SaveChangesAsync();
        //    return true;
        //}

    }
}
