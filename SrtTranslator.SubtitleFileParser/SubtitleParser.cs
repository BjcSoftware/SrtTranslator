using SrtTranslator.Core;
using SrtTranslator.SubtitleFileParser.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SrtTranslator.SubtitleFileParser
{
    using ISubtitleIdParser = IParser<SubtitleId, CharacterLine>;
    using ISubtitleTimestampsParser = IParser<SubtitleTimestamps, CharacterLine>;

    public class SubtitleParser : IParser<Subtitle, UnvalidatedSubtitle>
    {
        private readonly ISubtitleIdParser subtitleIdParser;
        private readonly ISubtitleTimestampsParser timestampsParser;

        public SubtitleParser(
            ISubtitleIdParser subtitleIdParser,
            ISubtitleTimestampsParser timestampsParser)
        {
            if (subtitleIdParser == null)
                throw new ArgumentNullException(nameof(subtitleIdParser));
            if (timestampsParser == null)
                throw new ArgumentNullException(nameof(timestampsParser));

            this.subtitleIdParser = subtitleIdParser;
            this.timestampsParser = timestampsParser;
        }

        public Subtitle Parse(UnvalidatedSubtitle unvalidatedSubtitle)
        {
            if (unvalidatedSubtitle == null)
                throw new ArgumentNullException(nameof(unvalidatedSubtitle));

            if (unvalidatedSubtitle.Value.Count() < 3)
                throw new ParsingException("A subtitle should at least be 3 lines long");

            return new Subtitle(
                subtitleIdParser.Parse(GetIdLine(unvalidatedSubtitle)),
                timestampsParser.Parse(GetTimestampsLine(unvalidatedSubtitle)),
                new SubtitleText(GetTextLines(unvalidatedSubtitle)));
        }

        private CharacterLine GetIdLine(UnvalidatedSubtitle subtitle)
        {
            return subtitle.Value.ElementAt(0);
        }

        private CharacterLine GetTimestampsLine(UnvalidatedSubtitle subtitle)
        {
            return subtitle.Value.ElementAt(1);
        }

        private List<CharacterLine> GetTextLines(UnvalidatedSubtitle subtitle)
        {
            return subtitle.Value.Skip(2).ToList();
        }
    }
}
