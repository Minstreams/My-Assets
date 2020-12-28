using GameSystem.UI;
using System.Net;
using UnityEngine;

namespace GameSystem.Operator
{
    /// <summary>
    /// 代理网络系统的各种功能
    /// </summary>
    [AddComponentMenu("[NetworkSystem]/Operator/NetworkSystemAgent")]
    public class NetworkSystemAgent : UIBase
    {
        [MinsHeader("Operator of NetworkSystem", SummaryType.PreTitleOperator, -1)]
        [MinsHeader("网络系统代理", SummaryType.TitleOrange, 0)]
        [MinsHeader("代理网络系统的各种功能", SummaryType.CommentCenter, 1)]

        [Label] public string serverAddress;

        //Output
        [MinsHeader("Events", SummaryType.Header, 2)]
        public SimpleEvent OnConnection;
        public SimpleEvent OnDisconnection;
        public SimpleEvent OnServerConnection;
        public SimpleEvent OnServerDisconnection;
        public StringEvent OnUDPReceive;
        public StringEvent OnUDPProcess;
        public StringEvent OnReceive;
        public StringEvent OnProcess;

        void Awake()
        {
            NetworkSystem.OnConnection += () => OnConnection?.Invoke();
            NetworkSystem.OnDisconnection += () => OnDisconnection?.Invoke();
            NetworkSystem.OnServerConnection += c => OnServerConnection?.Invoke();
            NetworkSystem.OnServerDisconnection += c => OnServerDisconnection?.Invoke();
            NetworkSystem.OnUDPReceive += val => OnUDPReceive?.Invoke(val.message);
            NetworkSystem.OnUDPProcess += val => OnUDPProcess?.Invoke(val.message);
            NetworkSystem.OnReceive += val => OnReceive?.Invoke(val);
            NetworkSystem.OnProcess += (val, c) => OnProcess?.Invoke(val);
        }

        //Input
        [ContextMenu("LaunchServer")]
        public void LaunchServer() => NetworkSystem.LaunchServer();
        [ContextMenu("LaunchClient")]
        public void LaunchClient() => NetworkSystem.LaunchClient();
        [ContextMenu("ShutdownServer")]
        public void ShutdownServer() => NetworkSystem.ShutdownServer();
        [ContextMenu("ShutdownClient")]
        public void ShutdownClient() => NetworkSystem.ShutdownClient();
        [ContextMenu("ServerOpenTCP")]
        public void ServerOpenTCP() => NetworkSystem.ServerOpenTCP();
        [ContextMenu("ServerCloseTCP")]
        public void ServerCloseTCP() => NetworkSystem.ServerCloseTCP();
        [ContextMenu("ClientOpenUDP")]
        public void ClientOpenUDP() => NetworkSystem.ClientOpenUDP();
        [ContextMenu("ClientCloseUDP")]
        public void ClientCloseUDP() => NetworkSystem.ClientCloseUDP();
        [ContextMenu("ClientConnectTo")]
        public void ClientConnectTo() => NetworkSystem.ClientConnectTo(IPAddress.Parse(serverAddress));
        [ContextMenu("ClientDisconnect")]
        public void ClientDisconnect() => NetworkSystem.ClientDisconnect();
        [ContextMenu("DetectLocalIPAddress")]
        public void DetectLocalIPAddress()
        {
            NetworkSystem.DetectLocalIPAddress();
            serverAddress = NetworkSystem.LocalIPAddress.ToString();
        }

        protected override void OnUI()
        {
            GUILayout.BeginHorizontal();
            GUILayout.Label("Server UDP Port:", GUILayout.Width(110));
            try
            {
                NetworkSystem.Setting.serverUDPPort = int.Parse(GUILayout.TextField(NetworkSystem.Setting.serverUDPPort.ToString()));
            }
            catch { };
            GUILayout.EndHorizontal();

            GUILayout.BeginHorizontal();
            GUILayout.Label("Client UDP Port:", GUILayout.Width(110));
            try
            {
                NetworkSystem.Setting.clientUDPPort = int.Parse(GUILayout.TextField(NetworkSystem.Setting.clientUDPPort.ToString()));
            }
            catch { };
            GUILayout.EndHorizontal();

            GUILayout.BeginHorizontal();
            GUILayout.Label("Server TCP Port:", GUILayout.Width(110));
            try
            {
                NetworkSystem.Setting.serverTCPPort = int.Parse(GUILayout.TextField(NetworkSystem.Setting.serverTCPPort.ToString()));
            }
            catch { };
            GUILayout.EndHorizontal();

            GUILayout.BeginHorizontal();
            GUILayout.Label("Client TCP Min:", GUILayout.Width(110));
            try
            {
                NetworkSystem.Setting.clientTCPPortRange.x = int.Parse(GUILayout.TextField(NetworkSystem.Setting.clientTCPPortRange.x.ToString()));
            }
            catch { };
            GUILayout.EndHorizontal();

            GUILayout.BeginHorizontal();
            GUILayout.Label("Client TCP Max:", GUILayout.Width(110));
            try
            {
                NetworkSystem.Setting.clientTCPPortRange.y = int.Parse(GUILayout.TextField(NetworkSystem.Setting.clientTCPPortRange.y.ToString()));
            }
            catch { };
            GUILayout.EndHorizontal();

            GUILayout.BeginHorizontal();
            GUILayout.Label("Server Address:", GUILayout.Width(100));
            serverAddress = GUILayout.TextField(serverAddress);
            GUILayout.EndHorizontal();
            if (GUILayout.Button("LaunchServer")) LaunchServer();
            if (GUILayout.Button("LaunchClient")) LaunchClient();
            if (GUILayout.Button("ShutdownServer")) ShutdownServer();
            if (GUILayout.Button("ShutdownClient")) ShutdownClient();
            if (GUILayout.Button("ServerOpenTCP")) ServerOpenTCP();
            if (GUILayout.Button("ServerCloseTCP")) ServerCloseTCP();
            if (GUILayout.Button("ClientOpenUDP")) ClientOpenUDP();
            if (GUILayout.Button("ClientCloseUDP")) ClientCloseUDP();
            if (GUILayout.Button("ClientConnectTo")) ClientConnectTo();
            if (GUILayout.Button("ClientDisconnect")) ClientDisconnect();
            if (GUILayout.Button("DetectLocalIPAddress")) DetectLocalIPAddress();
        }
    }
}