namespace SrtTranslator.ConsoleApplication
{
    public enum Answer {
        Yes,
        No
    };

    public interface IUserConfirmationService
    {
        Answer AskForConfirmation(string prompt);
    }
}
