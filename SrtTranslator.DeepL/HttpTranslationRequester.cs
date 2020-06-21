using SrtTranslator.Core.Translator;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;

namespace SrtTranslator.DeepL
{
    public class HttpTranslationRequester : IHttpTranslationRequester
    {
        private const string apiUrl = "https://api.deepl.com/v2/translate";

        private readonly AuthenticationKey key;
        private readonly LanguageToCodeMapper mapper;

        public HttpTranslationRequester(
            AuthenticationKey key,
            LanguageToCodeMapper mapper)
        {
            if (key == null)
                throw new ArgumentNullException(nameof(key));
            if (mapper == null)
                throw new ArgumentNullException(nameof(mapper));

            this.key = key;
            this.mapper = mapper;
        }

        public HttpResponseMessage Request(string textToTranslate, Language target, Language source)
        {
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("Accept", "*/*");
                client.DefaultRequestHeaders.Add("User-Agent", "SrtSubtitle");
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/x-www-form-urlencoded"));

                var Parameters = new List<KeyValuePair<string, string>> {
                    new KeyValuePair<string, string>("auth_key", key.Value),
                    new KeyValuePair<string, string>("text", textToTranslate),
                    new KeyValuePair<string, string>("target_lang", $"{mapper.CodeAssociatedTo(target)}"),
                    new KeyValuePair<string, string>("source_lang", $"{mapper.CodeAssociatedTo(source)}")
                };

                var Request = new HttpRequestMessage(HttpMethod.Post, apiUrl) {
                    Content = new FormUrlEncodedContent(Parameters)
                };

                return client.SendAsync(Request).Result;
            }
        }
    }
}
