using SrtTranslator.Core;
using SrtTranslator.Core.Translator;
using SrtTranslator.SubtitleFileParser;
using SrtTranslator.SubtitleSerializer;
using System;

namespace SrtTranslator.ConsoleApplication
{
    public class SubtitlesFileTranslator
        : ICommandService<TranslateSubtitlesFileToNewFile>
    {
        private readonly IParser<Subtitles, FilePath> parser;
        private readonly ISubtitlesSerializer serializer;
        private readonly ISubtitlesTranslator translator;
        private readonly IEventHandler<SubtitlesFileSuccessfullyTranslated> handler;

        public SubtitlesFileTranslator(
            IParser<Subtitles, FilePath> parser,
            ISubtitlesSerializer serializer,
            ISubtitlesTranslator translator,
            IEventHandler<SubtitlesFileSuccessfullyTranslated> handler)
        {
            if (parser == null)
                throw new ArgumentNullException(nameof(parser));
            if (serializer == null)
                throw new ArgumentNullException(nameof(serializer));
            if (translator == null)
                throw new ArgumentNullException(nameof(translator));
            if (handler == null)
                throw new ArgumentNullException(nameof(translator));

            this.parser = parser;
            this.serializer = serializer;
            this.translator = translator;
            this.handler = handler;
        }

        public void Execute(TranslateSubtitlesFileToNewFile command)
        {
            serializer.Serialize(
                translator.Translate(
                    parser.Parse(command.ToTranslate),
                    command.TargetLanguage),
                command.OutputFile);

            handler.Handle(
                new SubtitlesFileSuccessfullyTranslated(
                    command.OutputFile));
        }
    }
}
