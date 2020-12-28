﻿using System.Net;

namespace GameSystem.Networking
{
    public struct UDPPacket
    {
        public string message;
        public IPEndPoint endPoint;
        public PacketBase GetPacket() => NetworkSystem.StringToPacket(message);
        public UDPPacket(string message, IPEndPoint endPoint)
        {
            this.message = message;
            this.endPoint = endPoint;
        }
    }
}