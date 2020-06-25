using NSubstitute;
using NUnit.Framework;
using SrtTranslator.Core.Tests;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SrtTranslator.Core.Translator.Tests
{
    [TestFixture]
    public class SubtitleBatchTranslatorTests
    {
        [Test]
        public void Constructor_NullTranslator_Throws()
        {
            IBatchTextTranslator nullTranslator = null;
            var stubFormatter = Substitute.For<ISubtitleTextFormatter>();

            Assert.Throws<ArgumentNullException>(
                () => new SubtitleBatchTranslator(nullTranslator, stubFormatter));
        }

        [Test]
        public void Constructor_NullFormatter_Throws()
        {
            var stubTranslator = Substitute.For<IBatchTextTranslator>();
            ISubtitleTextFormatter nullFormatter = null;

            Assert.Throws<ArgumentNullException>(
                () => new SubtitleBatchTranslator(stubTranslator, nullFormatter));
        }

        [Test]
        public void Translate_TranslationSuccessful_ReturnsTranslatedBatch()
        {
            var batchToTranslate = new List<Subtitle> {
                new Subtitle(
                    new SubtitleId(1),
                    new SubtitleTimestamps(
                        TimestampTests.CreateTimestamp(0, 0, 0, 0),
                        TimestampTests.CreateTimestamp(0, 0, 0, 3)),
                    CreateSingleLineText("First subtitle")),
                new Subtitle(
                    new SubtitleId(2),
                    new SubtitleTimestamps(
                        TimestampTests.CreateTimestamp(0, 0, 0, 4),
                        TimestampTests.CreateTimestamp(0, 0, 0, 7)),
                    CreateSingleLineText("Second subtitle"))
            };

            var expectedTranslation = new List<Subtitle> {
                SubtitleTests.CopyWithNewText(batchToTranslate[0], "Premier sous-titre"),
                SubtitleTests.CopyWithNewText(batchToTranslate[1], "Deuxième sous-titre")
            };

            var stubTranslator = Substitute.For<IBatchTextTranslator>();
            stubTranslator
                .Translate(
                    Arg.Is<List<string>>(
                        a => a.SequenceEqual(new List<string> {
                            "First subtitle",
                            "Second subtitle"
                        })),
                    Arg.Is(Language.French))
                .Returns(
                    new List<string> {
                            "Premier sous-titre",
                            "Deuxième sous-titre"
                    });
                
            var stubFormatter = Substitute.For<ISubtitleTextFormatter>();
            stubFormatter
                .Format(Arg.Is(CreateSingleLineText("First subtitle")))
                .Returns("First subtitle");
            stubFormatter
                .Format(Arg.Is(CreateSingleLineText("Second subtitle")))
                .Returns("Second subtitle");

            var translator = CreateTranslator(stubTranslator, stubFormatter);

            var actualTranslation = translator.Translate(batchToTranslate, Language.French);

            Assert.AreEqual(expectedTranslation, actualTranslation);
        }

        [Test]
        public void Translate_NullBatch_Throws()
        {
            var translator = CreateTranslator();
            List<Subtitle> nullBatch = null;

            Assert.Throws<ArgumentNullException>(
                () => translator.Translate(nullBatch, Language.French));
        }

        private SubtitleBatchTranslator CreateTranslator()
        {
            return CreateTranslator(
                Substitute.For<IBatchTextTranslator>(),
                Substitute.For<ISubtitleTextFormatter>());
        }

        private SubtitleBatchTranslator CreateTranslator(
            IBatchTextTranslator translator,
            ISubtitleTextFormatter formatter)
        {
            return new SubtitleBatchTranslator(
                translator,
                formatter);
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
