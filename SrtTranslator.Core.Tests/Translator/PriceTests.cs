using NUnit.Framework;
using SrtTranslator.Core.Translation;
using System;

namespace SrtTranslator.Core.Translator.Tests
{
    [TestFixture]
    public class PriceTests
    {
        [Test]
        [TestCase(-1)]
        [TestCase(-5)]
        public void Constructor_NegativeAmount_Throws(decimal amount)
        {
            Assert.Throws<ArgumentException>(
                () => new Price(amount, Currency.Euro));
        }

        [Test]
        public void Equals_EqualPrices_ReturnsTrue()
        {
            var price1 = new Price(5, Currency.Euro);
            var price2 = new Price(5, Currency.Euro);

            Assert.IsTrue(price1.Equals(price2));
        }

        [Test]
        public void Equals_DifferentAmount_ReturnsFalse()
        {
            var price1 = new Price(5, Currency.Euro);
            var price2 = new Price(10, Currency.Euro);

            Assert.IsFalse(price1.Equals(price2));
        }

        [Test]
        public void Equals_DifferentCurrency_ReturnsFalse()
        {
            var price1 = new Price(5, Currency.Euro);
            var price2 = new Price(5, Currency.British_Pound);

            Assert.IsFalse(price1.Equals(price2));
        }
    }
}
