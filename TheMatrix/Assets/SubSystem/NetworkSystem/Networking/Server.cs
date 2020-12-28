using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace GameSystem.Networking
{
    /// <summary>
    /// Server of NetworkSystem
    /// </summary>
    public partial class Server
    {
        /// <summary>
        /// Reference of Setting
        /// </summary>
        static Setting.NetworkSystemSetting Setting => NetworkSystem.Setting;

        // interface
        public void Destroy()
        {
            if (isDestroyed)
            {
                Log("already disposed.");
                return;
            }
            isDestroyed = true;

            udpReceiveThread?.Abort();
            udpClient.Close();

            DisconnectAll();
            CloseTCP();

            Log("Destroyed.");
        }

        // inner code
        bool isDestroyed;
        public Server()
        {
            udpClient = new UdpClient(Setting.serverUDPPort, AddressFamily.InterNetwork)
            {
                EnableBroadcast = true
            };
            udpReceiveThread = new Thread(UDPReceiveThread);
            udpReceiveThread.Start();

            Log("Server Activated……|UDP:" + Setting.serverUDPPort);
        }
        ~Server()
        {
            Log("~Server");
            Destroy();
        }
    }
}
