using System;
using System.Collections.Generic;

namespace ProjectService.Db;

/// <summary>
/// 工序单位关联表
/// </summary>
public partial class ProcessUnit
{
    /// <summary>
    /// 关联ID
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// 工序ID
    /// </summary>
    public int ProcessId { get; set; }

    /// <summary>
    /// 单位ID
    /// </summary>
    public int UnitId { get; set; }

    /// <summary>
    /// 创建者
    /// </summary>
    public Guid? CreateBy { get; set; }

    /// <summary>
    /// 创建时间
    /// </summary>
    public DateTime? CreateTime { get; set; }

    /// <summary>
    /// 更新者
    /// </summary>
    public Guid? UpdateBy { get; set; }

    /// <summary>
    /// 更新时间
    /// </summary>
    public DateTime? UpdateTime { get; set; }

    /// <summary>
    /// 备注
    /// </summary>
    public string? Remarks { get; set; }

    public virtual Process Process { get; set; } = null!;

    public virtual ICollection<ProcessTemplateDetail> ProcessTemplateDetails { get; set; } = new List<ProcessTemplateDetail>();

    public virtual ICollection<ProjectProcessStaffRelated> ProjectProcessStaffRelateds { get; set; } = new List<ProjectProcessStaffRelated>();

    public virtual ICollection<ProjectProcess> ProjectProcesses { get; set; } = new List<ProjectProcess>();

    public virtual ProUnit Unit { get; set; } = null!;
}
