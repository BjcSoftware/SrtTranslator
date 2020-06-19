using System;
using System.Collections.Generic;

namespace SrtTranslator.Core.Translator
{
    public class SubtitleTranslator : ISubtitleTranslator
    {
        private readonly ITextTranslator translator;
        private readonly ISubtitleTextFormatter formatter;

        public SubtitleTranslator(
            ITextTranslator translator,
            ISubtitleTextFormatter formatter)
        {
            if (translator == null)
                throw new ArgumentNullException(nameof(translator));
            if (formatter == null)
                throw new ArgumentNullException(nameof(formatter));

            this.translator = translator;
            this.formatter = formatter;
        }

        public Subtitle Translate(
            Subtitle subtitle, 
            Language target, 
            Language source)
        {
            if (subtitle == null)
                throw new ArgumentNullException(nameof(subtitle));

            return new Subtitle(
                subtitle.Id,
                subtitle.Timestamps,
                TranslateSubtitleText(
                    subtitle.Text,
                    target,
                    source));
        }

        private SubtitleText TranslateSubtitleText(
            SubtitleText textLines,
            Language target,
            Language source)
        {
            return new SubtitleText(
                new List<CharacterLine> {
                    new CharacterLine(
                        translator.TranslateText(
                            formatter.Format(textLines), 
                            target, 
                            source))
                });
        }
    }
}
