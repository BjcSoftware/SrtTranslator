using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SrtTranslator.DeepL.Tests
{
    public abstract class BaseInMemoryMapperTests<TObject, TKey, TValue>
    {
        protected abstract Dictionary<TKey, TValue> Mapping { get; }

        protected abstract TKey NotRegisteredKey { get; }

        [Test]
        public void Constructor_NullMapping_Throws()
        {
            Dictionary<TKey, TValue> nullMapping = null;

            Assert.Throws<ArgumentNullException>(
                () => CreateMapper(nullMapping));
        }

        [Test]
        public void Map_RegisteredKey_ReturnsCorrectValue_1()
        {
            var mapper = CreateMapper(Mapping);

            var expectedValue = Mapping[Mapping.Keys.First()];

            var actualValue = GetValueAssociatedWithKey(mapper, Mapping.Keys.First());

            Assert.AreEqual(
                expectedValue,
                actualValue);
        }

        [Test]
        public void Map_RegisteredKey_ReturnsCorrectValue_2()
        {
            var mapper = CreateMapper(Mapping);

            var expectedValue = Mapping[Mapping.Keys.Last()];

            var actualValue = GetValueAssociatedWithKey(mapper, Mapping.Keys.Last());

            Assert.AreEqual(
                expectedValue,
                actualValue);
        }

        [Test]
        public void Map_NotRegisteredKey_ThrowsArgumentException()
        {
            var mapper = CreateMapper(Mapping);

            Assert.Throws<ArgumentException>(
                () => GetValueAssociatedWithKey(mapper, NotRegisteredKey));
        }

        protected abstract TObject CreateMapper(Dictionary<TKey, TValue> mapping);

        protected abstract TValue GetValueAssociatedWithKey(TObject obj, TKey key);
    }
}
