using System;

namespace GameSystem.Networking
{
    /// <summary>
    /// Called on the client when disconnected
    /// </summary>
    [AttributeUsage(AttributeTargets.Method)]
    class TCPDisconnectionAttribute : Attribute { }
}