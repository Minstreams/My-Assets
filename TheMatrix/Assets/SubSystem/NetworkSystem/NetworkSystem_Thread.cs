using System;
using System.Collections;
using UnityEngine;
using GameSystem.Setting;

namespace GameSystem
{
    public partial class NetworkSystem : SubSystem<NetworkSystemSetting>
    {
        // Thread control
        static Action MainThreadAction;

        [RuntimeInitializeOnLoadMethod]
        static void RuntimeInit()
        {
            _ = Setting;    // initialize Setting in main thread
            TheMatrix.OnGameStart += OnGameStart;
            Application.wantsToQuit += OnQuitting;
        }
        static void OnGameStart()
        {
            StartCoroutine(MainThread());
        }
        static bool OnQuitting()
        {
            StopAllCoroutines();
            server?.Destroy();
            client?.Destroy();
            return true;
        }
        static IEnumerator MainThread()
        {
            while (true)
            {
                yield return 0;
                if (MainThreadAction != null)
                {
                    MainThreadAction.Invoke();
                    MainThreadAction = null;
                }
            }
        }

        // interface
        /// <summary>
        /// Call an action in main thread
        /// </summary>
        public static void CallMainThread(Action action) => MainThreadAction += action;
        /// <summary>
        /// Call Debug.Log in main thread
        /// </summary>
        public static void CallLog(string message) => MainThreadAction += () => { Log(message); };
    }
}