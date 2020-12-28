using System;

namespace GameSystem.Networking
{
    /// <summary>
    /// Called on the server to process udp packet
    /// </summary>
    [AttributeUsage(AttributeTargets.Method)]
    class UDPProcessAttribute : Attribute { }
}