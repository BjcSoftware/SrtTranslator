using SubtitleFileParser.Core;
using SubtitleFileParser.Core.Exceptions;
using System;
using System.Linq;

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
            return new Subtitles(
                unvalidatedSubtitles.Value
                .Select(
                    s => ParseUnvalidatedSubtitle(s))
                .ToList());
        }

        private Subtitle ParseUnvalidatedSubtitle(UnvalidatedSubtitle subtitle)
        {
            try
            {
                return subtitleParser.Parse(subtitle);
            }
            catch(Exception)
            {
                throw new ParsingException();
            }
        }
    }
}
