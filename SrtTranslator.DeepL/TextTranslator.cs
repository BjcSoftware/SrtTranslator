using SrtTranslator.Core.Translator;
using System;
using System.Net;
using SrtTranslator.DeepL.Exceptions;

namespace SrtTranslator.DeepL
{
    public class TextTranslator : ITextTranslator
    {
        private const int QuotaExceededHttpCode = 456;

        private readonly IHttpTranslationRequester httpRequester;
        private readonly IHttpTranslationResponseDeserializer deserializer;
        private readonly AuthenticationKey key;

        public TextTranslator(
            IHttpTranslationRequester httpRequester,
            IHttpTranslationResponseDeserializer deserializer,
            AuthenticationKey key)
        {
            if (httpRequester == null)
                throw new ArgumentNullException(nameof(httpRequester));
            if (deserializer == null)
                throw new ArgumentNullException(nameof(deserializer));
            if (key == null)
                throw new ArgumentNullException(nameof(key));

            this.httpRequester = httpRequester;
            this.deserializer = deserializer;
            this.key = key;
        }

        public string TranslateText(string textToTranslate, Language target, Language source)
        {
            if (textToTranslate == null)
                throw new ArgumentNullException(nameof(textToTranslate));

            if (textToTranslate.Trim() == string.Empty)
                return string.Empty;

            var response = httpRequester.Request(textToTranslate, target, source);

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
                    return new AuthenticationException(key);
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