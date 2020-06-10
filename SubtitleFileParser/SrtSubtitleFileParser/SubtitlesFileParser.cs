using SubtitleFileParser.Core;
using SubtitleFileParser.Core.Exceptions;
using System;

namespace SrtSubtitleFileParser
{
    using ISubtitlesParser = IParser<Subtitles, UnvalidatedSubtitles>;

    public class SubtitlesFileParser : IParser<Subtitles, FilePath>
    {
        private readonly IUnvalidatedSubtitlesReader reader;
        private readonly ISubtitlesParser parser;

        public SubtitlesFileParser(
            IUnvalidatedSubtitlesReader reader,
            ISubtitlesParser parser)
        {
            if (reader == null)
                throw new ArgumentNullException(nameof(reader));
            if (parser == null)
                throw new ArgumentNullException(nameof(parser));

            this.reader = reader;
            this.parser = parser;
        }

        public Subtitles Parse(FilePath filePath)
        {
            if (filePath == null)
                throw new ArgumentNullException(nameof(filePath));

            return parser.Parse(ReadSubtitlesToParse(filePath));
        }

        private UnvalidatedSubtitles ReadSubtitlesToParse(FilePath filePath)
        {
            try
            {
                return reader.ReadUnvalidatedSubtitles(filePath);
            }
            catch(Exception)
            {
                throw new ParsingException();
            }
        }
    }
}
