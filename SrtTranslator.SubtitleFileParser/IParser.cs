namespace SrtTranslator.SubtitleFileParser
{
    public interface IParser<TResult, TInput>
    {
        TResult Parse(TInput input);
    }
}
