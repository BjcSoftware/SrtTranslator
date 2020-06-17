using NUnit.Framework;
using System;

namespace SrtTranslator.Core.Tests
{
    [TestFixture]
    public class CharacterLineTests : 
        BaseNullableTypeBasedValueObjectTests<string>
    {
        protected override string Value1 => "A line";

        protected override string Value2 => "Another line";

        [Test]
        [TestCase("FirstLine\nSecondLine")]
        [TestCase("FirstLine\n\n")]
        public void Constructor_MultipleLines_Throws(string multipleLines)
        {
            Assert.Catch<ArgumentException>(
                () => CreateValueObject(multipleLines));
        }

        protected override ValueObject<string> CreateValueObject(string value)
        {
            return new CharacterLine(value);
        }
    }
}
