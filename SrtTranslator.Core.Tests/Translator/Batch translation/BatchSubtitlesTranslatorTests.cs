using NSubstitute;
using NUnit.Framework;
using SrtTranslator.Core.Tests;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SrtTranslator.Core.Translator.Tests
{
    [TestFixture]
    public class BatchSubtitlesTranslatorTests
    {
        [Test]
        public void Constructor_NullTranslator_Throws()
        {
            ISubtitleBatchTranslator nullTranslator = null;
            int stubMaxBatchSize = 5;

            Assert.Throws<ArgumentNullException>(
                () => new BatchSubtitlesTranslator(
                    nullTranslator, stubMaxBatchSize));
        }

        [Test]
        [TestCase(0)]
        [TestCase(-1)]
        public void Constructor_MaxBatchSizeLessThanOne_Throws(int batchSize)
        {
            var stubTranslator = Substitute.For<ISubtitleBatchTranslator>();

            Assert.Throws<ArgumentOutOfRangeException>(
                () => new BatchSubtitlesTranslator(
                    stubTranslator, batchSize));
        }

        [Test]
        public void Translate_TranslationSuccessful_ReturnsTranslatedSubtitles()
        {
            var s1 = new Subtitle(
                new SubtitleId(1),
                new SubtitleTimestamps(
                    TimestampTests.CreateTimestamp(0, 0, 0, 0),
                    TimestampTests.CreateTimestamp(0, 0, 0, 3)),
                CreateSingleLineText("First subtitle"));
            var translated_s1 = SubtitleTests.CopyWithNewText(s1, "Premier sous-titre");

            var s2 = new Subtitle(
                new SubtitleId(2),
                new SubtitleTimestamps(
                    TimestampTests.CreateTimestamp(0, 0, 0, 4),
                    TimestampTests.CreateTimestamp(0, 0, 0, 7)),
                CreateSingleLineText("Second subtitle"));
            var translated_s2 = SubtitleTests.CopyWithNewText(s2, "Deuxième sous-titre");

            var s3 = new Subtitle(
                new SubtitleId(3),
                new SubtitleTimestamps(
                    TimestampTests.CreateTimestamp(0, 0, 0, 8),
                    TimestampTests.CreateTimestamp(0, 0, 0, 11)),
                CreateSingleLineText("Third subtitle"));
            var translated_s3 = SubtitleTests.CopyWithNewText(s2, "Troisième sous-titre");

            var stubBatchTranslator = Substitute.For<ISubtitleBatchTranslator>();
            stubBatchTranslator
                .Translate(
                    Arg.Is<List<Subtitle>>(
                        b => b.SequenceEqual(new List<Subtitle> { s1, s2 })),
                    Arg.Is(Language.French))
                .Returns(new List<Subtitle> { translated_s1, translated_s2 });
            stubBatchTranslator
                .Translate(
                    Arg.Is<List<Subtitle>>(
                        b => b.SequenceEqual(new List<Subtitle> { s3 })),
                    Arg.Is(Language.French))
                .Returns(new List<Subtitle> { translated_s3 });

            var toTranslate = new Subtitles(
                new List<Subtitle> { s1, s2, s3 });
            var expectedTranslation = new Subtitles(
                new List<Subtitle> { translated_s1, translated_s2, translated_s3 });
            var translator = new BatchSubtitlesTranslator(stubBatchTranslator, 2);

            var actualTranslation = translator.Translate(toTranslate, Language.French);

            Assert.AreEqual(expectedTranslation, actualTranslation);
        }

        [Test]
        public void Translate_NullSubtitles_Throws()
        {
            var translator = CreateTranslator();
            Subtitles nullSubtitles = null;

            Assert.Throws<ArgumentNullException>(
                () => translator.Translate(nullSubtitles, Language.French));
        }

        private BatchSubtitlesTranslator CreateTranslator()
        {
            return CreateTranslator(
                Substitute.For<ISubtitleBatchTranslator>(),
                5);
        }

        private BatchSubtitlesTranslator CreateTranslator(
            ISubtitleBatchTranslator translator, 
            int maxBatchSize)
        {
            return new BatchSubtitlesTranslator(translator, maxBatchSize);
        }

        private SubtitleText CreateSingleLineText(string text)
        {
            return new SubtitleText(
                new List<CharacterLine> {
                    new CharacterLine(text)
                });
        }
    }
}
