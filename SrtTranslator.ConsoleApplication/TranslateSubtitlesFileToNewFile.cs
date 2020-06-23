using SrtTranslator.Core;
using SrtTranslator.Core.Translator;
using System;

namespace SrtTranslator.ConsoleApplication
{
    public class TranslateSubtitlesFileToNewFile
    {
        public FilePath ToTranslate { get; }
        public Language TargetLanguage { get; }
        public FilePath OutputFile { get; }

        public TranslateSubtitlesFileToNewFile(
            FilePath toTranslate,
            Language targetLanguage,
            FilePath outputFile)
        {
            ToTranslate = toTranslate;
            TargetLanguage = targetLanguage;
            OutputFile = outputFile;
        }

        public override bool Equals(object obj)
        {
            return obj is TranslateSubtitlesFileToNewFile file &&
                   ToTranslate.Equals(file.ToTranslate) &&
                   TargetLanguage.Equals(file.TargetLanguage) &&
                   OutputFile.Equals(file.OutputFile);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(ToTranslate, TargetLanguage, OutputFile);
        }
    }
}
