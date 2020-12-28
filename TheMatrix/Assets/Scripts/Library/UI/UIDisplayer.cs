using System;
using UnityEngine;

namespace GameSystem.UI
{
    [AddComponentMenu("|UI/UIDisplayer")]
    public class UIDisplayer : MonoBehaviour
    {
        [Label] public UIPosition position;
        [Label] public bool horizontal;
        public event Action UIAction;

        const int padding = 4;
        void OnGUI()
        {
            bool isLeft = position == UIPosition.UpperLeft || position == UIPosition.LowerLeft;
            bool isUp = position == UIPosition.UpperLeft || position == UIPosition.UpperRight;
            GUILayout.BeginHorizontal(GUILayout.Width(Screen.width));
            if (!isLeft) GUILayout.FlexibleSpace();
            GUILayout.BeginVertical(GUILayout.Height(Screen.height - padding));
            if (!isUp) GUILayout.FlexibleSpace();
            GUILayout.Space(padding);
            if (horizontal) GUILayout.BeginHorizontal();
            UIAction?.Invoke();
            if (horizontal) GUILayout.EndHorizontal();
            if (isUp) GUILayout.FlexibleSpace();
            GUILayout.EndVertical();
            if (isLeft) GUILayout.FlexibleSpace();
            GUILayout.EndHorizontal();
        }
    }
    public enum UIPosition
    {
        UpperLeft,
        UpperRight,
        LowerLeft,
        LowerRight,
    }
}