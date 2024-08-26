using System;
using System.Collections.Generic;

namespace ProjectService.Db;

/// <summary>
/// 项目工序表
/// </summary>
public partial class ProjectProcess
{
    /// <summary>
    /// 工序ID
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// 项目ID号
    /// </summary>
    public Guid ProjectId { get; set; }

    /// <summary>
    /// 工序单位关联表ID
    /// </summary>
    public int ProcessUnitId { get; set; }

    /// <summary>
    /// 权重
    /// </summary>
    public decimal Weight { get; set; }

    /// <summary>
    /// 顺序号
    /// </summary>
    public int Sequence { get; set; }

    /// <summary>
    /// 工作总量
    /// </summary>
    public double Workload { get; set; }

    /// <summary>
    /// 阶段开始工作量起始量
    /// </summary>
    public double StartingWorkload { get; set; }

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

    public virtual ProcessUnit ProcessUnit { get; set; } = null!;

    public virtual Project Project { get; set; } = null!;

    public virtual ICollection<ProjectBonu> ProjectBonus { get; set; } = new List<ProjectBonu>();

    public virtual ICollection<ProjectDailyProcess> ProjectDailyProcesses { get; set; } = new List<ProjectDailyProcess>();

    public virtual ICollection<ProjectDailyWork> ProjectDailyWorks { get; set; } = new List<ProjectDailyWork>();
}
