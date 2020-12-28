using UnityEngine;
using System.Collections.Generic;

namespace GameSystem.UI
{
    [AddComponentMenu("[NetworkSystem]/UI/NetworkLogDisplayer")]
    public class NetworkLogDisplayer : UIBase
    {
        [Label] public int maxLogCount = 8;
        [Label] public GUIStyle labelStyle;

        readonly Queue<string> logQueue = new Queue<string>();

        void OnLog(string message)
        {
            logQueue.Enqueue(message);
            while (logQueue.Count > maxLogCount) logQueue.Dequeue();
        }
        protected override void OnEnable()
        {
            base.OnEnable();
            NetworkSystem.OnLog += OnLog;
        }
        protected override void OnDisable()
        {
            base.OnDisable();
            NetworkSystem.OnLog -= OnLog;
        }
        protected override void Reset()
        {
            base.Reset();
            SetStyle(ref labelStyle, "label");
        }
        protected override void OnUI()
        {
            if (logQueue.Count == 0)
            {
                GUILayout.Label("Network Log Displayer", labelStyle);
            }
            else
            {
                foreach (var msg in logQueue)
                {
                    GUILayout.Label(msg, labelStyle);
                }
            }
        }
    }
}
