using UnityEngine;
using GameSystem.Networking;
using GameSystem.UI;

namespace GameSystem.Operator
{
    /// <summary>
    /// 用于服务器向特定客户端发送和接收tcp消息
    /// </summary>
    [AddComponentMenu("[NetworkSystem]/Operator/ServerConnectionAgent")]
    public class ServerConnectionAgent : UIBase
    {
        [MinsHeader("Operator of NetworkSystem", SummaryType.PreTitleOperator, -1)]
        [MinsHeader("服务器连接代理", SummaryType.TitleOrange, 0)]
        [MinsHeader("用于服务器向特定客户端发送和接收tcp消息", SummaryType.CommentCenter, 1)]

        //Data
        [MinsHeader("Data", SummaryType.Header, 2)]
        [Label] public string message;
        [Label] public int connectionIndex;

        //Output
        [MinsHeader("Output", SummaryType.Header, 2)]
        public StringEvent output;

        Server.Connection connection;
        [ContextMenu("Init")]
        public void Init()
        {
            Init(NetworkSystem.server.GetConnection(connectionIndex));
        }
        public void Init(Server.Connection connection)
        {
            this.connection = connection;
            NetworkSystem.OnProcess += (msg, conn) => output?.Invoke(msg);
            NetworkSystem.OnUDPProcess += msg => output?.Invoke(msg.message);
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
            NetworkSystem.server.UDPSend(message, connection.UDPEndPoint);
        }

        [ContextMenu("UDPBroadcast")]
        public void UDPBroadcast() => NetworkSystem.server.UDPBroadcast(message);

        protected override void OnUI()
        {
            TitleLabel("Server Agent");

            message = StringField("Message", message);
            connectionIndex = IntField("Connection Index", connectionIndex);

            if (GUILayout.Button("Init")) Init();
            if (connection != null)
            {
                if (GUILayout.Button("Send")) Send();
                if (GUILayout.Button("UDPSend")) UDPSend();
            }
            if (GUILayout.Button("UDPBroadcast")) UDPBroadcast();
        }
    }
}