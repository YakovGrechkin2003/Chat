using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Client
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            //client.Connect(host, port);
            //stream = client.GetStream();
             string userName;
             const string host = "127.0.0.1";
             const int port = 8888;
             TcpClient client;
             NetworkStream stream;
        }

        Authorization autorization = new Authorization();
        Thread listenThread;

        //----------------------------------------------------------------------------
        private void UserNameGet()
        {

        }
        //----------------------------------------------------------------------------
        private void Button_Autoriz_Click(object sender, RoutedEventArgs e)
        {
            autorization.Show();
            Button_Autoriz.IsEnabled = false;
        }
    }
}
