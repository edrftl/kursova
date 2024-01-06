using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace TCPServer
{
    internal class Program
    {
        static int port = 2228;
        static string address = "127.0.0.1";
        static List<TcpClient> clients = new List<TcpClient>();

        static void Main(string[] args)
        {
            IPEndPoint ipPoint = new IPEndPoint(IPAddress.Parse(address), port);
            TcpListener listener = new TcpListener(ipPoint);

            listener.Start(10);
            Console.WriteLine("Server started! Waiting for connection...");

            while (true)
            {
                TcpClient client = listener.AcceptTcpClient();
                clients.Add(client);

                Task.Run(() => WorkWithClient(client));
            }
        }

        static void WorkWithClient(TcpClient client)
        {
            Console.WriteLine($"New client connected: {((IPEndPoint)client.Client.RemoteEndPoint).Address}");

            NetworkStream ns = client.GetStream();

            while (true)
            {
                StreamReader sr = new StreamReader(ns);

                // Read the incoming data from the client
                string jsonRequest = sr.ReadLine();

                try
                {
                    string message = JsonSerializer.Deserialize<string>(jsonRequest);
                    Console.WriteLine(message);

                    BroadcastMessage(message);
                }
                catch (JsonException ex)
                {
                    Console.WriteLine($"Error deserializing JSON: {ex.Message}");
                }
            }
        }

        static void BroadcastMessage(string message)
        {
            foreach (var client in clients)
            {
                try
                {
                    NetworkStream clientStream = client.GetStream();
                    StreamWriter sw = new StreamWriter(clientStream);
                    sw.WriteLine(message);
                    sw.Flush(); // clear buffer
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error broadcasting message to client: {ex.Message}");
                }
            }
        }
    }
}
