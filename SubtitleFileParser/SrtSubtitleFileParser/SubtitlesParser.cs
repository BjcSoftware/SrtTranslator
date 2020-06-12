using SubtitleFileParser.Core;
using SubtitleFileParser.Core.Exceptions;
using System;
using System.Collections.Generic;

namespace SrtSubtitleFileParser
{
    using ISubtitleParser = IParser<Subtitle, UnvalidatedSubtitle>;

    public class SubtitlesParser : IParser<Subtitles, UnvalidatedSubtitles>
    {
        private readonly ISubtitleParser subtitleParser;

        public SubtitlesParser(ISubtitleParser subtitleParser)
        {
            if (subtitleParser == null)
                throw new ArgumentNullException(nameof(Subtitle));

            this.subtitleParser = subtitleParser;
        }

        public Subtitles Parse(UnvalidatedSubtitles unvalidatedSubtitles)
        {
            if (unvalidatedSubtitles == null)
                throw new ArgumentNullException(nameof(unvalidatedSubtitles));

            return ParseSubtitles(unvalidatedSubtitles);
        }

        public Subtitles ParseSubtitles(UnvalidatedSubtitles unvalidatedSubtitles)
        {
            var subtitles = new List<Subtitle>();
            int currentLine = 1;
            foreach (var subtitle in unvalidatedSubtitles.Value)
            {
                try
                {
                    subtitles.Add(
                        subtitleParser.Parse(subtitle));
                }
                catch (Exception)
                {
                    throw new SubtitlesParsingException(currentLine);
                }

                currentLine++;
            }

            return new Subtitles(subtitles);
        }
    }
}
