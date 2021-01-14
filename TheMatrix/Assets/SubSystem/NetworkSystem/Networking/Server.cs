using System.Net.Sockets;
using System.Threading;

namespace GameSystem.Networking
{
    /// <summary>
    /// Server of NetworkSystem
    /// </summary>
    public partial class Server
    {
        // References
        static Server _Server;

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
            _Server = this;
            udpClient = new UdpClient(ServerUDPPort, AddressFamily.InterNetwork)
            {
                EnableBroadcast = true
            };
            udpReceiveThread = new Thread(UDPReceiveThread);
            udpReceiveThread.Start();

            Log("Server Activated……|UDP:" + ServerUDPPort);
        }
        ~Server()
        {
            Log("~Server");
            Destroy();
        }
    }
}
