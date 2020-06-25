using SrtTranslator.Core.Translator;
using System;
using System.Collections.Generic;
using System.Net.Http;

namespace SrtTranslator.DeepL
{
    public class HttpBatchTranslationRequestSender 
        : IHttpBatchTranslationRequestSender
    {
        private readonly IHttpRequestSender sender;
        private readonly IHttpBatchTranslationRequestGenerator generator;

        public HttpBatchTranslationRequestSender(
            IHttpRequestSender sender,
            IHttpBatchTranslationRequestGenerator generator)
        {
            if (sender == null)
                throw new ArgumentNullException(nameof(sender));
            if (generator == null)
                throw new ArgumentNullException(nameof(generator));

            this.sender = sender;
            this.generator = generator;
        }

        public HttpResponseMessage SendTranslationRequest(
            List<string> batch,
            Language target)
        {
            if (batch == null)
                throw new ArgumentNullException(nameof(batch));

            return sender.SendRequest(generator.GenerateRequest(batch, target));
        }
    }
}
