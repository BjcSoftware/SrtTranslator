using System;
using System.Collections.Generic;
using System.Linq;

namespace SrtTranslator.Core.Translator
{
    public class BatchSubtitlesTranslator : ISubtitlesTranslator
    {
        private readonly ISubtitleBatchTranslator batchTranslator;
        private readonly int maxBatchSize;

        public BatchSubtitlesTranslator(
            ISubtitleBatchTranslator batchTranslator,
            int maxBatchSize)
        {
            if (batchTranslator == null)
                throw new ArgumentNullException(nameof(batchTranslator));
            if (maxBatchSize < 1)
                throw new ArgumentOutOfRangeException(nameof(maxBatchSize));

            this.batchTranslator = batchTranslator;
            this.maxBatchSize = maxBatchSize;
        }

        public Subtitles Translate(Subtitles subtitles, Language target)
        {
            if (subtitles == null)
                throw new ArgumentNullException(nameof(subtitles));

            var translatedSubtitles = new List<Subtitle>();

            foreach (var batch in SplitIntoBatches(subtitles, maxBatchSize))
            {
                translatedSubtitles.AddRange(
                    batchTranslator.Translate(batch, target));
                Console.WriteLine($"{translatedSubtitles.Count} subtitles translated");
            }

            return new Subtitles(translatedSubtitles);
        }

        private static IEnumerable<List<Subtitle>> SplitIntoBatches(
            Subtitles subtitles, 
            int batchSize)
        {
            List<Subtitle> toSplit = subtitles.Value.ToList();

            for (int i = 0; i < toSplit.Count; i += batchSize)
            {
                yield return toSplit.GetRange(i, Math.Min(batchSize, toSplit.Count - i));
            }
        }
    }
}
