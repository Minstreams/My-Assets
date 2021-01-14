using System.Net;
using System.Text.RegularExpressions;

namespace GameSystem.Networking
{
    public partial class Client
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
        static IPAddress ServerIPAddress => NetworkSystem.ServerIPAddress;

        static int ServerTCPPort => NetworkSystem.Setting.serverTCPPort;
        static int ClientUDPPort => NetworkSystem.Setting.clientUDPPort;

        const int MaxMsgLength = NetworkSystem.maxMsgLength;
        const char DivisionMark = NetworkSystem.divisionMark;
        static Regex PacketCutter => NetworkSystem.packetCutter;

        // Methods
        static void CallLog(string message) => NetworkSystem.CallLog(message);
        static int CallNewValidPort() => NetworkSystem.NewValidPort();

        // Events
        static void CallShutdownClient() => NetworkSystem.ShutdownClient();
        static void CallConnection() => NetworkSystem.CallConnection();
        static void CallDisconnection() => NetworkSystem.CallDisconnection();
        static void CallReceive(string message) => NetworkSystem.CallReceive(message);
        static void CallUDPReceive(UDPPacket packet) => NetworkSystem.CallUDPReceive(packet);

    }
}
