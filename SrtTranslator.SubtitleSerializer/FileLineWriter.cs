using SrtTranslator.Core;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace SrtTranslator.SubtitleSerializer
{
    public class FileLineWriter : IFileLineWriter
    {
        public void WriteLines(
            FilePath filePath, 
            IEnumerable<CharacterLine> lines)
        {
            File.WriteAllLines(
                filePath.Value, 
                lines.Select(
                    l => l.Value));
        }
    }
}
