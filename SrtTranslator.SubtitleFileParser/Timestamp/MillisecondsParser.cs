﻿using SrtTranslator.Core;
using System;

namespace SrtTranslator.SubtitleFileParser
{
    using BoundedIntParser = IntBasedValueObjectParser<MillisecondsTimestamp, SubcharacterLine>;

    public class MillisecondsParser
        : IParser<MillisecondsTimestamp, SubcharacterLine>
    {
        private readonly BoundedIntParser boundedIntParser;

        public MillisecondsParser(BoundedIntParser boundedIntParser)
        {
            if (boundedIntParser == null)
                throw new ArgumentNullException(nameof(boundedIntParser));

            this.boundedIntParser = boundedIntParser;
        }

        public MillisecondsTimestamp Parse(SubcharacterLine subline)
        {
            if (subline == null)
                throw new ArgumentNullException();

            if (subline.Value == string.Empty)
                return new MillisecondsTimestamp(0);

            return boundedIntParser.Parse(subline);
        }
    }
}