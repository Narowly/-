using System;
using System.Collections.Generic;

namespace ProjectService.Db;

/// <summary>
/// 项目巡查表
/// </summary>
public partial class ProjectPatrol
{
    /// <summary>
    /// 巡查表主键
    /// </summary>
    public long Id { get; set; }

    /// <summary>
    /// 项目号
    /// </summary>
    public Guid ProjectId { get; set; }

    /// <summary>
    /// 员工号
    /// </summary>
    public Guid StaffId { get; set; }

    /// <summary>
    /// 巡查日期
    /// </summary>
    public DateTime PatrolDate { get; set; }

    /// <summary>
    /// 巡查状态（0无需整改，1已整改，2未整改）
    /// </summary>
    public int Status { get; set; }

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

    public virtual Project Project { get; set; } = null!;

    public virtual Staff Staff { get; set; } = null!;
}
