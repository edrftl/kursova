using KURSACHciCHARPnigga;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Text.Json;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace KURSACHciCHARPnigga
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    partial class MainWindow : Window
    {

        MyDB dbContext = new MyDB();
        TcpClient client;
        private ViewModel _viewModel;

        string Address;
        string Port;
        string PlayerName;

        public MainWindow()
        {
            InitializeComponent();
            _viewModel = new ViewModel();
            DataContext = _viewModel;
            MainMenu win = new MainMenu();
            win.Show();
            this.Close();
        }

        public MainWindow(string address, string port, string playerName)
        {
            Address = address; Port = port; PlayerName = playerName;
            InitializeComponent();
            _viewModel = new ViewModel();
            DataContext = _viewModel;

            
        }

        private async void InitializeTcpClientAsync()
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




        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            InitializeTcpClientAsync();
            _viewModel.OurPos = dbContext.getRoom(1);
        }

        private void SendBut_Click(object sender, RoutedEventArgs e)
        {
            string message = $"{_viewModel.OurPos.NameOfRoom}- {PlayerName} : {MessageTB.Text}";
            dbContext.addMessage(_viewModel.OurPos.Id, MessageTB.Text);
            SendMessage(message);
            MessageTB.Text = "";
        }
        public async Task ReceiveMessagesAsync()
        {
            try
            {
                while (true)
                {
                    NetworkStream ns = client.GetStream();
                    StreamReader responseReader = new StreamReader(ns);
                    string response = await responseReader.ReadLineAsync();
                    string wordBeforeHyphen = GetWordBeforeHyphen(response);
                    if (wordBeforeHyphen == _viewModel.OurPos.NameOfRoom)
                    {
                        if (response != null)
                        {
                            Application.Current.Dispatcher.Invoke(() =>
                            {
                                _viewModel.Messages.Add(new Message { Content = response });
                            });
                        }
                    }
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

        private void HandleException(Exception ex)
        {
            // Log the exception or display an error message to the user
            MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        private static string GetWordBeforeHyphen(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
            {
                return string.Empty;
            }

            int hyphenIndex = input.IndexOf('-');

            if (hyphenIndex > 0)
            {
                string beforeHyphen = input.Substring(0, hyphenIndex).Trim();
                return beforeHyphen;
            }

            return string.Empty;
        }

    }
}
