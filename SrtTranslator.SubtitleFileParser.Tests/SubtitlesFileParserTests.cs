using NSubstitute;
using NSubstitute.ExceptionExtensions;
using NUnit.Framework;
using SrtTranslator.Core;
using SrtTranslator.Core.Tests;
using SrtTranslator.SubtitleFileParser.Exceptions;
using System;
using System.IO;

namespace SrtTranslator.SubtitleFileParser.Tests
{
    using ISubtitlesParser = IParser<Subtitles, UnvalidatedSubtitles>;

    [TestFixture]
    public class SubtitlesFileParserTests
    {
        [Test]
        public void Constructor_NullUnvalidatedSubtitlesReader_Throws()
        {
            IUnvalidatedSubtitlesReader nullReader = null;
            var stubParser = Substitute.For<ISubtitlesParser>();

            Assert.Throws<ArgumentNullException>(
                () => new SubtitlesFileParser(nullReader, stubParser));
        }

        [Test]
        public void Constructor_NullParser_Throws()
        {
            var stubReader = Substitute.For<IUnvalidatedSubtitlesReader>();
            ISubtitlesParser nullParser = null;

            Assert.Throws<ArgumentNullException>(
                () => new SubtitlesFileParser(stubReader, nullParser));
        }

        [Test]
        public void Parse_SuccessfulParsing_ReturnsSubtitles()
        {
            var expectedSubtitles = SubtitlesTests.CreateSubtitles1();
            var stubSubtitlesParser = Substitute.For<ISubtitlesParser>();
            stubSubtitlesParser
                .Parse(Arg.Any<UnvalidatedSubtitles>())
                .Returns(expectedSubtitles);
            var parser = CreateParser(stubSubtitlesParser);

            var actualSubtitles = parser.Parse(new FilePath("file.srt"));

            Assert.AreEqual(
                expectedSubtitles,
                actualSubtitles);
        }

        [Test]
        public void Parse_NullFilePath_Throws()
        {
            var parser = CreateParser();
            FilePath nullPath = null;

            Assert.Throws<ArgumentNullException>(
                () => parser.Parse(nullPath));
        }

        [Test]
        public void Parse_ParserThrowsSubtitlesParsingException_ThrowsSameException()
        {
            var stubSubtitlesParser = Substitute.For<ISubtitlesParser>();
            var expectedException = new SubtitlesParsingException();
            stubSubtitlesParser
                .Parse(Arg.Any<UnvalidatedSubtitles>())
                .Throws(expectedException);
            var parser = CreateParser(stubSubtitlesParser);

            var actualException = Assert.Throws<SubtitlesParsingException>(
                () => parser.Parse(new FilePath("file.srt")));
            Assert.AreSame(
                expectedException,
                actualException);
        }

        [Test]
        public void Parse_FileNotFound_ThrowsFileNotFoundException()
        {
            var stubReader = Substitute.For<IUnvalidatedSubtitlesReader>();
            stubReader
                .ReadUnvalidatedSubtitles(Arg.Any<FilePath>())
                .Throws<FileNotFoundException>();
            var parser = CreateParser(stubReader);

            Assert.Throws<FileNotFoundException>(
                () => parser.Parse(new FilePath("file.srt")));
        }

        private SubtitlesFileParser CreateParser()
        {
            return CreateParser(
                Substitute.For<IUnvalidatedSubtitlesReader>(),
                Substitute.For<ISubtitlesParser>());
        }

        private SubtitlesFileParser CreateParser(IUnvalidatedSubtitlesReader reader)
        {
            return CreateParser(reader, Substitute.For<ISubtitlesParser>());
        }

        private SubtitlesFileParser CreateParser(ISubtitlesParser parser)
        {
            return CreateParser(Substitute.For<IUnvalidatedSubtitlesReader>(), parser);
        }

        private SubtitlesFileParser CreateParser(
            IUnvalidatedSubtitlesReader reader, 
            ISubtitlesParser parser)
        {
            return new SubtitlesFileParser(reader, parser);
        }
    }
}
