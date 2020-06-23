using SrtTranslator.Core;
using System;

namespace SrtTranslator.ConsoleApplication
{
    public class SubtitlesSuccessfullyTranslatedUserNotifier
        : IEventHandler<SubtitlesFileSuccessfullyTranslated>
    {
        private readonly IUserNotifier notifier;

        public SubtitlesSuccessfullyTranslatedUserNotifier(IUserNotifier notifier)
        {
            if (notifier == null)
                throw new ArgumentNullException(nameof(notifier));

            this.notifier = notifier;
        }

        public void Handle(SubtitlesFileSuccessfullyTranslated e)
        {
            notifier.Notify($"Subtitles successfully translated to \"{e.OutputFile}\".");
        }
    }
}
