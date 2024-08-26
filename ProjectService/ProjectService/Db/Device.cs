using System;
using System.Collections.Generic;

namespace ProjectService.Db;

/// <summary>
/// 设备表
/// </summary>
public partial class Device
{
    /// <summary>
    /// 设备ID
    /// </summary>
    public Guid DeviceId { get; set; }

    /// <summary>
    /// 设备编号
    /// </summary>
    public string DeviceNumber { get; set; } = null!;

    /// <summary>
    /// 设备类型ID
    /// </summary>
    public Guid DeviceTypeId { get; set; }

    /// <summary>
    /// 设备状态
    /// </summary>
    public int DeviceStatus { get; set; }

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

    public virtual DeviceType DeviceType { get; set; } = null!;

    public virtual ICollection<ProjectDevice> ProjectDevices { get; set; } = new List<ProjectDevice>();
}
