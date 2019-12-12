using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[AddComponentMenu("Context/CurveFloatEvent")]
public class CurveFloatEvent : MonoBehaviour
{
    public FloatEvent output;
    [Tooltip("time must be between 0 and 1")]
    public AnimationCurve curve;
    public float time;

    [ContextMenu("Invoke")]
    public void Invoke()
    {
        Invoke(time);
    }
    public void Invoke(float time)
    {
        StopAllCoroutines();
        StartCoroutine(invoke(time));
    }
    private IEnumerator invoke(float time)
    {
        float timer = 0;
        while (timer < 1)
        {
            yield return 0;
            output?.Invoke(curve.Evaluate(timer));
            timer += Time.deltaTime / time;
        }
        output?.Invoke(curve.Evaluate(1));
    }

}
