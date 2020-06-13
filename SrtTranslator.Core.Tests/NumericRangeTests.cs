using NUnit.Framework;
using SrtTranslator.Core.Exceptions;

namespace SrtTranslator.Core.Tests
{
    [TestFixture]
    public class NumericRangeTests
    {
        [Test]
        public void Construcor_MinGreaterThanMax_Throws()
        {
            Assert.Throws<InvalidRangeException>(
                () => new NumericRange(min: 10, max: 5));
        }

        [Test]
        public void Min_GivesMin()
        {
            var range = new NumericRange(min: 5, max: 10);

            Assert.AreEqual(5, range.Min);
        }

        [Test]
        public void Max_GivesMax()
        {
            var range = new NumericRange(min: 5, max: 10);

            Assert.AreEqual(10, range.Max);
        }

        [Test]
        [TestCase(5)]
        [TestCase(10)]
        [TestCase(8)]
        public void Contains_ValueInRange_ReturnsTrue(int valueInRange)
        {
            var range = new NumericRange(5, 10);

            Assert.IsTrue(range.Contains(valueInRange));
        }

        [Test]
        [TestCase(4)]
        [TestCase(11)]
        public void Contains_ValueNotInRange_ReturnsFalse(int valueNotInRange)
        {
            var range = new NumericRange(5, 10);

            Assert.IsFalse(range.Contains(valueNotInRange));
        }
    }
}
