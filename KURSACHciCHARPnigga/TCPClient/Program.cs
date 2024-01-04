using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Text.Json;
namespace TCPClient
{
    internal class Program
    {
        static int port = 8081;
        static string address = "127.0.0.1";
        static void Main(string[] args)
        {
            IPEndPoint ipPoint = new IPEndPoint(IPAddress.Parse(address), port);
            TcpListener listener = new TcpListener(ipPoint);
            //AutoNum autoNum = new AutoNum();


            listener.Start(10);
            Console.WriteLine("Server started! Waiting for connection...");

            while (true)
            {
                TcpClient client = listener.AcceptTcpClient();

                WorkWithClient(client);

            }
        }

        static void WorkWithClient(TcpClient client)
        {
            Task.Run(() =>
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

                        // Deserialize the JSON string to a string
                        string message = JsonSerializer.Deserialize<string>(jsonRequest);
                        Console.WriteLine(message);
                        //Console.WriteLine("Received auto number: " + autoNumber);

                        // Process the data (e.g., get city information)
                        //string city = autoNum.GetCity(autoNumber);

                        // Use StreamWriter for writing the response
                        StreamWriter sw = new StreamWriter(ns);
                        sw.WriteLine(message);
                        sw.Flush(); // clear buffer

                        //Console.WriteLine("Sent city response: " + city);
                    }
                    catch (JsonException ex)
                    {
                        Console.WriteLine($"Error deserializing JSON: {ex.Message}");
                    }
                }
            });
        }
    }
}