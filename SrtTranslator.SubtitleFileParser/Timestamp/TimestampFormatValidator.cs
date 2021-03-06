﻿using SrtTranslator.Core;
using System.Text.RegularExpressions;

namespace SrtTranslator.SubtitleFileParser
{
    public class TimestampFormatValidator : ITimestampFormatValidator
    {
        private const string timestampRegex = @"^\s*(\d?\d):(\d?\d):(\d?\d)(,(\d?\d?\d))?\s*$";

        public bool IsFormatCorrect(SubcharacterLine subline)
        {
            return Regex.IsMatch(subline.Value, timestampRegex);
        }
    }
}
