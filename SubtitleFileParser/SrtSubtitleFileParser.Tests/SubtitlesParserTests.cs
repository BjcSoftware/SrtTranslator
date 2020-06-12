using NSubstitute;
using NSubstitute.ExceptionExtensions;
using NUnit.Framework;
using SubtitleFileParser.Core;
using SubtitleFileParser.Core.Exceptions;
using SubtitleFileParser.Core.Tests;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SrtSubtitleFileParser.Tests
{
    using ISubtitleParser = IParser<Subtitle, UnvalidatedSubtitle>;

    [TestFixture]
    public class SubtitlesParserTests
    {
        [Test]
        public void Constructor_NullSubtitleParser_Throws()
        {
            ISubtitleParser nullSubtitleParser = null;

            Assert.Throws<ArgumentNullException>(
                () => new SubtitlesParser(nullSubtitleParser));
        }

        [Test]
        public void Parse_SuccessfulParsing_ReturnsSubtitles()
        {
            var unvalidatedSubtitles = new UnvalidatedSubtitles(
                new List<UnvalidatedSubtitle> {
                    UnvalidatedSubtitleTests.CreateUnvalidatedSubtitle1(),
                    UnvalidatedSubtitleTests.CreateUnvalidatedSubtitle2()
                });

            var expectedSubtitles = new Subtitles(
                new List<Subtitle> {
                    SubtitleTests.CreateSubtitle1(),
                    SubtitleTests.CreateSubtitle2()
                });

            var stubParser = Substitute.For<ISubtitleParser>();
            stubParser
                .Parse(Arg.Is(unvalidatedSubtitles.Value.ElementAt(0)))
                .Returns(expectedSubtitles.Value.ElementAt(0));

            stubParser
                .Parse(Arg.Is(unvalidatedSubtitles.Value.ElementAt(1)))
                .Returns(expectedSubtitles.Value.ElementAt(1));

            var parser = CreateParser(stubParser);

            var actualSubtitles = parser.Parse(unvalidatedSubtitles);

            Assert.AreEqual(
                expectedSubtitles,
                actualSubtitles);
        }

        [Test]
        public void Parse_NullUnvalidatedSubtitles_Throws()
        {
            UnvalidatedSubtitles nullSubtitles = null;
            var parser = CreateParser();

            Assert.Throws<ArgumentNullException>(
                () => parser.Parse(nullSubtitles));
        }

        [Test]
        [TestCase(0, 1)]
        [TestCase(1, 0)]
        public void Parse_SubtitleParserThrows_ThrowsParsingException(
            int indexOfCorrectSub,
            int indexOfIncorrectSub)
        {
            var subsToParse = new UnvalidatedSubtitles(
                new List<UnvalidatedSubtitle> {
                    UnvalidatedSubtitleTests.CreateUnvalidatedSubtitle1(),
                    UnvalidatedSubtitleTests.CreateUnvalidatedSubtitle2()
                });

            var stubSubtitleParser = Substitute.For<ISubtitleParser>();
            stubSubtitleParser
                .Parse(Arg.Is(subsToParse.Value.ElementAt(indexOfCorrectSub)))
                .Returns(SubtitleTests.CreateSubtitle1());

            stubSubtitleParser
                .Parse(Arg.Is(subsToParse.Value.ElementAt(indexOfIncorrectSub)))
                .Throws<Exception>();

            var parser = CreateParser(stubSubtitleParser);

            var exception = Assert.Throws<SubtitlesParsingException>(
                () => parser.Parse(subsToParse));
            Assert.AreEqual(
                indexOfIncorrectSub + 1,
                exception.IncorrectSubtitleId);
        }

        private SubtitlesParser CreateParser()
        {
            return CreateParser(
                Substitute.For<ISubtitleParser>());
        }

        private SubtitlesParser CreateParser(ISubtitleParser subtitleParser)
        {
            return new SubtitlesParser(subtitleParser);
        }
    }
}
