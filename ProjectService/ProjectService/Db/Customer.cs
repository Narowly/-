using System;
using System.Collections.Generic;

namespace ProjectService.Db;

/// <summary>
/// 客户表
/// </summary>
public partial class Customer
{
    /// <summary>
    /// 客户ID
    /// </summary>
    public Guid CustomerId { get; set; }

    /// <summary>
    /// 客户名称
    /// </summary>
    public string CustomerName { get; set; } = null!;

    /// <summary>
    /// 客户编号
    /// </summary>
    public string CustNo { get; set; } = null!;

    /// <summary>
    /// 客户地址
    /// </summary>
    public string? CustAddress { get; set; }

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

    public virtual ICollection<CustomerContact> CustomerContacts { get; set; } = new List<CustomerContact>();
}
