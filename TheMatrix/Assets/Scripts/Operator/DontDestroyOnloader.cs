﻿using UnityEngine;

namespace GameSystem.Operator
{
    [AddComponentMenu("|Operator/DontDestroyOnloader")]
    public class DontDestroyOnloader : MonoBehaviour
    {
#if UNITY_EDITOR
        [MinsHeader("Dont Destroy Onloader", SummaryType.TitleYellow, 0)]
        [MinsHeader("此物体会脱离场景长期存在", SummaryType.CommentCenter, 1)]
        [ConditionalShow, SerializeField] bool useless;
#endif

        void Start()
        {
            DontDestroyOnLoad(gameObject);
        }
    }
}