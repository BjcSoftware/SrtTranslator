using SubtitleFileParser.Core;
using System;

namespace SrtSubtitleFileParser
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
