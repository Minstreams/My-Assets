using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[AddComponentMenu("Context/AnyKeyEvent")]
public class AnyKeyEvent : MonoBehaviour
{
    public UnityEvent output;

    private void Update()
    {
        if (Input.anyKeyDown) output?.Invoke();
    }
}
