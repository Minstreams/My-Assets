using UnityEngine;
using GameSystem.Networking;
using System.Net;
using GameSystem.UI;

namespace GameSystem.Operator
{
    /// <summary>
    /// 用于客户端向服务器发送和接收tcp消息
    /// </summary>
    [AddComponentMenu("[NetworkSystem]/Operator/ClientConnectionAgent")]
    public class ClientConnectionAgent : UIBase
    {
        [MinsHeader("Operator of NetworkSystem", SummaryType.PreTitleOperator, -1)]
        [MinsHeader("客户端连接代理", SummaryType.TitleOrange, 0)]
        [MinsHeader("用于客户端向服务器发送和接收消息", SummaryType.CommentCenter, 1)]

        //Data
        [MinsHeader("Data", SummaryType.Header, 2)]
        [Label] public string message;

        //Output
        [MinsHeader("Output", SummaryType.Header, 2)]
        public StringEvent output;


        Client connection;
        [ContextMenu("Init")]
        public void Init()
        {
            connection = NetworkSystem.client;
            NetworkSystem.OnReceive += msg => output?.Invoke(msg);
            NetworkSystem.OnUDPReceive += packet => output?.Invoke(packet.message);
        }

        //Input
        [ContextMenu("Send")]
        public void Send() => Send(message);
        public void Send(string message)
        {
            if (connection == null)
            {
                Debug.LogWarning("No connection assigned");
                return;
            }
            connection.Send(message);
        }
        [ContextMenu("UDPSend")]
        public void UDPSend()
        {
            if (connection == null)
            {
                Debug.LogWarning("No connection assigned");
                return;
            }
            connection.UDPSend(message, new IPEndPoint(NetworkSystem.ServerIPAddress, NetworkSystem.Setting.serverUDPPort));
        }
        [ContextMenu("UDPBroadcastToServer")]
        public void UDPBroadcastToServer()
        {
            if (connection == null)
            {
                Debug.LogWarning("No connection assigned");
                return;
            }
            connection.UDPSend(message, new IPEndPoint(NetworkSystem.BroadcastAddress, NetworkSystem.Setting.serverUDPPort));
        }
        [ContextMenu("UDPBroadcastToClient")]
        public void UDPBroadcastToClient()
        {
            if (connection == null)
            {
                Debug.LogWarning("No connection assigned");
                return;
            }
            connection.UDPSend(message, new IPEndPoint(NetworkSystem.BroadcastAddress, NetworkSystem.Setting.clientUDPPort));
        }

        protected override void OnUI()
        {
            TitleLabel("Client Agent");
            if (GUILayout.Button("Init")) Init();
            if (connection != null)
            {
                message = StringField("Message", message);

                if (GUILayout.Button("Send")) Send();
                if (GUILayout.Button("UDPSend")) UDPSend();
                if (GUILayout.Button("UDPBroadcastToServer")) UDPBroadcastToServer();
                if (GUILayout.Button("UDPBroadcastToClient")) UDPBroadcastToClient();
            }
        }
    }
}