using System;
using System.Collections.Generic;

namespace ProjectService.Db;

/// <summary>
/// 项目附件表
/// </summary>
public partial class ProjectAttachment
{
    /// <summary>
    /// 附件表主键
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// 项目ID
    /// </summary>
    public Guid ProjectId { get; set; }

    /// <summary>
    /// 文件名称
    /// </summary>
    public string FileName { get; set; } = null!;

    /// <summary>
    /// 文件类型
    /// </summary>
    public string FileType { get; set; } = null!;

    /// <summary>
    /// 文件路径
    /// </summary>
    public string FileAddress { get; set; } = null!;

    /// <summary>
    /// 上传日期
    /// </summary>
    public DateTime UploadDate { get; set; }

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
