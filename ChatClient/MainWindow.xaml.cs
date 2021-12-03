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


namespace ChatClient
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        ServerConnector _connector;
        List<MessageItem> _chatMsgs;

        public MainWindow()
        {
            InitializeComponent();
            NewMessageBlock.IsEnabled = false;
            _connector = new();
            _chatMsgs = new();
        }

        private void GoButton_Click(object sender, RoutedEventArgs e)
        {
            if(!Validator.ValidateIp(IPadressBlock.Text))
            {
                MessageBox.Show("Некорректный IP адрес сервера!", "Ошибка");
                return;
            }

            int port;
            string resultmesg = "";
            if(int.TryParse(PortBlock.Text, out port))
            {
                resultmesg = _connector.ConnectToServer(IPadressBlock.Text, port);
            }
            MessageBox.Show(resultmesg);
            if (resultmesg == "Соединение установлено")
            {
                NewMessageBlock.IsEnabled = true;
            }                
        }

        private void SendButton_Click(object sender, RoutedEventArgs e)
        {

            if (!_connector.isConnectedToServer())
            {
                MessageBox.Show("Не установлено соединение\n с сервером", "Ошибка");
                return;
            }
            _chatMsgs.Add(new(NewMessageBlock.Text, new SolidColorBrush(Color.FromRgb(0, 255, 0)))); 
            _chatMsgs.Add(new(_connector.SendMessage(NewMessageBlock.Text), new SolidColorBrush(Color.FromRgb(255,0,0))));
            RefreshMessageBox();
            NewMessageBlock.Text = "";
        }

        private void RefreshMessageBox()
        {
            MyMessageBox.Items.Clear();
            foreach (var it in _chatMsgs)
            {
                MyMessageBox.Items.Add(it);
            }
        }

        private void IPadressBlock_LostFocus(object sender, RoutedEventArgs e)
        {
            if (IPadressBlock.Text == "")
            {
                IPadressBlock.Text = "Введите IP адресс";
                IPadressBlock.Foreground = Brushes.Gray;
            }
        }

        private void IPadressBlock_GotFocus(object sender, RoutedEventArgs e)
        {
            if(IPadressBlock.Text == "Введите IP адресс")
            {
                IPadressBlock.Text = "";
                IPadressBlock.Foreground = Brushes.Black;
            }
        }

        private void PortBlock_LostFocus(object sender, RoutedEventArgs e)
        {
            if (PortBlock.Text == "")
            {
                PortBlock.Text = "Введите номер порта";
                PortBlock.Foreground = Brushes.Gray;
            }
        }

        private void PortBlock_GotFocus(object sender, RoutedEventArgs e)
        {
            if(PortBlock.Text == "Введите номер порта")
            {
                PortBlock.Text = "";
                PortBlock.Foreground = Brushes.Black;
            }
        }

        private void NewMessageBlock_LostFocus(object sender, RoutedEventArgs e)
        {
            if(NewMessageBlock.Text == "")
            {
                NewMessageBlock.Text = "Введите сообщение";
                NewMessageBlock.Foreground = Brushes.Gray;
            }
        }

        private void NewMessageBlock_GotFocus(object sender, RoutedEventArgs e)
        {
            NewMessageBlock.Foreground = Brushes.Black;
            if (NewMessageBlock.Text == "Введите сообщение")
            {
                NewMessageBlock.Text = "";
            }
        }
    }
}
