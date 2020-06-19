using NUnit.Framework;
using SrtTranslator.Core.Translator;
using System;
using System.Collections.Generic;

namespace SrtTranslator.Core.Tests.Translator
{
    [TestFixture]
    public class SingleLineSubtitleTextFormatterTests
    {
        [Test]
        public void Format_Always_ReturnsLinesSeparatedBySpaces()
        {
            var toFormat = new SubtitleText(
                new List<CharacterLine> {
                    new CharacterLine("A line"),
                    new CharacterLine("Another line")
                });

            string expected = "A line Another line";

            var formatter = CreateFormatter();

            string actual = formatter.Format(toFormat);

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void Format_NullText_Throws()
        {
            SubtitleText nullText = null;

            var formatter = CreateFormatter();

            Assert.Throws<ArgumentNullException>(
                () => formatter.Format(nullText));
        }

        private SingleLineSubtitleTextFormatter CreateFormatter()
        {
            return new SingleLineSubtitleTextFormatter();
        }
    }
}
