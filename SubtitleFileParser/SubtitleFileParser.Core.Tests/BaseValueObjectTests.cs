using NUnit.Framework;

namespace SubtitleFileParser.Core.Tests
{
    public abstract class BaseValueObjectTests<T>
    {
        protected abstract T Value1 { get; }
        protected abstract T Value2 { get; }

        [Test]
        public void Value_GivesCorrectValue()
        {
            var valueObject = CreateValueObject(Value1);
            
            Assert.AreEqual(Value1, valueObject.Value);
        }

        [Test]
        public void Equals_EqualValues_ReturnsTrue()
        {
            var valueObject1 = CreateValueObject(Value1);
            var valueObject2 = CreateValueObject(Value1);

            Assert.IsTrue(valueObject1.Equals(valueObject2));
        }

        [Test]
        public void Equals_DifferentValues_ReturnsFalse()
        {
            var valueObject1 = CreateValueObject(Value1);
            var valueObject2 = CreateValueObject(Value2);

            Assert.IsFalse(valueObject1.Equals(valueObject2));
        }

        protected abstract ValueObject<T> CreateValueObject(T value);
    }
}
