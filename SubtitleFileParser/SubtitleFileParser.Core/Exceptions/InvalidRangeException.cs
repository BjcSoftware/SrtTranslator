using System;

namespace SubtitleFileParser.Core.Exceptions
{
    public class InvalidRangeException : ArgumentException
    {
        public InvalidRangeException()
        {
        }

        public InvalidRangeException(string message)
            : base(message)
        {
        }

        public InvalidRangeException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
