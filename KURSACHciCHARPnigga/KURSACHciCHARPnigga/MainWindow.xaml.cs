using KURSACHciCHARPnigga;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel;
using System.Text;
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
    public partial class MainWindow : Window
    {
        private ViewModel _viewModel;
        MyDB dbContext = new MyDB();

        public MainWindow()
        {
            InitializeComponent();
            //dbContext = new MyDB();
            _viewModel = new ViewModel(dbContext);
            DataContext = _viewModel;

            // Seed data after initializing the database context
            //dbContext.SeedData();

        }

        


        //private void CreateRooms()
        //{
        //    Room room1 = new Room("Room1 try \n to escape\n labirint");
        //    Room room2 = new Room("Room2");
        //    Room room3 = new Room("Room3");
        //    Room room4 = new Room("Room4");
        //    Room room5 = new Room("Room5");
        //    Room room6 = new Room("Room6");
        //    Room room7 = new Room("Room7");
        //    Room room8 = new Room("Room8");
        //    Room room9 = new Room("Room9");
        //    Room room10 = new Room("Room10");
        //    Room room11 = new Room("Room11");
        //    Room room12 = new Room("Room12");
        //    Room room13 = new Room("Room13");
        //    Room room14 = new Room("Room14");
        //    Room room15 = new Room("Room15");
        //    Room room16 = new Room("Room16");
        //    Room room17 = new Room("Room17");
        //    Room Finish = new Room("You won");

        //    room1.InitializeRoom(room2, room3, room4);
        //    room2.InitializeRoom(room1, room3, room4);
        //    room3.InitializeRoom(room2, room1, room4);
        //    room4.InitializeRoom(room6, room3, room5);
        //    room5.InitializeRoom(room4, room6, room7);
        //    room6.InitializeRoom(room4, room5, room7);
        //    room7.InitializeRoom(room9, room8, room6);
        //    room8.InitializeRoom(room6, room11, room10);
        //    room9.InitializeRoom(room17, room11, room7);
        //    room10.InitializeRoom(room8, room11, room12);
        //    room11.InitializeRoom(room9, room8, room11);
        //    room12.InitializeRoom(room10, room13, room14);
        //    room13.InitializeRoom(room16, room15, room14);
        //    room14.InitializeRoom(room12, room13, room15);
        //    room15.InitializeRoom(room17, room16, room14);
        //    room16.InitializeRoom(room2, room17, room15);
        //    room17.InitializeRoom(room9, Finish, room16);
        //    Finish.InitializeRoom(Finish, Finish, Finish);

        //    rooms.Add(room1);
        //    rooms.Add(room2);
        //    rooms.Add(room3);
        //    rooms.Add(room4);
        //    rooms.Add(room5);
        //    rooms.Add(room6);
        //    rooms.Add(room7);
        //    rooms.Add(room8);
        //    rooms.Add(room9);
        //    rooms.Add(room10);
        //    rooms.Add(room11);
        //    rooms.Add(room12);
        //    rooms.Add(room13);
        //    rooms.Add(room14);
        //    rooms.Add(room15);
        //    rooms.Add(room16);
        //    rooms.Add(room17);
        //    rooms.Add(Finish);
        //}


        //private void Window_Loaded(object sender, RoutedEventArgs e)
        //{
        //    //CreateRooms();
        //    //_viewModel.OurPos = rooms[0];
        //}

    }
}