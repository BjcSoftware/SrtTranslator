using System;

namespace SrtSubtitleFileParser.Exceptions
{
    public class SubtitlesReadingException : Exception
    {
        public SubtitlesReadingException()
        {
        }

        public SubtitlesReadingException(string message)
            : base(message)
        {
        }

        public SubtitlesReadingException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
