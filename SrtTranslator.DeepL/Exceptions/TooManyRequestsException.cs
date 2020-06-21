using System;

namespace SrtTranslator.DeepL.Exceptions
{
    public class TooManyRequestsException : Exception
    {
        public TooManyRequestsException()
        {
        }

        public TooManyRequestsException(string message)
            : base(message)
        {
        }

        public TooManyRequestsException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
