using System;
using System.Collections.Generic;

namespace ProjectService.Db;

public partial class ConsumableAskFor
{
    public Guid ConsumableAskForId { get; set; }

    public string Title { get; set; } = null!;

    public string? Content { get; set; }

    public Guid StaffId { get; set; }

    public Guid? ProjectId { get; set; }

    public int Status { get; set; }

    public Guid? CreateBy { get; set; }

    public DateTime? CreateTime { get; set; }

    public Guid? UpdateBy { get; set; }

    public DateTime? UpdateTime { get; set; }

    public string? Remarks { get; set; }

    public virtual ICollection<ConsumableAskForItem> ConsumableAskForItems { get; set; } = new List<ConsumableAskForItem>();

    public virtual Project? Project { get; set; }

    public virtual Staff Staff { get; set; } = null!;
}
