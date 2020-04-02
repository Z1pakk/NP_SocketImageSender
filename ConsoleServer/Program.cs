using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using WpfClient;

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
            s.Listen(-1);

            try
            {
                while(true)
                {
                    // Підключення з клієнтом
                    Socket client = s.Accept();
                    Console.WriteLine(client.RemoteEndPoint.ToString());
                    //тимчасова змінна для зберігання даних, які прийшли з сервера
                    byte[] buffer = new byte[10000000];
                    //скільки байті залишилось зчитати
                    int length;

                    do
                    {
                        // Hello World
                        // Hello - 1kb
                        // World - 1kb

                        //Зчитування даних які залишилось отримать з сервера
                        length = client.Receive(buffer);

                    } while (client.Available > 0);

                    ImageName imgName = new ImageName();

                    using (MemoryStream memStream = new MemoryStream(buffer))
                    {
                        BinaryFormatter binForm = new BinaryFormatter();
                        imgName = (ImageName)binForm.Deserialize(memStream);
                    }

                    imgName.Image.Save(imgName.Name);
                    

                    //client.Send(Encoding.UTF8.GetBytes($"Hello from server {DateTime.Now}"));
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
