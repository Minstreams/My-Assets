using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Text;
using System.Text.RegularExpressions;

namespace GameSystem.Networking
{
    public partial class Server
    {
        /// <summary>
        /// A tcp connection on server
        /// </summary>
        public class Connection
        {
            // interface
            /// <summary>
            /// Unique identification for each connection
            /// </summary>
            public string NetId { get; private set; }
            public bool IsHost => NetId.Equals(hostId);
            public IPEndPoint RemoteEndPoint => client.Client.RemoteEndPoint as IPEndPoint;
            public IPEndPoint UDPEndPoint => new IPEndPoint(RemoteEndPoint.Address, ClientUDPPort);

            public void Destroy()
            {
                if (isDestroyed)
                {
                    Log("Server.connection already disposed.");
                    return;
                }
                isDestroyed = true;

                CallServerDisconnection(this);
                stream?.Close();
                client?.Close();
                Log("Server.connection Destroyed.");
                receiveThread?.Abort();
            }
            public void Send(string message)
            {
                if (isDestroyed) return;
                byte[] messageBytes = Encoding.UTF8.GetBytes(message + DivisionMark);
                stream?.Write(messageBytes, 0, messageBytes.Length);
                Log("Send to " + NetId + ": " + message);
            }
            public void Send(PacketBase packet) => Send(CallPacketToString(packet));
            public void Log(string message) => Server.Log(message + $"(id:{NetId})");

            // inner code
            /// <summary>
            /// the client socket
            /// </summary>
            readonly TcpClient client;
            readonly NetworkStream stream;
            readonly Thread receiveThread;
            readonly byte[] buffer = new byte[MaxMsgLength];
            string bufferString = "";

            bool isDestroyed = false;

            public Connection(TcpClient client, string netId)
            {
                this.client = client;
                NetId = netId;
                stream = client.GetStream();

                receiveThread = new Thread(ReceiveThread);
                receiveThread.Start();

                // confirm the net id once connected
                Send(netId);

                CallServerConnection(this);

                Log("Connected!");
            }
            ~Connection()
            {
                Log("~Connection.");
                Destroy();
            }
            void ReceiveThread()
            {
                string receiveString;

                int count;
                try
                {
                    while (true)
                    {
                        count = stream.Read(buffer, 0, buffer.Length);
                        // Block --------------------------------
                        if (count <= 0)
                        {
                            Log("Disconnected from client.");
                            _Server.CloseConnection(this);
                            return;
                        }
                        receiveString = Encoding.UTF8.GetString(buffer, 0, count);
                        Log($"Receive{client.Client.LocalEndPoint}:{receiveString}");
                        bufferString += receiveString;

                        Match match = PacketCutter.Match(bufferString);
                        while (match.Success)
                        {
                            CallProcess(match.Groups[1].Value, this);
                            bufferString = match.Groups[2].Value;
                            match = PacketCutter.Match(bufferString);
                        }
                    }
                }
                catch (ThreadAbortException)
                {
                    Log("Receive Thread Aborted.");
                }
                catch (SocketException ex)
                {
                    Server.Log(ex);
                    _Server.CloseConnection(this);
                }
                catch (Exception ex)
                {
                    Server.Log(ex);
                    _Server.CloseConnection(this);
                }
            }
        }
    }
}
