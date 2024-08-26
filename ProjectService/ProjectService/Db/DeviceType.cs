using System;
using System.Collections.Generic;

namespace ProjectService.Db;

/// <summary>
/// 设备类型表
/// </summary>
public partial class DeviceType
{
    /// <summary>
    /// 设备类型ID
    /// </summary>
    public Guid DeviceTypeId { get; set; }

    /// <summary>
    /// 设备类型名称
    /// </summary>
    public string DeviceTypeName { get; set; } = null!;

    /// <summary>
    /// 设备型号名称
    /// </summary>
    public string? DeviceModel { get; set; }

    /// <summary>
    /// 设备单位
    /// </summary>
    public string? DeviceUnit { get; set; }

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

    public virtual ICollection<ApplicationDevice> ApplicationDevices { get; set; } = new List<ApplicationDevice>();

    public virtual ICollection<Device> Devices { get; set; } = new List<Device>();
}
