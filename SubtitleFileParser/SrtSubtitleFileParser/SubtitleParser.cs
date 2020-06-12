using SubtitleFileParser.Core;
using SubtitleFileParser.Core.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SrtSubtitleFileParser
{
    using ISubtitleTimestampsParser = IParser<SubtitleTimestamps, CharacterLine>;

    public class SubtitleParser : IParser<Subtitle, UnvalidatedSubtitle>
    {
        private readonly ISubtitleTimestampsParser timestampsParser;

        public SubtitleParser(ISubtitleTimestampsParser timestampsParser)
        {
            if (timestampsParser == null)
                throw new ArgumentNullException(nameof(timestampsParser));

            this.timestampsParser = timestampsParser;
        }

        public Subtitle Parse(UnvalidatedSubtitle unvalidatedSubtitle)
        {
            if (unvalidatedSubtitle == null)
                throw new ArgumentNullException(nameof(unvalidatedSubtitle));

            if (unvalidatedSubtitle.Value.Count() < 3)
                throw new ParsingException("A subtitle should at least be 3 lines long");

            return new Subtitle(
                ParseTimestamps(GetTimestampsLine(unvalidatedSubtitle)),
                new SubtitleText(
                    string.Join(
                        '\n',
                        GetTextLines(unvalidatedSubtitle)
                        .Select(l => l.Value))));
        }

        private CharacterLine GetTimestampsLine(UnvalidatedSubtitle subtitle)
        {
            return subtitle.Value.ElementAt(1);
        }

        private List<CharacterLine> GetTextLines(UnvalidatedSubtitle subtitle)
        {
            return subtitle.Value.Skip(2).ToList();
        }

        private SubtitleTimestamps ParseTimestamps(CharacterLine line)
        {
            try
            {
                return timestampsParser.Parse(line);
            }
            catch(Exception)
            {
                throw new ParsingException();
            }
        }
    }
}
