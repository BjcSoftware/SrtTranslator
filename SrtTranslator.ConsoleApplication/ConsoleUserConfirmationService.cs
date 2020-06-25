using System;

namespace SrtTranslator.ConsoleApplication
{
    public class ConsoleUserConfirmationService
        : IUserConfirmationService
    {
        public Answer AskForConfirmation(string prompt)
        {
            Console.Write($"{prompt} (yes/no) ");
            string answer = Console.ReadLine();

            return answer.ToLower() == "yes" ? Answer.Yes : Answer.No;
        }
    }
}
