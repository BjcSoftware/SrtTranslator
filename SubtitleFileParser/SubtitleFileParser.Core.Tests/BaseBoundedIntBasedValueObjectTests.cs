using NUnit.Framework;
using System;

namespace SubtitleFileParser.Core.Tests
{
    public abstract class BaseBoundedIntBasedValueObjectTests :
        BaseValueObjectTests<int>
    {
        protected abstract NumericRange Range { get; }

        protected override int Value1 => Range.Min;

        protected override int Value2 => Range.Max;

        [Test]
        public void Constructor_OutOfRangeValue_Throws_1()
        {
            int outOfRangeValue = Range.Min - 1;
            Assert.Catch<ArgumentOutOfRangeException>(
                () => CreateValueObject(outOfRangeValue));
        }

        [Test]
        public void Constructor_OutOfRangeValue_Throws_2()
        {
            int outOfRangeValue = Range.Max + 1;
            Assert.Catch<ArgumentOutOfRangeException>(
                () => CreateValueObject(outOfRangeValue));
        }

        [Test]
        public void ToString_ReturnsValue()
        {
            var valueObject = CreateValueObject(Range.Min);

            Assert.AreEqual(Range.Min.ToString(), valueObject.ToString());
        }
    }
}
