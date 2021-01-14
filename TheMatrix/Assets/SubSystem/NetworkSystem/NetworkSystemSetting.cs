using UnityEngine;
using System.Net;

namespace GameSystem.Setting
{
    [CreateAssetMenu(fileName = "NetworkSystemSetting", menuName = "系统配置文件/NetworkSystemSetting")]
    public class NetworkSystemSetting : ScriptableObject
    {
        [MinsHeader("NetworkSystem Setting", SummaryType.Title, -3)]
        [MinsHeader("Everything about network.", SummaryType.CommentCenter, -1)]

        [MinsHeader("PORT", SummaryType.Header), Space(16)]
        [Label] public int serverUDPPort = 7857;
        [Label] public int serverTCPPort = 7858;
        [Label] public int clientUDPPort = 7856;
        [Label] public Vector2Int clientTCPPortRange = new Vector2Int(12306, 17851);

        [MinsHeader("Connection", SummaryType.Header)]
        /// <summary>
        /// to check if the version matches
        /// </summary>
        [Label] public string authentication;
    }
}