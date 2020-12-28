﻿using UnityEngine;

namespace GameSystem.Linker
{
    [AddComponentMenu("|Linker/LoopLinker")]
    public class LoopLinker : MonoBehaviour
    {
        // Private Here
        int index = 0;

        [MinsHeader("Loop Linker", SummaryType.TitleCyan, 0)]

        // Output
        [MinsHeader("Output", SummaryType.Header, 3)]
        [Label("output")] public SimpleEvent[] outputs;

        // Input
        [ContextMenu("Invoke")]
        public void Invoke()
        {
            outputs[index++]?.Invoke();
            if (index > outputs.Length) index = 0;
        }
        public void ResetIndex()
        {
            index = 0;
        }

    }
}