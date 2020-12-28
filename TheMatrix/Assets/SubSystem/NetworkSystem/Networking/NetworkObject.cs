using System.Reflection;

namespace GameSystem.Networking
{
    /// <summary>
    /// General network object(without id)
    /// </summary>
    public abstract class NetworkObject : NetworkBaseBehaviour
    {
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
                    void mAction(PacketBase pktBase, Server.Connection Connection) => m.Invoke(this, new object[] { pktBase, Connection });
                    NetworkSystem.ProcessPacket(pType, mAction);
                    _onDestroyEvent += () => NetworkSystem.StopProcessPacket(pType, mAction);
                }
                else if (m.GetCustomAttribute<TCPReceiveAttribute>() != null)
                {
                    string pType = _ParametersCheck<TCPReceiveAttribute>(m, typeof(PacketBase));
                    void mAction(PacketBase pktBase) => m.Invoke(this, new object[] { pktBase });
                    NetworkSystem.ListenPacket(pType, mAction);
                    _onDestroyEvent += () => NetworkSystem.StopListenPacket(pType, mAction);
                }
            }
        }
    }
}