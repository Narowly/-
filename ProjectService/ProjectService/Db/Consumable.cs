using System;
using System.Collections.Generic;

namespace ProjectService.Db;

/// <summary>
/// 消耗品表
/// </summary>
public partial class Consumable
{
    /// <summary>
    /// 消耗品ID
    /// </summary>
    public Guid ConsumableId { get; set; }

    /// <summary>
    /// 消耗品编号
    /// </summary>
    public string ConsumableNumber { get; set; } = null!;

    /// <summary>
    /// 消耗品类型ID
    /// </summary>
    public Guid ConsumableTypeId { get; set; }

    /// <summary>
    /// 消耗品状态
    /// </summary>
    public int ConsumableStatus { get; set; }

    /// <summary>
    /// 数量
    /// </summary>
    public int Quantity { get; set; }

    public decimal? Price { get; set; }

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

    public virtual ConsumableType ConsumableType { get; set; } = null!;

    public virtual ICollection<StockInBound> StockInBounds { get; set; } = new List<StockInBound>();

    public virtual ICollection<StockOutBound> StockOutBounds { get; set; } = new List<StockOutBound>();
}
