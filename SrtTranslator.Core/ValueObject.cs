using System;

namespace SrtTranslator.Core
{
    public abstract class ValueObject<T>
    {
        public T Value { get; private set; }

        public ValueObject(T value)
        {
            if (value == null)
                throw new ArgumentNullException(nameof(value));

            Value = value;
        }

        public override bool Equals(object obj)
        {
            return obj is ValueObject<T> valueObject &&
                   Value.Equals(valueObject.Value);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Value);
        }

        public override string ToString()
        {
            return Value.ToString();
        }
    }
}
