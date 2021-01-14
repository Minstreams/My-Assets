using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameSystem.Setting;
using GameSystem.Networking;
using System;

namespace GameSystem
{
    public partial class NetworkSystem : SubSystem<NetworkSystemSetting>
    {
        public static float timer;
        /// <summary>
        /// time differece with the server
        /// </summary>
        public static float timerOffset;
        public static float ServerTimer => IsHost ? timer : timer + timerOffset;
        public static float latency = 0;

        public static float latencyOverride = 0;

        readonly static Queue<Action> delayThreadActionQueue = new Queue<Action>();
        static IEnumerator DelayThread()
        {
            while (true)
            {
                yield return 0;
                while (delayThreadActionQueue.Count > 0) StartCoroutine(DelayToMain(delayThreadActionQueue.Dequeue()));
            }
        }
        static IEnumerator DelayToMain(Action action)
        {
            yield return new WaitForSecondsRealtime(latencyOverride);
            action();
        }

        static void CallDelayMain(Action action) => delayThreadActionQueue.Enqueue(action);
    }
}
