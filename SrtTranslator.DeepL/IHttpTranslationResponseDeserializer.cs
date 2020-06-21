using System.Net.Http;

namespace SrtTranslator.DeepL
{
    public interface IHttpTranslationResponseDeserializer
    {
        string DeserializeTranslation(HttpResponseMessage response);
    }
}
