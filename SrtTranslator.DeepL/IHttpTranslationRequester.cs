using SrtTranslator.Core.Translator;
using System.Net.Http;

namespace SrtTranslator.DeepL
{
    public interface IHttpTranslationRequester
    {
        HttpResponseMessage Request(
            string textToTranslate, 
            Language target);
    }
}
