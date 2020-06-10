using NUnit.Framework;
using System;

namespace SubtitleFileParser.Core.Tests
{
    public abstract class BaseNullableTypeBasedValueObjectTests<T>
        : BaseValueObjectTests<T>
        where T : class
    {
        [Test]
        public void Constructor_NullValue_Throws()
        {
            T nullValue = null;

            Assert.Throws<ArgumentNullException>(
                () => CreateValueObject(nullValue));
        }
    }
}
