namespace SrtTranslator.Core
{
    public class SecondsTimestamp
        : BoundedIntBasedValueObject
    {
        public SecondsTimestamp(int seconds)
            : base(seconds, new NumericRange(0, 59))
        {
        }
    }
}
