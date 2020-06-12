using SubtitleFileParser.Core;
using System.Collections.Generic;

namespace SrtSubtitleFileParser
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
