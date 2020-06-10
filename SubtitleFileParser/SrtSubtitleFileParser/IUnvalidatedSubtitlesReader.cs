using SubtitleFileParser.Core;

namespace SrtSubtitleFileParser
{
    public interface IUnvalidatedSubtitlesReader
    {
        UnvalidatedSubtitles ReadUnvalidatedSubtitles(FilePath filePath);
    }
}
