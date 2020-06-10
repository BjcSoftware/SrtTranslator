using SubtitleFileParser.Core;
using System.Collections.Generic;

namespace SrtSubtitleFileParser
{
    public class UnvalidatedSubtitles 
        : EnumerableBasedValueObject<UnvalidatedSubtitle>
    {
        public UnvalidatedSubtitles(IEnumerable<UnvalidatedSubtitle> value)
            : base(value)
        {
        }
    }
}
