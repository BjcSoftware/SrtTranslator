using System;
using System.Linq;

namespace SrtTranslator.Core.Translator
{
    public class SubtitlesTranslator : ISubtitlesTranslator
    {
        private readonly ISubtitleTranslator translator;

        public SubtitlesTranslator(ISubtitleTranslator translator)
        {
            if (translator == null)
                throw new ArgumentNullException(nameof(translator));

            this.translator = translator;
        }

        public Subtitles Translate(
            Subtitles subtitles, 
            Language target)
        {
            if (subtitles == null)
                throw new ArgumentNullException(nameof(subtitles));

            return new Subtitles(
                subtitles.Value
                .Select(
                    s => translator.Translate(s, target))
                .ToList());
        }
    }
}
