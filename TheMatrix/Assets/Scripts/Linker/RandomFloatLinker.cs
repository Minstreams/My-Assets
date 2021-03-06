﻿using UnityEngine;

namespace GameSystem.Linker
{
    [AddComponentMenu("|Linker/RandomFloatLinker")]
    public class RandomFloatLinker : MonoBehaviour
    {
        [MinsHeader("Random Float Linker", SummaryType.TitleCyan, 0)]

        // Data
        [MinsHeader("Data", SummaryType.Header, 2)]
        [Label] public Vector2 range = Vector2.up;

        // Output
        [MinsHeader("Output", SummaryType.Header, 3)]
        [Label] public FloatEvent output;

        // Input
        public void Invoke()
        {
            output?.Invoke(Random.Range(range.x, range.y));
        }
        public void SetRange(Vector2 range)
        {
            this.range = range;
        }
    }
}