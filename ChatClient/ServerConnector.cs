using System;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Collections.Generic;

namespace ChatClient
{
    public class ServerConnector
    {
        private Socket connection;

        public string ConnectToServer(string ip, int port)
        {
            try
            {
                var ipServer = new IPEndPoint(IPAddress.Parse(ip), port);
                connection = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                connection.Connect(ipServer);
            }
            catch (Exception exc)
            {
                return exc.Message;
            }

            return "Соединение установлено";
        }

        public string SendMessage(string message)
        {
            connection.Send(Encoding.Unicode.GetBytes(message));
            var buffer = new byte[256];
            var data = new List<byte>();
            do
            {
                connection.Receive(buffer);
                data.AddRange(buffer);
            } while (connection.Available > 0);

            var t = data.ToArray();
            if (message == "Bye")
            {
                CloseConnection();
                return "Соединение разорвано.";
            }
                
            return Encoding.Unicode.GetString(t, 0, t.Length);
        }

        public bool isConnectedToServer()
        {
            return connection.Connected;
        }

        private void CloseConnection()
        {
            connection.Shutdown(SocketShutdown.Both);
            connection.Close();
        }
    }
}
