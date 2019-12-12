using System.Collections;
using UnityEngine;

namespace GameSystem
{
    namespace InnerSystem
    {
        namespace Instances
        {
            /// <summary>
            /// 系统参数设置器
            /// </summary>
            [DisallowMultipleComponent]
            [AddComponentMenu("Systems/Setter")]
            public class SetterInstance : SystemInstance<SetterInstance>
            {
#if UNITY_EDITOR
                [Header("【系统参数设置器】")]
                public EmptyStruct 一一一一一一一一一一一一一一一一一一一一一一一一一一一;
#endif
                public Setter.Setting setting;
            }
        }
    }

    /// <summary>
    /// 系统参数设置器
    /// </summary>
    public static class Setter
    {
        [System.Serializable]
        public class Setting
        {

        }

        /// <summary>
        /// 设置参数
        /// </summary>
        public static Setting setting { get { return Instance.setting; } }
        /// <summary>
        /// 实例
        /// </summary>
        private static InnerSystem.Instances.SetterInstance Instance { get { return InnerSystem.Instances.SetterInstance.Instance; } }
    }
}

