using System;

namespace SrtTranslator.ConsoleApplication
{
    public class ConsoleUserNotifier : IUserNotifier
    {
        public void Notify(string msg)
        {
            Console.WriteLine(msg);
        }
    }
}
