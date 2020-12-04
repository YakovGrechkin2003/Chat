using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Client
{
    /// <summary>
    /// Логика взаимодействия для Authorization.xaml
    /// </summary>
    public partial class Authorization : Window
    {
        public Authorization()
        {
            InitializeComponent();
        }

        private void Button_next_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (TextBox_Name.Text != "")
                {
                    string path = @"D:\All\ClientName.txt";
                    string temp;
                    using (StreamWriter sw = new StreamWriter(path))
                    {
                        temp = TextBox_Name.Text.ToString();
                        sw.WriteLine(temp);
                    }
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Введите имя!!");
                }
            }
            catch (Exception a)
            {
                MessageBox.Show(a.Message);
            }
        }
    }
}
