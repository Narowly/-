using System;
using System.Collections.Generic;

namespace ProjectService.Db;

/// <summary>
/// 申请表
/// </summary>
public partial class Application
{
    /// <summary>
    /// 申请ID
    /// </summary>
    public Guid ApplicationId { get; set; }

    /// <summary>
    /// 项目ID
    /// </summary>
    public Guid ProjectId { get; set; }

    /// <summary>
    /// 申请类型（1，设备，2消耗品，3人员调动）
    /// </summary>
    public int ApplicationType { get; set; }

    /// <summary>
    /// 申请人，项目主管
    /// </summary>
    public Guid ApplicationUser { get; set; }

    /// <summary>
    /// 申请标题
    /// </summary>
    public string ApplicationTitle { get; set; } = null!;

    /// <summary>
    /// 申请内容
    /// </summary>
    public string ApplicationContent { get; set; } = null!;

    /// <summary>
    /// 申请数量
    /// </summary>
    public int? ApplicationItemCount { get; set; }

    /// <summary>
    /// 申请收货地址
    /// </summary>
    public string? ApplicationDelivery { get; set; }

    /// <summary>
    /// 申请时间
    /// </summary>
    public DateTime ApplicationTime { get; set; }

    /// <summary>
    /// 是否删除
    /// </summary>
    public bool IsDeleted { get; set; }

    /// <summary>
    /// 申请状态（0申请，1通过，2未通过）
    /// </summary>
    public int ApplicationStatus { get; set; }

    /// <summary>
    /// 审批回复内容
    /// </summary>
    public string? ApplicationResContent { get; set; }

    /// <summary>
    /// 审批时间
    /// </summary>
    public DateTime? ApplicationResTime { get; set; }

    public Guid? ApplicationResUserId { get; set; }

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

    public virtual ICollection<ApplicationDevice> ApplicationDevices { get; set; } = new List<ApplicationDevice>();

    public virtual ICollection<ApplicationPerson> ApplicationPeople { get; set; } = new List<ApplicationPerson>();

    public virtual Staff ApplicationUserNavigation { get; set; } = null!;

    public virtual Project Project { get; set; } = null!;
}
