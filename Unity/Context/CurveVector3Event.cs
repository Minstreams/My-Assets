using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[AddComponentMenu("Context/CurveVector3Event")]
public class CurveVector3Event : MonoBehaviour
{
    public Vec3Event output;
    public AnimationCurve remapCurve;

    public Vector3 from;
    public Vector3 to;

    public float time;

    public void Invoke(float t)
    {
        output?.Invoke(Vector3.Lerp(from, to, remapCurve.Evaluate(t)));
    }

    public void InvokeInTime()
    {
        InvokeInTime(time);
    }
    public void InvokeInTime(float time)
    {
        StopAllCoroutines();
        StartCoroutine(invokeInTime(time));
    }
    private IEnumerator invokeInTime(float time)
    {
        float timer = 0;
        while (timer < 1)
        {
            yield return 0;
            output?.Invoke(Vector3.Lerp(from, to, remapCurve.Evaluate(timer)));
            timer += Time.deltaTime / time;
        }
        output?.Invoke(Vector3.Lerp(from, to, remapCurve.Evaluate(1)));
    }
}
