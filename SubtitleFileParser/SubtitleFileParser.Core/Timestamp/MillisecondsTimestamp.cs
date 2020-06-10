namespace SubtitleFileParser.Core
{
    public class MillisecondsTimestamp
        : BoundedIntBasedValueObject
    {
        public MillisecondsTimestamp(int milliseconds)
            : base(milliseconds, new NumericRange(0, 999))
        {
        }
    }
}
