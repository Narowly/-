using System;
using System.Collections.Generic;

namespace ProjectService.Db;

public partial class WorkAttendance
{
    public Guid WorkAttendanceId { get; set; }

    public string WorkYearMonth { get; set; } = null!;

    public Guid StaffId { get; set; }

    public string StaffAccount { get; set; } = null!;

    public string StaffName { get; set; } = null!;

    public string ProjectName { get; set; } = null!;

    public string? ClockIn01 { get; set; }

    public string? ClockOut01 { get; set; }

    public string? ClockIn02 { get; set; }

    public string? ClockOut02 { get; set; }

    public string? ClockIn03 { get; set; }

    public string? ClockOut03 { get; set; }

    public string? ClockIn04 { get; set; }

    public string? ClockOut04 { get; set; }

    public string? ClockIn05 { get; set; }

    public string? ClockOut05 { get; set; }

    public string? ClockIn06 { get; set; }

    public string? ClockOut06 { get; set; }

    public string? ClockIn07 { get; set; }

    public string? ClockOut07 { get; set; }

    public string? ClockIn08 { get; set; }

    public string? ClockOut08 { get; set; }

    public string? ClockIn09 { get; set; }

    public string? ClockOut09 { get; set; }

    public string? ClockIn10 { get; set; }

    public string? ClockOut10 { get; set; }

    public string? ClockIn11 { get; set; }

    public string? ClockOut11 { get; set; }

    public string? ClockIn12 { get; set; }

    public string? ClockOut12 { get; set; }

    public string? ClockIn13 { get; set; }

    public string? ClockOut13 { get; set; }

    public string? ClockIn14 { get; set; }

    public string? ClockOut14 { get; set; }

    public string? ClockIn15 { get; set; }

    public string? ClockOut15 { get; set; }

    public string? ClockIn16 { get; set; }

    public string? ClockOut16 { get; set; }

    public string? ClockIn17 { get; set; }

    public string? ClockOut17 { get; set; }

    public string? ClockIn18 { get; set; }

    public string? ClockOut18 { get; set; }

    public string? ClockIn19 { get; set; }

    public string? ClockOut19 { get; set; }

    public string? ClockIn20 { get; set; }

    public string? ClockOut20 { get; set; }

    public string? ClockIn21 { get; set; }

    public string? ClockOut21 { get; set; }

    public string? ClockIn22 { get; set; }

    public string? ClockOut22 { get; set; }

    public string? ClockIn23 { get; set; }

    public string? ClockOut23 { get; set; }

    public string? ClockIn24 { get; set; }

    public string? ClockOut24 { get; set; }

    public string? ClockIn25 { get; set; }

    public string? ClockOut25 { get; set; }

    public string? ClockIn26 { get; set; }

    public string? ClockOut26 { get; set; }

    public string? ClockIn27 { get; set; }

    public string? ClockOut27 { get; set; }

    public string? ClockIn28 { get; set; }

    public string? ClockOut28 { get; set; }

    public string? ClockIn29 { get; set; }

    public string? ClockOut29 { get; set; }

    public string? ClockIn30 { get; set; }

    public string? ClockOut30 { get; set; }

    public string? ClockIn31 { get; set; }

    public string? ClockOut31 { get; set; }

    public virtual Staff Staff { get; set; } = null!;
}
