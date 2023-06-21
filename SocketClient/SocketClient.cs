using System.Diagnostics;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace SocketClient
{
    internal class SocketClient
    {
        string crows;
        public SocketClient()
        {
            crows = CountingCrows();
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
            
            Stopwatch stopwatch = new();
            stopwatch.Start();
            int counter = 0;
            while (counter++ < 100) CreateMessage(sender);
            stopwatch.Stop();
            Console.WriteLine(stopwatch.Elapsed);
            Console.ReadKey();
        }


        void CreateMessage(Socket sender)
        {
            Console.Write("Message:");
            //Test1 
            string msg = CountingCrows() + "<EOM>";
            //string msg = Console.ReadLine() + "<EOM>";

            byte[] byteArr = Encoding.Unicode.GetBytes(msg);
            sender.Send(byteArr);

            string? returnMsg = ClassLibrary1.Class1.GetMessage(sender);
            Console.WriteLine(returnMsg);
        }


        string CountingCrows()
        {
            string str = "";
            for (int i = 0; i < 10000; i++)
            {
                str += i;
            }
            return str;
        }
    }
}