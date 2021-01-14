using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameSystem.UI
{

    [AddComponentMenu("[NetworkSystem]/UI/LatencySimulator")]
    public class LatencySimulator : UIBase
    {
#if UNITY_EDITOR
        [MinsHeader("LatencySimulator UI of NetworkSystem", SummaryType.PreTitleOperator, -2)]
        [MinsHeader("", SummaryType.CommentCenter, -1)]
        [ConditionalShow, SerializeField] bool useless; //在没有数据的时候让标题正常显示
#endif
        int latencyMS;
        protected override void OnUI()
        {
            TitleLabel("LatencySimulator");
            latencyMS = IntField("Latency Override:", latencyMS);
            NetworkSystem.latencyOverride = latencyMS / 1000.0f;
        }
    }
}