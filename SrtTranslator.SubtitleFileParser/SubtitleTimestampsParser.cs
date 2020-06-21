using SrtTranslator.Core;
using SrtTranslator.SubtitleFileParser.Exceptions;
using System;
using System.Linq;

namespace SrtTranslator.SubtitleFileParser
{
    using ITimestampParser = IParser<Timestamp, SubcharacterLine>;

    public class SubtitleTimestampsParser 
        : IParser<SubtitleTimestamps, CharacterLine>
    {
        private readonly ITimestampParser timestampParser;

        private const string TimestampsSeparator = "-->";

        public SubtitleTimestampsParser(ITimestampParser timestampParser)
        {
            if (timestampParser == null)
                throw new ArgumentNullException(nameof(timestampParser));

            this.timestampParser = timestampParser;
        }

        public SubtitleTimestamps Parse(CharacterLine line)
        {
            if (line == null)
                throw new ArgumentNullException(nameof(line));

            if (InvalidFormat(line))
                throw new ParsingException("Invalid timestamps format");

            var startAndEnd = line.Split(TimestampsSeparator);

            return new SubtitleTimestamps(
                timestampParser.Parse(startAndEnd[0]),
                timestampParser.Parse(startAndEnd[1]));
        }

        private bool InvalidFormat(CharacterLine line)
        {
            var startAndEnd = line.Split(TimestampsSeparator);

            if (startAndEnd.Count() != 2)
                return true;

            return false;
        }
    }
}
