using System;
using System.Collections.Generic;

namespace ProjectService.Db;

/// <summary>
/// 项目进度调整表
/// </summary>
public partial class ProjectUpdateSchedule
{
    /// <summary>
    /// 进度表主键
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// 项目ID
    /// </summary>
    public Guid ProjectId { get; set; }

    /// <summary>
    /// 原计划结束日期
    /// </summary>
    public DateTime PlanEndDate { get; set; }

    /// <summary>
    /// 更新后计划结束日期
    /// </summary>
    public DateTime UpdatedEndDate { get; set; }

    /// <summary>
    /// 原因类型
    /// </summary>
    public int ReasonType { get; set; }

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
}
