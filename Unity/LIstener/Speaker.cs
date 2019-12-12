using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[AddComponentMenu("Listener/Speaker")]
public class Speaker : MonoBehaviour
{
    public AudioClip[] clips;
    public AudioSource audioSource;

    public FloatEvent onSpeak;
    [Tooltip("to Test")]
    public int index;
    [Tooltip("0 = left Channel, 1 = right Channel")]
    public int channel = 0;
    public FFTWindow fTWindow;
    public int outputFrequencyIndex = 3;
    private float[] data = new float[64];

    public bool debug = false;

    [ContextMenu("Speak")]
    public void Speak() { Speak(index); }

    public void Speak(int index)
    {
        audioSource.clip = clips[index];
        StopAllCoroutines();
        StartCoroutine(_Speak());
    }

    public IEnumerator _Speak()
    {
        audioSource.Play();
        while (audioSource.isPlaying)
        {
            yield return 0;
            audioSource.GetSpectrumData(data, channel, fTWindow);
            if (debug)
                for (int i = 0; i < 64; i++)
                {
                    Debug.DrawLine(new Vector3(i * 0.1f, 0, 0), new Vector3(i * 0.1f, data[i], 0));
                }
            onSpeak?.Invoke(data[outputFrequencyIndex]);
        }
    }

}
