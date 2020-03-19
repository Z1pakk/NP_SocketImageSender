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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Net;
using System.Net.Sockets;

namespace WpfClient
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Socket mySocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            //ІП і порт сервера
            IPAddress iPAddress = IPAddress.Parse("127.0.0.1");
            IPEndPoint iPEndPoint = new IPEndPoint(iPAddress, 1098);
            try
            {
                //Підключення до сервера
                mySocket.Connect(iPEndPoint);
                if(mySocket.Connected)
                {
                    string str = "Hello Kozak";
                    //Відправляємо серверу повідомлення у байтах.
                    mySocket.Send(Encoding.ASCII.GetBytes(str));

                    //тимчасова змінна для зберігання даних, які прийшли з сервера
                    byte[] buffer = new byte[1024];
                    //скільки байті залишилось зчитати
                    int length;

                    do
                    {
                        // Hello World
                        // Hello - 1kb
                        // World - 1kb

                        //Зчитування даних які залишилось отримать з сервера
                        length = mySocket.Receive(buffer);
                        //Розкодування тексту і запис в текстбокс.
                        tbMessage.Text += Encoding.UTF8.GetString(buffer);
                    } while (length > 0);
                }
            }
            catch (Exception ex)
            {

            }
            finally
            {
                mySocket.Shutdown(SocketShutdown.Send);
                mySocket.Close();
            }

        }
    }
}
