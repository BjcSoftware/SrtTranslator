using SrtTranslator.Core.Translator;
using System.Collections.Generic;
using System.Net.Http;

namespace SrtTranslator.DeepL
{
    public interface IHttpBatchTranslationRequestGenerator
    {
        HttpRequestMessage GenerateRequest(List<string> batch, Language target);
    }
}
