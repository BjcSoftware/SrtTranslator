using NUnit.Framework;

namespace SrtTranslator.Core.Tests
{
    [TestFixture]
    public class MillisecondsTimestampTests : BaseBoundedIntBasedValueObjectTests
    {
        protected override NumericRange Range => new NumericRange(0, 999);

        protected override ValueObject<int> CreateValueObject(int value)
        {
            return new MillisecondsTimestamp(value);
        }
    }
}
