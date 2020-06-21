namespace SrtTranslator.Core.Translator
{
    public interface ISubtitlesTranslator
    {
        Subtitles Translate(
            Subtitles subtitles, 
            Language target, 
            Language source);
    }
}
