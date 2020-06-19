namespace SrtTranslator.Core.Translator
{
    public interface ISrtTranslator
    {
        Subtitles Translate(
            Subtitles subtitles, 
            Language target, 
            Language source);
    }
}
