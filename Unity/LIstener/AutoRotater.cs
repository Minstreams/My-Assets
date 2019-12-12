using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// simply keep rotating around y axis
/// </summary>
[AddComponentMenu("Listener/AutoRotater")]
public class AutoRotater : MonoBehaviour
{
    public float speed;
    [Range(0, 1)]
    public float factor = 1;
    private void Update()
    {
        transform.Rotate(Vector3.up * speed * factor * Time.deltaTime, Space.Self);
    }
    public void SetFactor(float factor)
    {
        this.factor = factor;
    }
}
