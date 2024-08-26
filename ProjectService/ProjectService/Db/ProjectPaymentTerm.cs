using System;
using System.Collections.Generic;

namespace ProjectService.Db;

public partial class ProjectPaymentTerm
{
    public long PaymentTermsId { get; set; }

    public Guid ProjectId { get; set; }

    public double WorkloadPercentage { get; set; }

    public Guid? CreateBy { get; set; }

    public DateTime? CreateTime { get; set; }

    public Guid? UpdateBy { get; set; }

    public DateTime? UpdateTime { get; set; }

    public string? Remarks { get; set; }

    public virtual Project Project { get; set; } = null!;
}
