using SrtTranslator.Core;
using SrtTranslator.Core.Translation;
using SrtTranslator.Core.Translator;
using System;
using System.Linq;

namespace SrtTranslator.DeepL
{
    public class SubtitlesTranslationCostCalculator
        : ISubtitlesTranslationCostCalculator
    {
        private readonly ICostForOneMillionCharactersProvider millionCharsCostprovider;
        private readonly ISubtitleTextFormatter formatter;

        public SubtitlesTranslationCostCalculator(
            ICostForOneMillionCharactersProvider provider,
            ISubtitleTextFormatter formatter)
        {
            if (provider == null)
                throw new ArgumentNullException(nameof(provider));
            if (formatter == null)
                throw new ArgumentNullException(nameof(formatter));

            millionCharsCostprovider = provider;
            this.formatter = formatter;
        }

        public Price Calculate(Subtitles subtitles, Currency currency)
        {
            if (subtitles == null)
                throw new ArgumentNullException(nameof(subtitles));

            decimal amount = millionCharsCostprovider.GetCostIn(currency) *
                CharacterCountInSubtitles(subtitles) / 1_000_000;

            return new Price(amount, currency);
        }

        private int CharacterCountInSubtitles(Subtitles subtitles)
        {
            return subtitles.Value
                .Sum(subtitle => formatter.Format(subtitle.Text).Length);
        }
    }
}
