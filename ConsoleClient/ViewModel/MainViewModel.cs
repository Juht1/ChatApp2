using ConsoleClient.Net;
using ConsoleClient.Model;
using System;
using System.Collections.ObjectModel;
using System.Linq;

namespace ConsoleClient.ViewModel
{
    class MainViewModel
    {
        public ObservableCollection<UserModel> Users { get; set; }
        public ObservableCollection<string> Messages { get; set; }

        public string Username { get; set; }
        public string Message { get; set; }

        private Server _server;

        public MainViewModel(string? name)
        {
            Users = new ObservableCollection<UserModel>();
            Messages = new ObservableCollection<string>();

            _server = new Server();
            _server.connectedEvent += UserConnected;
            _server.msgRecievedEvent += MessageReceived;
            _server.userDisonnectEvent += RemoveUser;

            if (string.IsNullOrWhiteSpace(name))
            {
                Console.WriteLine("Username cannot be null or empty.");
                return;
            }

            Username = name; // Store the username
        }

        // Synchronous method to connect to the server
        public bool ConnectToServer()
        {
            try
            {
                _server.ConnectToServer(Username);
                Console.WriteLine("Connected to server.");
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error connecting to server: {ex.Message}");
                return false;
            }
        }

        private void RemoveUser()
        {
            var uid = _server.PacketReader.ReadMessage();
            var user = Users.FirstOrDefault(x => x.UID == uid);

            if (user != null)
            {
                Console.WriteLine($"{user.UserName} has been removed");
            }
        }

        private void MessageReceived()
        {
            var message = _server.PacketReader.ReadMessage();
            if (!string.IsNullOrWhiteSpace(message))
            {
                Messages.Add(message);
                Console.WriteLine($"Received: {message}");
            }
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
                Users.Add(user);
                Console.WriteLine($"{user.UserName} has connected.");
            }
        }

        // Synchronous method to send a message to the server
        public void SendMessage()
        {
            if (string.IsNullOrWhiteSpace(Message))
            {
                Console.WriteLine("Message cannot be null or empty.");
                return;
            }

            try
            {
                _server.SendMessageToServer(Message);
                Messages.Add($"You: {Message}");
                Console.WriteLine($"Sent: {Message}");
                Message = string.Empty;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error sending message: {ex.Message}");
            }
        }
    }
}
