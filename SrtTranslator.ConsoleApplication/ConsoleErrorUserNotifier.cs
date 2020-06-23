using System;

namespace SrtTranslator.ConsoleApplication
{
    public class ConsoleErrorUserNotifier : IUserNotifier
    {
        public void Notify(string msg)
        {
            Console.WriteLine($"Error: {msg}");
        }
    }
}
