using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;

namespace SrtTranslator.DeepL
{
    public class JsonHttpBatchTranslationResponseDeserializer :
        IHttpBatchTranslationResponseDeserializer
    {
        public List<string> DeserializeTranslation(HttpResponseMessage response)
        {
            if (response == null)
                throw new ArgumentNullException(nameof(response));

            var responseContent = response.Content.ReadAsStringAsync().Result;
            var deserializedResponse = JsonConvert.DeserializeObject<TranslatorResponse>(responseContent);
            return deserializedResponse.Translations
                .Select(t => t["text"]).ToList();
        }

        private class TranslatorResponse
        {
            public List<Dictionary<string, string>> Translations { get; set; }
        }
    }
}
