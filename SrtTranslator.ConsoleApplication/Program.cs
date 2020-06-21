using SrtTranslator.Core;
using SrtTranslator.Core.Translator;
using SrtTranslator.DeepL;
using SrtTranslator.SubtitleFileParser;
using SrtTranslator.SubtitleFileParser.Exceptions;
using SrtTranslator.SubtitleSerializer;
using System;
using System.Collections.Generic;

namespace SrtTranslator.ConsoleApplication
{
    class Program
    {
        static void Main(string[] args)
        {
            var parser = CreateSubtitlesParser();

            Console.Write("Srt file path: ");
            var path = new FilePath(Console.ReadLine());

            Subtitles subtitles;
            try
            {
                subtitles = parser.Parse(path);
                var costCalculator = CreateDeepLCostCalculator();
                Console.WriteLine($"Translation cost = {costCalculator.Calculate(subtitles, Currency.Euro)}");

                Console.WriteLine("Do you wan't to translate the subtitles ?");
                var answer = Console.ReadLine();

                if(answer.Equals("yes"))
                {
                    Console.WriteLine("Please enter your DeepL authentication key: ");
                    var key = new AuthenticationKey(Console.ReadLine());
                    var subtitlesTranslator = CreateDeeplSubtitlesTranslator(key);

                    var translatedSubtitles = subtitlesTranslator.Translate(subtitles, Language.French, Language.English);

                    var serializer = CreateSerializer();
                    serializer.Serialize(translatedSubtitles, new FilePath("translation.srt"));
                }
            }
            catch(SubtitlesParsingException e)
            {
                Console.WriteLine(e.IncorrectSubtitleId);
                return;
            }
        }

        private static ISubtitlesTranslator CreateDeeplSubtitlesTranslator(AuthenticationKey authKey)
        {
            return new SubtitlesTranslator(
                new SubtitleTranslator(
                    CreateDeeplTextTranslator(authKey),
                    new SingleLineSubtitleTextFormatter()));
        }

        private static ITextTranslator CreateDeeplTextTranslator(AuthenticationKey authKey)
        {
            var textTranslator = new TextTranslator(
                new HttpTranslationRequester(
                    authKey,
                    new LanguageToCodeMapper(
                        new Dictionary<Language, string>
                        {
                            [Language.French] = "FR",
                            [Language.English] = "EN"
                        })),
                new JsonHttpTranslationResponseDeserializer(),
                authKey);

            return textTranslator;
        }

        private static SubtitlesFileParser CreateSubtitlesParser()
        {
            var intParser = new IntParser<SubcharacterLine>();

            return new SubtitlesFileParser(
                new UnvalidatedSubtitlesFileReader(
                    new FileLineReader()),
                new SubtitlesParser(
                    new SubtitleParser(
                        new IntBasedValueObjectParser<SubtitleId, CharacterLine>(new IntParser<CharacterLine>()),
                        new SubtitleTimestampsParser(
                            new TimestampParser(
                                new IntBasedValueObjectParser<HoursTimestamp, SubcharacterLine>(intParser),
                                new IntBasedValueObjectParser<MinutesTimestamp, SubcharacterLine>(intParser),
                                new IntBasedValueObjectParser<SecondsTimestamp, SubcharacterLine>(intParser),
                                new MillisecondsParser(
                                    new IntBasedValueObjectParser<MillisecondsTimestamp, SubcharacterLine>(intParser)),
                                new TimestampFormatValidator())))));
        }

        private static SubtitlesTranslationCostCalculator CreateDeepLCostCalculator()
        {
            return new SubtitlesTranslationCostCalculator(
                new InMemoryCostForOneMillionCharactersProvider(
                    new Dictionary<Currency, decimal>() {
                        [Currency.Euro] = 20m,
                        [Currency.US_Dollar] = 25m,
                        [Currency.CA_Dollar] = 30.03m
                    }),
                new SingleLineSubtitleTextFormatter());
        }

        private static SubtitlesSerializer CreateSerializer()
        {
            return new SubtitlesSerializer(
                new FileLineWriter(),
                new SubtitlesToUnvalidatedMapper(
                    new SubtitleToUnvalidatedMapper(
                        new SubtitleTimestampsToCharacterLineMapper())));
        }
    }
}
