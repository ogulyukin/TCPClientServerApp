using System;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Collections.Generic;

namespace ChatServer
{
    class Program
    {
        static void PrintMessages(string msg)
        {
            Console.WriteLine(msg);
        }

        static void Main(string[] args)
        {
            Chatter chatbot = new();
            Console.WriteLine("ChatBot Loaded...");
            Console.WriteLine("Starting server...");

            var ip = "127.0.0.1";
            var port = "8005";

            TCPServer server = new(PrintMessages, chatbot);
            server.StartServer(ip, port);
            /*
            var ipEndPoint = new IPEndPoint(IPAddress.Parse(ip), port);

            var listen = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            listen.Bind(ipEndPoint);
            listen.Listen(10);
            Console.WriteLine("Begin listening...");

            while(true)
            {
                var connect = listen.Accept();

                Console.WriteLine("Accept new Connection...");

                var buffer = new byte[256];
                var data = new List<byte>();
                string msg = "";
                bool isConnected = true;
                while(msg != "Bye" && isConnected)
                {
                    do
                    {
                        try
                        {
                            connect.Receive(buffer);
                            data.AddRange(buffer);
                        }catch(Exception exc)
                        {
                            Console.WriteLine(exc.Message);
                            isConnected = false;
                            break;
                        }
                       
                    } while (connect.Available > 0);
                    if(isConnected)
                    {
                        var t = data.ToArray();
                        msg = Encoding.Unicode.GetString(t, 0, t.Length);
                        Console.WriteLine($"Get client message: {msg}");
                        Console.WriteLine("Sending the answer...");
                        connect.Send(Encoding.Unicode.GetBytes(chatbot.GetNewText()));
                        data.Clear();
                    }
                }
                
                Console.WriteLine("Closing connection...");

                connect.Shutdown(SocketShutdown.Both);
                connect.Close();
            }*/
        }
    }
}
