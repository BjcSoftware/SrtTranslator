namespace SrtTranslator.Core
{
    public class HoursTimestamp
        : BoundedIntBasedValueObject
    {
        public HoursTimestamp(int hours) 
            : base(hours, new NumericRange(0, 99))
        {
        }

        public override string ToString()
        {
            return $"{Value:00}";
        }
    }
}
