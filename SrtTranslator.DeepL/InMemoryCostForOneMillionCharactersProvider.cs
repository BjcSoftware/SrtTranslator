using SrtTranslator.Core.Translator;
using System;
using System.Collections.Generic;

namespace SrtTranslator.DeepL
{
    public class InMemoryCostForOneMillionCharactersProvider
        : ICostForOneMillionCharactersProvider
    {
        private readonly Dictionary<Currency, decimal> mapping;

        public InMemoryCostForOneMillionCharactersProvider(
            Dictionary<Currency, decimal> mapping)
        {
            if (mapping == null)
                throw new ArgumentNullException(nameof(mapping));

            this.mapping = mapping;
        }

        public decimal GetCostIn(Currency currency)
        {
            if (!mapping.ContainsKey(currency))
                throw new ArgumentException(
                    $"The currency {currency} is not registered, give it a cost from this class's constructor.",
                    nameof(currency));

            return mapping[currency];
        }
    }
}
