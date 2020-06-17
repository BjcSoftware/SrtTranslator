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
    using ISubtitleIdParser = IParser<SubtitleId, CharacterLine>;
    using ISubtitleTimestampsParser = IParser<SubtitleTimestamps, CharacterLine>;

    [TestFixture]
    public class SubtitleParserTests
    {
        [Test]
        public void Constructor_NullSubtitleIdParser_Throws()
        {
            ISubtitleIdParser nullSubtitleIdParser = null;
            var stubTimestampsParser = Substitute.For<ISubtitleTimestampsParser>();

            Assert.Throws<ArgumentNullException>(
                () => new SubtitleParser(
                    nullSubtitleIdParser, 
                    stubTimestampsParser));
        }

        [Test]
        public void Constructor_NullTimestampsParser_Throws()
        {
            var stubSubtitleIdParser = Substitute.For<ISubtitleIdParser>();
            ISubtitleTimestampsParser nullTimestampsParser = null;

            Assert.Throws<ArgumentNullException>(
                () => new SubtitleParser(
                    stubSubtitleIdParser,
                    nullTimestampsParser));
        }

        [Test]
        public void Parse_SuccessfulParsing_ReturnsSubtitle()
        {
            var subtitleToParse = new UnvalidatedSubtitle(
                new List<CharacterLine> {
                    new CharacterLine("1"),
                    new CharacterLine("00:00:01,100 --> 00:00:03,200"),
                    new CharacterLine("Some text"),
                    new CharacterLine("More text")
                });

            var stubIdParser = Substitute.For<ISubtitleIdParser>();
            stubIdParser
                .Parse(Arg.Is(new CharacterLine("1")))
                .Returns(new SubtitleId(1));

            var expectedTimestamps = new SubtitleTimestamps(
                TimestampTests.CreateTimestamp(0, 0, 1, 100),
                TimestampTests.CreateTimestamp(0, 0, 3, 200));

            var stubTimestampsParser = Substitute.For<ISubtitleTimestampsParser>();
            stubTimestampsParser
                .Parse(Arg.Is(new CharacterLine("00:00:01,100 --> 00:00:03,200")))
                .Returns(expectedTimestamps);

            var parser = CreateParser(stubIdParser, stubTimestampsParser);

            var expectedSubtitle = new Subtitle(
                new SubtitleId(1),
                expectedTimestamps,
                new SubtitleText(
                    new List<CharacterLine> {
                        new CharacterLine("Some text"),
                        new CharacterLine("More text")
                    }));

            var actualSubtitle = parser.Parse(subtitleToParse);

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
        public void Parse_IdParserThrowsParsingException_Throws()
        {
            var stubIdParser = Substitute.For<ISubtitleIdParser>();
            stubIdParser
                .Parse(Arg.Any<CharacterLine>())
                .Throws<ParsingException>();

            var parser = CreateParser(stubIdParser);

            AssertThrowsParsingException(parser);
        }

        [Test]
        public void Parse_TimestampsParserThrowsParsingException_Throws()
        {
            var stubTimestampsParser = Substitute.For<ISubtitleTimestampsParser>();
            stubTimestampsParser
                .Parse(Arg.Any<CharacterLine>())
                .Throws<ParsingException>();

            var parser = CreateParser(stubTimestampsParser);

            AssertThrowsParsingException(parser);
        }

        private void AssertThrowsParsingException(SubtitleParser parser)
        {
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
                Substitute.For<ISubtitleIdParser>(),
                Substitute.For<ISubtitleTimestampsParser>());
        }

        private SubtitleParser CreateParser(ISubtitleIdParser idParser)
        {
            return CreateParser(
                idParser,
                Substitute.For<ISubtitleTimestampsParser>());
        }

        private SubtitleParser CreateParser(ISubtitleTimestampsParser timestampsParser)
        {
            return CreateParser(
                Substitute.For<ISubtitleIdParser>(),
                timestampsParser);
        }

        private SubtitleParser CreateParser(
            ISubtitleIdParser idParser,
            ISubtitleTimestampsParser timestampsParser)
        {
            return new SubtitleParser(
                idParser,
                timestampsParser);
        }
    }
}
