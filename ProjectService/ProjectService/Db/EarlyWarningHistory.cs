using System;
using System.Collections.Generic;

namespace ProjectService.Db;

public partial class EarlyWarningHistory
{
    public long Id { get; set; }

    public Guid ProjectId { get; set; }

    public int WarningType { get; set; }

    public double? WarningValue { get; set; }

    public string? WarningMessages { get; set; }

    public int? Status { get; set; }

    public string? StaffReason { get; set; }

    public string? ManagerReason { get; set; }

    public string? Suggestions { get; set; }

    public Guid? CreateBy { get; set; }

    public DateTime? CreateTime { get; set; }

    public Guid? UpdateBy { get; set; }

    public DateTime? UpdateTime { get; set; }

    public string? Remarks { get; set; }

    public virtual Project Project { get; set; } = null!;
}
