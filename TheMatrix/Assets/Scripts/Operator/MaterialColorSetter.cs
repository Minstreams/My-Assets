﻿using UnityEngine;

namespace GameSystem.Operator
{
    [AddComponentMenu("|Operator/MaterialColorSetter")]
    public class MaterialColorSetter : MonoBehaviour
    {
        [MinsHeader("Material Color Setter", SummaryType.TitleYellow, 0)]

        // Data
        [MinsHeader("Data", SummaryType.Header, 2)]
        [Label] public Color target = Color.white;
        [Label] public string paramName = "_EmissionFactor";
        [System.Serializable]
        public struct MaterialColorPair
        {
            public Renderer renderer;
            public int index;
            [HideInInspector]
            public Color value;
        }
        [Label("Mat-Color Pair")] public MaterialColorPair[] materialColorPairs;

        [Label(true)] public bool setOnStart = true;
        private void Start()
        {
            if (setOnStart) Set();
        }

        // Input
        [ContextMenu("Set")]
        public void Set()
        {
            Set(target);
        }
        public void Set(Color target)
        {
            foreach (MaterialColorPair mfp in materialColorPairs)
            {
                mfp.renderer.materials[mfp.index].SetColor(paramName, target);
            }
        }
    }
}