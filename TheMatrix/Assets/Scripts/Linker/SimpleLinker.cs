﻿using UnityEngine;

namespace GameSystem.Linker
{
    [AddComponentMenu("|Linker/SimpleLinker")]
    public class SimpleLinker : MonoBehaviour
    {
        [MinsHeader("Simple Linker", SummaryType.TitleCyan, 0)]

        // Data
        [MinsHeader("Data", SummaryType.Header, 2)]
        [Label(true)] public bool invokeOnStart;

        void Start()
        {
            if (invokeOnStart) Invoke();
        }

        // Output
        [MinsHeader("Output", SummaryType.Header, 3)]
        [Label] public SimpleEvent output;

        // Input
        [ContextMenu("Invoke")]
        public void Invoke() => output?.Invoke();
    }
}