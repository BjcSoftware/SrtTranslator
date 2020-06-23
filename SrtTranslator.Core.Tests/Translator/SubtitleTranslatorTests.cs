using NSubstitute;
using NSubstitute.ExceptionExtensions;
using NUnit.Framework;
using SrtTranslator.Core.Tests;
using System;
using System.Collections.Generic;

namespace SrtTranslator.Core.Translator.Tests
{
    [TestFixture]
    public class SubtitleTranslatorTests
    {
        [Test]
        public void Constructor_NullTranslator_Throws()
        {
            ITextTranslator nullTranslator = null;
            var stubFormatter = Substitute.For<ISubtitleTextFormatter>();

            Assert.Throws<ArgumentNullException>(
                () => new SubtitleTranslator(
                    nullTranslator, 
                    stubFormatter));
        }

        [Test]
        public void Constructor_NullFormatter_Throws()
        {
            var stubTranslator = Substitute.For<ITextTranslator>();
            ISubtitleTextFormatter nullFormatter = null;

            Assert.Throws<ArgumentNullException>(
                () => new SubtitleTranslator(
                    stubTranslator,
                    nullFormatter));
        }

        [Test]
        public void Translate_SuccessfulTranslation_ReturnsTranslatedSubtitle()
        {
            var subtitleText = new SubtitleText(
                new List<CharacterLine> {
                    new CharacterLine("first line"),
                    new CharacterLine("second line")
                });

            var toTranslate = new Subtitle(
                new SubtitleId(1),
                new SubtitleTimestamps(
                    TimestampTests.CreateTimestamp(5, 4, 3, 2),
                    TimestampTests.CreateTimestamp(5, 4, 4, 0)),
                subtitleText);

            var expectedTranslation = new Subtitle(
                toTranslate.Id,
                toTranslate.Timestamps,
                new SubtitleText(
                    new List<CharacterLine> {
                        new CharacterLine("première ligne deuxième ligne")
                    }));

            var stubTranslator = Substitute.For<ITextTranslator>();
            stubTranslator
                .TranslateText(Arg.Is("formatted text"), Language.French)
                .Returns("première ligne deuxième ligne");

            var stubFormatter = Substitute.For<ISubtitleTextFormatter>();
            stubFormatter
                .Format(Arg.Is(subtitleText))
                .Returns("formatted text");

            var translator = CreateSubtitleTranslator(stubTranslator, stubFormatter);

            var actualTranslation = translator.Translate(toTranslate, Language.French);

            Assert.AreEqual(
                expectedTranslation,
                actualTranslation);
        }

        [Test]
        public void Translate_NullSubtitle_Throws()
        {
            Subtitle nullSubtitle = null;
            var translator = CreateSubtitleTranslator();

            Assert.Throws<ArgumentNullException>(
                () => translator.Translate(nullSubtitle, Language.French));
        }

        [Test]
        public void Translate_TranslatorThrows_Throws()
        {
            var stubTextTranslator = Substitute.For<ITextTranslator>();
            var expectedException = new Exception();
            stubTextTranslator
                .TranslateText(Arg.Any<string>(), Arg.Any<Language>())
                .Throws(expectedException);

            var srtTranslator = CreateSubtitleTranslator(stubTextTranslator);

            var stubSubtitle = SubtitleTests.CreateSubtitle1();

            var actualException = Assert.Throws<Exception>(() =>
                srtTranslator.Translate(stubSubtitle, Language.French));
            Assert.AreEqual(expectedException, actualException);
        }

        private SubtitleTranslator CreateSubtitleTranslator()
        {
            return CreateSubtitleTranslator(
                Substitute.For<ITextTranslator>(),
                Substitute.For<ISubtitleTextFormatter>());
        }

        private SubtitleTranslator CreateSubtitleTranslator(ITextTranslator translator)
        {
            return CreateSubtitleTranslator(
                translator,
                Substitute.For<ISubtitleTextFormatter>());
        }

        private SubtitleTranslator CreateSubtitleTranslator(
            ITextTranslator translator,
            ISubtitleTextFormatter formatter)
        {
            return new SubtitleTranslator(translator, formatter);
        }
    }
}
