using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ProjectService.Db;
using ProjectViewModels;

namespace ProjectService.Services
{
    public class ProjectAttachmentService : UserService
    {
        private readonly ProjectDbContext _context;
        public ProjectAttachmentService(IHttpContextAccessor httpContextAccessor, ProjectDbContext context) :base(httpContextAccessor)
        {
            _context = context;
        }
        public async Task<bool> SaveProjectAttachment(List<ProjectAttachment> list)
        {
            //var projectId = list.Select(m => m.ProjectId).FirstOrDefault();
            //var project = _context.Projects.FirstOrDefault(p => p.ProjectId == projectId);
            foreach (var attachment in list)
            {
                var dto = _context.ProjectAttachments.FirstOrDefault(m => m.ProjectId == attachment.ProjectId && m.FileName.Equals(attachment.FileName));
                if (dto != null)
                {
                    dto.UpdateTime = DateTime.Now;
                    dto.UploadDate = attachment.UploadDate;
                    dto.UpdateBy = GetUserId();
                    dto.FileAddress = attachment.FileAddress;
                    dto.FileType = attachment.FileType;
                    dto.Remarks = attachment.Remarks;
                }
                else
                {
                    dto = new ProjectAttachment
                    {
                        Id = Guid.NewGuid(),
                        Remarks = attachment.Remarks,
                        CreateBy = GetUserId(),
                        CreateTime = DateTime.Now,
                        FileAddress = attachment.FileAddress,
                        FileName = attachment.FileName,
                        FileType = attachment.FileType,
                        ProjectId = attachment.ProjectId,
                        UploadDate = attachment.UploadDate
                    };
                    _context.ProjectAttachments.Add(dto);
                }
            }
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<List<ProjectAttachment>> GetProjectAttachments(Guid projectId)
        {
            var projectAttachments = await _context.ProjectAttachments.Where(m=>m.ProjectId == projectId).ToListAsync();
            return projectAttachments;
        }
        public async Task<bool> RemoveProjectAttachment(ProjectAttachmentVm vm)
        {
            var attachment = await _context.ProjectAttachments.FirstOrDefaultAsync(m => m.Id == vm.Id);
            if (attachment != null)
            {
                _context.ProjectAttachments.Remove(attachment);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<PaginatedList<ProjectAttachment>> PaginatedProjectAttachments(ProjectReqs req)
        {
            var query = _context.ProjectAttachments.AsQueryable();
            if (req.ProjectId != null)
            {
                query = query.Where(m => m.ProjectId == req.ProjectId);
            }
            else if (!string.IsNullOrWhiteSpace(req.Content))
            {
                query = query.Where(m => m.Project.ProjectName.Contains(req.Content) || m.Project.Contract.ContractName.Contains(req.Content) || m.FileName.Contains(req.Content));
            }

            return await query.OrderByDescending(m=> m.CreateTime).AsNoTracking().ToPaginatedListAsync(req.Pagination);

        }

        public async Task<List<AttachmentRequirement>> GetAttachmentRequirementList()
        {
            return await _context.AttachmentRequirements.ToListAsync();
        }
    }
}
