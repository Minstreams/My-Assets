using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameSystem.Operator
{
    /// <summary>
    /// 此操作节点用来发送游戏消息
    /// </summary>
    [AddComponentMenu("[GameFlowSystem]/Operator/GameMessageSender")]
    public class GameMessageSender : MonoBehaviour
    {
#if UNITY_EDITOR
        [MinsHeader("Operator of GameFlowSystem", SummaryType.PreTitleOperator, -1)]
        [MinsHeader("游戏消息发送器", SummaryType.TitleOrange, 0)]
        [MinsHeader("此操作节点用来发送游戏消息", SummaryType.CommentCenter, 1)]
        [ConditionalShow, SerializeField] bool useless; // 在没有数据的时候让标题正常显示
#endif

        // Data
        [MinsHeader("Data", SummaryType.Header, 2)]
        [Label] public GameMessage message;
        [Label(true)] public bool sendOnStart;

        // Inner code here
        void Start()
        {
            if (sendOnStart) SendGameMessage();
        }

        // Input
        [ContextMenu("Send")]
        public void SendGameMessage()
        {
            GameFlowSystem.SendGameMessage(message);
        }
    }
}