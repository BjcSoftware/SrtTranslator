using System.Collections.Generic;

namespace SubtitleFileParser.Core
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
