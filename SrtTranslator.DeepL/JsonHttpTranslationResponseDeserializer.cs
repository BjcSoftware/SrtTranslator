using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;

namespace SrtTranslator.DeepL
{
    public class JsonHttpTranslationResponseDeserializer
        : IHttpTranslationResponseDeserializer
    {
        public string DeserializeTranslation(HttpResponseMessage response)
        {
            if (response == null)
                throw new ArgumentNullException(nameof(response));

            var responseContent = response.Content.ReadAsStringAsync().Result;
            var deserializedResponse = JsonConvert.DeserializeObject<TranslatorResponse>(responseContent);
            return deserializedResponse.Translations[0]["text"];
        }

        private class TranslatorResponse
        {
            public List<Dictionary<string, string>> Translations { get; set; }
        }
    }
}
