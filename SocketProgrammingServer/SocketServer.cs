using System.Net;
using System.Net.Sockets;
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
        internal void StartServer()
        {
            #region Endpoint Creation
            IPHostEntry iPHostEntry = Dns.GetHostEntry(Dns.GetHostName(), AddressFamily.InterNetwork);
            int choice = ChooseIpAddress(iPHostEntry.AddressList);
            IPAddress iPAddress = iPHostEntry.AddressList[choice];
            //Or convert a string to type of IpAddress
            //IPAddress iPAddress2 = IPAddress.Parse("192.168.1.2");

            IPEndPoint iPEndPoint = new(iPAddress, 22222);
            #endregion
            Socket listener = new(
                iPAddress.AddressFamily,
                SocketType.Stream,
                ProtocolType.Tcp);

            listener.Bind(iPEndPoint);
            listener.Listen(10);
            Console.WriteLine("Server listens on " + iPEndPoint);

            while (true)
            {
                Socket handler = listener.Accept();
                Thread thread = new(new ThreadStart(() => ConnectToClient(handler)));
                thread.Start();
                //ConnectToClient(handler);
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
            while (true)
            {
                string? data = GetMessage(handler);
                byte[] returnMsg = Encoding.ASCII.GetBytes("Server received msg<EOM>");
                handler.Send(returnMsg);
                //handler.Shutdown(SocketShutdown.Both);
                //handler.Close();

                Console.WriteLine(data);
            }
        }

        /// <summary>
        /// Recieves message from client
        /// </summary>
        /// <param name="socket"></param>
        /// <returns></returns>
        string GetMessage(Socket socket)
        {
            string? data = null;
            byte[] bytes;

            while (true)
            {
                bytes = new byte[4096];
                int bytesRec = socket.Receive(bytes);
                data += Encoding.ASCII.GetString(bytes, 0, bytesRec);
                if (data.Contains("<EOM>")) break;
            }

            return data;
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
