using System;
using System.Collections.Generic;

namespace ProjectService.Db;

/// <summary>
/// 项目暂停历史表
/// </summary>
public partial class ProjectSuspendedHistory
{
    /// <summary>
    /// 历史表主键
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// 项目ID
    /// </summary>
    public Guid ProjectId { get; set; }

    /// <summary>
    /// 开工日期
    /// </summary>
    public DateTime StartDate { get; set; }

    /// <summary>
    /// 暂停日期
    /// </summary>
    public DateTime? SuspendedDate { get; set; }

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
