﻿using System;
using System.Collections.Generic;

namespace ProjectService.Db;

public partial class ConsumableAskForItem
{
    public Guid ConsumableAskForItemId { get; set; }

    public Guid ConsumableAskForId { get; set; }

    public Guid ConsumableTypeId { get; set; }

    public int Quantity { get; set; }

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

    public virtual ConsumableAskFor ConsumableAskFor { get; set; } = null!;

    public virtual ConsumableType ConsumableType { get; set; } = null!;
}
