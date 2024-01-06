using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
        public MainMenu()
        {
            InitializeComponent();
        }
        private void PlayB_Click(object sender, RoutedEventArgs e)
        {
            string serverIp = ServerIPTB.Text;
            string playerName = NameTB.Text;

            MainWindow window = new MainWindow(serverIp, "2228", playerName);
            //127.0.0.1
            window.Show();
            this.Close();
        }
    }
}
