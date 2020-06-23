using SrtTranslator.Core;
using System;

namespace SrtTranslator.ConsoleApplication
{
    public class TranslationCostConfirmationDecorator
        : ICommandService<TranslateSubtitlesFileToNewFile>
    {
        private readonly ICommandService<TranslateSubtitlesFileToNewFile> decoratee;
        private readonly ISubtitlesFileCostCalculator costCalculator;
        private readonly IUserConfirmationService confirmationService;

        public TranslationCostConfirmationDecorator(
            ICommandService<TranslateSubtitlesFileToNewFile> decoratee,
            ISubtitlesFileCostCalculator costCalculator,
            IUserConfirmationService confirmationService)
        {
            if (decoratee == null)
                throw new ArgumentNullException(nameof(decoratee));
            if (costCalculator == null)
                throw new ArgumentNullException(nameof(decoratee));
            if (confirmationService == null)
                throw new ArgumentNullException(nameof(confirmationService));

            this.decoratee = decoratee;
            this.costCalculator = costCalculator;
            this.confirmationService = confirmationService;
        }

        public void Execute(TranslateSubtitlesFileToNewFile command)
        {
            if (command == null)
                throw new ArgumentNullException(nameof(command));

            var cost = costCalculator.Calculate(command.ToTranslate);

            var answer = confirmationService.AskForConfirmation(
                $"The translation cost for \"{command.ToTranslate}\" is {cost}. Do you wan't to translate ?");

            if (answer == Answer.No)
                return;

            decoratee.Execute(command);
        }
    }
}
