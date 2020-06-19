using SrtTranslator.Core.Translator;

namespace SrtTranslator.DeepL
{
    public interface ICostForOneMillionCharactersProvider
    {
        decimal GetCostIn(Currency currency);
    }
}
