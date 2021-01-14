using System.Collections;
using GameSystem.Setting;
using System;
using UnityEngine;

namespace GameSystem
{
    public partial class GameFlowSystem : SubSystem<GameFlowSystemSetting>
    {
        // 游戏流程 -------------
        public static event Action OnFlowStart;
        public static event Action OnLevelStart;

#if UNITY_EDITOR
        static void QuickTest()
        {
            OnFlowStart?.Invoke();
            UnityEngine.SceneManagement.SceneManager.UnloadSceneAsync(0);
        }
#endif
        static IEnumerator Start()
        {
            OnFlowStart?.Invoke();
            yield return 0;
            StartCoroutine(CheckExit());
            StartCoroutine(StartMenu());
        }
        static IEnumerator CheckExit()
        {
            while (true)
            {
                yield return 0;
                if (GetGameMessage(GameMessage.Exit))
                {
                    TheMatrix.canQuit = true;
                    Application.Quit();
                    yield break;
                }
            }
        }
        static IEnumerator StartMenu()
        {
            SceneSystem.LoadScene(SceneCode.startMenu);
            yield return 0;

            ResetGameMessage();
            while (true)
            {
                yield return 0;
                if (GetGameMessage(GameMessage.Next)) break;
            }

            StartCoroutine(Level0());
        }
        static IEnumerator Level0()
        {
            yield return SceneSystem.LoadSceneCoroutine(SceneCode.level0);
            yield return 0;

            OnLevelStart?.Invoke();

            ResetGameMessage();
            while (true)
            {
                yield return 0;
            }
        }
    }
}