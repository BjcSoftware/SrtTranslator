using NUnit.Framework;
using SubtitleFileParser.Core;
using SubtitleFileParser.Core.Tests;
using System;

namespace SrtSubtitleFileParser.Tests
{
    [TestFixture]
    public class SubcharacterLineTests
        : BaseNullableTypeBasedValueObjectTests<string>
    {
        protected override string Value1 => "123";

        protected override string Value2 => "456";

        [Test]
        [TestCase("\n")]
        [TestCase("Line\n")]
        public void Constructor_ValueContainsNewLineChar_Throws(string value)
        {
            Assert.Throws<ArgumentException>(
                () => new SubcharacterLine(value));
        }

        protected override ValueObject<string> CreateValueObject(string value)
        {
            return new SubcharacterLine(value);
        }
    }
}
