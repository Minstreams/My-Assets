using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 角度平滑追踪
/// </summary>
[AddComponentMenu("My Assets/Rotation Tracker")]
[DisallowMultipleComponent]
public class RotationTracker : MonoBehaviour
{
    [Header("【角度平滑追踪器】")]
    public Transform target;
    [Header("平滑度")]
    [Range(0.05f, 0.95f)]
    public float smoothness = 0.5f;

    private void FixedUpdate()
    {
        transform.rotation = Quaternion.Slerp(transform.rotation, target.rotation, 1 - smoothness);
    }
}
