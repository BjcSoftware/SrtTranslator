using NUnit.Framework;
using SrtTranslator.SubtitleFileParser.Exceptions;
using System;

namespace SrtTranslator.SubtitleFileParser.Tests
{
    [TestFixture]
    public class IntParserTests
    {
        [Test]
        public void Parse_NullInput_Throws()
        {
            var parser = CreateParser();
            CharacterLine nullInput = null;

            Assert.Throws<ArgumentNullException>(
                () => parser.Parse(nullInput));
        }

        [Test]
        [TestCase("a1")]
        [TestCase("1b")]
        public void Parse_IncorrectInt_Throws(string incorrectInt)
        {
            var parser = CreateParser();

            Assert.Throws<ParsingException>(
                () => parser.Parse(
                    new CharacterLine(incorrectInt)));
        }

        [Test]
        [TestCase("12", 12)]
        [TestCase("50", 50)]
        public void Parse_CorrectInt_ReturnsInt(string correctInt, int expectedInt)
        {
            var parser = CreateParser();

            int actualInt = parser.Parse(new CharacterLine(correctInt));

            Assert.AreEqual(expectedInt, actualInt);
        }

        private IntParser<CharacterLine> CreateParser()
        {
            return new IntParser<CharacterLine>();
        }
    }
}
