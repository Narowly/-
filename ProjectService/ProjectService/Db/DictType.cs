using System;
using System.Collections.Generic;

namespace ProjectService.Db;

/// <summary>
/// 字典类型表
/// </summary>
public partial class DictType
{
    /// <summary>
    /// 字典主键
    /// </summary>
    public int DictId { get; set; }

    /// <summary>
    /// 字典名称
    /// </summary>
    public string? DictName { get; set; }

    /// <summary>
    /// 字典类型
    /// </summary>
    public string? TypeName { get; set; }

    /// <summary>
    /// 状态（1正常0停用）
    /// </summary>
    public bool Status { get; set; }

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

    public virtual ICollection<DictDatum> DictData { get; set; } = new List<DictDatum>();
}
