using System;
using System.Collections.Generic;

namespace ProjectService.Db;

public partial class ProjectProcessStaffRelated
{
    public Guid RelatedId { get; set; }

    public Guid ProjectId { get; set; }

    public Guid StaffId { get; set; }

    public int ProcessUnitId { get; set; }

    public virtual ProcessUnit ProcessUnit { get; set; } = null!;

    public virtual Project Project { get; set; } = null!;

    public virtual Staff Staff { get; set; } = null!;
}
