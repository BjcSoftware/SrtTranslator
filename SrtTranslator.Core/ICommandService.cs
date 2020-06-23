namespace SrtTranslator.Core
{
    public interface ICommandService<TCommand>
    {
        void Execute(TCommand command);
    }
}
