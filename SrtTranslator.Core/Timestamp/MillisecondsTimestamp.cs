namespace SrtTranslator.Core
{
    public class MillisecondsTimestamp
        : BoundedIntBasedValueObject
    {
        public MillisecondsTimestamp(int milliseconds)
            : base(milliseconds, new NumericRange(0, 999))
        {
        }

        public override string ToString()
        {
            return $"{Value:000}";
        }
    }
}
