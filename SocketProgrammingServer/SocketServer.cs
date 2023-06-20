using System.Net;
using System.Net.Sockets;
using System.Text;

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
            int choice = ChooseIpAddress(iPHostEntry.AddressList);
            IPAddress iPAddress = iPHostEntry.AddressList[choice];
            //Or convert a string to type of IpAddress
            //IPAddress iPAddress2 = IPAddress.Parse("192.168.1.2");

            IPEndPoint iPEndPoint = new(iPAddress, 22222);

            Socket listener = new(
                iPAddress.AddressFamily, 
                SocketType.Stream, 
                ProtocolType.Tcp);

            listener.Bind(iPEndPoint);
            listener.Listen(10);

            Socket handler = listener.Accept();

            string data = null;
            byte[] bytes = null;

            while (true)
            {
                bytes = new byte[4096];
                int bytesRec = handler.Receive(bytes);
                data += Encoding.ASCII.GetString(bytes, 0, bytesRec);
                if (data.Contains("<EOM>")) break;
            }

            Console.WriteLine(data);
        }

        /// <summary>
        /// Selects IP from ip address array
        /// </summary>
        /// <param name="addressList"></param>
        /// <returns>integer on array</returns>
        private int ChooseIpAddress(IPAddress[] addressList)
        {
            int i = 0;
            foreach (var item in addressList)
                Console.WriteLine($"[{i++}] {item}");

            int j;
            do { Console.Write("Input number of Ipaddress: "); }
            while (!int.TryParse(Console.ReadLine(), out j)
                || j < 0
                || j >= addressList.Length);
            return j;
        }
    }
}
