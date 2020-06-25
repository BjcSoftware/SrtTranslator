using System.Collections.Generic;
using System.Net.Http;

namespace SrtTranslator.DeepL
{
    public interface IHttpBatchTranslationResponseDeserializer
    {
        List<string> DeserializeTranslation(HttpResponseMessage response);
    }
}
