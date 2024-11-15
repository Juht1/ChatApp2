using ChatClient.Net;
using ConsoleClient.Model;
using ConsoleClient.Net;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ConsoleClient.ViewModel
{
    class MainViewModel
    {
        public ObservableCollection<UserModel> Users { get; set; }
        public ObservableCollection<string> Messages { get; set; }


        public string Username {  get; set; }
        public string Message { get; set; }

        private Server _server;

        public MainViewModel(string? name) 
        {
            Users = new ObservableCollection<UserModel>();
            Messages = new ObservableCollection<string>();

            _server = new Server();
            _server.connectedEvent += UserConnected;
            _server.msgRecievedEvent += MessageRecieved;
            _server.userDisonnectEvent += RemoveUser;
            _server.ConnectToServer(name);

            _server.SendMessageToServer(Message);
        }

        private void RemoveUser()
        {
            var uid = _server.PacketReader.ReadMessage();
            var user = Users.Where(x => x.UID == uid).FirstOrDefault();

            Console.WriteLine($"{user.UserName} has been removed");
            //Application.Current.Dispatcher.Invoke(() => Users.Remove(user));
        }

        private void MessageRecieved()
        {
            var msg = _server.PacketReader.ReadMessage();
            Messages.Add(msg);
            Console.WriteLine(msg);
            //Application.Current.Dispatcher.Invoke(() => Messages.Add(msg));
        }

        private void UserConnected()
        {
            var user = new UserModel
            {
                UserName = _server.PacketReader.ReadMessage(),
                UID = _server.PacketReader.ReadMessage(),
            };

            if (!Users.Any(x => x.UID == user.UID))
            {
                //Application.Current.Dispatcher.Invoke(() => Users.Add(user));
            }
        }


    }
}
