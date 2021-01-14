using System;

namespace GameSystem.Networking
{
    /// <summary>
    /// Client and Connection of NetworkSystem
    /// </summary>
    public partial class Client
    {
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
                port = CallNewValidPort();
                Log("Client activated……:" + port);
            }
            catch (Exception ex)
            {
                Log(ex);
                CallShutdownClient();
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
