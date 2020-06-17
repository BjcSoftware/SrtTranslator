using SrtTranslator.Core;
using System;
using System.Collections.Generic;

namespace SrtTranslator.SubtitleSerializer
{
    public class SubtitlesSerializer : ISubtitlesSerializer
    {
        private static readonly CharacterLine subtitleSeparator = new CharacterLine(string.Empty);

        private readonly IFileLineWriter writer;
        private readonly IMapper<UnvalidatedSubtitles, Subtitles> mapper;

        public SubtitlesSerializer(
            IFileLineWriter writer, 
            IMapper<UnvalidatedSubtitles, Subtitles> mapper)
        {
            if (writer == null)
                throw new ArgumentNullException(nameof(writer));
            if (mapper == null)
                throw new ArgumentNullException(nameof(mapper));

            this.writer = writer;
            this.mapper = mapper;
        }

        public void Serialize(Subtitles subtitles, FilePath filePath)
        {
            if (subtitles == null)
                throw new ArgumentNullException(nameof(subtitles));
            if (filePath == null)
                throw new ArgumentNullException(nameof(filePath));

            writer.WriteLines(
                filePath, 
                ConvertSubtitlesToLines(subtitles));
        }

        private List<CharacterLine> ConvertSubtitlesToLines(Subtitles subtitles)
        {
            var lines = new List<CharacterLine>();

            var serializableSubs = mapper.Map(subtitles);
            foreach (var subtitleLines in serializableSubs.Value)
            {
                lines.AddRange(subtitleLines.Value);
                lines.Add(subtitleSeparator);
            }

            return lines;
        }
    }
}
