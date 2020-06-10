using System;

namespace SubtitleFileParser.Core
{
    public class FilePath : ValueObject<string>
    {
        public FilePath(string filePath)
            : base(filePath)
        {
            if (string.IsNullOrEmpty(filePath))
                throw new ArgumentNullException(nameof(filePath));
        }
    }
}
