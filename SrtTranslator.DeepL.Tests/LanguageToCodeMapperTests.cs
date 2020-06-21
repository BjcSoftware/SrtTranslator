using NUnit.Framework;
using SrtTranslator.Core.Translator;
using System.Collections.Generic;

namespace SrtTranslator.DeepL.Tests
{
    [TestFixture]
    public class LanguageToCodeMapperTests
        : BaseInMemoryMapperTests<LanguageToCodeMapper, Language, string>
    {
        protected override Dictionary<Language, string> Mapping => new Dictionary<Language, string> {
            [Language.French] = "FR",
            [Language.German] = "DE"
        };

        protected override Language NotRegisteredKey => Language.Chinese;

        protected override LanguageToCodeMapper CreateMapper(Dictionary<Language, string> mapping)
        {
            return new LanguageToCodeMapper(mapping);
        }

        protected override string GetValueAssociatedWithKey(LanguageToCodeMapper obj, Language key)
        {
            return obj.CodeAssociatedTo(key);
        }
    }
}
