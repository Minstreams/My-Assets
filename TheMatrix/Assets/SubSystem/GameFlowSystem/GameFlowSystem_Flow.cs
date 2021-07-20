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
            StartCoroutine(QuickTestCoroutine());
            OnLevelStart?.Invoke();
        }
        static IEnumerator QuickTestCoroutine()
        {
            //while (true)
            //{
            //    yield return 0;
            //    // ...
            //}
        }
#endif
        static IEnumerator Start()
        {
            OnFlowStart?.Invoke();
            yield return 0;
            StartCoroutine(CheckExit());
            StartCoroutine(Logo());
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
        static IEnumerator Logo()
        {
            Cursor.visible = false;
            SceneSystem.LoadScene(SceneCode.logo);
            SceneSystem.ConfirmLoadScene();
            yield return 0;

            ResetGameMessage();
            while (true)
            {
                yield return 0;
                if (GetGameMessage(GameMessage.Next)) break;
            }

            StartCoroutine(StartMenu());
        }
        static IEnumerator StartMenu()
        {
            // todo
        }
    }
}