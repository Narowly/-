using System;
using System.Collections.Generic;

namespace ProjectService.Db;

public partial class WorkDelayClock
{
    public string DelayClockId { get; set; } = null!;

    public string WorkYearMonth { get; set; } = null!;

    public Guid StaffId { get; set; }

    public string Reason { get; set; } = null!;

    public string ApplyWorkTime { get; set; } = null!;

    public DateTime DelayClockTime { get; set; }

    public virtual Staff Staff { get; set; } = null!;
}
