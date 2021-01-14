using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace GameSystem.Networking
{
    public partial class Server
    {
        // UDP
        // interface
        public void UDPSend(string message, IPEndPoint remote)
        {
            byte[] messageBytes = Encoding.UTF8.GetBytes(message);
            udpClient.Send(messageBytes, messageBytes.Length, remote);
            Log($"UDPSend{remote}:{message}");
        }
        public void UDPBroadcast(string message)
        {
            byte[] messageBytes = Encoding.UTF8.GetBytes(message);
            udpClient.Send(messageBytes, messageBytes.Length, new IPEndPoint(BroadcastAddress, ClientUDPPort));
            Log($"UDPBroadcast:{message}");
        }

        // inner code
        readonly UdpClient udpClient;
        readonly Thread udpReceiveThread;

        void UDPReceiveThread()
        {
            Log("Start receiving udp packet……");
            while (true)
            {
                try
                {
                    IPEndPoint remoteIP = new IPEndPoint(IPAddress.Any, ClientUDPPort);
                    byte[] buffer = udpClient.Receive(ref remoteIP);
                    string receiveString = Encoding.UTF8.GetString(buffer, 0, buffer.Length);
                    Log($"UDPReceive{remoteIP}:{receiveString}");
                    UDPPacket packet = new UDPPacket(receiveString, remoteIP);
                    CallUDPProcess(packet);
                }
                catch (SocketException ex)
                {
                    if (ex.SocketErrorCode == SocketError.ConnectionReset) Log("Target ip doesn't has a udp receiver!");
                    else Log(ex);
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
                    CallShutdownServer();
                    return;
                }
            }
        }
    }
}