using System;
using System.Collections.Generic;

namespace ProjectService.Db;

public partial class WorkApplyLeave
{
    public string WorkApplyLeaveId { get; set; } = null!;

    public string WorkYearMonth { get; set; } = null!;

    public Guid StaffId { get; set; }

    public string LeaveType { get; set; } = null!;

    public DateTime StartTime { get; set; }

    public DateTime EndTime { get; set; }

    public virtual Staff Staff { get; set; } = null!;
}
