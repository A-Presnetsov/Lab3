using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace lab3_server
{
    class Program
    {
        static void Main(string[] args) {
            TcpListener serverSocket = new TcpListener(1918);
            serverSocket.Start();

            Console.WriteLine("Сервер запущен");

            while (true) {
                try {
                    TcpClient socket = serverSocket.AcceptTcpClient();
                    NetworkStream nwStream = socket.GetStream();
                    BinaryReader reader = new BinaryReader(nwStream);
                    BinaryWriter writer = new BinaryWriter(nwStream);
                    int length = reader.ReadInt32(); 
                    string str = reader.ReadString();
                    Console.WriteLine("Клиент прислал строку: " + str);
                    string response = Encoding.UTF8.GetByteCount(str) == length ? str.Length + " символов" : "Передача данных не удалась";


                    writer.Write(Encoding.UTF8.GetByteCount(response)); 
                    writer.Write(response); 
                    socket.Close();
                } catch(Exception e) { Console.WriteLine(e.Message); }
            }
            serverSocket.Stop();
            Console.ReadKey();
        }
    }
}
