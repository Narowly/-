using System;
using System.Collections.Generic;

namespace ProjectService.Db;

/// <summary>
/// 项目预警表
/// </summary>
public partial class ProjectEarlyWarning
{
    /// <summary>
    /// 预警ID
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// 项目ID
    /// </summary>
    public Guid ProjectId { get; set; }

    /// <summary>
    /// 预警类型
    /// </summary>
    public int WarningType { get; set; }

    /// <summary>
    /// 预警值
    /// </summary>
    public double WarningValue { get; set; }

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
