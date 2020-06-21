using System;

namespace SrtTranslator.DeepL.Exceptions
{
    public class QuotaExceededException : Exception
    {
        public QuotaExceededException()
        {
        }

        public QuotaExceededException(string message)
            : base(message)
        {
        }

        public QuotaExceededException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
