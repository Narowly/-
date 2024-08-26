using System;
using System.Collections.Generic;

namespace ProjectService.Db;

/// <summary>
/// 消耗品调入表
/// </summary>
public partial class StockInBound
{
    /// <summary>
    /// 调入ID
    /// </summary>
    public Guid InBoundId { get; set; }

    /// <summary>
    /// 消耗品ID
    /// </summary>
    public Guid ConsumableId { get; set; }

    /// <summary>
    /// 数量
    /// </summary>
    public int Quantity { get; set; }

    /// <summary>
    /// 调入日期
    /// </summary>
    public DateTime InBoundDate { get; set; }

    /// <summary>
    /// 项目ID
    /// </summary>
    public Guid? ProjectId { get; set; }

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

    public virtual Consumable Consumable { get; set; } = null!;

    public virtual Project? Project { get; set; }
}
