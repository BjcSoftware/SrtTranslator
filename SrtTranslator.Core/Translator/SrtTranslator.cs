using System;
using System.Linq;

namespace SrtTranslator.Core.Translator
{
    public class SrtTranslator : ISrtTranslator
    {
        private readonly ISubtitleTranslator translator;

        public SrtTranslator(ISubtitleTranslator translator)
        {
            if (translator == null)
                throw new ArgumentNullException(nameof(translator));

            this.translator = translator;
        }

        public Subtitles Translate(
            Subtitles subtitles, 
            Language target, 
            Language source)
        {
            if (subtitles == null)
                throw new ArgumentNullException(nameof(subtitles));

            return new Subtitles(
                subtitles.Value
                .Select(
                    s => translator.Translate(s, target, source))
                .ToList());
        }
    }
}
