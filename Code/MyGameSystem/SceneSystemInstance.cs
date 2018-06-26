using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace GameSystem
{
    namespace InnerSystem
    {
        namespace Instances
        {
            /// <summary>
            /// 场景管理系统
            /// </summary>
            [DisallowMultipleComponent]
            [AddComponentMenu("Systems/Scene System")]
            public class SceneSystemInstance : SystemInstance<SceneSystemInstance>
            {
#if UNITY_EDITOR
                [Header("【场景管理系统】")]
                public EmptyStruct 一一一一一一一一一一一一一一一一一一一一一一一一一一一;
#endif
                public SceneSystem.Setting setting;
            }
        }
    }

    /// <summary>
    /// 场景管理系统
    /// </summary>
    public static class SceneSystem
    {
        [System.Serializable]
        public class Setting { }

        /// <summary>
        /// 设置参数
        /// </summary>
        private static Setting setting { get { return Instance.setting; } }
        /// <summary>
        /// 实例
        /// </summary>
        private static InnerSystem.Instances.SceneSystemInstance Instance { get { return InnerSystem.Instances.SceneSystemInstance.Instance; } }


        //变量--------------------------------
        private static Stack<string> currentSceneName = new Stack<string>();



        //事件--------------------------------
        /// <summary>
        /// 更新加载进度事件
        /// </summary>
        public static event System.Action<float> UpdateProgress;



        //方法--------------------------------
        /// <summary>
        /// 加载场景协程
        /// </summary>
        /// <param name="sceneName">加载场景名</param>
        /// <param name="showLoadingScene">是否加载Loading场景</param>
        /// <returns></returns>
        private static IEnumerator YieldPushScene(string sceneName, bool showLoadingScene)
        {
            if (showLoadingScene)
            {
                //加载Loading场景
                SceneManager.LoadScene("LoadingScene", LoadSceneMode.Additive);

                //加载前动画
                //TODO
            }
            //加载新场景
            AsyncOperation progress = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
            currentSceneName.Push(sceneName);

            if (showLoadingScene)
            {
                while (!progress.isDone)
                {
                    //播放加载效果
                    if (UpdateProgress != null)
                    {
                        //虽然可以把判断置于循环外减少判断次数，不过意义不大，得不偿失
                        UpdateProgress(progress.progress);
                    }
                    yield return 0;
                }

                //加载后动画
                //TODO

                //卸载Loading场景
                SceneManager.UnloadSceneAsync("LoadingScene");

            }

            yield return 0;
        }

        public static void PushScene(string sceneName, bool showLoadingScene = false)
        {
            Debug.Log("push场景" + sceneName);
            Instance.StartCoroutine(YieldPushScene(sceneName, showLoadingScene));
        }

        public static void PopScene()
        {
            if (currentSceneName.Count == 0)
            {
                Debug.LogError("场景栈空惹");
                return;
            }
            SceneManager.UnloadSceneAsync(currentSceneName.Pop());
        }

        /// <summary>
        /// 加载并替换游戏场景
        /// </summary>
        /// <param name="sceneName">场景名</param>
        public static void ChangeScene(string sceneName, bool showLoadingScene = false)
        {
            PopScene();
            PushScene(sceneName, showLoadingScene);
        }
    }
}