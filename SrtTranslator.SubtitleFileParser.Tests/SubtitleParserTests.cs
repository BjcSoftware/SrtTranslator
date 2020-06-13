using NSubstitute;
using NSubstitute.ExceptionExtensions;
using NUnit.Framework;
using SrtTranslator.Core;
using SrtTranslator.Core.Tests;
using SrtTranslator.SubtitleFileParser.Exceptions;
using System;
using System.Collections.Generic;

namespace SrtTranslator.SubtitleFileParser.Tests
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

            var expectedTimestamps = new SubtitleTimestamps(
                TimestampTests.CreateTimestamp(0, 0, 1, 100),
                TimestampTests.CreateTimestamp(0, 0, 3, 200));

            var stubTimestampsParser = Substitute.For<ISubtitleTimestampsParser>();
            stubTimestampsParser
                .Parse(Arg.Is(new CharacterLine("00:00:01,100 --> 00:00:03,200")))
                .Returns(expectedTimestamps);

            var parser = CreateParser(stubTimestampsParser);

            var expectedSubtitle = new Subtitle(
                expectedTimestamps,
                new SubtitleText("Some text\nMore text"));

            var actualSubtitle = parser.Parse(unvalidatedSubtitle);

            Assert.AreEqual(expectedSubtitle, actualSubtitle);
        }

        [Test]
        public void Parse_NullUnvalidatedSubtitle_Throws()
        {
            UnvalidatedSubtitle nullSubtitle = null;
            var parser = CreateParser();

            Assert.Throws<ArgumentNullException>(
                () => parser.Parse(nullSubtitle));
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


        private static object[] lessThanThreeLinesCases =
        {
            new object[] { new List<CharacterLine>() },
            new object[] { new List<CharacterLine> {
                new CharacterLine("First Line")
            }},
            new object[] { new List<CharacterLine> {
                new CharacterLine("First Line"),
                new CharacterLine("Second Line")
            }}
        };

        [TestCaseSource(nameof(lessThanThreeLinesCases))]
        public void Parse_LessThanThreeLinesSubtitle_ThrowsParsingException(List<CharacterLine> lines)
        {
            var subtitle = new UnvalidatedSubtitle(lines);

            var parser = CreateParser();

            Assert.Throws<ParsingException>(
                () => parser.Parse(subtitle));
        }

        private SubtitleParser CreateParser()
        {
            return CreateParser(
                Substitute.For<ISubtitleTimestampsParser>());
        }

        private SubtitleParser CreateParser(ISubtitleTimestampsParser timestampsParser)
        {
            return new SubtitleParser(timestampsParser);
        }
    }
}
