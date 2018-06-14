using System;
using System.Net;
using System.Net.Sockets;
using System.Collections;

namespace YGOSharp
{
    public class PortConfig
    {
        public static int GetFirstAvailablePort()
        {
            int MAX_PORT = 25565;
            int BEGIN_PORT = 10000;
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

        public static bool PortIsAvailable(int port)
        {
            bool isAvailable = true;

            try
            {
                TcpListener test_listener = new TcpListener(IPAddress.Any, port);
                test_listener.Start();
                test_listener.Stop();
            }
            catch (Exception)
            {
                isAvailable = false;
            }

            return isAvailable;
        }
    }
}
