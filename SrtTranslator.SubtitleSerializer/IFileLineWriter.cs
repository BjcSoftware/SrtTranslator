using SrtTranslator.Core;
using System.Collections.Generic;

namespace SrtTranslator.SubtitleSerializer
{
    public interface IFileLineWriter
    {
        void WriteLines(
            FilePath filePath, 
            IEnumerable<CharacterLine> lines);
    }
}
