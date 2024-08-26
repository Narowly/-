using System;
using System.Collections.Generic;

namespace ProjectService.Db;

/// <summary>
/// 项目表
/// </summary>
public partial class Project
{
    /// <summary>
    /// 项目ID
    /// </summary>
    public Guid ProjectId { get; set; }

    /// <summary>
    /// 项目名称
    /// </summary>
    public string ProjectName { get; set; } = null!;

    /// <summary>
    /// 合同ID
    /// </summary>
    public Guid ContractId { get; set; }

    /// <summary>
    /// 开工日期
    /// </summary>
    public DateTime? StartDate { get; set; }

    /// <summary>
    /// 项目状态
    /// </summary>
    public int Status { get; set; }

    /// <summary>
    /// 计划结束日期
    /// </summary>
    public DateTime? PlanEndDate { get; set; }

    /// <summary>
    /// 计划人天数
    /// </summary>
    public int? PlanPersonDays { get; set; }

    /// <summary>
    /// 销售人员ID
    /// </summary>
    public Guid SalesManagerId { get; set; }

    /// <summary>
    /// 主管人员ID
    /// </summary>
    public Guid ProjectManagerId { get; set; }

    public string? Address { get; set; }

    /// <summary>
    /// 验收日期
    /// </summary>
    public DateTime? AcceptanceDate { get; set; }

    /// <summary>
    /// 区域ID
    /// </summary>
    public int? RegionId { get; set; }

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

    public virtual ICollection<Application> Applications { get; set; } = new List<Application>();

    public virtual ICollection<ConsumableAskFor> ConsumableAskFors { get; set; } = new List<ConsumableAskFor>();

    public virtual Contract Contract { get; set; } = null!;

    public virtual ICollection<EarlyWarningHistory> EarlyWarningHistories { get; set; } = new List<EarlyWarningHistory>();

    public virtual ICollection<PlaceOnFile> PlaceOnFiles { get; set; } = new List<PlaceOnFile>();

    public virtual ICollection<ProjectAttachment> ProjectAttachments { get; set; } = new List<ProjectAttachment>();

    public virtual ICollection<ProjectBonusEx> ProjectBonusexes { get; set; } = new List<ProjectBonusEx>();

    public virtual ICollection<ProjectDevice> ProjectDevices { get; set; } = new List<ProjectDevice>();

    public virtual ICollection<ProjectEarlyWarning> ProjectEarlyWarnings { get; set; } = new List<ProjectEarlyWarning>();

    public virtual Staff ProjectManager { get; set; } = null!;

    public virtual ICollection<ProjectPatrol> ProjectPatrols { get; set; } = new List<ProjectPatrol>();

    public virtual ICollection<ProjectPaymentTerm> ProjectPaymentTerms { get; set; } = new List<ProjectPaymentTerm>();

    public virtual ICollection<ProjectProcessStaffRelated> ProjectProcessStaffRelateds { get; set; } = new List<ProjectProcessStaffRelated>();

    public virtual ICollection<ProjectProcess> ProjectProcesses { get; set; } = new List<ProjectProcess>();

    public virtual ICollection<ProjectStaff> ProjectStaffs { get; set; } = new List<ProjectStaff>();

    public virtual ICollection<ProjectSuspendedHistory> ProjectSuspendedHistories { get; set; } = new List<ProjectSuspendedHistory>();

    public virtual ICollection<ProjectUpdateSchedule> ProjectUpdateSchedules { get; set; } = new List<ProjectUpdateSchedule>();

    public virtual Staff SalesManager { get; set; } = null!;

    public virtual ICollection<StockInBound> StockInBounds { get; set; } = new List<StockInBound>();

    public virtual ICollection<StockOutBound> StockOutBounds { get; set; } = new List<StockOutBound>();
}
