using System;
using System.Collections.Generic;

namespace ProjectService.Db;

/// <summary>
/// 员工表
/// </summary>
public partial class Staff
{
    /// <summary>
    /// 员工ID
    /// </summary>
    public Guid StaffId { get; set; }

    /// <summary>
    /// 员工编号
    /// </summary>
    public string StaffCode { get; set; } = null!;

    /// <summary>
    /// 员工名称
    /// </summary>
    public string StaffName { get; set; } = null!;

    /// <summary>
    /// 员工手机号
    /// </summary>
    public string? StaffPhone { get; set; }

    /// <summary>
    /// 员工身份证号
    /// </summary>
    public string? StaffCard { get; set; }

    /// <summary>
    /// 员工职位
    /// </summary>
    public int StaffDuty { get; set; }

    /// <summary>
    /// 员工性别
    /// </summary>
    public string? StaffSex { get; set; }

    /// <summary>
    /// 员工状态
    /// </summary>
    public int? StaffStatus { get; set; }

    /// <summary>
    /// 员工费率
    /// </summary>
    public decimal StaffFees { get; set; }

    /// <summary>
    /// 员工部门
    /// </summary>
    public int? StaffDepartment { get; set; }

    /// <summary>
    /// 员工工资
    /// </summary>
    public decimal? StaffWages { get; set; }

    /// <summary>
    /// 员工转正工资
    /// </summary>
    public decimal? StaffzzWages { get; set; }

    /// <summary>
    /// 员工保险金额
    /// </summary>
    public decimal? StaffInsuranceAmount { get; set; }

    /// <summary>
    /// 员工补贴
    /// </summary>
    public decimal? StaffSubsidy { get; set; }

    /// <summary>
    /// 员工付款类型
    /// </summary>
    public int? StaffGiveMoneyType { get; set; }

    /// <summary>
    /// 员工重编码
    /// </summary>
    public int? StaffRecode { get; set; }

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

    public virtual ICollection<Contract> Contracts { get; set; } = new List<Contract>();

    public virtual ICollection<ProjectDailyWork> ProjectDailyWorks { get; set; } = new List<ProjectDailyWork>();

    public virtual ICollection<ProjectDevice> ProjectDevices { get; set; } = new List<ProjectDevice>();

    public virtual ICollection<ProjectPatrol> ProjectPatrols { get; set; } = new List<ProjectPatrol>();

    public virtual ICollection<ProjectProcessStaffRelated> ProjectProcessStaffRelateds { get; set; } = new List<ProjectProcessStaffRelated>();

    public virtual ICollection<Project> ProjectProjectManagers { get; set; } = new List<Project>();

    public virtual ICollection<Project> ProjectSalesManagers { get; set; } = new List<Project>();

    public virtual ICollection<ProjectStaff> ProjectStaffs { get; set; } = new List<ProjectStaff>();

    public virtual ICollection<WorkApplyLeave> WorkApplyLeaves { get; set; } = new List<WorkApplyLeave>();

    public virtual ICollection<WorkAttendance> WorkAttendances { get; set; } = new List<WorkAttendance>();

    public virtual ICollection<WorkDelayClock> WorkDelayClocks { get; set; } = new List<WorkDelayClock>();

    public virtual ICollection<WorkOutClock> WorkOutClocks { get; set; } = new List<WorkOutClock>();
}
