using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.NetworkInformation;
using System.Net;
using System.Collections;

namespace YGOSharp
{
    public class PortConfig
    {
        public static int GetFirstAvailablePort()
        {
            int MAX_PORT = 25565;        
            int BEGIN_PORT = 10000;
            IList portUsed = PortIsUsed();
            IList portAvailable = new ArrayList();
            Random rd = new Random();
            for (int i = 0; i < 1000; i++)
            {
                int try_port = rd.Next(BEGIN_PORT, MAX_PORT);
                if (PortIsAvailable(try_port))
                {
                    return try_port;
                }
            }
            return -1;
        }

        public static IList PortIsUsed()
        {
            IPGlobalProperties ipGlobalProperties = IPGlobalProperties.GetIPGlobalProperties();

            IPEndPoint[] ipsTCP = ipGlobalProperties.GetActiveTcpListeners();

            IPEndPoint[] ipsUDP = ipGlobalProperties.GetActiveUdpListeners();

            TcpConnectionInformation[] tcpConnInfoArray = ipGlobalProperties.GetActiveTcpConnections();

            IList allPorts = new ArrayList();
            foreach (IPEndPoint ep in ipsTCP) allPorts.Add(ep.Port);
            foreach (IPEndPoint ep in ipsUDP) allPorts.Add(ep.Port);
            foreach (TcpConnectionInformation conn in tcpConnInfoArray) allPorts.Add(conn.LocalEndPoint.Port);

            return allPorts;
        }

        public static bool PortIsAvailable(int port)
        {
            bool isAvailable = true;

            IList portUsed = PortIsUsed();

            foreach (int p in portUsed)
            {
                if (p == port)
                {
                    isAvailable = false; break;
                }
            }

            return isAvailable;
        }
    }
}
