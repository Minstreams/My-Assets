using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[AddComponentMenu("Listener/DontDestroyOnLoader")]
public class DontDestroyOnLoader : MonoBehaviour
{
    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }
}
