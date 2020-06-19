using NUnit.Framework;
using SrtTranslator.Core.Translator;
using System;
using System.Collections.Generic;

namespace SrtTranslator.DeepL.Tests
{
    [TestFixture]
    public class InMemoryCostForOneMillionCharactersProviderTests
    {
        [Test]
        public void Constructor_NullMapping_Throws()
        {
            Dictionary<Currency, decimal> nullMapping = null;

            Assert.Throws<ArgumentNullException>(
                () => new InMemoryCostForOneMillionCharactersProvider(nullMapping));
        }

        [Test]
        public void GetCostFor_RegisteredCurrency_ReturnsCorrectCost()
        {
            var mapping = new Dictionary<Currency, decimal>()
            {
                [Currency.Euro] = 20,
                [Currency.British_Pound] = 18
            };

            var provider = CreateProvider(mapping);

            var actualCost = provider.GetCostIn(Currency.Euro);

            Assert.AreEqual(
                20,
                actualCost);
        }

        [Test]
        public void GetCostFor_NotRegisteredCurrency_ThrowsArgumentException()
        {
            var mapping = new Dictionary<Currency, decimal>()
            {
                [Currency.Euro] = 20,
                [Currency.British_Pound] = 18
            };

            var provider = CreateProvider(mapping);
            
            Assert.Throws<ArgumentException>(
                () => provider.GetCostIn(Currency.CA_Dollar));
        }

        private InMemoryCostForOneMillionCharactersProvider CreateProvider(
            Dictionary<Currency, decimal> mapping)
        {
            return new InMemoryCostForOneMillionCharactersProvider(mapping);
        }
    }
}
