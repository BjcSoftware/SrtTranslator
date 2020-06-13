using NSubstitute;
using NSubstitute.ExceptionExtensions;
using NUnit.Framework;
using SrtTranslator.Core;
using SrtTranslator.Core.Tests;
using SrtTranslator.SubtitleFileParser.Exceptions;
using System;

namespace SrtTranslator.SubtitleFileParser.Tests
{
    using IHoursParser = IParser<HoursTimestamp, SubcharacterLine>;
    using IMinutesParser = IParser<MinutesTimestamp, SubcharacterLine>;
    using ISecondsParser = IParser<SecondsTimestamp, SubcharacterLine>;
    using IMillisecondsParser = IParser<MillisecondsTimestamp, SubcharacterLine>;

    [TestFixture]
    public class TimestampParserTests
    {
        [Test]
        public void Parse_NullHoursParser_Throws()
        {
            IHoursParser nullHoursParser = null;

            Assert.Catch<ArgumentNullException>(
                () => new TimestampParser(
                    nullHoursParser,
                    Substitute.For<IMinutesParser>(),
                    Substitute.For<ISecondsParser>(),
                    Substitute.For<IMillisecondsParser>(),
                    Substitute.For<ITimestampFormatValidator>()));
        }

        [Test]
        public void Parse_NullMinutesParser_Throws()
        {
            IMinutesParser nullMinutesParser = null;

            Assert.Catch<ArgumentNullException>(
                () => new TimestampParser(
                    Substitute.For<IHoursParser>(),
                    nullMinutesParser,
                    Substitute.For<ISecondsParser>(),
                    Substitute.For<IMillisecondsParser>(),
                    Substitute.For<ITimestampFormatValidator>()));
        }

        [Test]
        public void Parse_NullSecondsParser_Throws()
        {
            ISecondsParser nullSecondsParser = null;

            Assert.Catch<ArgumentNullException>(
                () => new TimestampParser(
                    Substitute.For<IHoursParser>(),
                    Substitute.For<IMinutesParser>(),
                    nullSecondsParser,
                    Substitute.For<IMillisecondsParser>(),
                    Substitute.For<ITimestampFormatValidator>()));
        }

        [Test]
        public void Parse_NullMillisecondsParser_Throws()
        {
            IMillisecondsParser nullMillisecondsParser = null;

            Assert.Catch<ArgumentNullException>(
                () => new TimestampParser(
                    Substitute.For<IHoursParser>(),
                    Substitute.For<IMinutesParser>(),
                    Substitute.For<ISecondsParser>(),
                    nullMillisecondsParser,
                    Substitute.For<ITimestampFormatValidator>()));
        }

        [Test]
        public void Parse_NullValidator_Throws()
        {
            ITimestampFormatValidator nullValidator = null;

            Assert.Catch<ArgumentNullException>(
                () => new TimestampParser(
                    Substitute.For<IHoursParser>(),
                    Substitute.For<IMinutesParser>(),
                    Substitute.For<ISecondsParser>(),
                    Substitute.For<IMillisecondsParser>(),
                    nullValidator));
        }

        [Test]
        public void Parse_ParsingSuccessful_ReturnsTimestamp()
        {
            var stubValidator = Substitute.For<ITimestampFormatValidator>();
            stubValidator
                .IsFormatCorrect(Arg.Any<SubcharacterLine>())
                .Returns(true);

            var parser = CreateParser(
                CreateParserReturning(new HoursTimestamp(4)),
                CreateParserReturning(new MinutesTimestamp(3)),
                CreateParserReturning(new SecondsTimestamp(2)),
                CreateParserReturning(new MillisecondsTimestamp(1)),
                stubValidator);
            var expectedTimestamp = TimestampTests.CreateTimestamp(4, 3, 2, 1);

            var actualTimestamp = parser.Parse(new SubcharacterLine("04:03:02,001"));

            Assert.AreEqual(
                expectedTimestamp,
                actualTimestamp);
        }
        
        [Test]
        public void Parse_IncorrectTimestampFormat_ThrowsParsingException()
        {
            var stubValidator = Substitute.For<ITimestampFormatValidator>();
            stubValidator
                .IsFormatCorrect(Arg.Any<SubcharacterLine>())
                .Returns(false);
            var parser = CreateParser(stubValidator);

            Assert.Throws<ParsingException>(
                () => parser.Parse(new SubcharacterLine("incorrect format timestamp")));
        }

        [Test]
        public void Parse_IncorrectHours_Throws()
        {
            var parser = CreateParserIgnoringFormat(
                CreateParserThrowingParsingException<HoursTimestamp>(),
                CreateParserReturning(new MinutesTimestamp(2)),
                CreateParserReturning(new SecondsTimestamp(2)),
                CreateParserReturning(new MillisecondsTimestamp(2)));

            Assert.Throws<ParsingException>(
                () => parser.Parse(new SubcharacterLine("01:01:01,000")));
        }

        [Test]
        public void Parse_IncorrectMinutes_Throws()
        {
            var parser = CreateParserIgnoringFormat(
                CreateParserReturning(new HoursTimestamp(2)),
                CreateParserThrowingParsingException<MinutesTimestamp>(),
                CreateParserReturning(new SecondsTimestamp(2)),
                CreateParserReturning(new MillisecondsTimestamp(2)));

            Assert.Throws<ParsingException>(
                () => parser.Parse(new SubcharacterLine("01:01:01,000")));
        }

        [Test]
        public void Parse_IncorrectSeconds_Throws()
        {
            var parser = CreateParserIgnoringFormat(
                CreateParserReturning(new HoursTimestamp(2)),
                CreateParserReturning(new MinutesTimestamp(2)),
                CreateParserThrowingParsingException<SecondsTimestamp>(),
                CreateParserReturning(new MillisecondsTimestamp(2)));

            Assert.Throws<ParsingException>(
                () => parser.Parse(new SubcharacterLine("01:01:01,000")));
        }

        [Test]
        public void Parse_IncorrectMilliseconds_Throws()
        {
            var parser = CreateParserIgnoringFormat(
                CreateParserReturning(new HoursTimestamp(2)),
                CreateParserReturning(new MinutesTimestamp(2)),
                CreateParserReturning(new SecondsTimestamp(2)),
                CreateParserThrowingParsingException<MillisecondsTimestamp>());
        }

        private TimestampParser CreateParser(
            IHoursParser hoursParser,
            IMinutesParser minutesParser,
            ISecondsParser secondsParser,
            IMillisecondsParser millisecondsParser,
            ITimestampFormatValidator validator)
        {
            return new TimestampParser(
                hoursParser,
                minutesParser,
                secondsParser,
                millisecondsParser,
                validator);
        }

        private TimestampParser CreateParser(ITimestampFormatValidator validator)
        {
            return CreateParser(
                CreateParserReturning(new HoursTimestamp(4)),
                CreateParserReturning(new MinutesTimestamp(3)),
                CreateParserReturning(new SecondsTimestamp(2)),
                CreateParserReturning(new MillisecondsTimestamp(1)),
                validator);
        }

        private TimestampParser CreateParserIgnoringFormat(
            IHoursParser hoursParser,
            IMinutesParser minutesParser,
            ISecondsParser secondsParser,
            IMillisecondsParser millisecondsParser)
        {
            var stubValidator = Substitute.For<ITimestampFormatValidator>();
            stubValidator
                .IsFormatCorrect(Arg.Any<SubcharacterLine>())
                .Returns(true);

            return CreateParser(
                hoursParser,
                minutesParser,
                secondsParser,
                millisecondsParser,
                stubValidator);
        }

        private IParser<TResult, SubcharacterLine> CreateParserReturning<TResult>(TResult value)
        {
            var parser = Substitute.For<IParser<TResult, SubcharacterLine>>();
            parser
                .Parse(Arg.Any<SubcharacterLine>())
                .Returns(value);

            return parser;
        }

        private IParser<TResult, SubcharacterLine> CreateParserThrowingParsingException<TResult>()
        {
            var parser = Substitute.For<IParser<TResult, SubcharacterLine>>();
            parser
                .Parse(Arg.Any<SubcharacterLine>())
                .Throws(new ParsingException());

            return parser;
        }
    }
}
