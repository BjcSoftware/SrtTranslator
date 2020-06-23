using SrtTranslator.Core;
using SrtTranslator.Core.Translator;
using SrtTranslator.DeepL;
using SrtTranslator.SubtitleFileParser;
using SrtTranslator.SubtitleSerializer;
using System.Collections.Generic;

namespace SrtTranslator.ConsoleApplication
{
    public static class AppComposer
    {
        public static ICommandService<TranslateSubtitlesFileToNewFile> CreateDeeplSubtitlesFileTranslator(
            AuthenticationKey authKey)
        {
            return new ErrorHandlerSubtitlesFileTranslator(
                new TranslationCostConfirmationDecorator(
                    new SubtitlesFileTranslator(
                        CreateSubtitlesParser(),
                        CreateSubtitlesSerializer(),
                        CreateDeeplSubtitlesTranslator(authKey),
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
                        })),
                new JsonHttpTranslationResponseDeserializer(),
                authKey);

            return textTranslator;
        }
    }
}
