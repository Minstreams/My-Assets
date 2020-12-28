using System;

namespace GameSystem.Networking
{
    /// <summary>
    /// Called on the server when disconnected
    /// </summary>
    [AttributeUsage(AttributeTargets.Method)]
    class TCPServerDisconnectionAttribute : Attribute { }
}