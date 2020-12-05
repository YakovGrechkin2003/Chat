using System;
using System.Collections.Generic;
using System.IO;
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
        }

        public static NetworkStream stream;
        public static TcpClient client = new TcpClient();
        Authorization autorization = new Authorization();
        Thread listenThread;
        string userName;
        public string message;
        const string host = "127.0.0.1";
        const int port = 8888;

        //----------------------------------------------------------------------------
        private void UserNameGet(string temp)
        {
            string path = @"D:\All\ClientName.txt";
            using (StreamReader s = new StreamReader(path))
            {
                if(s.ReadLine() == "")
                {
                    MessageBox.Show("ERROR");
                    Disconnect();
                }
                else
                {
                    temp = s.ReadLine();
                    message = temp;
                    MessageBox.Show(message);
                    ListBox_Message.Items.Add(temp);
                }
            }
        }
        //----------------------------------------------------------------------------
        private void Button_Autoriz_Click(object sender, RoutedEventArgs e)
        {
            autorization.Show();
            Button_Autoriz.IsEnabled = false;
            Button_SendMessage.IsEnabled = true;
            Button_Connect.IsEnabled = true;
        }

        private void Button_Connect_Click(object sender, RoutedEventArgs e)
        {

            client = new TcpClient();
            try
            {
                client.Connect(host, port); //подключение клиента
                stream = client.GetStream(); // получаем поток

                message = userName;
                byte[] data = Encoding.Unicode.GetBytes(message);
                stream.Write(data, 0, data.Length);

                // запускаем новый поток для получения данных
                Thread receiveThread = new Thread(new ThreadStart(ReceiveMessage));
                receiveThread.Start(); //старт потока
                ListBox_Message.Items.Add($"добро пожаловать, {userName}");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                Disconnect();
            }
        }

        private void button_SendMessage_Click(object sender, RoutedEventArgs e)
        {
            if(TextBox_Message.Text.ToString() != "")
            {
                message = TextBox_Message.Text.ToString();
                byte[] data = Encoding.Unicode.GetBytes(message);
                stream.Write(data, 0, data.Length);
            }
            else
            {
                MessageBox.Show("ERROR");
                Disconnect();
            }
        }

        // получение сообщений
        void ReceiveMessage()
        {
            while (true)
            {
                try
                {
                    byte[] data = new byte[64]; // буфер для получаемых данных
                    StringBuilder builder = new StringBuilder();
                    int bytes = 0;
                    do
                    {
                        bytes = stream.Read(data, 0, data.Length);
                        builder.Append(Encoding.Unicode.GetString(data, 0, bytes));
                    }
                    while (stream.DataAvailable);

                    message = builder.ToString();
                    ListBox_Message.Items.Add(message);
                    //Console.WriteLine(message);//вывод сообщения
                }
                catch
                {
                    MessageBox.Show("Подключение прервано!"); //соединение было прервано
                    Disconnect();
                }
            }
        }

        static void Disconnect()
        {
            client = new TcpClient();
            if (stream != null)
                stream.Close();//отключение потока
            if (client != null)
                client.Close();//отключение клиента
            Environment.Exit(0); //завершение процесса
        }
    }
}
