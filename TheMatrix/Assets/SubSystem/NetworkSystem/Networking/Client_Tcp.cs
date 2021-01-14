using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Text.RegularExpressions;

namespace GameSystem.Networking
{
    public partial class Client
    {
        // TCP
        // interface
        public string NetId { get; private set; }
        public bool IsConnected => client != null && client.Connected;

        /// <summary>
        /// Start connecting to given IP address (LoaclIPAddress & ServerIPAddress should be set)
        /// </summary>
        public void StartTCPConnecting()
        {
            if (client != null)
            {
                Log("Connection already existed!");
                return;
            }
            while (true)
            {
                try
                {
                    client = new TcpClient(new IPEndPoint(LocalIPAddress, port));
                    break;
                }
                catch (SocketException ex)
                {
                    Log(ex);
                    if (ex.SocketErrorCode == SocketError.AddressAlreadyInUse)
                    {
                        // Port already in use. Retry with a new port
                        port = CallNewValidPort();
                    }
                    else throw ex;
                }
                catch (Exception ex)
                {
                    Log(ex);
                    client.Close();
                    client = null;
                    throw ex;
                }
            }
            connectThread = new Thread(ConnectThread);
            connectThread.Start();
        }
        public void StopTCPConnecting()
        {
            if (IsConnected) CallDisconnection();
            else if (client == null)
            {
                Log("Connection already closed!");
                return;
            }
            connectThread?.Abort();
            receiveThread?.Abort();
            stream?.Close();
            client?.Close();
            client = null;
        }
        /// <summary>
        /// Send message to server
        /// </summary>
        public void Send(string message)
        {
            if (stream == null || !stream.CanWrite)
            {
                Log("Sending failed.");
                return;
            }
            byte[] messageBytes = Encoding.UTF8.GetBytes(message + DivisionMark);
            stream.Write(messageBytes, 0, messageBytes.Length);
            Log("Send: " + message);
        }


        // inner code
        TcpClient client;
        int port;
        Thread connectThread;
        NetworkStream stream;
        Thread receiveThread;
        string bufferString = "";
        readonly byte[] buffer = new byte[MaxMsgLength];

        void ConnectThread()
        {
            do
            {
                Log($"Connecting……|ip:{LocalIPAddress}:{port}|remote:{ServerIPAddress}:{ServerTCPPort}");
                try
                {
                    client.Connect(new IPEndPoint(ServerIPAddress, ServerTCPPort));
                    // Block --------------------------------
                }
                catch (SocketException ex)
                {
                    Log(ex);
                    Log("Connection failed! Reconnecting……:" + port);
                    if (ex.SocketErrorCode == SocketError.AddressAlreadyInUse)
                    {
                        while (true)
                        {
                            client?.Close();
                            port = CallNewValidPort();
                            Log("Reconnecting with a new port:" + port);
                            try
                            {
                                client = new TcpClient(new IPEndPoint(LocalIPAddress, port));
                                break;
                            }
                            catch (SocketException exx)
                            {
                                if (exx.SocketErrorCode == SocketError.AddressAlreadyInUse) continue;
                            }
                        }
                    }
                    else if (ex.SocketErrorCode == SocketError.AddressNotAvailable)
                    {
                        client.Close();
                        client = null;
                        return;
                    }
                    Thread.Sleep(1000);
                    continue;
                }
                catch (ThreadAbortException)
                {
                    Log("Connect Thread Aborted.");
                    return;
                }
                catch (Exception ex)
                {
                    Log(ex);
                    client.Close();
                    client = null;
                    return;
                }
            } while (!client.Connected);

            Log("Connected!");
            stream = client.GetStream();
            receiveThread = new Thread(ReceiveThread);
            receiveThread.Start();
        }
        void ReceiveThread()
        {
            string receiveString;

            int count;
            count = stream.Read(buffer, 0, buffer.Length);
            // Block --------------------------------
            if (count <= 0)
            {
                Log("Disconnected from server!");
                stream?.Close();
                client?.Close();
                client = null;
                return;
            }
            receiveString = Encoding.UTF8.GetString(buffer, 0, count);
            NetId = receiveString.Substring(0, receiveString.Length - 1);
            CallConnection();

            try
            {
                while (true)
                {
                    count = stream.Read(buffer, 0, buffer.Length);
                    // Block --------------------------------
                    if (count <= 0)
                    {
                        Log("Disconnected from server!");
                        stream?.Close();
                        client?.Close();
                        client = null;
                        CallDisconnection();
                        return;
                    }
                    receiveString = Encoding.UTF8.GetString(buffer, 0, count);
                    Log($"Receive{client.Client.LocalEndPoint}:{receiveString}");
                    bufferString += receiveString;

                    Match match = PacketCutter.Match(bufferString);
                    while (match.Success)
                    {
                        CallReceive(match.Groups[1].Value);
                        bufferString = match.Groups[2].Value;
                        match = PacketCutter.Match(bufferString);
                    }
                }
            }
            catch (ThreadAbortException)
            {
                Log("Receive Thread Aborted.");
            }
            catch (Exception ex)
            {
                Log(ex);
                stream?.Close();
                client?.Close();
                client = null;
                CallDisconnection();
                return;
            }
        }
    }
}
