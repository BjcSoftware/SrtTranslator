namespace SrtTranslator.SubtitleSerializer
{
    public interface IMapper<Destination, Source>
    {
        Destination Map(Source toMap);
    }
}
