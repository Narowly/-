using System;
using System.Collections.Generic;

namespace ProjectService.Db;

/// <summary>
/// 合同表
/// </summary>
public partial class Contract
{
    /// <summary>
    /// 合同ID
    /// </summary>
    public Guid ContractId { get; set; }

    /// <summary>
    /// 合同编号
    /// </summary>
    public string ContractNumber { get; set; } = null!;

    /// <summary>
    /// 合同名称
    /// </summary>
    public string ContractName { get; set; } = null!;

    /// <summary>
    /// 合同金额
    /// </summary>
    public decimal ContractAmount { get; set; }

    /// <summary>
    /// 合同付款
    /// </summary>
    public decimal ContractPayAmount { get; set; }

    /// <summary>
    /// 客户ID
    /// </summary>
    public Guid CustomerId { get; set; }

    /// <summary>
    /// 客户联系人ID
    /// </summary>
    public Guid? CustomerContactId { get; set; }

    public Guid? SalesManagerId { get; set; }

    /// <summary>
    /// 合同开始时间
    /// </summary>
    public DateTime ContractStartDate { get; set; }

    /// <summary>
    /// 合同结束时间
    /// </summary>
    public DateTime ContractEndDate { get; set; }

    /// <summary>
    /// 是否删除
    /// </summary>
    public bool? IsDeleted { get; set; }

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

    public virtual Customer Customer { get; set; } = null!;

    public virtual CustomerContact? CustomerContact { get; set; }

    public virtual ICollection<Project> Projects { get; set; } = new List<Project>();

    public virtual Staff? SalesManager { get; set; }
}
