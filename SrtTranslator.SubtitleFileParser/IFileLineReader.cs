using SrtTranslator.Core;
using System.Collections.Generic;

namespace SrtTranslator.SubtitleFileParser
{
    public interface IFileLineReader
    {
        List<CharacterLine> ReadAllLines(FilePath filePath);
    }
}
