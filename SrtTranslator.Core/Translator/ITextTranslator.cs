namespace SrtTranslator.Core.Translator
{
    public interface ITextTranslator
    {
        string TranslateText(
            string text, 
            Language target);
    }
}
