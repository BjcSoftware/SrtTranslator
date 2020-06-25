using System.Collections.Generic;

namespace SrtTranslator.Core.Translator
{
    public interface ISubtitleBatchTranslator
    {
        List<Subtitle> Translate(
            List<Subtitle> batch,
            Language target);
    }
}
