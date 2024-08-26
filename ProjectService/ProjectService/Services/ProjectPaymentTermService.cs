using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using ProjectService.Db;
using ProjectViewModels;

namespace ProjectService.Services
{
    public class ProjectPaymentTermService : UserService
    {
        private readonly ProjectDbContext _context;
        public ProjectPaymentTermService(ProjectDbContext context, IHttpContextAccessor httpContextAccessor): base(httpContextAccessor)
        {
            _context = context;
        }

        public async Task<bool> SaveProjectPaymentTerm(ProjectPaymentTermVm vm)
        {
            ProjectPaymentTerm? term;
            if (vm.PaymentTermsId == null)
            {
                term = new ProjectPaymentTerm();
                term.CreateBy = GetUserId();
                term.CreateTime = DateTime.Now;
            }
            else
            {
                term = _context.ProjectPaymentTerms.FirstOrDefault(m => m.PaymentTermsId == vm.PaymentTermsId.Value);
                if (term == null) throw new Exception($"payment term id {vm.PaymentTermsId} 未找到");
                term.UpdateBy = GetUserId();
                term.UpdateTime = DateTime.Now;
            }
            term.Remarks = vm.Remarks;
            term.WorkloadPercentage = vm.WorkloadPercentage;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> SaveProjectPaymentTerms(ProjectVm vm)
        {
            var paymentTerms = _context.ProjectPaymentTerms.Where(m => m.ProjectId == vm.ProjectId).ToList();
            if (vm.ProjectPyamentTerms == null || vm.ProjectPyamentTerms.Count == 0)
            {
                _context.ProjectPaymentTerms.RemoveRange(paymentTerms);
                await _context.SaveChangesAsync();
                return true;
            }            
            
            var paymentTermVms = vm.ProjectPyamentTerms.ToList();
            var paymentTermVmIds = paymentTermVms.Select(m => m.PaymentTermsId).ToList();
            var removeTerms = paymentTerms.Where(m => !paymentTermVmIds.Contains(m.PaymentTermsId)).ToList();
            _context.ProjectPaymentTerms.RemoveRange(removeTerms);
            foreach (var termVm in paymentTermVms)
            {
                var term = paymentTerms.FirstOrDefault(m => m.PaymentTermsId == termVm.PaymentTermsId);
                if (term == null)
                {
                    term = new ProjectPaymentTerm();
                    term.Remarks = termVm.Remarks;
                    term.WorkloadPercentage = termVm.WorkloadPercentage;
                    term.ProjectId = vm.ProjectId.Value;
                    term.CreateBy = GetUserId();
                    term.CreateTime = DateTime.Now;
                    _context.ProjectPaymentTerms.Add(term);
                }
                else
                {
                    term.Remarks = termVm.Remarks;
                    term.WorkloadPercentage = termVm.WorkloadPercentage;
                    term.UpdateBy = GetUserId();
                    term.UpdateTime = DateTime.Now;
                }                
            }
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<List<ProjectPaymentTerm>> GetProjectPaymentTermList(Guid projectId)
        {
            return await _context.ProjectPaymentTerms.Where(m=>m.ProjectId == projectId).ToListAsync();
        }
    }
}
