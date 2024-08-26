﻿using System;
using System.Collections.Generic;

namespace ProjectService.Db;

/// <summary>
/// 客户联系人表
/// </summary>
public partial class CustomerContact
{
    /// <summary>
    /// 客户联系人表
    /// </summary>
    public Guid CustomerContactId { get; set; }

    /// <summary>
    /// 联系人名称
    /// </summary>
    public string ContactName { get; set; } = null!;

    /// <summary>
    /// 联系方式
    /// </summary>
    public string? Mobile { get; set; }

    /// <summary>
    /// 客户ID
    /// </summary>
    public Guid CustomerId { get; set; }

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

    public virtual ICollection<Contract> Contracts { get; set; } = new List<Contract>();

    public virtual Customer Customer { get; set; } = null!;
}
