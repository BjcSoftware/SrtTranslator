using SrtTranslator.Core.Translator;
using SrtTranslator.DeepL.Exceptions;
using System;
using System.Collections.Generic;
using System.Net;

namespace SrtTranslator.DeepL
{
    public class RestApiBatchTextTranslator : IBatchTextTranslator
    {
        private const int QuotaExceededHttpCode = 456;

        private readonly IHttpBatchTranslationRequestSender sender;
        private readonly IHttpBatchTranslationResponseDeserializer deserializer;
        private readonly AuthenticationKey authKey;

        public RestApiBatchTextTranslator(
            IHttpBatchTranslationRequestSender sender,
            IHttpBatchTranslationResponseDeserializer deserializer,
            AuthenticationKey authKey)
        {
            if (sender == null)
                throw new ArgumentNullException(nameof(sender));
            if (deserializer == null)
                throw new ArgumentNullException(nameof(deserializer));
            if (authKey == null)
                throw new ArgumentNullException(nameof(authKey));

            this.sender = sender;
            this.deserializer = deserializer;
            this.authKey = authKey;
        }

        public List<string> Translate(List<string> batch, Language target)
        {
            if (batch == null)
                throw new ArgumentNullException(nameof(batch));

            var response = sender.SendTranslationRequest(batch, target);

            if (response.IsSuccessStatusCode)
                return deserializer.DeserializeTranslation(response);
            else
                throw AppropriateException(response.StatusCode);
        }

        private Exception AppropriateException(HttpStatusCode statusCode)
        {
            switch (statusCode)
            {
                case HttpStatusCode.Forbidden:
                    return new AuthenticationException(authKey);
                case HttpStatusCode.TooManyRequests:
                    return new TooManyRequestsException();
                case (HttpStatusCode)QuotaExceededHttpCode:
                    return new QuotaExceededException();
                default:
                    return new InternalErrorException();
            }
        }
    }
}
