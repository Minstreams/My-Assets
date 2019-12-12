
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[AddComponentMenu("Context/RoundEvent")]
public class RoundEvent : MonoBehaviour
{
    public UnityEvent[] preEvents;
    public UnityEvent[] events;
    private int index = 0;

    private int GetPreEventsLength()
    {
        if (preEvents == null) return 0;
        return preEvents.Length;
    }
    [ContextMenu("Invoke")]
    public void Invoke()
    {
        if (index < GetPreEventsLength()) preEvents[index++]?.Invoke();
        else events[index++ - GetPreEventsLength()]?.Invoke();
        if (index >= GetPreEventsLength() + events.Length) index = GetPreEventsLength();
    }
}
