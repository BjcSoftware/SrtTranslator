using System;
using System.Linq;

namespace SrtTranslator.Core.Translator
{
    public class SingleLineSubtitleTextFormatter
        : ISubtitleTextFormatter
    {
        public string Format(SubtitleText textLines)
        {
            if (textLines == null)
                throw new ArgumentNullException(nameof(textLines));

            return string.Join(
                ' ',
                textLines.Value
                .Select(l => l.Value).ToList());
        }
    }
}
