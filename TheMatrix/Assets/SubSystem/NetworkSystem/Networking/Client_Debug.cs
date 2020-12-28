using System;
using System.Net.Sockets;

namespace GameSystem.Networking
{
    public partial class Client
    {
        // Debug
        void Log(object msg)
        {
#if UNITY_EDITOR
            if (!TheMatrix.Setting.debug) return;
#endif
            string msgStr = "[Client]" + msg.ToString();
            NetworkSystem.CallLog(msgStr);
        }
        void Log(SocketException ex)
        {
            NetworkSystem.CallLog("[Client Exception:" + port + "]" + ex.GetType().Name + "|" + ex.SocketErrorCode + ":" + ex.Message + "\n" + ex.StackTrace);
        }
        void Log(Exception ex)
        {
            NetworkSystem.CallLog("[Client Exception]" + ex.GetType().Name + ":" + ex.Message + "\n" + ex.StackTrace);
        }
    }
}