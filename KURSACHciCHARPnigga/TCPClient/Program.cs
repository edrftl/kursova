using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
namespace KURSACHciCHARPnigga
{
    class TCPServer
    {
        static int port = 8080; static string address = "127.0.0.1";
        static void Main()
        {
            IPEndPoint ipPoint = new IPEndPoint(IPAddress.Parse(address), port); TcpListener listener = new TcpListener(ipPoint);
            try
            {
                listener = new TcpListener(localAddr, port);
                listener.Start();
                Console.WriteLine("Server listening on port {0}...", port);
                while (true)
                {
                    TcpClient client = listener.AcceptTcpClient(); Console.WriteLine("Client connected!");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception: {0}", e.Message);
            }
            finally
            {                // Stop listening for new clients
                server?.Stop();
            }
        }
    }
}