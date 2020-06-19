using SrtTranslator.Core.Translator;
using System;

namespace SrtTranslator.Core.Translation
{
    public class Price
    {
        public decimal Amount { get; private set; }
        public Currency Currency { get; private set; }
        
        public Price(decimal amount, Currency currency)
        {
            if (amount < 0)
                throw new ArgumentException(
                    "A price amount cannot be negative",
                    nameof(currency));

            Amount = amount;
            Currency = currency;
        }
        
        public override string ToString()
        {
            return $"{Amount} {Currency}";
        }

        public override bool Equals(object obj)
        {
            return obj is Price price &&
                   Amount == price.Amount &&
                   Currency == price.Currency;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Amount, Currency);
        }
    }
}
