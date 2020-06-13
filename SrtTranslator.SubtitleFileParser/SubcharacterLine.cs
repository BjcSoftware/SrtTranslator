using SrtTranslator.Core;
using System;

namespace SrtTranslator.SubtitleFileParser
{
    public class SubcharacterLine : ValueObject<string>
    {
        private const char newLineCharacter = '\n';

        public SubcharacterLine(string value) 
            : base(value)
        {
            if (value.Contains(newLineCharacter))
                throw new ArgumentException(nameof(value));
        }
    }
}
