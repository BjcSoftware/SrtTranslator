using SrtTranslator.Core;
using System;
using System.Linq;

namespace SrtTranslator.SubtitleFileParser
{
    public class CharacterLine : ValueObject<string>
    {
        public CharacterLine(string value) 
            : base(value)
        {
            if (value.ContainsMultipleLines())
                throw new ArgumentException(nameof(value));
        }

        public SubcharacterLine[] Split(string separator)
        {
            return Value
                .Split(separator)
                .Select(s => new SubcharacterLine(s)).ToArray();
        }
    }

    static class StringExtensionMethod
    {
        public static bool ContainsMultipleLines(this string s)
        {
            return s.Split('\n').Count() > 1;
        }
    }
}
