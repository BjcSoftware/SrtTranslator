using SubtitleFileParser.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SrtSubtitleFileParser
{
    public class UnvalidatedSubtitle 
        : EnumerableBasedValueObject<CharacterLine>
    {
        public UnvalidatedSubtitle(IEnumerable<CharacterLine> value)
            : base(value)
        {
            if (value.Count() < 3)
                throw new ArgumentException(nameof(value));
        }

        public CharacterLine TimestampsLine => Value.ElementAt(1);

        public List<CharacterLine> TextLines => Value.Skip(2).ToList();
    }
}
