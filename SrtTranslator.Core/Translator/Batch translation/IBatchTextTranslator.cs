using System.Collections.Generic;

namespace SrtTranslator.Core.Translator
{
    public interface IBatchTextTranslator
    {
        List<string> Translate(
            List<string> batch, 
            Language target);
    }
}
