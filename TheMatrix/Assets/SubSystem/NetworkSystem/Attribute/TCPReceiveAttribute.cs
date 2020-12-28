using System;

namespace GameSystem.Networking
{
    /// <summary>
    /// Called on the client to receive tcp packet
    /// </summary>
    [AttributeUsage(AttributeTargets.Method)]
    class TCPReceiveAttribute : Attribute { }
}