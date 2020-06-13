using NUnit.Framework;

namespace SrtTranslator.Core.Tests
{
    [TestFixture]
    public class MinutesTimestampTests : BaseBoundedIntBasedValueObjectTests
    {
        protected override NumericRange Range => new NumericRange(0, 59);

        protected override ValueObject<int> CreateValueObject(int value)
        {
            return new MinutesTimestamp(value);
        }
    }
}
