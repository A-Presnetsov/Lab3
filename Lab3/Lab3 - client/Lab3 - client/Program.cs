using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Lab3___client
{
    class Program
    {
        static void Main(string[] args) {
            while (true) {
                try {
                    Console.Write("Введите строку: ");
                    string str = Console.ReadLine();
                    TcpClient socket = new TcpClient("192.168.0.103", 1918);
                    NetworkStream nwStream = socket.GetStream();
                    BinaryReader reader = new BinaryReader(nwStream);
                    BinaryWriter writer = new BinaryWriter(nwStream);
                    writer.Write(Encoding.UTF8.GetByteCount(str)); 
                    writer.Write(str); 

                    int length = reader.ReadInt32(); 
                    string response = reader.ReadString();
                    if (Encoding.UTF8.GetByteCount(response) != length)
                        Console.WriteLine("Передача данных не удалась");
                    else
                        Console.WriteLine(response);
                }
                catch (Exception e) { Console.WriteLine(e.Message); }
            }
            Console.ReadKey();
        }
    }
}
