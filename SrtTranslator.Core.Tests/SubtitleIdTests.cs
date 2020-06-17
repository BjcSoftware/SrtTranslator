using NUnit.Framework;
using System;

namespace SrtTranslator.Core.Tests
{
    [TestFixture]
    public class SubtitleIdTests
        : BaseValueObjectTests<int>
    {
        protected override int Value1 => 1;

        protected override int Value2 => 2;

        [Test]
        [TestCase(0)]
        [TestCase(-1)]
        public void Constructor_LessThanOneId_ThrowsOutOfRangeException(int id)
        {
            Assert.Throws<ArgumentOutOfRangeException>(
                () => CreateValueObject(id));
        }

        protected override ValueObject<int> CreateValueObject(int value)
        {
            return new SubtitleId(value);
        }

        public static SubtitleId CreateId1()
        {
            return new SubtitleId(1);
        }

        public static SubtitleId CreateId2()
        {
            return new SubtitleId(2);
        }
    }
}
