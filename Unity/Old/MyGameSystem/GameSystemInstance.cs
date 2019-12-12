using System.Collections;
using UnityEngine;

namespace GameSystem
{
    namespace InnerSystem
    {
        namespace Instances
        {
            /// <summary>
            /// 游戏系统流程管理实例
            /// </summary>
            [AddComponentMenu("Systems/Game System")]
            public class GameSystemInstance : MonoBehaviour
            {
#if UNITY_EDITOR
                [Header("【游戏系统】")]
                public EmptyStruct 一一一一一一一一一一一一一一一一一一一一一一一一一一一;
                //编辑器模式下，全局查重，防止有多个游戏系统
                private static GameSystemInstance instance = null;
                private void Awake()
                {
                    if (instance != null)
                    {
                        Debug.LogError("不允许有多个游戏系统！");
                    }
                    instance = this;
                }
                private void Reset()
                {
                    gameObject.tag = "GameController";
                }
#endif
                //游戏启动----------------------------
                private void Start()
                {
                    StartCoroutine(start());
                }


                //流程--------------------------------
                private IEnumerator start()
                {
                    //这里写游戏流程控制代码
                    throw new System.NotImplementedException();
                }

            }

            /// <summary>
            /// 游戏子系统父类
            /// </summary>
            /// <typeparam name="InstanceClass"></typeparam>
            public class SystemInstance<InstanceClass> : MonoBehaviour
            {
                private static InstanceClass instance;
                /// <summary>
                /// 实例
                /// </summary>
                public static InstanceClass Instance
                {
                    get
                    {
                        if (instance == null) instance = GameObject.FindGameObjectWithTag("GameController").GetComponent<InstanceClass>();
                        return instance;
                    }
                }
            }
        }


        /// <summary>
        /// 游戏控制信息枚举
        /// </summary>
        public enum GameMessage
        {
            Start,
            Win,
            Lose,
            Exit
        }

        /// <summary>
        /// 游戏控制消息管理器
        /// 用于提供游戏控制消息接收与查询功能
        /// </summary>
        public static class GameMessageManager
        {
            //游戏控制信息--------------------------------------------

            /// <summary>
            /// 记录游戏控制信息
            /// </summary>
            private static bool[] gameMessageReciver = new bool[System.Enum.GetValues(typeof(GameMessage)).Length];

            /// <summary>
            /// 检查游戏控制信息，收到则返回true
            /// </summary>
            /// <param name="message">要检查的信息</param>
            /// <param name="reset">是否在接收后重置</param>
            /// <returns>检查按钮信息，收到则返回true</returns>
            public static bool GetGameMessage(GameMessage message, bool reset = true)
            {
                if (gameMessageReciver[(int)message])
                {
                    gameMessageReciver[(int)message] = false;
                    return true;
                }
                return false;
            }
            /// <summary>
            /// 发送 游戏控制信息
            /// </summary>
            /// <param name="message">信息</param>
            public static void SendGameMessage(GameMessage message)
            {
                gameMessageReciver[(int)message] = true;
            }
            /// <summary>
            /// 重置
            /// </summary>
            public static void ResetGameMessage()
            {
                gameMessageReciver.Initialize();
            }


            //游戏控制事件--------------------------------------------

            /// <summary>
            /// 游戏关卡初始化事件
            /// </summary>
            public static event System.Action gameInit;

            /// <summary>
            /// 游戏关卡初始化
            /// </summary>
            public static void GameInit()
            {
                if (gameInit != null) gameInit();
            }
        }
    }
}

#if UNITY_EDITOR
[System.Serializable]
public struct EmptyStruct { }
#endif
