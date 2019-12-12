using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[AddComponentMenu("Listener/BGMChanger")]
public class BGMChanger : MonoBehaviour
{
    public AudioClip clip;
    public bool loop;

    [ContextMenu("ChangeBGM")]
    public void ChangeBGM()
    {
        GameSystem.TheMatrix.ChangeBGM(clip, loop);
    }
}
