using SrtTranslator.Core.Translator;
using System.Collections.Generic;
using System.Net.Http;

namespace SrtTranslator.DeepL
{
    public interface IHttpBatchTranslationRequestSender
    {
        HttpResponseMessage SendTranslationRequest(
            List<string> batch,
            Language target);
    }
}
