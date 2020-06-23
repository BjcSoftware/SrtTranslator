using SrtTranslator.Core;
using System;

namespace SrtTranslator.ConsoleApplication
{
    public class SubtitlesFileSuccessfullyTranslated
    {
        public FilePath OutputFile { get; }

        public SubtitlesFileSuccessfullyTranslated(FilePath outputFile)
        {
            if (outputFile == null)
                throw new ArgumentNullException(nameof(outputFile));

            OutputFile = outputFile;
        }

        public override bool Equals(object obj)
        {
            return obj is SubtitlesFileSuccessfullyTranslated translated &&
                   OutputFile.Equals(translated.OutputFile);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(OutputFile);
        }
    }
}
