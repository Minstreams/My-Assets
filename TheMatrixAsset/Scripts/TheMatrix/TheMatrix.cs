﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using GameSystem.Setting;
using GameSystem.Savable;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace GameSystem
{
    /// <summary>
    /// 母体，游戏流程控制与消息处理
    /// </summary>
    [DisallowMultipleComponent]
    public class TheMatrix : MonoBehaviour
    {
        // 场景 -------------------------------------
        /// <summary>
        /// 游戏场景枚举
        /// </summary>
        public enum GameScene
        {
            logo,
            startScene,
        }

        // 游戏流程 ---------------------------------
        private IEnumerator _Awake()
        {
            onGameAwake?.Invoke();
            yield return 0;

            StartCoroutine(_CheckExit());
            StartCoroutine(_Logo());
        }

        private IEnumerator _CheckExit()
        {
            while (true)
            {
                yield return 0;
                if (GetGameMessage(GameMessage.Exit))
                {
                    Application.Quit();
                }
            }
        }

        private IEnumerator _Logo()
        {
            SceneSystem.LoadScene(GameScene.logo);

            //在进入每个状态前重置控制信息
            ResetGameMessage();
            while (true)
            {
                //提前return，延迟一帧开始检测
                yield return 0;
                if (GetGameMessage(GameMessage.Next)) break;
            }

            //不直接用嵌套，防止嵌套层数过深（是否有自动优化？没查到）
            StartCoroutine(_Start());
        }

        // 游戏开始
        private IEnumerator _Start()
        {
            SceneSystem.LoadScene(GameScene.startScene);
            InputSystem.ChangeState(new InputSystem.MoveState());

            onGameStart?.Invoke();
            yield return 0;
        }



        #region 应用 & 参数 =================================
        /// <summary>
        /// 配置引用
        /// </summary>
        public static TheMatrixSetting Setting { get { return Resources.Load<TheMatrixSetting>("System/TheMatrixSetting"); } }
        public static string GetScene(GameScene gameScene) { return Setting.gameSceneMap[gameScene]; }

        private static TheMatrix instance;
        public static TheMatrix Instance
        {
            get
            {
#if UNITY_EDITOR
                if (!EditorApplication.isPlaying)
                {
                    Dialog("在编辑器状态下调用游戏代码很危险，请在游戏中调用");
                    return null;
                }
#endif
                if (instance == null)
                {
                    Error("没有加载TheMatrix！");
                }
                return instance;
            }
        }


        /// <summary>
        /// 游戏初始化委托，在进入游戏第一个场景时调用
        /// </summary>
        public static event System.Action onGameAwake;
        /// <summary>
        /// 游戏开始委托，在主场景游戏开始时和游戏重新开始时调用
        /// </summary>
        public static event System.Action onGameStart;

#if UNITY_EDITOR
        [MinsHeader("By Minstreams. The Matrix组件的核心，只能有一个。", SummaryType.CommentCenter)]
        [MinsHeader("The Matrix 母体系统", SummaryType.Title)]
        [Label("是否进行完整测试", true)]
        public bool testAll = false;
        [Label("是否测试文件保存", true)]
        public bool saveData = false;
        [Label]
        public bool debug = false;
#endif
        #endregion

        #region 游戏控制 ====================================
        //游戏控制----------------------------
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
                if (reset)
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
            Debug("Receive Message: " + message);
            gameMessageReciver[(int)message] = true;
        }
        /// <summary>
        /// 重置
        /// </summary>
        public static void ResetGameMessage()
        {
            gameMessageReciver.Initialize();
        }
        public static void Debug(string msg)
        {
#if UNITY_EDITOR
            if (!Instance.debug) return;
            UnityEngine.Debug.Log("【TheMatrix Debug】" + msg);
#endif
        }
        public static void Error(string msg)
        {
            UnityEngine.Debug.LogError("【TheMatrix Debug】" + msg);
        }
        public static void Dialog(string msg, string ok = "OK")
        {
#if UNITY_EDITOR
            EditorUtility.DisplayDialog("The Matrix", msg, ok);
#endif
        }
        #endregion

        #region 协程控制 ====================================
        //协程控制----------------------------
        private static Dictionary<System.Type, LinkedList<Coroutine>> routineDictionaty = new Dictionary<System.Type, LinkedList<Coroutine>>();

        public static LinkedListNode<Coroutine> StartCoroutine(IEnumerator routine, System.Type key)
        {
            LinkedList<Coroutine> linkedList;
            if (routineDictionaty.ContainsKey(key))
            {
                linkedList = routineDictionaty[key];
            }
            else
            {
                linkedList = new LinkedList<Coroutine>();
                routineDictionaty.Add(key, linkedList);
            }
            LinkedListNode<Coroutine> node = new LinkedListNode<Coroutine>(null);
            node.Value = Instance.StartCoroutine(SubCoroutine(routine, node));
            linkedList.AddLast(node);

            return node;
        }
        public static void StopAllCoroutines(System.Type key)
        {
            if (!routineDictionaty.ContainsKey(key)) return;
            LinkedList<Coroutine> linkedList = routineDictionaty[key];

            foreach (Coroutine c in linkedList)
            {
                Instance.StopCoroutine(c);
            }

            linkedList.Clear();
        }
        public static void StopCoroutine(LinkedListNode<Coroutine> node)
        {
            if (node == null || node.List == null) return;
            Instance.StopCoroutine(node.Value);
            node.List.Remove(node);
        }
        private static IEnumerator SubCoroutine(IEnumerator inCoroutine, LinkedListNode<Coroutine> node)
        {
            yield return inCoroutine;
            node.List.Remove(node);
        }
        #endregion

        #region 存档控制 ====================================
        //存档控制----------------------------
        private static void SaveTemporary(SavableObject data)
        {
            //此方法将数据保存到内存，但不保存到磁盘
            data.UpdateData();
            string stream = JsonUtility.ToJson(data);
            PlayerPrefs.SetString(data.ToString(), stream);
            Debug(data.name + " \tsaved!");
        }
        /// <summary>
        /// 手动保存一个对象
        /// </summary>
        public static void Save(SavableObject data)
        {
            SaveTemporary(data);
            PlayerPrefs.Save();
            Debug("Data saved to disc.");
        }
        /// <summary>
        /// 手动读取一个对象
        /// </summary>
        public static void Load(SavableObject data)
        {
            if (!PlayerPrefs.HasKey(data.ToString()))
            {
                Debug("No data found for " + data.name);
                return;
            }
            string stream = PlayerPrefs.GetString(data.ToString());
            JsonUtility.FromJsonOverwrite(stream, data);
            data.ApplyData();
            Debug(data.name + " \tloaded!");
        }

        [ContextMenu("Save All Data")]
        public void SaveAll()
        {
            if (Setting.dataAutoSave == null || Setting.dataAutoSave.Length == 0) return;
            foreach (SavableObject so in Setting.dataAutoSave)
            {
                SaveTemporary(so);
            }
            PlayerPrefs.Save();
            Debug("Data saved to disc.");
        }
        public void LoadAll()
        {
            foreach (SavableObject so in Setting.dataAutoSave)
            {
                Load(so);
            }
        }
        [ContextMenu("Delete All Data")]
        public void DeleteAll()
        {
            PlayerPrefs.DeleteAll();
            PlayerPrefs.Save();
            Dialog("All saved data deleted!");
        }
        #endregion

        #region 游戏启动 ====================================
        //游戏启动----------------------------
        private void Awake()
        {
            instance = this;
        }
        private void Start()
        {
            DontDestroyOnLoad(gameObject);
#if UNITY_EDITOR
            if (testAll)
#endif
                StartCoroutine(_Awake());

#if UNITY_EDITOR
            else
            {
                onGameAwake?.Invoke();
                onGameStart?.Invoke();
                SceneManager.UnloadSceneAsync("System");
            }
            if (saveData)
#endif
                LoadAll();
#if UNITY_EDITOR
            else
            {
                foreach (SavableObject so in Setting.dataAutoSave)
                {
                    so.ApplyData();
                    Debug("All Savable applied.");
                }
            }
#endif
        }
        private void OnDestroy()
        {
#if UNITY_EDITOR
            if (saveData)
#endif
                SaveAll();
#if UNITY_EDITOR
            else
            {
                foreach (SavableObject so in Setting.dataAutoSave)
                {
                    so.UpdateData();
                    EditorUtility.SetDirty(so);
                    AssetDatabase.SaveAssets();
                    Debug("All Savable updated. And assets saved.");
                }
            }
#endif
        }
        #endregion
    }

    /// <summary>
    /// 控制信息枚举
    /// </summary>
    public enum GameMessage
    {
        Next,
        Return,
        Exit,
        GameOver,
        GameWin
    }
}
