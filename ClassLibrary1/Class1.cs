using System.Net.Sockets;
using System.Text;

namespace ClassLibrary1
{
    public class Class1
    {
        public static string? GetMessage(Socket socket)
        {
            string? data = null;
            while (socket.Connected && (data == null || !data.Contains("<EOM>")))
            {
                try
                {
                    byte[] bytes = new byte[4096];
                    int bytesRec = socket.Receive(bytes);
                    data += Encoding.Unicode.GetString(bytes, 0, bytesRec);
                    Console.WriteLine("Bytes recieved: "+bytesRec);
                }
                catch (Exception)
                {
                    Console.WriteLine("Connection suddenly lost!");
                    return null;
                }

            }
            return data;
        }
    }
}