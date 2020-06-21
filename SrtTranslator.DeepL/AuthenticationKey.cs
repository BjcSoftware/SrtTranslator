using SrtTranslator.Core;
using System;

namespace SrtTranslator.DeepL
{
    public class AuthenticationKey : ValueObject<string>
    {
        public AuthenticationKey(string authKey)
            : base(authKey)
        {
            if (string.IsNullOrWhiteSpace(authKey))
                throw new ArgumentNullException(nameof(authKey));
        }
    }
}
