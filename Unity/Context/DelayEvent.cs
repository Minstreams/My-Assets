using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[AddComponentMenu("Context/CurveFloatEvent")]
public class DelayEvent : MonoBehaviour
{
    public UnityEvent output;

    public float delay = 0.5f;

    [ContextMenu("Invoke")]
    public void Invoke()
    {
        Invoke(delay);
    }

    public void Invoke(float delay)
    {
        Invoke("DoInvoke", delay);
    }

    private void DoInvoke()
    {
        output?.Invoke();
    }
}
