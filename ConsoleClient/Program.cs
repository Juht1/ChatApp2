using ConsoleClient.ViewModel;
using System;

namespace ConsoleClient
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Enter your username: ");
            var name = Console.ReadLine();

            MainViewModel mainViewModel = new MainViewModel(name);

            Console.WriteLine("You can start sending messages. Type 'exit' to quit.");

            while (true)
            {
                Console.Write("Enter message: ");
                var message = Console.ReadLine();

                if (message.Equals("exit", StringComparison.OrdinalIgnoreCase))
                {
                    Console.WriteLine("Exiting the chat client...");
                    break; 
                }
                mainViewModel.Message = message;
                mainViewModel.SendMessage();
            }
        }
    }
}