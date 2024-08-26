using System;
using System.Collections.Generic;

namespace ProjectService.Db;

/// <summary>
/// 字典数据表
/// </summary>
public partial class DictDatum
{
    /// <summary>
    /// 字典编码
    /// </summary>
    public int DictCode { get; set; }

    /// <summary>
    /// 字典标签
    /// </summary>
    public string? DictLabel { get; set; }

    /// <summary>
    /// 字典键值
    /// </summary>
    public string? DictValue { get; set; }

    /// <summary>
    /// 字典类型ID
    /// </summary>
    public int? DictTypeId { get; set; }

    /// <summary>
    /// 父编码
    /// </summary>
    public int? ParentCode { get; set; }

    /// <summary>
    /// 状态（1正常0停用）
    /// </summary>
    public bool? Status { get; set; }

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

    public virtual DictType? DictType { get; set; }
}
