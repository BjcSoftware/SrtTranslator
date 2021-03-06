﻿using SrtTranslator.Core;
using System;

namespace SrtTranslator.SubtitleFileParser
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

            return parser.Parse(
                reader.ReadUnvalidatedSubtitles(filePath));
        }
    }
}
