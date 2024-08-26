using System;
using System.Collections.Generic;

namespace ProjectService.Db;

/// <summary>
/// 员工项目日工作量报量表
/// </summary>
public partial class ProjectDailyWork
{
    /// <summary>
    /// 主键
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// 项目工序ID
    /// </summary>
    public Guid ProjectProcessId { get; set; }

    /// <summary>
    /// 员工ID
    /// </summary>
    public Guid StaffId { get; set; }

    /// <summary>
    /// 工作量
    /// </summary>
    public double Workload { get; set; }

    public DateOnly BillDate { get; set; }

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

    public virtual Staff Staff { get; set; } = null!;
}
