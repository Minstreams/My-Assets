using System.Net;
using System.Reflection;
using UnityEngine;

namespace GameSystem.Networking
{
    /// <summary>
    /// Base class for interacting with network system, 
    /// encapsulating network interface, 
    /// handling atrributes
    /// </summary>
    public abstract class NetworkBaseBehaviour : MonoBehaviour
    {
        // Properties
        protected static Setting.NetworkSystemSetting Setting => NetworkSystem.Setting;
        protected static bool IsHost => NetworkSystem.IsHost;
        protected static bool IsConnected => NetworkSystem.IsConnected;
        protected static IPAddress LocalIPAddress { get => NetworkSystem.LocalIPAddress; }
        protected static IPAddress ServerIPAddress => NetworkSystem.ServerIPAddress;
        protected static IPAddress BroadcastAddress => NetworkSystem.BroadcastAddress;

        // Thread
        protected void CallMainThread(System.Action action) => NetworkSystem.CallMainThread(action);

        // Packet Serialization
        protected static PacketBase StringToPacket(string str) => NetworkSystem.StringToPacket(str);
        protected static string PacketToString(PacketBase pkt) => NetworkSystem.PacketToString(pkt);

        // Packet Send
        protected static void ClientSendPacket(PacketBase pkt) => NetworkSystem.ClientSendPacket(pkt);
        protected static void ClientUDPSendPacket(PacketBase pkt, IPEndPoint endPoint) => NetworkSystem.ClientUDPSendPacket(pkt, endPoint);
        protected static void ServerSendPacket(PacketBase pkt, Server.Connection connection) => NetworkSystem.ServerSendPacket(pkt, connection);
        protected static void ServerBroadcastPacket(PacketBase pkt) => NetworkSystem.ServerBroadcastPacket(pkt);
        protected static void ServerUDPSendPacket(PacketBase pkt, IPEndPoint endPoint) => NetworkSystem.ServerUDPSendPacket(pkt, endPoint);
        protected static void ServerUDPBroadcastPacket(PacketBase pkt) => NetworkSystem.ServerUDPBroadcastPacket(pkt);

        // Inner Code
        protected event System.Action _onDestroyEvent;
        protected virtual private void OnDestroy()
        {
            _onDestroyEvent?.Invoke();
        }

        /// <summary>
        /// 处理通用的Attribute
        /// </summary>
        protected bool _AttributeHandle(MethodInfo m)
        {
            if (m.GetCustomAttribute<UDPProcessAttribute>() != null)
            {
                _ParametersCheck<UDPProcessAttribute>(m, typeof(UDPPacket));
                void mAction(UDPPacket pkt) => m.Invoke(this, new object[] { pkt });
                NetworkSystem.OnUDPProcess += mAction;
                _onDestroyEvent += () => NetworkSystem.OnUDPProcess -= mAction;
                return true;
            }
            else if (m.GetCustomAttribute<UDPReceiveAttribute>() != null)
            {
                _ParametersCheck<UDPReceiveAttribute>(m, typeof(UDPPacket));
                void mAction(UDPPacket pkt) => m.Invoke(this, new object[] { pkt });
                NetworkSystem.OnUDPReceive += mAction;
                _onDestroyEvent += () => NetworkSystem.OnUDPReceive -= mAction;
                return true;
            }
            else if (m.GetCustomAttribute<TCPConnectionAttribute>() != null)
            {
                _ParametersCheck<TCPConnectionAttribute>(m);
                void mAction() => m.Invoke(this, new object[] { });
                NetworkSystem.OnConnection += mAction;
                _onDestroyEvent += () => NetworkSystem.OnConnection -= mAction;
                return true;
            }
            else if (m.GetCustomAttribute<TCPDisconnectionAttribute>() != null)
            {
                _ParametersCheck<TCPDisconnectionAttribute>(m);
                void mAction() => m.Invoke(this, new object[] { });
                NetworkSystem.OnDisconnection += mAction;
                _onDestroyEvent += () => NetworkSystem.OnDisconnection -= mAction;
                return true;
            }
            else if (m.GetCustomAttribute<TCPServerConnectionAttribute>() != null)
            {
                _ParametersCheck<TCPServerConnectionAttribute>(m, typeof(Server.Connection));
                void mAction(Server.Connection conn) => m.Invoke(this, new object[] { conn });
                NetworkSystem.OnServerConnection += mAction;
                _onDestroyEvent += () => NetworkSystem.OnServerConnection -= mAction;
                return true;
            }
            else if (m.GetCustomAttribute<TCPServerDisconnectionAttribute>() != null)
            {
                _ParametersCheck<TCPServerDisconnectionAttribute>(m, typeof(Server.Connection));
                void mAction(Server.Connection conn) => m.Invoke(this, new object[] { conn });
                NetworkSystem.OnServerDisconnection += mAction;
                _onDestroyEvent += () => NetworkSystem.OnServerDisconnection -= mAction;
                return true;
            }
            return false;
        }
        /// <summary>
        /// Check if the parameter list is as expected. Throw exception if not.
        /// </summary>
        ///<returns>The fullname of the type of the first parameter</returns>
        protected string _ParametersCheck<Attri>(MethodInfo m, params System.Type[] parameters)
        {
            var ps = m.GetParameters();
            bool check = true;
            if (ps.Length != parameters.Length) check = false;
            for (int i = 0; i < parameters.Length; ++i)
            {
                var pt = ps[i].ParameterType;
                if (!pt.Equals(parameters[i]) && !pt.IsSubclassOf(parameters[i])) check = false;
            }
            if (!check)
            {
                string errMsg = "Parameters of " + this.name + "." + m.Name + " does not match what " + typeof(Attri).Name + " expected.\nExpected as below(";
                for (int i = 0; i < parameters.Length; i++) errMsg += $"[{i}]{parameters[i].FullName},";
                errMsg += ").\nBut the parameters are(";
                for (int i = 0; i < ps.Length; i++) errMsg += $"[{i}]{ps[i].ParameterType.FullName},";
                errMsg += ").";
                throw new System.Exception(errMsg);
            }
            return ps.Length > 0 ? ps[0].ParameterType.FullName : "";
        }
    }
}