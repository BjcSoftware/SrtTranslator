using System;

namespace SubtitleFileParser.Core
{
    public abstract class BoundedIntBasedValueObject
        : ValueObject<int>
    {
        public BoundedIntBasedValueObject(int value, NumericRange range)
            : base(value)
        {
            if (range == null)
                throw new ArgumentNullException(nameof(range));
            if (!range.Contains(value))
                throw new ArgumentOutOfRangeException(nameof(value));
        }

        public override string ToString()
        {
            return Value.ToString();
        }
    }
}
