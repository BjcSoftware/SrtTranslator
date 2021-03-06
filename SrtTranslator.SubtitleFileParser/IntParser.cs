﻿using SrtTranslator.Core;
using SrtTranslator.SubtitleFileParser.Exceptions;
using System;

namespace SrtTranslator.SubtitleFileParser
{
    public class IntParser<TInput>
        : IParser<int, TInput>
        where TInput : ValueObject<string>
    {
        public int Parse(TInput input)
        {
            if (input == null)
                throw new ArgumentNullException(nameof(input));

            int value;
            if (int.TryParse(input.Value, out value))
            {
                return value;
            }
            else
            {
                throw new ParsingException("Invalid integer");
            }
        }
    }
}
