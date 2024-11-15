using ConsoleClient.ViewModel;

namespace ConsoleClient
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var name = Console.ReadLine();
            MainViewModel mainViewModel = new MainViewModel(name);
            var message = string.Empty;

            while (true) { 
            
                message = Console.ReadLine();
                Console.WriteLine(message);

            }
        }
    }
}
