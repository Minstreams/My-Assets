﻿using System;
using UnityEngine;

/// <summary>
/// 代替原变量标签
/// to replace default label style
/// </summary>
[AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
public class LabelAttribute : PropertyAttribute
{
    /// <summary>
    /// 标签内容，留空显示默认内容
    /// the content of label. leave empty to show default label.
    /// </summary>
    public string Label = "";
    /// <summary>
    /// 常量在游戏中不允许改变
    /// if Const, disallow changes during play
    /// </summary>
    public bool Const = false;

    public LabelAttribute(bool isConst = false)
    {
        this.Const = isConst;
    }
    public LabelAttribute(string label, bool isConst = false)
    {
        this.Label = label;
        this.Const = isConst;
    }
}