using System;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.Net;
using System.Net.Sockets;
using System.Collections.Generic;


namespace ChatServer
{
    delegate void Message(string msg);

    class TCPServer
    {
        private Message _msg;
        Socket _listen;
        Chatter _chatbot;

        public TCPServer(Message msg, Chatter chatbot)
        {
            _msg = msg;
            _chatbot = chatbot;
        }

        public void StartServer(string ip, string strport)
        {
            if (!ValidateIp(ip))
            {
                _msg("Incorrect ip addres! Aborting Server start");
                return;
            }
            int port;
            int.TryParse(strport, out port);
            if (port == 0)
            {
                _msg("Incorrect port number! Aborting Server start");
                return;
            }

            try
            {
                var ipServer = new IPEndPoint(IPAddress.Parse(ip), port);
                _listen = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                _listen.Bind(ipServer);
                _listen.Listen(10);
                _msg("Begin listening...");
            }
            catch (Exception exc)
            {
                _msg(exc.Message);
                return;
            }

            while (true)
            {
                var connect = _listen.Accept();

                _msg("Accept new Connection...");

                var buffer = new byte[256];
                var data = new List<byte>();
                string msg = "";
                bool isConnected = true;
                while (msg != "Bye" && isConnected)
                {
                    do
                    {
                        try
                        {
                            connect.Receive(buffer);
                            data.AddRange(buffer);
                        }
                        catch (Exception exc)
                        {
                            _msg(exc.Message);
                            isConnected = false;
                            break;
                        }

                    } while (connect.Available > 0);
                    if (isConnected)
                    {
                        var t = data.ToArray();
                        msg = Encoding.Unicode.GetString(t, 0, t.Length);
                        _msg($"Get client message: {msg}");
                        _msg("Sending the answer...");
                        connect.Send(Encoding.Unicode.GetBytes(_chatbot.GetNewText()));
                        data.Clear();
                    }
                }

                _msg("Closing connection...");

                connect.Shutdown(SocketShutdown.Both);
                connect.Close();
            }
        }

        private bool ValidateIp(string ip)
        {
            Regex regex = new(@"[0-9]{1,3}[.][0-9]{1,3}[.][0-9]{1,3}[.][0-9]{1,3}");
            return regex.IsMatch(ip);
        }
    }
}
