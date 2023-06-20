using System.Net;
using System.Net.Sockets;
using System.Text;

namespace SocketClient
{
    internal class SocketClient
    {
        public SocketClient()
        {
            StartClient();
        }

        void StartClient()
        {
            IPAddress iPServerAddress = IPAddress.Parse("192.168.1.2");
            IPEndPoint serverEndPoint = new(iPServerAddress, 22222);

            Socket sender = new(
                iPServerAddress.AddressFamily,
                SocketType.Stream,
                ProtocolType.Tcp);
            sender.Connect(serverEndPoint);
            Console.WriteLine("Connected to: " + serverEndPoint.ToString());

            while(true) CreateMessage(sender);
        }

        void CreateMessage(Socket sender)
        {
            Console.Write("Message:");
            string msg = Console.ReadLine() + "<EOM>";

            byte[] byteArr = Encoding.ASCII.GetBytes(msg);
            sender.Send(byteArr);

            string returnMsg = GetMessage(sender);
            Console.WriteLine(returnMsg);
        }

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
    }
}
