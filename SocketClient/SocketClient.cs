using System.Net;
using System.Net.Sockets;
using System.Text;

namespace SocketClient
{
    public class SocketClient
    {
        public void StartClient()
        {
            Console.WriteLine("Input server ip to connect: ");
            IPAddress iPServerAddress = IPAddress.Parse(Console.ReadLine());
            //IPAddress iPServerAddress = IPAddress.Parse("10.233.149.105");
            IPEndPoint serverEndPoint = new(iPServerAddress, 22222);

            Socket sender = new(
                iPServerAddress.AddressFamily,
                SocketType.Stream,
                ProtocolType.Tcp);
            sender.Connect(serverEndPoint);
            Console.WriteLine("Connected to: " + serverEndPoint.ToString());

            while (sender.Connected) CreateMessage(sender);
        }

        void CreateMessage(Socket sender)
        {
            Console.Write("Message:");
            string msg = Console.ReadLine() + "<EOM>";

            byte[] byteArr = Encoding.Unicode.GetBytes(msg);
            sender.Send(byteArr);

            string? returnMsg = ClassLibrary1.Class1.GetMessage(sender);
            Console.WriteLine(returnMsg);
        }
    }
}