using System.Net;

namespace SocketProgrammingServer
{
    internal class SocketServer
    {
        internal void StartServer()
        {
            IPHostEntry iPHostEntry = Dns.GetHostEntry(
                Dns.GetHostName(), 
                System.Net.Sockets.AddressFamily.InterNetwork
                );
            IPAddress iPAddress = iPHostEntry.AddressList[0];
            //Or convert a string to type of IpAddress
            //IPAddress iPAddress2 = IPAddress.Parse("192.168.1.2");
            IPEndPoint iPEndPoint = new(iPAddress, 22222);
        }

    }
}
