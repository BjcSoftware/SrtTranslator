namespace SrtTranslator.Core.Translator
{
    public interface ISubtitleTranslator
    {
        Subtitle Translate(
            Subtitle subtitle,
            Language target);
    }
}
