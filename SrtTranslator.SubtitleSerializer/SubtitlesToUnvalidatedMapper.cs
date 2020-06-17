using SrtTranslator.Core;
using System;
using System.Linq;

namespace SrtTranslator.SubtitleSerializer
{
    public class SubtitlesToUnvalidatedMapper 
        : IMapper<UnvalidatedSubtitles, Subtitles>
    {
        private readonly IMapper<UnvalidatedSubtitle, Subtitle> subtitleMapper;

        public SubtitlesToUnvalidatedMapper(
            IMapper<UnvalidatedSubtitle, Subtitle> subtitleMapper)
        {
            if (subtitleMapper == null)
                throw new ArgumentNullException(nameof(subtitleMapper));

            this.subtitleMapper = subtitleMapper;
        }

        public UnvalidatedSubtitles Map(Subtitles subsToMap)
        {
            if (subsToMap == null)
                throw new ArgumentNullException(nameof(subsToMap));

            return new UnvalidatedSubtitles(
                subsToMap.Value
                .Select(s => subtitleMapper.Map(s))
                .ToList());
        }
    }
}
