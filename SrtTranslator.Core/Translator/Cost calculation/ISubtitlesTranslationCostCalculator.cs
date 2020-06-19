using SrtTranslator.Core.Translator;

namespace SrtTranslator.Core.Translation
{
    public interface ISubtitlesTranslationCostCalculator
    {
        Price Calculate(Subtitles subtitles, Currency currency);
    }
}
