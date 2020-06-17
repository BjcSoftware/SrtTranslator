using SrtTranslator.Core;
using System;
using System.Collections.Generic;

namespace SrtTranslator.SubtitleSerializer
{
    using ITimestampsMapper = IMapper<CharacterLine, SubtitleTimestamps>;

    public class SubtitleToUnvalidatedMapper
        : IMapper<UnvalidatedSubtitle, Subtitle>
    {
        private readonly ITimestampsMapper timestampsMapper;

        public SubtitleToUnvalidatedMapper(
            ITimestampsMapper timestampsMapper)
        {
            if (timestampsMapper == null)
                throw new ArgumentNullException(nameof(timestampsMapper));

            this.timestampsMapper = timestampsMapper;
        }

        public UnvalidatedSubtitle Map(Subtitle subToMap)
        {
            if (subToMap == null)
                throw new ArgumentNullException(nameof(subToMap));

            return new UnvalidatedSubtitle(
                ConvertSubtitleToLines(subToMap));
        }

        private List<CharacterLine> ConvertSubtitleToLines(Subtitle subtitle)
        {
            var lines = new List<CharacterLine> {
                new CharacterLine(subtitle.Id.ToString()),
                timestampsMapper.Map(subtitle.Timestamps)
            };

            lines.AddRange(subtitle.Text.Value);

            return lines;
        }
    }
}
