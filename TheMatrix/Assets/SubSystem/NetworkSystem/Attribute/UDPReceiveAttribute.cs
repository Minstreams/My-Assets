using System;

namespace GameSystem.Networking
{
    /// <summary>
    /// Called on the client to receive udp packet
    /// </summary>
    [AttributeUsage(AttributeTargets.Method)]
    class UDPReceiveAttribute : Attribute { }
}