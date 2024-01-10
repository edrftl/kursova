using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using static Azure.Core.HttpHeader;

namespace KURSACHciCHARPnigga
{
    /// <summary>
    /// Interaction logic for MainMenu.xaml
    /// </summary>
    public partial class MainMenu : Window
    {
        TcpClient client;
        const string Port = "7654";
        public MainMenu()
        {
            InitializeComponent();
        }

        private void RegisterB_Click(object sender, RoutedEventArgs e)
        {
            string serverIp = ServerIPTB.Text;
            string playerName = NameTB.Text;
            string playerPassword = PasswordTB.Text;

            InitializeTcpClientAsync(serverIp, Port);

            SendMessage($"Register {playerName} {playerPassword}");
        }

        private void LoginB_Click(object sender, RoutedEventArgs e)
        {
            string serverIp = ServerIPTB.Text;
            string playerName = NameTB.Text;
            string playerPassword = PasswordTB.Text;

            InitializeTcpClientAsync(serverIp, Port);

            SendMessage($"Login {playerName} {playerPassword}");
        }



        private async void InitializeTcpClientAsync(string Address, string Port)
        {
            try
            {
                IPEndPoint ipPoint = new IPEndPoint(IPAddress.Parse(Address), int.Parse(Port));
                client = new TcpClient();
                await client.ConnectAsync(ipPoint.Address, ipPoint.Port);

                await ReceiveMessagesAsync();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
                MainMenu win = new MainMenu();
                win.Show();
                this.Close();
            }
        }

        public async Task ReceiveMessagesAsync()
        {
            try
            {
                while (true)
                {
                    await Task.Run(async () =>
                    {
                        NetworkStream ns = client.GetStream();
                        StreamReader responseReader = new StreamReader(ns);

                        while (!responseReader.EndOfStream)
                        {
                            string response = await responseReader.ReadLineAsync();
                            string GetWord = GetWordAtIndex(response, 0);

                            if (GetWord == "System")
                            {
                                GetWord = GetWordAtIndex(response, 1);

                                if (GetWord == "Logined")
                                {
                                    Login();
                                }
                                else if (GetWord == "IncorrectPassword")
                                {
                                    MessageBox.Show("Incorrect password");
                                }
                                else if (GetWord == "IncorrectName")
                                {
                                    MessageBox.Show("Incorrect name");
                                }
                                if (GetWord == "Registered")
                                {
                                    Login();
                                }
                                else if (GetWord == "NameIsTaken")
                                {
                                    MessageBox.Show("Name Is Taken");
                                }
                            }
                        }
                    });

                    await Task.Delay(100);
                }
            }
            catch (IOException ex)
            {
                MessageBox.Show("WTF");
            }
            catch (Exception ex)
            {
                HandleException(ex);
            }
        }

        private void Login()
        {
            string serverIp = ServerIPTB.Text;
            string playerName = NameTB.Text;
            Disconnect();

            MainWindow window = new MainWindow(serverIp, Port, playerName);
            //127.0.0.1
            window.Show();
            this.Close();
        }
        private void Disconnect()
        {
            try
            {
                if (client != null && client.Connected)
                {
                    client.GetStream().Close();
                    client.Close();
                }
            }
            catch (Exception ex)
            {
                HandleException(ex);
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
        private void HandleException(Exception ex)
        {
            MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        }
        public void SendMessage(string Message)
        {
            try
            {
                NetworkStream ns = client.GetStream();

                // Serialize the string to JSON and send it to the server
                string jsonRequest = JsonSerializer.Serialize(Message);
                StreamWriter sw = new StreamWriter(ns);
                sw.WriteLine(jsonRequest);
                sw.Flush();

                Message = "";
            }
            catch (Exception ex)
            {
                HandleException(ex);
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

        }
    }
}
