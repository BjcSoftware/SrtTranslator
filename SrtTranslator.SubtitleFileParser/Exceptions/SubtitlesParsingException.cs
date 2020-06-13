using System;

namespace SrtTranslator.SubtitleFileParser.Exceptions
{
    public class SubtitlesParsingException : Exception
    {
        public int IncorrectSubtitleId { get; }

        public SubtitlesParsingException(int incorrectSubtitleId)
            : base(incorrectSubtitleId.ToString())
        {
            IncorrectSubtitleId = incorrectSubtitleId;
        }

        public SubtitlesParsingException()
        {
        }

        public SubtitlesParsingException(string message)
            : base(message)
        {
        }

        public SubtitlesParsingException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
