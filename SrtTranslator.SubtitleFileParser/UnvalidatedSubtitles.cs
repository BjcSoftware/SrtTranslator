using SrtTranslator.Core;
using System.Collections.Generic;

namespace SrtTranslator.SubtitleFileParser
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
