using SrtTranslator.Core;
using SrtTranslator.Core.Translator;
using SrtTranslator.DeepL;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SrtTranslator.ConsoleApplication
{
    class Program
    {
        static void Main(string[] args)
        {
            if(InvalidInputArgs(args))
            {
                ShowHelp();
                return;
            }

            var srtFileToTranslate = new FilePath(args[0]);
            Language targetLanguage = ParseLanguage(args[2]);
            var outputFile = new FilePath(args[4]);
            var authKey = new AuthenticationKey(args[6]);

            TranslateSubtitles(
                srtFileToTranslate,
                targetLanguage,
                outputFile,
                authKey);
        }

        private static void TranslateSubtitles(
            FilePath toTranslate,
            Language targetLanguage,
            FilePath outputFile,
            AuthenticationKey authKey)
        {
            var translator = AppComposer.CreateDeeplBatchSubtitlesFileTranslator(authKey);

            translator.Execute(
                new TranslateSubtitlesFileToNewFile(
                    toTranslate,
                    targetLanguage,
                    outputFile));
        }

        private static List<string> AvailableLanguages =
            Enum.GetValues(typeof(Language))
                .Cast<Language>()
                .Select(v => v.ToString().ToLower())
                .ToList();

        private static bool InvalidInputArgs(string[] args)
        {
            return args.Length != 7 ||
                args[1] != "-t" || args[3] != "-o" || args[5] != "-k" ||
                !AvailableLanguages.Contains(args[2].ToLower());
        }

        private static void ShowHelp()
        {
            Console.WriteLine();
            Console.WriteLine("usage: SrtTranslator file_to_translate.srt -t TARGET_LANG -o OUTPUT_FILE -k AUTH_KEY");
            Console.WriteLine();
            Console.WriteLine("TARGET_LANG must be one of the following languages:");
            foreach (var language in AvailableLanguages)
            {
                Console.WriteLine($"\t- {language}");
            }

            Console.WriteLine();
            Console.WriteLine("OUTPUT_FILE is the output file containing the translation.");
            Console.WriteLine();
            Console.WriteLine("AUTH_KEY is your authentication key for the API of DeepL, in the following format: xxxxxxxx-xxxx-xxxx-xxxx-xxxxxxxxxxxx.");
            Console.WriteLine();
        }

        private static Language ParseLanguage(string input)
        {
            string titleCaseInput = $"{char.ToUpper(input[0])}{string.Join("", input.Skip(1))}";
            return Enum.Parse<Language>(titleCaseInput);
        }
    }
}
