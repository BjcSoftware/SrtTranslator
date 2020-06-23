using SrtTranslator.Core;
using SrtTranslator.Core.Translation;
using SrtTranslator.Core.Translator;
using SrtTranslator.SubtitleFileParser;
using System;

namespace SrtTranslator.ConsoleApplication
{
    public class SubtitlesFileCostCalculator : ISubtitlesFileCostCalculator
    {
        private readonly IParser<Subtitles, FilePath> parser;
        private readonly ISubtitlesTranslationCostCalculator calculator;

        public SubtitlesFileCostCalculator(
            IParser<Subtitles, FilePath> parser,
            ISubtitlesTranslationCostCalculator calculator)
        {
            if (parser == null)
                throw new ArgumentNullException(nameof(parser));
            if (calculator == null)
                throw new ArgumentNullException(nameof(calculator));

            this.parser = parser;
            this.calculator = calculator;
        }

        public Price Calculate(FilePath filePath)
        {
            if (filePath == null)
                throw new ArgumentNullException(nameof(filePath));

            return calculator.Calculate(
                parser.Parse(filePath), 
                Currency.Euro);
        }
    }
}
