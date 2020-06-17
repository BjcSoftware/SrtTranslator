using System.Collections.Generic;

namespace SrtTranslator.Core
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
