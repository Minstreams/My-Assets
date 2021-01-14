using System.Net;
using System.Text.RegularExpressions;

namespace GameSystem.Networking
{
    public partial class Server
    {
        // Call Functions Definition

        // References
        static bool Debug
        {
            get
            {
#if UNITY_EDITOR
                if (!TheMatrix.Setting.debug) return false;
#endif
                return true;
            }
        }
        static IPAddress LocalIPAddress => NetworkSystem.LocalIPAddress;
        static IPAddress BroadcastAddress => NetworkSystem.BroadcastAddress;

        static int ServerUDPPort => NetworkSystem.Setting.serverUDPPort;
        static int ServerTCPPort => NetworkSystem.Setting.serverTCPPort;
        static int ClientUDPPort => NetworkSystem.Setting.clientUDPPort;
        static int LocalIPPort => NetworkSystem.LocalIPPort;

        const int MaxMsgLength = NetworkSystem.maxMsgLength;
        const char DivisionMark = NetworkSystem.divisionMark;
        static Regex PacketCutter => NetworkSystem.packetCutter;

        // Methods
        static void CallLog(string message) => NetworkSystem.CallLog(message);
        static void CallShutdownServer() => NetworkSystem.CallMainThread(NetworkSystem.ShutdownServer);
        static string CallPacketToString(PacketBase packet) => NetworkSystem.PacketToString(packet);

        // Events
        static void CallUDPProcess(UDPPacket packet) => NetworkSystem.CallUDPProcess(packet);
        static void CallProcess(string message, Connection connection) => NetworkSystem.CallProcess(message, connection);
        static void CallServerConnection(Connection connection) => NetworkSystem.CallServerConnection(connection);
        static void CallServerDisconnection(Connection connection) => NetworkSystem.CallServerDisconnection(connection);
    }
}
