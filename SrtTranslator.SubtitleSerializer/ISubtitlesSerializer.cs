using SrtTranslator.Core;

namespace SrtTranslator.SubtitleSerializer
{
    public interface ISubtitlesSerializer
    {
        void Serialize(Subtitles subtitles, FilePath filepath);
    }
}
