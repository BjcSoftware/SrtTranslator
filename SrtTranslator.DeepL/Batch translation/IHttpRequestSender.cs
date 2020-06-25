using System.Net.Http;

namespace SrtTranslator.DeepL
{
    public interface IHttpRequestSender
    {
        HttpResponseMessage SendRequest(HttpRequestMessage request);
    }
}
