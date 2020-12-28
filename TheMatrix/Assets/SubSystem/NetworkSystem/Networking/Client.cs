using System;

namespace GameSystem.Networking
{
    /// <summary>
    /// Client and Connection of NetworkSystem
    /// </summary>
    public partial class Client
    {
        Setting.NetworkSystemSetting Setting => NetworkSystem.Setting;
        bool isDestroyed = false;

        public void Destroy()
        {
            if (isDestroyed)
            {
                Log("Disposed.");
                return;
            }
            isDestroyed = true;

            Log("Destroy");
            CloseUDP();
            StopTCPConnecting();
        }

        public Client()
        {
            try
            {
                NetworkSystem.DetectLocalIPAddress();
                port = NetworkSystem.NewValidPort();
                Log("Client activated……:" + port);
            }
            catch (Exception ex)
            {
                Log(ex);
                NetworkSystem.ShutdownClient();
                return;
            }
        }
        ~Client()
        {
            Log("~Client");
            Destroy();
        }

    }
}
