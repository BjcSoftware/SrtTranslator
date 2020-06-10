using SubtitleFileParser.Core.Exceptions;

namespace SubtitleFileParser.Core
{
    public class NumericRange
    {
        public int Min { get; private set; }
        public int Max { get; private set; }

        public NumericRange(int min, int max)
        {
            if (min > max)
                throw new InvalidRangeException("Min should be less than max");

            Min = min;
            Max = max;
        }

        public bool Contains(int value)
        {
            return Min <= value && value <= Max;
        }
    }
}