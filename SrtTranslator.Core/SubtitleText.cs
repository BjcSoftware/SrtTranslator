using System.Collections.Generic;

namespace SrtTranslator.Core
{
    public class SubtitleText : EnumerableBasedValueObject<CharacterLine>
    {
        public SubtitleText(IEnumerable<CharacterLine> text)
            : base(text)
        {
        }
    }
}
