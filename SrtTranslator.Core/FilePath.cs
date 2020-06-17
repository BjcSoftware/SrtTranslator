using System;
using System.IO;
using System.Linq;

namespace SrtTranslator.Core
{
    public class FilePath : ValueObject<string>
    {
        public FilePath(string filePath)
            : base(filePath)
        {
            if (string.IsNullOrWhiteSpace(filePath))
                throw new ArgumentNullException(nameof(filePath));

            if (Path.GetInvalidPathChars().Any(c => filePath.Contains(c)))
                throw new ArgumentException(
                    $"Path '{filePath}' contains invalid chars", 
                    nameof(filePath));
        }
    }
}
