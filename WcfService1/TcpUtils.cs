using System.Net;
using System.Net.Sockets;

namespace ServicioWebNomina
{
    public class TcpUtils
    {
        public static int FreeTcpPort()
        {
            TcpListener l = new TcpListener(IPAddress.Parse("127.0.0.1"), 0);
            l.Start();
            int port = ((IPEndPoint)l.LocalEndpoint).Port;
            l.Stop();
            return port;
        }


        public static string GetIpAddress()
        {
            string localIp
                = null;
            var direcciones = System.Net.Dns.GetHostEntry(Dns.GetHostName());
            foreach (IPAddress ip in direcciones.AddressList)
            {
                if (ip.AddressFamily.ToString() == "InterNetwork")
                {
                    localIp = ip.ToString();
                    break;
                }
            }
            return localIp;
        }

    }
}
