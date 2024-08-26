using System;
using System.Collections.Generic;

namespace ProjectService.Db;

public partial class ApplicationPerson
{
    public Guid ApplicationPersonId { get; set; }

    public Guid ApplicationId { get; set; }

    public int? ProcessId { get; set; }

    public int? Count { get; set; }

    public Guid? CreateBy { get; set; }

    public DateTime? CreateTime { get; set; }

    public Guid? UpdateBy { get; set; }

    public DateTime? UpdateTime { get; set; }

    public string? Remarks { get; set; }

    public virtual Application Application { get; set; } = null!;

    public virtual Process? Process { get; set; }
}
