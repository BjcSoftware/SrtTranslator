namespace SubtitleFileParser.Core
{
    public class MinutesTimestamp
        : BoundedIntBasedValueObject
    {
        public MinutesTimestamp(int minutes)
            : base(minutes, new NumericRange(0, 59))
        {
        }
    }
}
