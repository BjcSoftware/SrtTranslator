using SrtTranslator.Core;
using SrtTranslator.DeepL.Exceptions;
using SrtTranslator.SubtitleFileParser.Exceptions;
using System;
using System.IO;

namespace SrtTranslator.ConsoleApplication
{
    public class ErrorHandlerSubtitlesFileTranslator
        : ICommandService<TranslateSubtitlesFileToNewFile>
    {
        private readonly ICommandService<TranslateSubtitlesFileToNewFile> decoratee;
        private readonly IUserNotifier notifier;

        public ErrorHandlerSubtitlesFileTranslator(
            ICommandService<TranslateSubtitlesFileToNewFile> decoratee,
            IUserNotifier notifier)
        {
            if (decoratee == null)
                throw new ArgumentNullException(nameof(decoratee));
            if (notifier == null)
                throw new ArgumentNullException(nameof(notifier));

            this.decoratee = decoratee;
            this.notifier = notifier;
        }

        public void Execute(TranslateSubtitlesFileToNewFile command)
        {
            try
            {
                decoratee.Execute(command);
            }
            catch (AuthenticationException e)
            {
                notifier.Notify($"The authentication key \"{e.Key}\" is not valid. Please check your key in your DeepL Pro dashboard.");
            }
            catch (InternalErrorException)
            {
                notifier.Notify("An internal error occured. Please try again later.");
            }
            catch (FileNotFoundException e)
            {
                notifier.Notify($"The file \"{e.FileName}\" does not exist. Please give a correct srt file to translate.");
            }
            catch(SubtitlesParsingException e)
            {
                notifier.Notify($"Unable to parse the file \"{command.ToTranslate}\" because of a problem in the subtitle number {e.IncorrectSubtitleId}.");
            }
        }
    }
}
