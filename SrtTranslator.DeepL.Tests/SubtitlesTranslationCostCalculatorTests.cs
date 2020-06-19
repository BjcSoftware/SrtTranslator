using NSubstitute;
using NUnit.Framework;
using SrtTranslator.Core;
using SrtTranslator.Core.Tests;
using SrtTranslator.Core.Translation;
using SrtTranslator.Core.Translator;
using System;
using System.Collections.Generic;

namespace SrtTranslator.DeepL.Tests
{
    [TestFixture]
    class SubtitlesTranslationCostCalculatorTests
    {
        [Test]
        public void Constructor_NullProvider_Throws()
        {
            ICostForOneMillionCharactersProvider nullProvider = null;
            var stubFormatter = Substitute.For<ISubtitleTextFormatter>();

            Assert.Throws<ArgumentNullException>(
                () => new SubtitlesTranslationCostCalculator(
                    nullProvider,
                    stubFormatter));
        }

        [Test]
        public void Constructor_NullFormatter_Throws()
        {
            var stubProvider = Substitute.For<ICostForOneMillionCharactersProvider>();
            ISubtitleTextFormatter nullFormatter = null;

            Assert.Throws<ArgumentNullException>(
                () => new SubtitlesTranslationCostCalculator(
                    stubProvider,
                    nullFormatter));
        }

        [Test]
        public void Format_Always_ReturnsCostRelatedToGivenCurrency()
        {
            var stubProvider = Substitute.For<ICostForOneMillionCharactersProvider>();
            stubProvider
                .GetCostIn(Arg.Is(Currency.Euro))
                .Returns(20);

            var stubFormatter = Substitute.For<ISubtitleTextFormatter>();
            stubFormatter
                .Format(Arg.Any<SubtitleText>())
                .Returns("25 characters long string");

            var calculator = CreateCalculator(stubProvider, stubFormatter);

            var twoSubtitles = new Subtitles(
                new List<Subtitle> {
                    SubtitleTests.CreateSubtitle1(),
                    SubtitleTests.CreateSubtitle2()
                });

            var expectedCost = new Price(0.001m, Currency.Euro);

            var actualCost = calculator.Calculate(twoSubtitles, Currency.Euro);

            Assert.AreEqual(expectedCost, actualCost);
        }

        [Test]
        public void Format_NullSubtitles_Throws()
        {
            Subtitles nullSubtitles = null;

            var calculator = CreateCalculator();

            Assert.Throws<ArgumentNullException>(
                () => calculator.Calculate(nullSubtitles, Currency.Euro));
        }

        private SubtitlesTranslationCostCalculator CreateCalculator()
        {
            return CreateCalculator(
                Substitute.For<ICostForOneMillionCharactersProvider>(),
                Substitute.For<ISubtitleTextFormatter>());
        }

        private SubtitlesTranslationCostCalculator CreateCalculator(
            ICostForOneMillionCharactersProvider provider,
            ISubtitleTextFormatter formatter)
        {
            return new SubtitlesTranslationCostCalculator(provider, formatter);
        }
    }
}
