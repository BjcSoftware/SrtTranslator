using SrtTranslator.Core.Translator;
using System;
using System.Collections.Generic;
using System.Net.Http;

namespace SrtTranslator.DeepL
{
    public class HttpBatchTranslationRequestGenerator 
        : IHttpBatchTranslationRequestGenerator
    {
        private readonly string apiUrl;
        private readonly IHttpBatchTranslationRequestContentGenerator generator;

        public HttpBatchTranslationRequestGenerator(
            string apiUrl,
            IHttpBatchTranslationRequestContentGenerator generator)
        {
            if (apiUrl == null)
                throw new ArgumentNullException(nameof(apiUrl));
            if (generator == null)
                throw new ArgumentNullException(nameof(generator));

            this.apiUrl = apiUrl;
            this.generator = generator;
        }

        public HttpRequestMessage GenerateRequest(List<string> batch, Language target)
        {
            if (batch == null)
                throw new ArgumentNullException(nameof(batch));

            return new HttpRequestMessage(HttpMethod.Post, apiUrl) {
                Content = generator.GenerateContent(batch, target)
            };
        }
    }
}
