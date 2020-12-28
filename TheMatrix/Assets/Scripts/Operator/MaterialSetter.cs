﻿using UnityEngine;

namespace GameSystem.Operator
{
    [AddComponentMenu("|Operator/MaterialSetter")]
    public class MaterialSetter : MonoBehaviour
    {
        [MinsHeader("Material Setter", SummaryType.TitleYellow, 0)]

        // Data
        [MinsHeader("Data", SummaryType.Header, 2)]
        [Label] public int index;

        [System.Serializable]
        public struct MaterialPair
        {
            public Renderer renderer;
            public int index;
            public Material mat;
        }
        [System.Serializable]
        public struct MaterialSettingPreset
        {
            [Label("Mat Pair")]
            public MaterialPair[] materialPairs;
        }
        [Label("Present")] public MaterialSettingPreset[] presets;

        [Label] public bool setOnEnable;

        private void OnEnable()
        {
            if (setOnEnable) Set();
        }


        // Input
        [ContextMenu("Set")]
        public void Set()
        {
            Set(index);
        }
        public void Set(int index)
        {
            foreach (MaterialPair mp in presets[index].materialPairs)
            {
                Material[] ms = mp.renderer.sharedMaterials;
                ms[mp.index] = mp.mat;
                mp.renderer.sharedMaterials = ms;
            }
        }
    }
}