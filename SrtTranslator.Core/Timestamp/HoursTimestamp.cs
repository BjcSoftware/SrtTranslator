namespace SrtTranslator.Core
{
    public class HoursTimestamp
        : BoundedIntBasedValueObject
    {
        public HoursTimestamp(int hours) 
            : base(hours, new NumericRange(0, 99))
        {
        }
    }
}
