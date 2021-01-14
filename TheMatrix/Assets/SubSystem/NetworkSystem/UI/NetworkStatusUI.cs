using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameSystem.UI
{

    [AddComponentMenu("[NetworkSystem]/UI/NetworkStatusUI")]
    public class NetworkStatusUI : UIBase
    {
#if UNITY_EDITOR
        [MinsHeader("NetworkStatusUI UI of NetworkSystem", SummaryType.PreTitleOperator, -2)]
        [MinsHeader("", SummaryType.CommentCenter, -1)]
        [ConditionalShow, SerializeField] bool useless; //在没有数据的时候让标题正常显示
#endif

        protected override void OnUI()
        {
            TitleLabel("NetworkStatusUI");
            GUILayout.Label("LocalIP:" + NetworkSystem.LocalIPAddress);
            GUILayout.Label("ServerIP:" + NetworkSystem.ServerIPAddress);
            GUILayout.Label("Timer:" + NetworkSystem.timer);
            GUILayout.Label("TimerOffset:" + NetworkSystem.timerOffset);
            GUILayout.Label("ServerTimer:" + NetworkSystem.ServerTimer);
            GUILayout.Label("Latency:" + NetworkSystem.latency);
            GUILayout.Label("Latency Override:" + NetworkSystem.latencyOverride);
            GUILayout.Label("Id: " + NetworkSystem.NetId);
        }
    }
}