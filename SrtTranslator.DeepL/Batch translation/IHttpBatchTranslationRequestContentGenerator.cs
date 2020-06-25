using SrtTranslator.Core.Translator;
using System.Collections.Generic;
using System.Net.Http;

namespace SrtTranslator.DeepL
{
    public interface IHttpBatchTranslationRequestContentGenerator
    {
        HttpContent GenerateContent(List<string> batch, Language target);
    }
}
