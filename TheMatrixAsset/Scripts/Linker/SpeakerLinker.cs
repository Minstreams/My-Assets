﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace GameSystem
{
    namespace Linker
    {
        [AddComponentMenu("|Linker/SpeakerLinker")]
        public class SpeakerLinker : MonoBehaviour
        {
#if UNITY_EDITOR
            [MinsHeader("Speaker Linker", SummaryType.TitleCyan, 0)]
            [ConditionalShow, SerializeField] private bool useless; //在没有数据的时候让标题正常显示
#endif

            public enum SpeakerChannel
            {
                leftChannel = 0,
                rightChannel = 1
            }

            //Data
            [MinsHeader("Data", SummaryType.Header, 2)]
            [Label]
            public AudioSource audioSource;

            [Label]
            public SpeakerChannel channel = 0;
            [Label]
            public FFTWindow fTWindow;
            [Label]
            public int outputFrequencyIndex = 3;

            private float[] data = new float[64];

            private void Update()
            {
                audioSource.GetSpectrumData(data, (int)channel, fTWindow);
                onSpeak?.Invoke(data[outputFrequencyIndex]);
            }

            //Output
            [MinsHeader("Output", SummaryType.Header, 3)]
            public FloatEvent onSpeak;
        }
    }
}
