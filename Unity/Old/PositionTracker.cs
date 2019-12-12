using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 位置平滑追踪
/// </summary>
[AddComponentMenu("My Assets/Position Tracker")]
[DisallowMultipleComponent]
public class PositionTracker : MonoBehaviour
{
    [Header("【位置平滑追踪器】")]
    public Transform target;
    [Header("平滑度")]
    [Range(0.05f, 0.95f)]
    public float smoothness = 0.5f;

    private void FixedUpdate()
    {
        transform.position += ((1 - smoothness) * (target.position - transform.position));
    }
}
