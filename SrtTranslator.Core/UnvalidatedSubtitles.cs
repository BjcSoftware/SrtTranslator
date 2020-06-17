using System.Collections.Generic;

namespace SrtTranslator.Core
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
