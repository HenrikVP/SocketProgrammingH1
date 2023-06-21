using System.Net;
using System.Net.Sockets;
using System.Reflection;
using System.Text;

namespace SocketProgrammingServer
{
    internal class SocketServer
    {
        public SocketServer()
        {
            //new MultiThreading().StartThreading();
            StartServer();
        }
        void StartServer()
        {
            #region Endpoint Creation
            //Creates an endpoint by selecting the PC's hostname and getting the 
            //ipv4 network interfaces ip address list
            IPHostEntry iPHostEntry = Dns.GetHostEntry(Dns.GetHostName(), AddressFamily.InterNetwork);
            int choice = ChooseIpAddress(iPHostEntry.AddressList);
            IPAddress iPAddress = iPHostEntry.AddressList[choice];
            //Or convert a string to type of IpAddress
            //IPAddress iPAddress2 = IPAddress.Parse("192.168.1.2");

            IPEndPoint iPEndPoint = new(iPAddress, 22222);
            #endregion
            //Creates socket that only listens and binds it with the server endpoint
            Socket listener = new(
                iPAddress.AddressFamily,
                SocketType.Stream,
                ProtocolType.Tcp);

            listener.Bind(iPEndPoint);
            listener.Listen(10);
            Console.WriteLine("Server listens on " + iPEndPoint);

            while (true)
            {
                //If a connection have been made with the listener socket,
                //a new thread is created that handles the traffic, therefore not
                //blocking 
                Socket handler = listener.Accept();
                Thread thread = new(new ThreadStart(() => ConnectToClient(handler)));
                thread.Start();
            }
        }

        /// <summary>
        /// Accepts connection from client, Recieves message 
        /// and returns message to client
        /// </summary>
        /// <param name="listener"></param>
        void ConnectToClient(Socket handler)
        {
            Console.WriteLine("Connect to: " + handler.RemoteEndPoint);
            while (handler.Connected)
            {
                //This method will be a thread by itself, and keeps the 
                //connection open with the client socket, thus being
                //able to recieve messages without interuption.
                string? data = ClassLibrary1.Class1.GetMessage(handler);
                byte[] returnMsg = Encoding.Unicode.GetBytes("Server received msg<EOM>");
                if (handler.Connected) handler.Send(returnMsg);
                Console.WriteLine(data + $" ({handler.RemoteEndPoint})");
            }
            Console.WriteLine("Disconnected with client " + handler.RemoteEndPoint);
        }

        /// <summary>
        /// Selects IP from ip address array
        /// </summary>
        /// <param name="addressList"></param>
        /// <returns>integer on array</returns>
        int ChooseIpAddress(IPAddress[] addressList)
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
