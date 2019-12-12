using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameSystem;

[AddComponentMenu("Listener/GameMessageSender")]
public class GameMessageSender : MonoBehaviour
{
    public GameMessage toSend;

    [ContextMenu("Send")]
    public void Send()
    {
        TheMatrix.SendGameMessage(toSend);
    }
}
