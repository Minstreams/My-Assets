using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace GameSystem.Networking
{
    public partial class Server
    {
        // TCP
        /// <summary>
        /// net id of host connection
        /// </summary>
        const string hostId = "0";

        // interface
        public Connection GetConnection(int index) => connections[index];
        public bool TcpOn => listener != null && listener.Pending();
        public void OpenTCP()
        {
            if (listener != null)
            {
                Log("TCP already opened!");
                return;
            }
            listener = new TcpListener(new IPEndPoint(NetworkSystem.LocalIPAddress, Setting.serverTCPPort));
            listenThread = new Thread(ListenThread);
            listenThread.Start();
        }
        public void CloseTCP()
        {
            if (listener == null)
            {
                Log("TCP already closed!");
                return;
            }
            listenThread?.Abort();
            listener?.Stop();
            listener = null;
        }
        public void DisconnectAll()
        {
            connections?.ForEach(conn => { conn.Destroy(); });
            connections?.Clear();
            Log("All connections closed!");
        }
        public void CloseConnection(Connection conn)
        {
            conn.Destroy();
            connections.Remove(conn);
        }
        public void Broadcast(string message) => connections.ForEach(conn => conn.Send(message));

        // inner code
        readonly List<Connection> connections = new List<Connection>();
        TcpListener listener;
        Thread listenThread;

        void ListenThread()
        {
            try
            {
                listener.Start();
                while (true)
                {
                    Log("Listening……:" + Setting.serverTCPPort);
                    var client = listener.AcceptTcpClient();
                    // Block --------------------------------
                    connections.Add(new Connection(client, NewTcpId(client.Client.RemoteEndPoint as IPEndPoint)));
                }
            }
            catch (ThreadAbortException)
            {
                Log("Listen Thread Aborted");
            }
            catch (Exception ex)
            {
                Log(ex);
                listener?.Stop();
                listener = null;
                return;
            }
        }
        string NewTcpId(IPEndPoint ip)
        {
            if (ip.Address.Equals(NetworkSystem.LocalIPAddress) && ip.Port.Equals(NetworkSystem.LocalIPPort)) return hostId;
            // generate a new net id
            int output = 1;
            foreach (Connection conn in connections)
            {
                int id = int.Parse(conn.NetId);
                if (output <= id) output = id + 1;
            }
            return output.ToString();
        }
    }
}