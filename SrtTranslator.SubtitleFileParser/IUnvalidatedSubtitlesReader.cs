namespace SrtTranslator.SubtitleFileParser
{
    public interface IUnvalidatedSubtitlesReader
    {
        UnvalidatedSubtitles ReadUnvalidatedSubtitles(FilePath filePath);
    }
}
