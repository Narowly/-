using System;
using System.Collections.Generic;

namespace ProjectService.Db;

public partial class ProjectBonusEx
{
    public long Id { get; set; }

    public Guid? ProjectId { get; set; }

    public int? PlanPersonDays { get; set; }

    public double? Bonus { get; set; }

    public double? Rewards { get; set; }

    public double? Penalty { get; set; }

    public Guid? CreateBy { get; set; }

    public DateTime? CreateTime { get; set; }

    public Guid? UpdateBy { get; set; }

    public DateTime? UpdateTime { get; set; }

    public string? Remarks { get; set; }

    public virtual Project? Project { get; set; }
}
