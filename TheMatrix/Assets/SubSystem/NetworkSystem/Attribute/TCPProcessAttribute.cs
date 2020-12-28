using System;

namespace GameSystem.Networking
{
    /// <summary>
    /// Called on the server to process tcp packet
    /// </summary>
    [AttributeUsage(AttributeTargets.Method)]
    class TCPProcessAttribute : Attribute { }
}