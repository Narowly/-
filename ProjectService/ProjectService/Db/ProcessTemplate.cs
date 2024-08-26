using System;
using System.Collections.Generic;

namespace ProjectService.Db;

/// <summary>
/// 工序模板表
/// </summary>
public partial class ProcessTemplate
{
    /// <summary>
    /// 模板ID
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// 模板名称
    /// </summary>
    public string Name { get; set; } = null!;

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

    public virtual ICollection<ProcessTemplateDetail> ProcessTemplateDetails { get; set; } = new List<ProcessTemplateDetail>();
}
