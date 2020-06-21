using NSubstitute;
using NSubstitute.ExceptionExtensions;
using NUnit.Framework;
using SrtTranslator.Core.Tests;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SrtTranslator.Core.Translator.Tests
{
    [TestFixture]
    public class SubtitlesTranslatorTests
    {
        [Test]
        public void Constructor_NullSubtitleTranslator_Throws()
        {
            ISubtitleTranslator nullTranslator = null;

            Assert.Throws<ArgumentNullException>(
                () => new SubtitlesTranslator(nullTranslator));
        }

        [Test]
        public void Translate_SuccessfulTranslation_ReturnsTranslatedSubtitles()
        {
            var firstSubtitle = new Subtitle(
                new SubtitleId(1),
                new SubtitleTimestamps(
                    TimestampTests.CreateTimestamp(5, 4, 3, 2),
                    TimestampTests.CreateTimestamp(5, 4, 4, 0)),
                CreateSubtitleText("first line"));
            
            var secondSubtitle = new Subtitle(
                new SubtitleId(2),
                new SubtitleTimestamps(
                    TimestampTests.CreateTimestamp(5, 4, 5, 0),
                    TimestampTests.CreateTimestamp(5, 4, 6, 0)),
                CreateSubtitleText("a line"));

            var toTranslate = new Subtitles(
                new List<Subtitle> {
                    firstSubtitle,
                    secondSubtitle
                });

            var expectedTranslation = new Subtitles(
                new List<Subtitle> {
                    new Subtitle(
                        firstSubtitle.Id,
                        firstSubtitle.Timestamps,
                        CreateSubtitleText("première ligne")),
                    new Subtitle(
                        secondSubtitle.Id,
                        secondSubtitle.Timestamps,
                        CreateSubtitleText("deuxième ligne"))
                });

            var stubTranslator = Substitute.For<ISubtitleTranslator>();
            stubTranslator
                .Translate(Arg.Is(firstSubtitle), Language.French, Language.English)
                .Returns(expectedTranslation.Value.ElementAt(0));
            stubTranslator
                .Translate(Arg.Is(secondSubtitle), Language.French, Language.English)
                .Returns(expectedTranslation.Value.ElementAt(1));

            var srtTranslator = CreateSrtTranslator(stubTranslator);

            var actualTranslation = srtTranslator.Translate(toTranslate, Language.French, Language.English);

            Assert.AreEqual(
                expectedTranslation,
                actualTranslation);
        }

        [Test]
        public void Translate_NullSubtitles_Throws()
        {
            Subtitles nullSubtitles = null;

            var srtTranslator = CreateSrtTranslator();

            Assert.Throws<ArgumentNullException>(
                () => srtTranslator.Translate(nullSubtitles, Language.French, Language.English));
        }

        [Test]
        public void Translate_SubtitleTranslatorThrows_Throws()
        {
            var stubSubtitleTranslator = Substitute.For<ISubtitleTranslator>();
            var expectedException = new Exception();
            stubSubtitleTranslator
                .Translate(Arg.Any<Subtitle>(), Arg.Any<Language>(), Arg.Any<Language>())
                .Throws(expectedException);

            var srtTranslator = CreateSrtTranslator(stubSubtitleTranslator);

            var stubSubtitles = SubtitlesTests.CreateSubtitles1();

            var actualException = Assert.Throws<Exception>(() =>
                srtTranslator.Translate(stubSubtitles, Language.French, Language.English));
            Assert.AreEqual(expectedException, actualException);
        }

        private SubtitlesTranslator CreateSrtTranslator()
        {
            return CreateSrtTranslator(
                Substitute.For<ISubtitleTranslator>());
        }

        private SubtitlesTranslator CreateSrtTranslator(ISubtitleTranslator translator)
        {
            return new SubtitlesTranslator(translator);
        }

        private SubtitleText CreateSubtitleText(string line)
        {
            return new SubtitleText(
                new List<CharacterLine> {
                    new CharacterLine(line)
                });
        }
    }
}
