using System;
using System.Collections.Generic;

namespace ProjectService.Db;

/// <summary>
/// 项目员工表
/// </summary>
public partial class ProjectStaff
{
    /// <summary>
    /// 项目员工表ID
    /// </summary>
    public Guid AssociationId { get; set; }

    /// <summary>
    /// 员工ID
    /// </summary>
    public Guid StaffId { get; set; }

    /// <summary>
    /// 项目ID
    /// </summary>
    public Guid ProjectId { get; set; }

    /// <summary>
    /// 调入日期
    /// </summary>
    public DateTime TransferInDate { get; set; }

    /// <summary>
    /// 调出日期
    /// </summary>
    public DateTime? TransferOutDate { get; set; }

    /// <summary>
    /// 调入操作人员
    /// </summary>
    public Guid TransferInOperator { get; set; }

    /// <summary>
    /// 调出操作人员
    /// </summary>
    public Guid? TransferOutOperator { get; set; }

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

    public virtual Project Project { get; set; } = null!;

    public virtual Staff Staff { get; set; } = null!;
}
