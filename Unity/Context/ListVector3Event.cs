using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[AddComponentMenu("Context/ListVector3Event")]
public class ListVector3Event : MonoBehaviour
{
    public Vec3Event output;
    public Vector3[] input;
    public int index;
    public bool invokeOnStart;
    public bool invokeOnEnable;

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
        output?.Invoke(input[index]);
    }

    public void Invoke(int index)
    {
        output?.Invoke(input[index]);
    }
}
