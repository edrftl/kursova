﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace TCPServer
{
    internal class Program
    {
        static int port = 7654;
        static string address = "127.0.0.1";
        static List<TcpClient> clients = new List<TcpClient>();
        static readonly object lockObject = new object();

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

            using (var dbContext = new ServerDB()) // Create a new instance for each client
            {
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

                        string firstMessage = GetWordAtIndex(message, 0);
                        if (firstMessage == "Login")
                        {
                            dbContext.Login(GetWordAtIndex(message, 1), GetWordAtIndex(message, 2));
                            //DisconnectClient(client); // Disconnect after handling Login
                        }
                        else if (firstMessage == "Register")
                        {
                            dbContext.AddPlayer(GetWordAtIndex(message, 1), GetWordAtIndex(message, 2));
                            //DisconnectClient(client); // Disconnect after handling AddPlayer
                        }
                        else
                        {
                            BroadcastMessage(message);
                        }
                    }
                    catch (JsonException ex)
                    {
                        Console.WriteLine($"Error deserializing JSON: {ex.Message}");
                    }
                }
            }
        }

        static void DisconnectClient(TcpClient client)
        {
            lock (lockObject)
            {
                try
                {
                    clients.Remove(client);
                    client.GetStream().Close();
                    client.Close();
                    Console.WriteLine($"Client disconnected: {((IPEndPoint)client.Client.RemoteEndPoint).Address}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error disconnecting client: {ex.Message}");
                }
            }
        }

        public static void BroadcastMessage(string message)
        {
            lock (lockObject)
            {
                foreach (var client in clients)
                {
                    try
                    {
                        NetworkStream clientStream = client.GetStream();
                        StreamWriter sw = new StreamWriter(clientStream);
                        sw.WriteLine(message);
                        sw.Flush();
                        Console.WriteLine($"Broadcasted message : {message}");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Error broadcasting message to client: {ex.Message}");
                    }
                }
            }
        }

        public static string GetWordAtIndex(string input, int index)
        {
            string[] words = input.Split(' ');

            if (index >= 0 && index < words.Length)
            {
                return words[index];
            }
            else
            {
                return string.Empty;
            }
        }
    }

    public class ServerDB : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Data Source=DESKTOP-LONMETH\SQLEXPRESS;
                            Initial Catalog = PlayerLabDB;
                            Integrated Security=True;Connect Timeout=30;
                            Encrypt=False;Trust Server Certificate=False;
                            Application Intent=ReadWrite;
                            Multi Subnet Failover=False");

            //optionsBuilder.UseSqlServer(@"Data Source=DESKTOP-3A1T100\SQLEXPRESS;
            //                Initial Catalog = PlayerLabDB;
            //                Integrated Security=True;Connect Timeout=30;
            //                Encrypt=False;Trust Server Certificate=False;
            //                Application Intent=ReadWrite;
            //                Multi Subnet Failover=False");
        }

        public DbSet<Player> Players { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {


            modelBuilder.Entity<Player>().HasData(

                 new Player { Id = 1, Login = "Admin", Password = "9087654321" }

             ) ;
        }

        public void Login(string name, string password)
        {
            var player = Players.FirstOrDefault(p => p.Login == name);

            if (player != null)
            {
                if (player.Password == password)
                {
                    Console.WriteLine($"Player {name} successfully logged in!");
                    Program.BroadcastMessage("System Logined");
                }
                else
                {
                    Console.WriteLine($"Incorrect password for player {name}.");
                    Program.BroadcastMessage("System IncorrectPassword");
                }
            }
            else
            {
                Console.WriteLine($"Player {name} does not exist.");
                Program.BroadcastMessage("System IncorrectName");
            }
        }


        public void AddPlayer(string name, string password)
        {
            if (!Players.Any(p => p.Login == name))
            {
                var newPlayer = new Player { Login = name, Password = password };
                Players.Add(newPlayer);
                SaveChanges();
                Console.WriteLine($"Player {name} added successfully!");
                Program.BroadcastMessage("System Registered");
            }
            else
            {
                Console.WriteLine($"Player {name} already exists!");
                Program.BroadcastMessage("System : NameIsTaken");
            }
        }
    }

    public class Player
    {
        public int Id { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
    }
}
