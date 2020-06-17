using SrtTranslator.Core;
using System;

namespace SrtTranslator.SubtitleSerializer
{
    public class SubtitleTimestampsToCharacterLineMapper
        : IMapper<CharacterLine, SubtitleTimestamps>
    {
        public CharacterLine Map(SubtitleTimestamps timestamps)
        {
            if (timestamps == null)
                throw new ArgumentNullException(nameof(timestamps));

            return new CharacterLine($"{timestamps.Start} --> {timestamps.End}");
        }
    }
}
