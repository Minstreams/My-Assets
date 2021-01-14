using System;
using System.Net.Sockets;

namespace GameSystem.Networking
{
    public partial class Client
    {
        // Debug
        void Log(object msg)
        {
            if (!Debug) return;
            CallLog("[Client]" + msg.ToString());
        }
        void Log(SocketException ex)
        {
            CallLog("[Client Exception:" + port + "]" + ex.GetType().Name + "|" + ex.SocketErrorCode + ":" + ex.Message + "\n" + ex.StackTrace);
        }
        void Log(Exception ex)
        {
            CallLog("[Client Exception]" + ex.GetType().Name + ":" + ex.Message + "\n" + ex.StackTrace);
        }
    }
}