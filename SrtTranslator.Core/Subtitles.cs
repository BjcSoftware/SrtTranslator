using System.Collections.Generic;

namespace SrtTranslator.Core
{
    public class Subtitles
        : EnumerableBasedValueObject<Subtitle>
    {
        public Subtitles(IEnumerable<Subtitle> value)
            : base(value)
        {
        }
    }
}
