using NUnit.Framework;
using SrtTranslator.Core.Translator;
using System.Collections.Generic;

namespace SrtTranslator.DeepL.Tests
{
    [TestFixture]
    public class InMemoryCostForOneMillionCharactersProviderTests
        : BaseInMemoryMapperTests<InMemoryCostForOneMillionCharactersProvider, Currency, decimal>
    {
        protected override Dictionary<Currency, decimal> Mapping => new Dictionary<Currency, decimal>() {
            [Currency.Euro] = 20,
            [Currency.British_Pound] = 18
        };

        protected override Currency NotRegisteredKey => Currency.Japanese_Yen;

        protected override InMemoryCostForOneMillionCharactersProvider CreateMapper(
            Dictionary<Currency, decimal> mapping)
        {
            return new InMemoryCostForOneMillionCharactersProvider(mapping);
        }

        protected override decimal GetValueAssociatedWithKey(
            InMemoryCostForOneMillionCharactersProvider obj, 
            Currency key)
        {
            return obj.GetCostIn(key);
        }
    }
}
