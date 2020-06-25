using SrtTranslator.Core;
using SrtTranslator.Core.Translator;
using SrtTranslator.DeepL;
using SrtTranslator.SubtitleFileParser;
using SrtTranslator.SubtitleSerializer;
using System.Collections.Generic;
using System.Net.Http;

namespace SrtTranslator.ConsoleApplication
{
    public static class AppComposer
    {
        private static HttpClient httpClient = new HttpClient();

        private static string deeplTranslationApiUrl = "https://api.deepl.com/v2/translate";
        private static LanguageToCodeMapper deeplLanguageToCodeMapper = new LanguageToCodeMapper(
            new Dictionary<Language, string> {
                [Language.German] = "DE",
                [Language.English] = "EN",
                [Language.French] = "FR",
                [Language.Italian] = "IT",
                [Language.Japanese] = "JA",
                [Language.Spanish] = "ES",
                [Language.Dutch] = "NL",
                [Language.Polish] = "PL",
                [Language.Portuguese] = "PT-PT",
                [Language.Portuguese_Brazilian] = "PT-BR",
                [Language.Russian] = "RU",
                [Language.Chinese] = "ZH"
            });

        public static ICommandService<TranslateSubtitlesFileToNewFile> CreateDeeplBatchSubtitlesFileTranslator(
            AuthenticationKey authKey)
        {
            return new ErrorHandlerSubtitlesFileTranslator(
                new TranslationCostConfirmationDecorator(
                    new SubtitlesFileTranslator(
                        CreateSubtitlesParser(),
                        CreateSubtitlesSerializer(),
                        CreateDeeplBatchSubtitlesTranslator(authKey),
                        new SubtitlesSuccessfullyTranslatedUserNotifier(
                            new ConsoleUserNotifier())),
                    CreateDeeplSubtitlesFileTranslationCostCalculator(),
                    new ConsoleUserConfirmationService()),
                new ConsoleErrorUserNotifier());
        }

        private static SubtitlesFileCostCalculator CreateDeeplSubtitlesFileTranslationCostCalculator()
        {
            return new SubtitlesFileCostCalculator(
                CreateSubtitlesParser(),
                CreateDeeplCostCalculator());
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

        private static SubtitlesTranslationCostCalculator CreateDeeplCostCalculator()
        {
            return new SubtitlesTranslationCostCalculator(
                new InMemoryCostForOneMillionCharactersProvider(
                    new Dictionary<Currency, decimal>()
                    {
                        [Currency.Euro] = 20m,
                        [Currency.US_Dollar] = 25m,
                        [Currency.CA_Dollar] = 30.03m
                    }),
                new SingleLineSubtitleTextFormatter());
        }

        private static ISubtitlesSerializer CreateSubtitlesSerializer()
        {
            return new SubtitlesSerializer(
                new FileLineWriter(),
                new SubtitlesToUnvalidatedMapper(
                    new SubtitleToUnvalidatedMapper(
                        new SubtitleTimestampsToCharacterLineMapper())));
        }

        private static ISubtitlesTranslator CreateDeeplBatchSubtitlesTranslator(AuthenticationKey authKey)
        {
            const int deeplMaxBatchSize = 50;

            return new BatchSubtitlesTranslator(
                new SubtitleBatchTranslator(
                    CreateDeeplBatchTextTranslator(authKey),
                    new SingleLineSubtitleTextFormatter()),
                deeplMaxBatchSize);
        }

        private static IBatchTextTranslator CreateDeeplBatchTextTranslator(AuthenticationKey authKey)
        {
            var textTranslator = new RestApiBatchTextTranslator(
                new HttpBatchTranslationRequestSender(
                    new ApiHttpRequestSender(httpClient),
                    new HttpBatchTranslationRequestGenerator(
                        deeplTranslationApiUrl,
                        new HttpBatchTranslationRequestContentGenerator(
                            authKey,
                            deeplLanguageToCodeMapper))),
                new JsonHttpBatchTranslationResponseDeserializer(),
                authKey);

            return textTranslator;
        }
    }
}
