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
        }
    }
}
