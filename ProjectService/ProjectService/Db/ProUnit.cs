using System;
using System.Collections.Generic;

namespace ProjectService.Db;

public partial class ProUnit
{
    /// <summary>
    /// 单位ID号
    /// </summary>
    public int UnitId { get; set; }

    /// <summary>
    /// 单位名称
    /// </summary>
    public string UnitName { get; set; } = null!;

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

    public virtual ICollection<ProcessUnit> ProcessUnits { get; set; } = new List<ProcessUnit>();
}
