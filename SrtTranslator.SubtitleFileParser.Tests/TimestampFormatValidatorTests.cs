using NUnit.Framework;

namespace SrtTranslator.SubtitleFileParser.Tests
{
    [TestFixture]
    public class TimestampFormatValidatorTests
    {
        [Test]
        [TestCase("01:01:01,000")]
        [TestCase("1:1:1,0")]
        [TestCase("1:1:1,01")]
        [TestCase(" 01:01:01,000 ")]
        [TestCase("01:01:01")]
        public void IsFormatCorrect_CorrectFormat_ReturnsTrue(string correctTimestamp)
        {
            var validator = CreateValidator();

            Assert.IsTrue(
                validator.IsFormatCorrect(
                    new SubcharacterLine(correctTimestamp)));
        }

        [Test]
        [TestCase(":01:01,000")]
        [TestCase("01::01,000")]
        [TestCase("01:01:,000")]
        [TestCase("01:01:,000,01")]
        [TestCase("01:01:01,")]
        [TestCase("01:01,000")]
        [TestCase("01,000")]
        [TestCase("01")]
        [TestCase(",000")]
        public void IsFormatCorrect_IncorrectFormat_ReturnsFalse(string incorrectTimestamp)
        {
            var validator = CreateValidator();

            Assert.IsFalse(
                validator.IsFormatCorrect(
                    new SubcharacterLine(incorrectTimestamp)));
        }

        private TimestampFormatValidator CreateValidator()
        {
            return new TimestampFormatValidator();
        }
    }
}
