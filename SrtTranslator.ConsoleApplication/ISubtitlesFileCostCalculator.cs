using SrtTranslator.Core;
using SrtTranslator.Core.Translation;

namespace SrtTranslator.ConsoleApplication
{
    public interface ISubtitlesFileCostCalculator
    {
        Price Calculate(FilePath filePath);
    }
}
