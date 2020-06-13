using NSubstitute;
using NSubstitute.ExceptionExtensions;
using NUnit.Framework;
using SrtTranslator.Core;
using SrtTranslator.Core.Tests;
using SrtTranslator.SubtitleFileParser.Exceptions;
using System;

namespace SrtTranslator.SubtitleFileParser.Tests
{
    using ITimestampParser = IParser<Timestamp, SubcharacterLine>;

    [TestFixture]
    public class SubtitleTimestampsParserTests
    {
        [Test]
        public void Constructor_NullParser_Throws()
        {
            ITimestampParser nullParser = null;

            Assert.Throws<ArgumentNullException>(
                () => new SubtitleTimestampsParser(nullParser));
        }

        [Test]
        public void Parse_NullSubline_Throws()
        {
            var parser = CreateParser();
            CharacterLine nullLine = null;

            Assert.Throws<ArgumentNullException>(
                () => parser.Parse(nullLine));
        }

        [Test]
        [TestCase("05:05:05,005 --> 05:05:05,005")]
        [TestCase(" 05:05:05,005  -->  05:05:05,005 ")]
        public void Parse_SuccessfulParsing_ReturnsTimestamps(string timestamps)
        {
            var timestampParser = Substitute.For<ITimestampParser>();
            timestampParser
                .Parse(Arg.Any<SubcharacterLine>())
                .Returns(TimestampTests.CreateTimestamp(5, 5, 5, 5));
            var expectedTimestamps = new SubtitleTimestamps(
                TimestampTests.CreateTimestamp(5, 5, 5, 5),
                TimestampTests.CreateTimestamp(5, 5, 5, 5));

            var timestampsParser = CreateParser(timestampParser);

            var actualTimestamps = timestampsParser.Parse(
                new CharacterLine(timestamps));

            Assert.AreEqual(
                expectedTimestamps, 
                actualTimestamps);
        }

        [Test]
        [TestCase("05:05:05,005 -> 05:05:05,005")]
        [TestCase("05:05:05,005  05:05:05,005")]
        public void Parse_IncorrectFormat_Throws(string incorrectFormat)
        {
            var parser = CreateParser();

            Assert.Throws<ParsingException>(
                () => parser.Parse(new CharacterLine(incorrectFormat)));
        }

        [Test]
        public void Parse_TimestampParserThrows_ThrowsParsingException()
        {
            var stubTimestampParser = Substitute.For<ITimestampParser>();
            stubTimestampParser
                .Parse(Arg.Any<SubcharacterLine>())
                .Throws(new Exception());

            var parser = CreateParser(stubTimestampParser);

            Assert.Throws<ParsingException>(
                () => parser.Parse(
                    new CharacterLine("05:05:05,005 --> 05:05:05,005")));
        }

        private SubtitleTimestampsParser CreateParser()
        {
            return CreateParser(
                Substitute.For<ITimestampParser>());
        }

        private SubtitleTimestampsParser CreateParser(ITimestampParser timestampParser)
        {
            return new SubtitleTimestampsParser(timestampParser);
        }
    }
}
