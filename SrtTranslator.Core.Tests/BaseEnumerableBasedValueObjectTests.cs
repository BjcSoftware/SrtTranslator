using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SrtTranslator.Core.Tests
{
    public abstract class BaseEnumerableBasedValueObjectTests<T>
    {
        protected abstract IEnumerable<T> value1 { get; }
        protected abstract IEnumerable<T> value2 { get; }

        [Test]
        public void Constructor_NullValue_Throws()
        {
            IEnumerable<T> nullValue = null;

            Assert.Throws<ArgumentNullException>(
                () => CreateValueObject(nullValue));
        }

        [Test]
        public void Equals_SameValues_ReturnsTrue()
        {
            var valueObject1 = CreateValueObject(value1.ToList());
            var valueObject2 = CreateValueObject(value1.ToList());

            Assert.IsTrue(valueObject1.Equals(valueObject2));
        }

        [Test]
        public void Equals_DifferentValues_ReturnsFalse()
        {
            var valueObject1 = CreateValueObject(value1.ToList());
            var valueObject2 = CreateValueObject(value2.ToList());

            Assert.IsFalse(valueObject1.Equals(valueObject2));
        }

        [Test]
        public void Value_GivesCorrectValue()
        {
            var valueObject = CreateValueObject(value1);

            Assert.AreEqual(
                value1,
                valueObject.Value);
        }

        protected abstract EnumerableBasedValueObject<T> CreateValueObject(IEnumerable<T> value);
    }
}
