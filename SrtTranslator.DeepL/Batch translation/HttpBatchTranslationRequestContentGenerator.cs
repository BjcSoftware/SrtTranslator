using SrtTranslator.Core.Translator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;

namespace SrtTranslator.DeepL
{
    public class HttpBatchTranslationRequestContentGenerator
        : IHttpBatchTranslationRequestContentGenerator
    {
        private readonly AuthenticationKey authKey;
        private readonly LanguageToCodeMapper mapper;

        public HttpBatchTranslationRequestContentGenerator(
            AuthenticationKey authKey,
            LanguageToCodeMapper mapper)
        {
            if (authKey == null)
                throw new ArgumentNullException(nameof(authKey));
            if (mapper == null)
                throw new ArgumentNullException(nameof(mapper));

            this.authKey = authKey;
            this.mapper = mapper;
        }

        public HttpContent GenerateContent(List<string> batch, Language target)
        {
            if (batch == null)
                throw new ArgumentNullException(nameof(batch));

            var parameters = new List<KeyValuePair<string, string>> {
                new KeyValuePair<string, string>("auth_key", authKey.Value),
                new KeyValuePair<string, string>("target_lang", $"{mapper.CodeAssociatedTo(target)}")
            };

            parameters.AddRange(
                batch
                    .Select(t => new KeyValuePair<string, string>("text", t))
                    .ToList());

            return new FormUrlEncodedContent(parameters);
        }
    }
}
