using NSubstitute;
using NSubstitute.ExceptionExtensions;
using NUnit.Framework;
using SrtSubtitleFileParser.Exceptions;
using SubtitleFileParser.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SrtSubtitleFileParser.Tests
{
    [TestFixture]
    public class UnvalidatedSubtitlesFileReaderTests
    {
        [Test]
        public void Constructor_NullReader_Throws()
        {
            IFileLineReader nullReader = null;

            Assert.Throws<ArgumentNullException>(
                () => new UnvalidatedSubtitlesFileReader(nullReader));
        }

        [Test]
        public void ReadUnvalidatedSubtitles_ReadSuccessful_ReturnsUnvalidatedSubtitles()
        {
            var stubReader = Substitute.For<IFileLineReader>();
            stubReader
                .ReadAllLines(Arg.Any<FilePath>())
                .Returns(new string[] {
                    "1",
                    "00:01:01,000 --> 00:01:02,000",
                    "First subtitle",
                    "",
                    "2",
                    "00:01:03,000 --> 00:01:04,000",
                    "Second subtitle",
                    ""
                });

            var expected = new UnvalidatedSubtitles(
                new List<UnvalidatedSubtitle>
                {
                    CreateUnvalidatedSubtitle(new List<string> {"1", "00:01:01,000 --> 00:01:02,000", "First subtitle"}),
                    CreateUnvalidatedSubtitle(new List<string> { "2", "00:01:03,000 --> 00:01:04,000", "Second subtitle" })
                });

            var reader = CreateReader(stubReader);

            var actual = reader.ReadUnvalidatedSubtitles(new FilePath("a file"));

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void ReadUnvalidatedSubtitles_ReaderThrows_ThrowsSubtitlesReadingException()
        {
            var stubReader = Substitute.For<IFileLineReader>();
            stubReader
                .ReadAllLines(Arg.Any<FilePath>())
                .Throws<Exception>();
            var reader = CreateReader(stubReader);

            Assert.Throws<SubtitlesReadingException>(
                () => reader.ReadUnvalidatedSubtitles(new FilePath("a file")));
        }

        private UnvalidatedSubtitlesFileReader CreateReader(IFileLineReader reader)
        {
            return new UnvalidatedSubtitlesFileReader(reader);
        }

        private UnvalidatedSubtitle CreateUnvalidatedSubtitle(List<string> subtitleLines)
        {
            return new UnvalidatedSubtitle(
                subtitleLines.Select(s => new CharacterLine(s)));
        }
    }
}
