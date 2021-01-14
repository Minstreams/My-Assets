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
            TitleLabel("Network System");

            NetworkSystem.Setting.serverUDPPort = IntField("Server UDP Port", NetworkSystem.Setting.serverUDPPort);
            NetworkSystem.Setting.clientUDPPort = IntField("Client UDP Port", NetworkSystem.Setting.clientUDPPort);
            NetworkSystem.Setting.serverTCPPort = IntField("Server TCP Port", NetworkSystem.Setting.serverTCPPort);
            NetworkSystem.Setting.clientTCPPortRange.x = IntField("Client TCP Min", NetworkSystem.Setting.clientTCPPortRange.x);
            NetworkSystem.Setting.clientTCPPortRange.y = IntField("Client TCP Max", NetworkSystem.Setting.clientTCPPortRange.y);
            serverAddress = StringField("Server Address", serverAddress);

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