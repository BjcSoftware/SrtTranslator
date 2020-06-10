namespace SrtSubtitleFileParser
{
    public interface ITimestampFormatValidator
    {
        bool IsFormatCorrect(SubcharacterLine subline);
    }
}
