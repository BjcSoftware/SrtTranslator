using SrtTranslator.Core.Translator;
using System;
using System.Collections.Generic;

namespace SrtTranslator.DeepL
{
    public class LanguageToCodeMapper
    {
        private readonly Dictionary<Language, string> mapping;

        public LanguageToCodeMapper(
            Dictionary<Language, string> mapping)
        {
            if (mapping == null)
                throw new ArgumentNullException(nameof(mapping));

            this.mapping = mapping;
        }

        public string CodeAssociatedTo(Language language)
        {
            if (!mapping.ContainsKey(language))
                throw new ArgumentException(
                    $"The Language {language} is not registered, give it a code from this class's constructor.",
                    nameof(language));

            return mapping[language];
        }
    }
}
