using System;
using System.Reflection;
using System.Collections.Generic;

namespace GameSystem.Networking
{
    /// <summary>
    /// Network object with net id
    /// </summary>
    public abstract class NetworkPlayer : NetworkBaseBehaviour
    {
        [Label] public string netId;

        // interface
        /// <summary>
        /// must call this manually!
        /// </summary>
        public void ActivateId(string netId)
        {
            if (isIdActive) return;
            isIdActive = true;
            this.netId = netId;
            NetworkSystem.ProcessPacketFromId(netId, _TCPProcess);
            NetworkSystem.ListenPacketToId(netId, _TCPReceive);
        }
        public void DeactivateId()
        {
            if (!isIdActive) return;
            isIdActive = false;
            NetworkSystem.StopProcessPacketFromId(netId, _TCPProcess);
            NetworkSystem.StopListenPacketToId(netId, _TCPReceive);
        }

        // inner code
        bool isIdActive = false;
        /// <summary>
        /// for server to process tcp packet
        /// </summary>
        readonly Dictionary<string, Func<object, object[], object>> tcpProcessors = new Dictionary<string, Func<object, object[], object>>();
        /// <summary>
        /// for client to receive tcp packet
        /// </summary>
        readonly Dictionary<string, Func<object, object[], object>> tcpDistributors = new Dictionary<string, Func<object, object[], object>>();

        void _TCPProcess(PacketBase packet, Server.Connection connection)
        {
            string tp = packet.t;
            if (tcpProcessors.ContainsKey(tp)) tcpProcessors[tp]?.Invoke(this, new object[] { packet, connection });
        }
        void _TCPReceive(Pktid packet)
        {
            string tp = packet.t;
            if (tcpDistributors.ContainsKey(tp)) tcpDistributors[tp]?.Invoke(this, new object[] { packet });
        }

        protected virtual void Awake()
        {
            var ms = this.GetType().GetRuntimeMethods();
            //Debug.Log("Start Processing. Type:" + this.GetType().FullName);
            foreach (var m in ms)
            {
                if (_AttributeHandle(m)) continue;
                if (m.GetCustomAttribute<TCPProcessAttribute>() != null)
                {
                    string pType = _ParametersCheck<TCPProcessAttribute>(m, typeof(PacketBase), typeof(Server.Connection));
                    if (!tcpProcessors.ContainsKey(pType)) tcpProcessors.Add(pType, null);
                    tcpProcessors[pType] += m.Invoke;
                }
                else if (m.GetCustomAttribute<TCPReceiveAttribute>() != null)
                {
                    string pType = _ParametersCheck<TCPReceiveAttribute>(m, typeof(Pktid));
                    if (!tcpDistributors.ContainsKey(pType)) tcpDistributors.Add(pType, null);
                    tcpDistributors[pType] += m.Invoke;
                }
            }
            _onDestroyEvent += DeactivateId;
        }
    }
}
