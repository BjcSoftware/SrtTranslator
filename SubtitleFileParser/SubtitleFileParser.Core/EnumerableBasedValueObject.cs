using System;
using System.Collections.Generic;
using System.Linq;

namespace SubtitleFileParser.Core
{
    public abstract class EnumerableBasedValueObject<ElemType>
        : ValueObject<IEnumerable<ElemType>>
    {
        public EnumerableBasedValueObject(IEnumerable<ElemType> value)
            : base(value)
        {
        }

        public override bool Equals(object obj)
        {
            return obj is EnumerableBasedValueObject<ElemType> @object &&
                Value.SequenceEqual(@object.Value);
        }
    }
}
