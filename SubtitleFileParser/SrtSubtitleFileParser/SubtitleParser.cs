using SubtitleFileParser.Core;
using SubtitleFileParser.Core.Exceptions;
using System;
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
            return new Subtitle(
                ParseTimestamps(unvalidatedSubtitle.TimestampsLine),
                new SubtitleText(
                    string.Join(
                        '\n', 
                        unvalidatedSubtitle.TextLines
                        .Select(l => l.Value))));
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
