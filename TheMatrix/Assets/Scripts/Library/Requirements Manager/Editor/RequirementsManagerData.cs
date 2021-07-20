﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameSystem.Requirements
{
    [CreateAssetMenu(menuName = "Requirements Manager/RequirementsManagerData", fileName = "RequirementsManagerData")]
    public class RequirementsManagerData : ScriptableObject
    {
        [MinsHeader("Data", SummaryType.Title, -1)]
        [MinsHeader("Requirements")]
        [Label] public List<Requirement> requirementList = new List<Requirement>();

        [Separator]
        [MinsHeader("Requirements Manager", SummaryType.Title, -1)]
        [MinsHeader("Window")]
        [Label] public Texture managerIcon;
        [Label] public string managerTitle;
        [Label] public Texture inspectorIcon;
        [Label] public string inspectorTitle;

        [MinsHeader("General")]
        [Label] public float reqLabelHeight = 16;
        [Label] public Vector2 reqRectMargin = new Vector2(4, 8);
        [Label] public Vector3 notificationPointPos;
        [Label] public Color notificationPointColor;
        [Label] public GUIStyle notificationPointStyle;

        [MinsHeader("Selection")]
        [Label] public GUIStyle selectionStyle;
        [Label] public Color unselectedColor;
        [Label] public Color selectedColor;

        [MinsHeader("Status Box")]
        [Label] public float statusWidth = 16;
        [Label] public float statusPaddingY = 4;
        [Label] public GUIStyle statusStyle;
        [Label] public Color pendingColor;
        [Label] public Color uncheckedColor;
        [Label] public Color checkedColor;
        [Label] public Color stableColor;
        [Label] public Texture pendingTex;
        [Label] public Texture uncheckedTex;
        [Label] public Texture checkedTex;
        [Label] public Texture stableTex;

        [MinsHeader("Name & Path Box")]
        [Label] public float reqNameWidth = 128;
        [Label] public GUIStyle nameBoxStyle;
        [Label] public GUIStyle pathBoxStyle;

        [MinsHeader("Priority")]
        [Label] public Color optionalColor;
        [Label] public Color normalColor;
        [Label] public Color urgentColor;

        [MinsHeader("Responsible Person")]
        [Label] public float responsiblePersonWidth = 64;
        [Label] public GUIStyle responsiblePersonStyle;
        [Label] public Texture responsiblePersonTex;

        [MinsHeader("Top Area")]
        [Label] public float basicHeight;
        [Label] public float indentWidth;
        [Label] public GUIStyle foldoutStyle;
        [Label] public float filterHeight;
        [Label] public float filterOptionWidth;
        [Label] public Color filterLocalColor;
        [Label] public GUIStyle filterLocalStyle;
        [Label] public GUIStyle filterWorldStyle;
        [Label] public GUIStyle filterPathStyle;
        [Label] public float sorterHeight;
        [Label] public GUIStyle sorterButtonStyle;
        [Label] public GUIStyle sorterLabelStyle;
        [Label] public GUIStyle sorterNormalStyle;
        [Label] public GUIStyle sorterInvertedStyle;
        [Label] public Color sorterFirstColor;
        [Label] public Color sorterNormalColor;

        [Separator]
        [MinsHeader("Inspector", SummaryType.Title, -1)]
        [Label] public GUIStyle nameStyle;
        [Label] public GUIStyle headerStyle;
        [Label] public GUIStyle miniHeaderStyle;
        [Label] public GUIStyle miniButtonSytle;
        [Label] public GUIStyle singlelineStyle;
        [Label] public GUIStyle multilineStyle;
        [Label] public GUIStyle multilineAreaStyle;
    }
}