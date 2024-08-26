using System;
using System.Collections.Generic;

namespace ProjectService.Db;

/// <summary>
/// 工序模板详情表
/// </summary>
public partial class ProcessTemplateDetail
{
    /// <summary>
    /// 详情ID号
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// 模板ID号
    /// </summary>
    public int TemplateId { get; set; }

    /// <summary>
    /// 工序单位关联ID
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

    public virtual ProcessTemplate Template { get; set; } = null!;
}
