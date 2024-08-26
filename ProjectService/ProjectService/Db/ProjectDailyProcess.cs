using System;
using System.Collections.Generic;

namespace ProjectService.Db;

/// <summary>
/// 项目日工作量设定表
/// </summary>
public partial class ProjectDailyProcess
{
    /// <summary>
    /// 日工作量表ID
    /// </summary>
    public long Id { get; set; }

    /// <summary>
    /// 项目工序ID
    /// </summary>
    public Guid ProjectProcessId { get; set; }

    /// <summary>
    /// 日工作量
    /// </summary>
    public double DailyWorkload { get; set; }

    /// <summary>
    /// 开始时间
    /// </summary>
    public DateTime StartDate { get; set; }

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

    public virtual ProjectProcess ProjectProcess { get; set; } = null!;
}
