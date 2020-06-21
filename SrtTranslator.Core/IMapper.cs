namespace SrtTranslator.Core
{
    public interface IMapper<Destination, Source>
    {
        Destination Map(Source toMap);
    }
}
