using System;

namespace GameSystem.Networking
{
    /// <summary>
    /// Called on the server when establishing tcp connection
    /// </summary>
    [AttributeUsage(AttributeTargets.Method)]
    class TCPServerConnectionAttribute : Attribute { }
}