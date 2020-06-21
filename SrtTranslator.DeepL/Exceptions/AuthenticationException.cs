using System;

namespace SrtTranslator.DeepL.Exceptions
{
    public class AuthenticationException : Exception
    {
        public AuthenticationKey Key { get; }

        public AuthenticationException()
        {
        }

        public AuthenticationException(string message)
            : base(message)
        {
        }

        public AuthenticationException(string message, Exception inner)
            : base(message, inner)
        {
        }

        public AuthenticationException(AuthenticationKey key)
            : base($"The key \"{key}\" is not valid. Please supply a valid authentication key.")
        {
            Key = key;
        }
    }
}
