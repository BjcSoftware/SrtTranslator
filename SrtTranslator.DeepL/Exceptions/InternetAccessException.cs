using System;

namespace SrtTranslator.DeepL.Exceptions
{
    public class InternetAccessException : Exception
    {
        public InternetAccessException()
        {
        }

        public InternetAccessException(string message)
            : base(message)
        {
        }

        public InternetAccessException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
