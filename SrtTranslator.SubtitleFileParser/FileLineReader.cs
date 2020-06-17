using SrtTranslator.Core;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace SrtTranslator.SubtitleFileParser
{
    public class FileLineReader : IFileLineReader
    {
        public List<CharacterLine> ReadAllLines(FilePath filePath)
        {
            return File.ReadAllLines(filePath.Value)
                .Select(s => new CharacterLine(s))
                .ToList();
        }
    }
}
