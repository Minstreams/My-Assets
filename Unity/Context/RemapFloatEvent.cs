using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[AddComponentMenu("Context/RemapFloatEvent")]
public class RemapFloatEvent : MonoBehaviour
{
    public FloatEvent output;
    public AnimationCurve remapCurve;

    public bool clamp = false;
    public Vector2 outputRange = Vector2.up;
    public bool debug = false;

    public void Input(float input)
    {
        if (debug) print("input:" + input);
        if (clamp) output?.Invoke(Mathf.Clamp(remapCurve.Evaluate(input), outputRange.x, outputRange.y));
        else output?.Invoke(remapCurve.Evaluate(input));
    }
}
