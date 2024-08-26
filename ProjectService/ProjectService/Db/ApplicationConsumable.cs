using System;
using System.Collections.Generic;

namespace ProjectService.Db;

public partial class ApplicationConsumable
{
    public Guid ApplicationConsumableId { get; set; }

    public Guid ApplicationId { get; set; }

    public Guid? ConsumableTypeId { get; set; }

    public int? Quantity { get; set; }

    public Guid? CreateBy { get; set; }

    public DateTime? CreateTime { get; set; }

    public Guid? UpdateBy { get; set; }

    public DateTime? UpdateTime { get; set; }

    public string? Remarks { get; set; }

    public virtual Application Application { get; set; } = null!;

    public virtual ConsumableType? ConsumableType { get; set; }
}
