using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[AddComponentMenu("Context/StepEvent")]
public class StepEvent : MonoBehaviour
{
    public float step;

    public UnityEvent output;

    private float stepper = 0;

    public void StepForward(float input)
    {
        stepper += input;
        while (stepper > step)
        {
            stepper -= step;
            output?.Invoke();
        }
    }
}
