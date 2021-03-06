﻿using UnityEngine;

namespace GameSystem.Linker
{
    [AddComponentMenu("|Linker/SimpleColorLinker")]
    public class SimpleColorLinker : MonoBehaviour
    {
        [MinsHeader("Simple Color Linker", SummaryType.TitleCyan, 0)]

        // Data
        [MinsHeader("Data", SummaryType.Header, 2)]
        [Label] public Color data = Color.white;
        [Label(true)] public bool invokeOnStart;

        void Start()
        {
            if (invokeOnStart) Invoke();
        }

        // Output
        [MinsHeader("Output", SummaryType.Header, 3)]
        [Label] public ColorEvent output;

        // Input
        [ContextMenu("Invoke")]
        public void Invoke() => output?.Invoke(data);
        public void SetR(float r) => data.r = r;
        public void SetG(float g) => data.g = g;
        public void SetB(float b) => data.b = b;
        public void SetA(float a) => data.a = a;
    }
}