using System;
using System.Collections.Generic;

namespace ProjectService.Db;

public partial class PlaceOnFile
{
    public Guid PlaceOnFileId { get; set; }

    public Guid ProjectId { get; set; }

    public Guid ApplicationUserId { get; set; }

    public Guid? ReviewerId { get; set; }

    public string? Reason { get; set; }

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
}
