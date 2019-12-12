using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[AddComponentMenu("Context/SimpleVector3Event")]
public class SimpleVector3Event : MonoBehaviour
{
    public Vec3Event output;
    public bool invokeOnStart;
    public bool invokeOnEnable;
    public Vector3 input;

    private void Start()
    {
        if (invokeOnStart) Invoke();
    }

    private void OnEnable()
    {
        if (invokeOnEnable) Invoke();
    }

    [ContextMenu("Invoke")]
    public void Invoke()
    {
        output?.Invoke(input);
    }

    public void Invoke(Vector3 input)
    {
        output?.Invoke(input);
    }

    public void SetX(float value)
    {
        input.x = value;
    }
    public void SetY(float value)
    {
        input.y = value;
    }
    public void SetZ(float value)
    {
        input.z = value;
    }
}
