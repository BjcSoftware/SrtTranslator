using NSubstitute;
using NSubstitute.ExceptionExtensions;
using NUnit.Framework;
using SubtitleFileParser.Core;
using SubtitleFileParser.Core.Exceptions;
using SubtitleFileParser.Core.Tests;
using System;
using System.Collections.Generic;

namespace SrtSubtitleFileParser.Tests
{
    using ISubtitleTimestampsParser = IParser<SubtitleTimestamps, CharacterLine>;

    [TestFixture]
    public class SubtitleParserTests
    {
        [Test]
        public void Constructor_NullTimestampsParser_Throws()
        {
            ISubtitleTimestampsParser nullTimestampsParser = null;

            Assert.Throws<ArgumentNullException>(
                () => new SubtitleParser(nullTimestampsParser));
        }

        [Test]
        public void Parse_SuccessfulParsing_ReturnsSubtitle()
        {
            var unvalidatedSubtitle = new UnvalidatedSubtitle(
                new List<CharacterLine> {
                    new CharacterLine("1"),
                    new CharacterLine("00:00:01,100 --> 00:00:03,200"),
                    new CharacterLine("Some text"),
                    new CharacterLine("More text")
                });

            var timestamps = new SubtitleTimestamps(
                TimestampTests.CreateTimestamp(0, 0, 1, 100),
                TimestampTests.CreateTimestamp(0, 0, 3, 200));

            var parser = CreateParser(
                CreateStubTimestampsParserReturning(timestamps));

            var expected = new Subtitle(
                timestamps,
                new SubtitleText("Some text\nMore text"));

            var actual = parser.Parse(unvalidatedSubtitle);

            Assert.AreEqual(expected, actual);
        }

        private ISubtitleTimestampsParser CreateStubTimestampsParserReturning(
            SubtitleTimestamps timestamps)
        {
            var stubTimestampsParser = Substitute.For<ISubtitleTimestampsParser>();
            stubTimestampsParser
                .Parse(Arg.Any<CharacterLine>())
                .Returns(timestamps);

            return stubTimestampsParser;
        }

        [Test]
        public void Parse_TimestampsParserThrows_ThrowsParsingException()
        {
            var stubTimestampsParser = Substitute.For<ISubtitleTimestampsParser>();
            stubTimestampsParser
                .Parse(Arg.Any<CharacterLine>())
                .Throws<Exception>();

            var parser = CreateParser(stubTimestampsParser);

            Assert.Throws<ParsingException>(
                () => parser.Parse(
                    UnvalidatedSubtitleTests.CreateUnvalidatedSubtitle1()));
        }

        private SubtitleParser CreateParser(ISubtitleTimestampsParser timestampsParser)
        {
            return new SubtitleParser(timestampsParser);
        }
    }
}
