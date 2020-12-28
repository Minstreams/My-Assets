using UnityEngine;

namespace GameSystem.UI
{
    public abstract class UIBase : MonoBehaviour
    {
        protected void SetStyle(ref GUIStyle target, string styleName)
        {
#if UNITY_EDITOR
            target = new GUIStyle(UnityEditor.EditorGUIUtility.GetBuiltinSkin(UnityEditor.EditorSkin.Game).GetStyle(styleName));
#endif
        }
        [Label] public GUIStyle boxStyle;

        UIDisplayer displayer;
        protected virtual void OnEnable()
        {
            displayer = GetComponentInParent<UIDisplayer>();
            if (displayer != null) displayer.UIAction += UIAction;
            else this.enabled = false;
        }
        protected virtual void OnDisable()
        {
            if (displayer != null) displayer.UIAction -= UIAction;
        }
        protected virtual void Reset()
        {
            SetStyle(ref boxStyle, "box");
        }
        public void UIAction()
        {
            GUILayout.BeginVertical(boxStyle, GUILayout.ExpandWidth(false));
            OnUI();
            GUILayout.EndVertical();
        }
        protected abstract void OnUI();
    }
}
