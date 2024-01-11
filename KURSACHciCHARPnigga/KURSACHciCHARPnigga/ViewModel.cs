﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using static Azure.Core.HttpHeader;
using System.Security.Permissions;

namespace KURSACHciCHARPnigga
{
    public class ViewModel : INotifyPropertyChanged
    {

        MyDB dbContext = new MyDB();
        TcpClient client;

        const int Port = 7654;
        const string ServetIP = "127.0.0.1";


        private Room _ourPos;

        public Room OurPos
        {
            get { return _ourPos; }
            set
            {
                if (_ourPos != value)
                {
                    _ourPos = value;
                    OnPropertyChanged(nameof(OurPos));
                    OnPropertyChanged(nameof(PlaceName));

                    // Update messages when OurPos changes
                    App.Current.Dispatcher.Invoke(() =>
                    {
                        Messages = new ObservableCollection<Message>(_ourPos.Messages.ToList());
                    });
                }
            }
        }
        private ObservableCollection<Message> _messages;

        public ObservableCollection<Message> Messages
        {
            get { return _messages; }
            set
            {
                _messages = value;
                OnPropertyChanged(nameof(Messages));
            }
        }

        RelayCommand firstButtonCommand;
        RelayCommand secondButtonCommand;
        RelayCommand thirdButtonCommand;

        RelayCommand registerButtonCommand;
        RelayCommand loginButtonCommand;

        RelayCommand sendButtonCommand;
        public ICommand firstCommand => firstButtonCommand;
        public ICommand secondCommand => secondButtonCommand;
        public ICommand thirdCommand => thirdButtonCommand;

        /// ///////
        public ICommand registerCommand => registerButtonCommand;
        public ICommand loginCommand => loginButtonCommand;

        /// ///////
        public ICommand sendCommand => sendButtonCommand;
        
        public string ServerIp { get; set; }
        public string PlayerName { get; set; }
        public string PlayerPassword { get; set; }

        public ViewModel()
        {
            OurPos = dbContext.getRoom(1); // Set the initial room

            // Load messages for the initial room
            Messages = new ObservableCollection<Message>(OurPos.Messages.ToList());

            firstButtonCommand = new RelayCommand((o) => { MoveTo(1); },
                    (o) => firstCommand != null);
            secondButtonCommand = new RelayCommand((o) => { MoveTo(2); },
                    (o) => secondCommand != null);
            thirdButtonCommand = new RelayCommand((o) => { MoveTo(3); },
                   (o) => thirdCommand != null);
            registerButtonCommand = new RelayCommand((o) => RegisterBtn());
            loginButtonCommand = new RelayCommand((o) => LoginBtn());

            sendButtonCommand = new RelayCommand((o) => SendBtn());
        }

        public void MoveTo(int num)
        {
            if (OurPos != null)
            {
                Room newRoom = null;

                switch (num)
                {
                    case 1:
                        newRoom = dbContext.getRoom((int)OurPos?.FirstRoomId);
                        break;
                    case 2:
                        newRoom = dbContext.getRoom((int)OurPos?.SecondRoomId);
                        break;
                    case 3:
                        newRoom = dbContext.getRoom((int)OurPos?.ThirdRoomId);
                        break;
                    default:
                        break;
                }

                if (newRoom != null)
                {
                    OurPos = newRoom;
                }
            }
        }


        public string MessageText {  get; set; }

        private void SendBtn()
        {
            string message = $"{OurPos.NameOfRoom}- {PlayerName} : {MessageText}";
            dbContext.addMessage(OurPos.Id, MessageText);
            SendMessage(message);
            MessageText = "";
        }





        private void RegisterBtn()
        {
            InitializeTcpClientAsync(ServerIp, Port);

            SendMessage($"Register {PlayerName} {PlayerPassword}");
        }

        private void LoginBtn()
        {
            InitializeTcpClientAsync(ServerIp, Port);

            SendMessage($"Login {PlayerName} {PlayerPassword}");
        }



        public async void InitializeTcpClientAsync(string Address, int Port)
        {
            try
            {
                if (string.IsNullOrEmpty(Address) || !IPAddress.TryParse(Address, out IPAddress ipAddress))
                {
                    //MessageBox.Show("Invalid or empty IP address");
                    //MainMenu win = new MainMenu();
                    //win.Show();
                    //this.Close();
                    //Application.Current.MainWindow?.Close();
                    return; // Exit the method if the address is invalid
                }
                IPEndPoint ipPoint = new IPEndPoint(IPAddress.Parse(Address), Port);
                client = new TcpClient();
                await client.ConnectAsync(ipPoint.Address, ipPoint.Port);

                await ReceiveMessagesAsync();



            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
                MainMenu win = new MainMenu();
                win.Show();
                Application.Current.MainWindow?.Close();

            }
        }

        public async Task ReceiveMessagesAsync()
        {
            try
            {
                while (true)
                {

                    await Task.Run(() =>
                    {
                        // Inside Task.Run, avoid async/await to prevent issues with disposal
                        using (NetworkStream ns = client.GetStream())
                        using (StreamReader responseReader = new StreamReader(ns))
                        {
                            while (!responseReader.EndOfStream)
                            {
                                string response = responseReader.ReadLine();
                                string GetWord = GetWordAtIndex(response, 0);

                                if (GetWord == "System")
                                {
                                    GetWord = GetWordAtIndex(response, 1);

                                    if (GetWord == "Logined")
                                    {
                                        Application.Current.Dispatcher.Invoke(() => Login());
                                    }
                                    else if (GetWord == "IncorrectPassword")
                                    {
                                        Application.Current.Dispatcher.Invoke(() => MessageBox.Show("Incorrect password"));
                                    }
                                    else if (GetWord == "IncorrectName")
                                    {
                                        Application.Current.Dispatcher.Invoke(() => MessageBox.Show("Incorrect name"));
                                    }
                                    if (GetWord == "Registered")
                                    {
                                        Application.Current.Dispatcher.Invoke(() => Login());
                                    }
                                    else if (GetWord == "NameIsTaken")
                                    {
                                        Application.Current.Dispatcher.Invoke(() => MessageBox.Show("Name Is Taken"));
                                    }
                                }
                            }
                        }
                    });

                    await Task.Delay(100);
                }
            }
            catch (IOException ex)
            {
                // Handle IOException
                // MessageBox.Show("WTF");
            }
            catch (ObjectDisposedException ex)
            {
                // Handle ObjectDisposedException
                // MessageBox.Show("NetworkStream disposed");
            }
            catch (Exception ex)
            {
                // Handle other exceptions
                // MessageBox.Show("An unexpected error occurred: " + ex.Message);
            }
        }


        //public async Task ReceiveMessagesAsync()
        //{
        //    try
        //    {
        //        while (true)
        //        {
        //            await Task.Run(() =>
        //            {
        //                // Inside Task.Run, avoid async/await to prevent issues with disposal
        //                NetworkStream ns = client.GetStream();
        //                StreamReader responseReader = new StreamReader(ns);

        //                while (!responseReader.EndOfStream)
        //                {
        //                    string response = responseReader.ReadLine();
        //                    string GetWord = GetWordAtIndex(response, 0);

        //                    if (GetWord == "System")
        //                    {
        //                        GetWord = GetWordAtIndex(response, 1);

        //                        if (GetWord == "Logined")
        //                        {
        //                            Application.Current.Dispatcher.Invoke(() => Login());
        //                        }
        //                        else if (GetWord == "IncorrectPassword")
        //                        {
        //                            Application.Current.Dispatcher.Invoke(() => MessageBox.Show("Incorrect password"));
        //                        }
        //                        else if (GetWord == "IncorrectName")
        //                        {
        //                            Application.Current.Dispatcher.Invoke(() => MessageBox.Show("Incorrect name"));
        //                        }
        //                        if (GetWord == "Registered")
        //                        {
        //                            Application.Current.Dispatcher.Invoke(() => Login());
        //                        }
        //                        else if (GetWord == "NameIsTaken")
        //                        {
        //                            Application.Current.Dispatcher.Invoke(() => MessageBox.Show("Name Is Taken"));
        //                        }
        //                    }
        //                }
        //            });

        //            await Task.Delay(100);
        //        }
        //    }
        //    catch (IOException ex)
        //    {
        //        // Handle IOException
        //        //MessageBox.Show("WTF");
        //    }
        //    catch (ObjectDisposedException ex)
        //    {
        //        // Handle ObjectDisposedException
        //        //MessageBox.Show("NetworkStream disposed");
        //    }
        //    catch (Exception ex)
        //    {
        //        // Handle other exceptions
        //        //MessageBox.Show("An unexpected error occurred: " + ex.Message);
        //    }
        //}

        private void Login()
        {
           
            Disconnect();


            MainWindow window = new MainWindow(ServerIp, Port, PlayerName);
            window.Show();

            // Закриття вікна MainMenu
            Application.Current.MainWindow?.Close();



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
            //try
            //{
                NetworkStream ns = client.GetStream();

                // Serialize the string to JSON and send it to the server
                string jsonRequest = JsonSerializer.Serialize(Message);
                StreamWriter sw = new StreamWriter(ns);
                sw.WriteLine(jsonRequest);
                sw.Flush();

                //Message = "";
            //}
            //catch (Exception ex)
            //{
            //    HandleException(ex);
            //}
        }
        public string PlaceName => OurPos?.NameOfRoom;

        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}
