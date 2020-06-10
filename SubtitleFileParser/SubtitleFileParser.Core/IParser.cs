namespace SubtitleFileParser.Core
{
    public interface IParser<TResult, TInput>
    {
        TResult Parse(TInput input);
    }
}
