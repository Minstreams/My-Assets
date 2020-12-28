using System;
using System.Net.Sockets;

namespace GameSystem.Networking
{
    public partial class Server
    {
        // Debug
        static void Log(object msg)
        {
#if UNITY_EDITOR
            if (!TheMatrix.Setting.debug) return;
#endif
            string msgStr = "[Server]" + msg.ToString();
            NetworkSystem.CallLog(msgStr);
        }
        static void Log(SocketException ex)
        {
            NetworkSystem.CallLog("[Server Exception]" + ex.GetType().Name + "|" + ex.SocketErrorCode + ":" + ex.Message + "\n" + ex.StackTrace);
        }
        static void Log(Exception ex)
        {
            NetworkSystem.CallLog("[Server Exception]" + ex.GetType().Name + ":" + ex.Message + "\n" + ex.StackTrace);
        }
    }
}