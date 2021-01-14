using System;
using System.Net.Sockets;

namespace GameSystem.Networking
{
    public partial class Server
    {
        // Debug
        static void Log(object msg)
        {
            if (!Debug) return;
            CallLog("[Server]" + msg.ToString());
        }
        static void Log(SocketException ex)
        {
            CallLog("[Server Exception]" + ex.GetType().Name + "|" + ex.SocketErrorCode + ":" + ex.Message + "\n" + ex.StackTrace);
        }
        static void Log(Exception ex)
        {
            CallLog("[Server Exception]" + ex.GetType().Name + ":" + ex.Message + "\n" + ex.StackTrace);
        }
    }
}