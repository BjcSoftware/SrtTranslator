using NUnit.Framework;

namespace SubtitleFileParser.Core.Tests
{
    [TestFixture]
    public class SubtitleTextTests
        : BaseNullableTypeBasedValueObjectTests<string>
    {
        protected override string Value1 => "a line\nanother line";

        protected override string Value2 => "some other line";

        protected override ValueObject<string> CreateValueObject(string value)
        {
            return new SubtitleText(value);
        }
    }
}
