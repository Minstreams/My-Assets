using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR

/// <summary>
/// 编辑器模式下自动对齐
/// </summary>
[ExecuteInEditMode]
[AddComponentMenu("Function/Auto Align")]
public class AutoAlignBySection : MonoBehaviour
{
    [Header("【自动对齐组件】"), Header("对齐单元数"), Range(1, 8)]
    public int section = 1;
    [Header("对齐单元大小")]
    public float unit = 0.16f;

    public bool alignX;
    public bool alignY;
    public bool alignZ;
    private void Update()
    {
        Vector3 pos = transform.position;
        if (alignX)
        {
            pos.x =
                ((int)((
            transform.position.x > section * unit / 2 ?
            transform.position.x + section * unit / 2 :
            transform.position.x - section * unit / 2
            ) / (section * unit))) * (section * unit);
        }
        if (alignY)
        {
            pos.y =
                ((int)((
            transform.position.y > section * unit / 2 ?
            transform.position.y + section * unit / 2 :
            transform.position.y - section * unit / 2
            ) / (section * unit))) * (section * unit);
        }
        if (alignZ)
        {
            pos.z =
                ((int)((
            transform.position.z > section * unit / 2 ?
            transform.position.z + section * unit / 2 :
            transform.position.z - section * unit / 2
            ) / (section * unit))) * (section * unit);
        }
        transform.position = pos;
    }
}
#endif