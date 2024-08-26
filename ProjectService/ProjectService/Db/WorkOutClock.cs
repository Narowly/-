using System;
using System.Collections.Generic;

namespace ProjectService.Db;

public partial class WorkOutClock
{
    public Guid WorkOutClockId { get; set; }

    public string WorkYearMonth { get; set; } = null!;

    public DateTime OutClockDateTime { get; set; }

    public Guid StaffId { get; set; }

    public virtual Staff Staff { get; set; } = null!;
}
