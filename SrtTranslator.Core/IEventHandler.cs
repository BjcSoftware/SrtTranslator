namespace SrtTranslator.Core
{
    public interface IEventHandler<TEvent>
    {
        void Handle(TEvent e);
    }
}
