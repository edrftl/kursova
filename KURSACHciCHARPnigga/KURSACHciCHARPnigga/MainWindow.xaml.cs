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
    partial class MainWindow : Window
    {

        MyDB dbContext = new MyDB();
        //
        private ViewModel _viewModel;
        public MainWindow()
        {
            InitializeComponent();
            _viewModel = new ViewModel();
            DataContext = _viewModel;
            //this.DataContext = dbContext;
            //Room room = dbContext.getRoom(8);
            //MessageBox.Show(room.NameOfRoom);

        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            _viewModel.OurPos = dbContext.getRoom(1);
        }
    }
}
