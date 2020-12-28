using System;

namespace GameSystem.Networking
{
    /// <summary>
    /// Called on the client when establishing tcp connection
    /// </summary>
    [AttributeUsage(AttributeTargets.Method)]
    class TCPConnectionAttribute : Attribute { }
}