using NSubstitute;
using NUnit.Framework;
using SrtTranslator.Core;
using SrtTranslator.Core.Translation;
using SrtTranslator.Core.Translator;
using System;

namespace SrtTranslator.ConsoleApplication.Tests
{
    [TestFixture]
    public class TranslationCostConfirmationDecoratorTests
    {
        [Test]
        public void Constructor_NullDecoratee_Throws()
        {
            ICommandService<TranslateSubtitlesFileToNewFile> nullDecoratee = null;
            var stubCalculator = Substitute.For<ISubtitlesFileCostCalculator>();
            var stubConfirmationService = Substitute.For<IUserConfirmationService>();

            Assert.Throws<ArgumentNullException>(
                () => new TranslationCostConfirmationDecorator(
                    nullDecoratee, stubCalculator, stubConfirmationService));
        }

        [Test]
        public void Constructor_NullCostCalculator_Throws()
        {
            var stubDecoratee = Substitute.For<ICommandService<TranslateSubtitlesFileToNewFile>>();
            ISubtitlesFileCostCalculator nullCalculator = null;
            var stubConfirmationService = Substitute.For<IUserConfirmationService>();

            Assert.Throws<ArgumentNullException>(
                () => new TranslationCostConfirmationDecorator(
                    stubDecoratee, nullCalculator, stubConfirmationService));
        }

        [Test]
        public void Constructor_ConfirmationServiceNull_Throws()
        {
            var stubDecoratee = Substitute.For<ICommandService<TranslateSubtitlesFileToNewFile>>();
            var stubCalculator = Substitute.For<ISubtitlesFileCostCalculator>();
            IUserConfirmationService nullConfirmationService = null;

            Assert.Throws<ArgumentNullException>(
                () => new TranslationCostConfirmationDecorator(
                    stubDecoratee, stubCalculator, nullConfirmationService));
        }

        [Test]
        public void Execute_AnswerYesToConfirmation_CallDecoratee()
        {
            var mockDecoratee = Substitute.For<ICommandService<TranslateSubtitlesFileToNewFile>>();

            var stubConfirmationService = Substitute.For<IUserConfirmationService>();
            stubConfirmationService
                .AskForConfirmation(Arg.Any<string>())
                .Returns(Answer.Yes);

            var decorator = CreateDecorator(mockDecoratee, stubConfirmationService);

            var stubCommand = CreateStubCommand();
            decorator.Execute(stubCommand);

            mockDecoratee.Received()
                .Execute(Arg.Is(stubCommand));
        }

        [Test]
        public void Execute_AnswerNoToConfirmation_DoNotCallDecoratee()
        {
            var mockDecoratee = Substitute.For<ICommandService<TranslateSubtitlesFileToNewFile>>();

            var stubConfirmationService = Substitute.For<IUserConfirmationService>();
            stubConfirmationService
                .AskForConfirmation(Arg.Any<string>())
                .Returns(Answer.No);

            var decorator = CreateDecorator(mockDecoratee, stubConfirmationService);

            var stubCommand = CreateStubCommand();
            decorator.Execute(stubCommand);

            mockDecoratee.DidNotReceive()
                .Execute(Arg.Any<TranslateSubtitlesFileToNewFile>());
        }

        [Test]
        public void Execute_NullCommand_Throws()
        {
            var decorator = CreateDecorator();
            TranslateSubtitlesFileToNewFile nullCommand = null;

            Assert.Throws<ArgumentNullException>(
                () => decorator.Execute(nullCommand));
        }

        private TranslationCostConfirmationDecorator CreateDecorator()
        {
            return CreateDecorator(
                Substitute.For<ICommandService<TranslateSubtitlesFileToNewFile>>(),
                Substitute.For<IUserConfirmationService>());
        }

        private TranslationCostConfirmationDecorator CreateDecorator(
            ICommandService<TranslateSubtitlesFileToNewFile> decoratee,
            IUserConfirmationService confirmationService)
        {
            var stubCalculator = Substitute.For<ISubtitlesFileCostCalculator>();
            stubCalculator
                .Calculate(Arg.Any<FilePath>())
                .Returns(new Price(5, Currency.Euro));

            return new TranslationCostConfirmationDecorator(
                decoratee,
                stubCalculator,
                confirmationService);
        }

        private TranslateSubtitlesFileToNewFile CreateStubCommand()
        {
            return new TranslateSubtitlesFileToNewFile(
                new FilePath("/a/file.srt"),
                Language.French,
                new FilePath("/an/output_file.srt"));
        }
    }
}
