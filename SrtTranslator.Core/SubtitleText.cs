namespace SrtTranslator.Core
{
    public class SubtitleText : ValueObject<string>
    {
        public SubtitleText(string text)
            : base(text)
        {
        }
    }
}
