using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 平滑追踪
/// </summary>
[AddComponentMenu("My Assets/Tracker")]
[DisallowMultipleComponent]
public class Tracker : MonoBehaviour
{
    [Header("【平滑追踪器】")]
    public Transform target;
    [Header("平滑度")]
    [Range(0.05f, 0.95f)]
    public float smoothness = 0.5f;

    private void FixedUpdate()
    {
        transform.position += ((1 - smoothness) * (target.position - transform.position));
        transform.rotation = Quaternion.Slerp(transform.rotation, target.rotation, 1 - smoothness);
    }
}
