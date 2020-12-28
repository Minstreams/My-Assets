using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace GameSystem.Networking
{
    public partial class Client
    {
        // UDP
        // interface
        public void OpenUDP()
        {
            if (udpClient != null)
            {
                Log("UDP already opened!");
                return;
            }
            while (true)
            {
                try
                {
                    udpClient = new UdpClient(Setting.clientUDPPort, AddressFamily.InterNetwork) { EnableBroadcast = true };
                    break;
                }
                catch (SocketException ex)
                {
                    Log(ex);
                    // TODO: handle the exception here
                    return;
                }
                catch (Exception ex)
                {
                    Log(ex);
                    CloseUDP();
                    return;
                }
            }
            udpReceiveThread = new Thread(UDPReceiveThread);
            udpReceiveThread.Start();
        }
        public void CloseUDP()
        {
            if (udpClient == null)
            {
                Log("UDP already closed!");
                return;
            }
            udpReceiveThread?.Abort();
            udpClient?.Close();
            udpClient = null;
        }
        public void UDPSend(string message, IPEndPoint remote)
        {
            if (udpClient == null)
            {
                Log("UDP not opened!");
                return;
            }
            byte[] messageBytes = Encoding.UTF8.GetBytes(message);
            udpClient.Send(messageBytes, messageBytes.Length, remote);
            Log($"UDPSend{remote}:{message}");
        }

        // inner code
        UdpClient udpClient;
        Thread udpReceiveThread;

        void UDPReceiveThread()
        {
            Log("Start receiving udp packet……:" + Setting.clientUDPPort);
            while (true)
            {
                try
                {
                    IPEndPoint remoteIP = new IPEndPoint(IPAddress.Any, Setting.clientUDPPort);
                    byte[] buffer = udpClient.Receive(ref remoteIP);
                    string receiveString = Encoding.UTF8.GetString(buffer, 0, buffer.Length);
                    Log($"UDPReceive{remoteIP}:{receiveString}");
                    NetworkSystem.CallUDPReceive(new UDPPacket(receiveString, remoteIP));
                }
                catch (SocketException ex)
                {
                    Log(ex);
                    continue;
                }
                catch (ThreadAbortException)
                {
                    Log("UDPReceive Thread Aborted.");
                    return;
                }
                catch (Exception ex)
                {
                    Log(ex);
                    udpClient?.Close();
                    udpClient = null;
                    return;
                }
            }
        }

    }
}
