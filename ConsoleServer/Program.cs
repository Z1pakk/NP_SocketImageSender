using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;

namespace ConsoleServer
{
    class Program
    {
        static void Main(string[] args)
        {
            // InterNetwork - 10.7.0.7
            Socket s = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            // Адрес на який хоститься наш сервер. 127.0.0.1 - локальний адрес(працює тільки на вашому пк)
            IPAddress iPAddress = IPAddress.Parse("127.0.0.1");
            // Зв'язує іп адресу і порт. Порт вказуємо любий.
            IPEndPoint iPEndPoint = new IPEndPoint(iPAddress, 1098);
            // Вказуємо що сервер працює на цій кінцевій точці
            s.Bind(iPEndPoint);
            //Вказуємо серверу що він має слухати і його черга на повідомлення становить 10
            s.Listen(10);

            try
            {
                while(true)
                {
                    // Підключення з клієнтом
                    Socket client = s.Accept();
                    Console.WriteLine(client.RemoteEndPoint.ToString());
                    client.Send(Encoding.UTF8.GetBytes($"Hello from server {DateTime.Now}"));
                    //Закрить підключення з клієнтом
                    client.Shutdown(SocketShutdown.Both);
                    client.Close();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        }
    }
}
