using ConsoleClient.ViewModel;

namespace ConsoleClient
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Enter your username: ");
            var name = Console.ReadLine();
            MainViewModel mainViewModel = new MainViewModel(name);

            while (true)
            {
                var message = Console.ReadLine();
                mainViewModel.Message = message;
                mainViewModel.SendMessage();
            }
        }
    }
}