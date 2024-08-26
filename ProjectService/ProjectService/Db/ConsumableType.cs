using System;
using System.Collections.Generic;

namespace ProjectService.Db;

/// <summary>
/// 消耗品类型表
/// </summary>
public partial class ConsumableType
{
    /// <summary>
    /// 消耗品类型ID
    /// </summary>
    public Guid ConsumableTypeId { get; set; }

    /// <summary>
    /// 消耗品类型名称
    /// </summary>
    public string ConsumableTypeName { get; set; } = null!;

    /// <summary>
    /// 消耗品型号
    /// </summary>
    public string? ConsumableModel { get; set; }

    /// <summary>
    /// 消耗品单位
    /// </summary>
    public string? ConsumableUnit { get; set; }

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

    public virtual ICollection<ApplicationConsumable> ApplicationConsumables { get; set; } = new List<ApplicationConsumable>();

    public virtual ICollection<ConsumableAskForItem> ConsumableAskForItems { get; set; } = new List<ConsumableAskForItem>();

    public virtual ICollection<Consumable> Consumables { get; set; } = new List<Consumable>();
}
