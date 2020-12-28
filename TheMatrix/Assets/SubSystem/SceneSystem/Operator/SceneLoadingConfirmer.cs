using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameSystem.Operator
{
    /// <summary>
    /// 用来响应场景加载事件
    /// </summary>
    [AddComponentMenu("[SceneSystem]/Operator/SceneLoadingConfirmer")]
    public class SceneLoadingConfirmer : MonoBehaviour
    {
#if UNITY_EDITOR
        [MinsHeader("Operator of SceneSystem", SummaryType.PreTitleOperator, -1)]
        [MinsHeader("场景加载确认器", SummaryType.TitleOrange, 0)]
        [MinsHeader("用来响应场景加载事件", SummaryType.CommentCenter, 1)]
        [ConditionalShow, SerializeField] bool useless; // 在没有数据的时候让标题正常显示
#endif

        // Data
        [MinsHeader("Data", SummaryType.Header, 2)]
        [Label(true)] public SceneCode sceneToLoad;

        // Output
        public SimpleEvent onPendingLoadScene;

        // Input
        public void Confirm() => SceneSystem.ConfirmLoadScene();

        void OnPendingLoadScene()
        {
            if (SceneSystem.SceneToLoad == sceneToLoad) onPendingLoadScene?.Invoke();
        }

        void Start()
        {
            SceneSystem.OnPendingLoadScene += OnPendingLoadScene;
        }
        void OnDestroy()
        {
            SceneSystem.OnPendingLoadScene -= OnPendingLoadScene;
        }
    }
}