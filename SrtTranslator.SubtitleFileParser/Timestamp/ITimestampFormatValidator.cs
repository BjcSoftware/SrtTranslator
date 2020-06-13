namespace SrtTranslator.SubtitleFileParser
{
    public interface ITimestampFormatValidator
    {
        bool IsFormatCorrect(SubcharacterLine subline);
    }
}
