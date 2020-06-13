using SrtTranslator.Core;
using SrtTranslator.SubtitleFileParser.Exceptions;
using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace SrtTranslator.SubtitleFileParser
{
    using IHoursParser = IParser<HoursTimestamp, SubcharacterLine>;
    using IMinutesParser = IParser<MinutesTimestamp, SubcharacterLine>;
    using ISecondsParser = IParser<SecondsTimestamp, SubcharacterLine>;
    using IMillisecondsParser = IParser<MillisecondsTimestamp, SubcharacterLine>;

    public class TimestampParser : IParser<Timestamp, SubcharacterLine>
    {
        private readonly IHoursParser hoursParser;
        private readonly IMinutesParser minutesParser;
        private readonly ISecondsParser secondsParser;
        private readonly IMillisecondsParser millisecondsParser;
        private readonly ITimestampFormatValidator validator;

        private const string timestampRegex = @"^\s*(\d?\d):(\d?\d):(\d?\d)(,(\d?\d?\d))?\s*$";

        public TimestampParser(
            IHoursParser hoursParser,
            IMinutesParser minutesParser,
            ISecondsParser secondsParser,
            IMillisecondsParser millisecondsParser,
            ITimestampFormatValidator validator)
        {
            if (hoursParser == null)
                throw new ArgumentNullException(nameof(hoursParser));
            if (minutesParser == null)
                throw new ArgumentNullException(nameof(minutesParser));
            if (secondsParser == null)
                throw new ArgumentNullException(nameof(secondsParser));
            if (millisecondsParser == null)
                throw new ArgumentNullException(nameof(millisecondsParser));
            if (validator == null)
                throw new ArgumentNullException(nameof(validator));

            this.hoursParser = hoursParser;
            this.minutesParser = minutesParser;
            this.secondsParser = secondsParser;
            this.millisecondsParser = millisecondsParser;
            this.validator = validator;
        }

        public Timestamp Parse(SubcharacterLine subline)
        {
            if (subline == null)
                throw new ArgumentNullException(nameof(subline));
            if (!validator.IsFormatCorrect(subline))
                throw new ParsingException("Invalid timestamp format");

            var matches =
                Regex.Match(subline.Value, timestampRegex).Groups.Values
                .Select(g => new SubcharacterLine(g.Value)).ToList();

            return new Timestamp(
                hoursParser.Parse(matches[1]),
                minutesParser.Parse(matches[2]),
                secondsParser.Parse(matches[3]),
                millisecondsParser.Parse(matches[5]));
        }
    }
}