using UnityEngine;
using GameSystem.Savable;

namespace GameSystem.Setting
{
    [CreateAssetMenu(fileName = "TheMatrixSetting", menuName = "系统配置文件/TheMatrixSetting")]
    public class TheMatrixSetting : ScriptableObject
    {
        [MinsHeader("TheMatrix Setting", SummaryType.Title, -2)]
        [MinsHeader("母体，基本框架", SummaryType.CommentCenter, -1)]

        [MinsHeader("所有要自动保存的数据", SummaryType.Header), Space(16)]
        [Label("Savable", true)] public SavableObject[] dataAutoSave;

        [HideInInspector] public bool fullTest;
        [HideInInspector] public bool saveData;
        [HideInInspector] public bool debug;
    }
}