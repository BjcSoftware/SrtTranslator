using System;

namespace SrtTranslator.Core
{
    public class SubtitleId
        : ValueObject<int>
    {
        public SubtitleId(int id)
            : base(id)
        {
            if (id < 1)
                throw new ArgumentOutOfRangeException(
                    "A subtitle ID should be greater than 0", 
                    nameof(id));
        }
    }
}
