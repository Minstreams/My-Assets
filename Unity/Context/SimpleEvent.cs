using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[AddComponentMenu("Context/SimpleEvent")]
public class SimpleEvent : MonoBehaviour
{
    public UnityEvent output;
    public bool invokeOnStart;

    private void Start()
    {
        if (invokeOnStart) Invoke();
    }

    [ContextMenu("Invoke")]
    public void Invoke()
    {
        output?.Invoke();
    }
}
