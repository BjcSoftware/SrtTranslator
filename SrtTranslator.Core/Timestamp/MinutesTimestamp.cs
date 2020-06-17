namespace SrtTranslator.Core
{
    public class MinutesTimestamp
        : BoundedIntBasedValueObject
    {
        public MinutesTimestamp(int minutes)
            : base(minutes, new NumericRange(0, 59))
        {
        }

        public override string ToString()
        {
            return $"{Value:00}";
        }
    }
}
