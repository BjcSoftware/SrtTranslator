using SrtTranslator.Core;
using System.Collections.Generic;

namespace SrtTranslator.SubtitleFileParser
{
    public class UnvalidatedSubtitle 
        : EnumerableBasedValueObject<CharacterLine>
    {
        public UnvalidatedSubtitle(IEnumerable<CharacterLine> value)
            : base(value)
        {
        }
    }
}
