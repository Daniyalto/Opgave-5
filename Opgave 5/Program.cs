using System.Net.Sockets;
using System.Net;

namespace Opgave_5
{
    public class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("TCP Server with JSON");

            int port = 1010;
            TcpListener listener = new TcpListener(IPAddress.Any, port);
            listener.Start();

            while (true)
            {

                TcpClient client = listener.AcceptTcpClient();
                Task.Run(() => new ClientHandler(client)); ;

            }

            listener.Stop();
        }
    }
}
