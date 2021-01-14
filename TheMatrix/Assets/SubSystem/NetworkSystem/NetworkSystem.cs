using GameSystem.Setting;
using GameSystem.Networking;
using System.Net;
using System.Text.RegularExpressions;

namespace GameSystem
{
    /// <summary>
    /// 网络系统。用于多人联机和服务器服务。
    /// <para>
    /// Everything about network.
    /// </para>
    /// </summary>
    public partial class NetworkSystem : SubSystem<NetworkSystemSetting>
    {
        /// <summary>
        /// max length of TCP buffer
        /// </summary>
        public const int maxMsgLength = 2048;
        /// <summary>
        /// TCP packet division mark
        /// </summary>
        public const char divisionMark = '✡';
        public static readonly Regex packetCutter = new Regex("^([^" + divisionMark + "]*)" + divisionMark + "(.*)$");

        public static Server server = null;
        public static Client client = null;

        public static bool IsHost => server != null;
        public static bool IsConnected => client != null && client.IsConnected;
        public static string NetId => client == null ? "" : client.NetId;

        public static IPAddress ServerIPAddress { get; private set; } = IPAddress.Any;
        public static IPAddress LocalIPAddress { get; private set; } = IPAddress.Any;
        public static IPAddress BroadcastAddress { get; private set; } = IPAddress.Broadcast;
        public static int LocalIPPort { get; private set; } = 0;
        /// <summary>
        /// Generate a new local ip port and return it
        /// </summary>
        public static int NewValidPort() => LocalIPPort = LocalIPPort < Setting.clientTCPPortRange.x || LocalIPPort >= Setting.clientTCPPortRange.y ? Setting.clientTCPPortRange.x : LocalIPPort + 1;

        public static void DetectLocalIPAddress()
        {
            try
            {
                IPHostEntry ipEntry = Dns.GetHostEntry(Dns.GetHostName());
                foreach (var item in ipEntry.AddressList)
                {
                    if (item.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                    {
                        LocalIPAddress = item;
                        Log("LocalIP:" + item);
                        break;
                    }
                }
                //string local = LocalIPAddress.ToString();
                //BroadcastAddress = IPAddress.Parse(local.Substring(0, local.LastIndexOf('.')) + ".255");
            }
            catch (System.Exception ex)
            {
                LogAssertion(ex.Message);
            }
        }


        // API---------------------------------
        public static void LaunchServer()
        {
            if (server != null)
            {
                Dialog("Server already existed!");
                return;
            }
            Log("Launch Server");
            server = new Server();
        }
        public static void LaunchClient()
        {
            if (client != null)
            {
                Dialog("Client already existed!");
                return;
            }
            Log("Launch Client");
            client = new Client();
        }
        public static void ShutdownServer()
        {
            Log("Shutdown Server");
            server?.Destroy();
            server = null;
        }
        public static void ShutdownClient()
        {
            Log("Shutdown Client");
            client?.Destroy();
            client = null;
        }

        public static void ServerOpenTCP() => server?.OpenTCP();
        public static void ServerCloseTCP() => server?.CloseTCP();
        public static void ServerDisconnectAll() => server?.DisconnectAll();
        public static void ClientOpenUDP() => client?.OpenUDP();
        public static void ClientCloseUDP() => client?.CloseUDP();

        public static void ClientConnectTo(IPAddress serverIPAddress)
        {
            ServerIPAddress = serverIPAddress;
            client?.StartTCPConnecting();
        }
        public static void ClientDisconnect() => client?.StopTCPConnecting();

        // TCP
        public static void ClientSendPacket(PacketBase pkt)
        {
            if (client == null || !client.IsConnected) return;
            client.Send(PacketToString(pkt));
        }
        public static void ServerSendPacket(PacketBase pkt, Server.Connection connection)
        {
            connection.Send(PacketToString(pkt));
        }
        public static void ServerBroadcastPacket(PacketBase pkt)
        {
            server?.Broadcast(PacketToString(pkt));
        }

        // UDP
        public static void ClientUDPSendPacket(PacketBase pkt, IPEndPoint endPoint)
        {
            client?.UDPSend(PacketToString(pkt), endPoint);
        }
        public static void ServerUDPSendPacket(PacketBase pkt, IPEndPoint endPoint)
        {
            server?.UDPSend(PacketToString(pkt), endPoint);
        }
        public static void ServerUDPBroadcastPacket(PacketBase pkt)
        {
            server?.UDPBroadcast(PacketToString(pkt));
        }
    }
}
