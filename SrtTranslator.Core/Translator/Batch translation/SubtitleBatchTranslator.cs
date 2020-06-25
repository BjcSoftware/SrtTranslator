using System;
using System.Collections.Generic;
using System.Linq;

namespace SrtTranslator.Core.Translator
{
    public class SubtitleBatchTranslator : ISubtitleBatchTranslator
    {
        private readonly IBatchTextTranslator translator;
        private readonly ISubtitleTextFormatter formatter;

        public SubtitleBatchTranslator(
            IBatchTextTranslator translator,
            ISubtitleTextFormatter formatter)
        {
            if (translator == null)
                throw new ArgumentNullException(nameof(translator));
            if (formatter == null)
                throw new ArgumentNullException(nameof(formatter));

            this.translator = translator;
            this.formatter = formatter;
        }

        public List<Subtitle> Translate(
            List<Subtitle> batch, 
            Language target)
        {
            if (batch == null)
                throw new ArgumentNullException(nameof(batch));

            var translatedSubtitleTexts = TranslateSubtitleTexts(
                batch.Select(s => s.Text).ToList(),
                target);

            return batch.Zip(
                translatedSubtitleTexts,
                (subtitle, translatedText) =>
                     new Subtitle(
                         subtitle.Id,
                         subtitle.Timestamps,
                         translatedText)).ToList();
        }

        private List<SubtitleText> TranslateSubtitleTexts(
            List<SubtitleText> texts, 
            Language target)
        {
            return
                translator.Translate(
                    texts.Select(t => formatter.Format(t)).ToList(),
                    target)
                .Select(
                    translation => new SubtitleText(
                        new List<CharacterLine> {
                            new CharacterLine(translation)
                        })).ToList();
        }
    }
}
