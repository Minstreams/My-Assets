using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameSystem.Setting;
using GameSystem.Networking;

namespace GameSystem
{
    public partial class NetworkSystem : SubSystem<NetworkSystemSetting>
    {
        // Events & Packet distribution

        // ditributors
        /// <summary>
        /// for client to receive tcp packets
        /// </summary>
        static readonly Dictionary<string, System.Action<PacketBase>> tcpDistributors = new Dictionary<string, System.Action<PacketBase>>();
        /// <summary>
        /// for client to receive tcp packets for given id
        /// </summary>
        static readonly Dictionary<string, System.Action<Pktid>> tcpIdDistributors = new Dictionary<string, System.Action<Pktid>>();
        /// <summary>
        /// for server to process tcp packets
        /// </summary>
        static readonly Dictionary<string, System.Action<PacketBase, Server.Connection>> tcpProcessors = new Dictionary<string, System.Action<PacketBase, Server.Connection>>();
        /// <summary>
        /// for server to process tcp packets from client with given id
        /// </summary>
        static readonly Dictionary<string, System.Action<PacketBase, Server.Connection>> tcpIdProcessors = new Dictionary<string, System.Action<PacketBase, Server.Connection>>();

        // events
        public static event System.Action OnConnection;
        public static event System.Action OnDisconnection;
        public static event System.Action<Server.Connection> OnServerConnection;
        public static event System.Action<Server.Connection> OnServerDisconnection;
        public static event System.Action<UDPPacket> OnUDPReceive;
        public static event System.Action<UDPPacket> OnUDPProcess;
        public static event System.Action<string> OnReceive;        // for debug
        public static event System.Action<string, Server.Connection> OnProcess; // for debug

        // register
        public static void ListenPacket(string typeStr, System.Action<PacketBase> listener)
        {
            if (!tcpDistributors.ContainsKey(typeStr)) tcpDistributors.Add(typeStr, null);
            tcpDistributors[typeStr] += listener;
        }
        public static void StopListenPacket(string typeStr, System.Action<PacketBase> listener)
        {
            if (tcpDistributors.ContainsKey(typeStr))
            {
                tcpDistributors[typeStr] -= listener;
            }
        }
        public static void ListenPacketToId(string id, System.Action<Pktid> listener)
        {
            if (!tcpIdDistributors.ContainsKey(id)) tcpIdDistributors.Add(id, null);
            tcpIdDistributors[id] += listener;
        }
        public static void StopListenPacketToId(string id, System.Action<Pktid> listener)
        {
            if (tcpIdDistributors.ContainsKey(id))
            {
                tcpIdDistributors[id] -= listener;
            }
        }
        public static void ProcessPacket(string typeStr, System.Action<PacketBase, Server.Connection> processor)
        {
            if (!tcpProcessors.ContainsKey(typeStr)) tcpProcessors.Add(typeStr, null);
            tcpProcessors[typeStr] += processor;
        }
        public static void StopProcessPacket(string typeStr, System.Action<PacketBase, Server.Connection> processor)
        {
            if (tcpProcessors.ContainsKey(typeStr))
            {
                tcpProcessors[typeStr] -= processor;
            }
        }
        public static void ProcessPacketFromId(string id, System.Action<PacketBase, Server.Connection> processor)
        {
            if (!tcpIdProcessors.ContainsKey(id)) tcpIdProcessors.Add(id, null);
            tcpIdProcessors[id] += processor;
        }
        public static void StopProcessPacketFromId(string id, System.Action<PacketBase, Server.Connection> processor)
        {
            if (tcpIdProcessors.ContainsKey(id))
            {
                tcpIdProcessors[id] -= processor;
            }
        }


        // client interface
        public static void CallConnection() => OnConnection?.Invoke();
        public static void CallDisconnection() => OnDisconnection?.Invoke();
        public static void CallUDPReceive(UDPPacket packet) => OnUDPReceive?.Invoke(packet);
        public static void CallReceive(string message)
        {
            if (latencyOverride > 0 && !IsHost) CallDelayMain(() =>
            {
                OnReceive?.Invoke(message);
                var pkt = StringToPacket(message);
                if (pkt == null) return;
                if (tcpDistributors.ContainsKey(pkt.t))
                {
                    tcpDistributors[pkt.t]?.Invoke(pkt);
                }
                if (pkt.IsSubclassOf(typeof(Pktid)))
                {
                    var pktTid = pkt as Pktid;
                    if (tcpIdDistributors.ContainsKey(pktTid.id))
                    {
                        tcpIdDistributors[pktTid.id]?.Invoke(pktTid);
                    }
                }
            });
            else
            {
                OnReceive?.Invoke(message);
                var pkt = StringToPacket(message);
                if (pkt == null) return;
                if (tcpDistributors.ContainsKey(pkt.t))
                {
                    tcpDistributors[pkt.t]?.Invoke(pkt);
                }
                if (pkt.IsSubclassOf(typeof(Pktid)))
                {
                    var pktTid = pkt as Pktid;
                    if (tcpIdDistributors.ContainsKey(pktTid.id))
                    {
                        tcpIdDistributors[pktTid.id]?.Invoke(pktTid);
                    }
                }
            }
        }

        // server interface
        public static void CallServerConnection(Server.Connection connection) => OnServerConnection?.Invoke(connection);
        public static void CallServerDisconnection(Server.Connection connection) => OnServerDisconnection?.Invoke(connection);
        public static void CallUDPProcess(UDPPacket packet) => OnUDPProcess?.Invoke(packet);
        public static void CallProcess(string message, Server.Connection connection)
        {
            OnProcess?.Invoke(message, connection);
            PacketBase pkt = StringToPacket(message);
            if (pkt == null) return;
            if (tcpProcessors.ContainsKey(pkt.t))
            {
                tcpProcessors[pkt.t]?.Invoke(pkt, connection);
            }
            if (tcpIdProcessors.ContainsKey(connection.NetId))
            {
                tcpIdProcessors[connection.NetId]?.Invoke(pkt, connection);
            }
        }

    }
}