using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameSystem.Setting;

namespace GameSystem
{
    public partial class NetworkSystem : SubSystem<NetworkSystemSetting>
    {
        // Thread control
        readonly static Queue<Action> mainThreadActionQueue = new Queue<Action>();

        [RuntimeInitializeOnLoadMethod]
        static void RuntimeInit()
        {
            _ = Setting;    // initialize Setting in main thread
            TheMatrix.OnGameStart += OnGameStart;
            TheMatrix.OnQuitting += OnQuitting;
        }
        static void OnGameStart()
        {
            StartCoroutine(MainThread());
        }
        static void OnQuitting()
        {
            StopAllCoroutines();
            server?.Destroy();
            client?.Destroy();
        }
        static IEnumerator MainThread()
        {
            while (true)
            {
                yield return 0;
                while (mainThreadActionQueue.Count > 0) mainThreadActionQueue.Dequeue()?.Invoke();
            }
        }

        // interface
        /// <summary>
        /// Call an action in main thread
        /// </summary>
        public static void CallMainThread(Action action) => mainThreadActionQueue.Enqueue(action);
        /// <summary>
        /// Call Debug.Log in main thread
        /// </summary>
        public static void CallLog(string message) => mainThreadActionQueue.Enqueue(() => Log(message));
    }
}