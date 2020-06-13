using NUnit.Framework;
using SrtTranslator.Core;
using SrtTranslator.SubtitleFileParser.Exceptions;
using System;

namespace SrtTranslator.SubtitleFileParser.Tests
{
    using BoundedIntParser = BoundedIntValueObjectParser<MillisecondsTimestamp, SubcharacterLine>;

    [TestFixture]
    public class MillisecondsParserTests
    {
        [Test]
        public void Constructor_NullParser_Throws()
        {
            BoundedIntParser nullParser = null;

            Assert.Throws<ArgumentNullException>(
                () => new MillisecondsParser(nullParser));
        }

        [Test]
        public void Parse_NullSubline_Throws()
        {
            var parser = CreateParser();
            SubcharacterLine nullSubline = null;

            Assert.Throws<ArgumentNullException>(
                () => parser.Parse(nullSubline));
        }

        [Test]
        [TestCase("0", 0)]
        [TestCase("999", 999)]
        [TestCase("500", 500)]
        public void Parse_SuccessfulParsing_ReturnsMilliseconds(string input, int expectedMs)
        {
            var parser = CreateParser();

            var actualMs = parser.Parse(new SubcharacterLine(input));

            Assert.AreEqual(
                new MillisecondsTimestamp(expectedMs), 
                actualMs);
        }

        [Test]
        public void Parse_EmptySubline_ReturnsZeroMilliseconds()
        {
            var parser = CreateParser();

            var actualMs = parser.Parse(new SubcharacterLine(string.Empty));

            Assert.AreEqual(
                new MillisecondsTimestamp(0), 
                actualMs);
        }

        [Test]
        [TestCase("-1")]
        [TestCase("1000")]
        public void Parse_OutOfRangeMilliseconds_Throws(string outOfRange)
        {
            var parser = CreateParser();

            Assert.Throws<ParsingException>(
                () => parser.Parse(new SubcharacterLine(outOfRange)));
        }

        [Test]
        [TestCase("a1")]
        [TestCase("1a")]
        public void Parse_InvalidInt_Throws(string invalidInt)
        {
            var parser = CreateParser();

            Assert.Throws<ParsingException>(
                () => parser.Parse(new SubcharacterLine(invalidInt)));
        }

        private MillisecondsParser CreateParser()
        {
            return new MillisecondsParser(
                new BoundedIntParser(
                    new IntParser<SubcharacterLine>()));
        }
    }
}
