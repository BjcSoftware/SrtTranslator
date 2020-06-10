using NUnit.Framework;

namespace SubtitleFileParser.Core.Tests
{
    [TestFixture]
    public class HoursTimestampTests : BaseBoundedIntBasedValueObjectTests
    {
        protected override NumericRange Range => new NumericRange(0, 99);

        protected override ValueObject<int> CreateValueObject(int value)
        {
            return new HoursTimestamp(value);
        }
    }
}
