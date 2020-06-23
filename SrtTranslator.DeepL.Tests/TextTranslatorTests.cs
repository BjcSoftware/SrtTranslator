using NSubstitute;
using NUnit.Framework;
using System;
using SrtTranslator.Core.Translator;
using System.Net.Http;
using System.Net;
using SrtTranslator.DeepL.Exceptions;

namespace SrtTranslator.DeepL.Tests
{
    [TestFixture]
    public class TextTranslatorTests
    {
        [Test]
        public void Constructor_NullRequester_Throws()
        {
            IHttpTranslationRequester nullRequester = null;
            var stubDeserializer = Substitute.For<IHttpTranslationResponseDeserializer>();
            var stubKey = AuthenticationKeyTests.CreateKey();

            Assert.Throws<ArgumentNullException>(
                () => new TextTranslator(nullRequester, stubDeserializer, stubKey));
        }

        [Test]
        public void Constructor_NullDeserializer_Throws()
        {
            var stubRequester = Substitute.For<IHttpTranslationRequester>();
            IHttpTranslationResponseDeserializer nullDeserializer = null;
            var stubKey = AuthenticationKeyTests.CreateKey();

            Assert.Throws<ArgumentNullException>(
                () => new TextTranslator(stubRequester, nullDeserializer, stubKey));
        }

        [Test]
        public void Constructor_NullKey_Throws()
        {
            var stubRequester = Substitute.For<IHttpTranslationRequester>();
            var stubDeserializer = Substitute.For<IHttpTranslationResponseDeserializer>();
            AuthenticationKey nullKey = null;

            Assert.Throws<ArgumentNullException>(
                () => new TextTranslator(stubRequester, stubDeserializer, nullKey));
        }
        
        [Test]
        public void TranslateText_TranslationSuccessful_ReturnsTranslation()
        {
            var successfulHttpResponse = new HttpResponseMessage(HttpStatusCode.OK);

            var stubRequester = Substitute.For<IHttpTranslationRequester>();
            stubRequester
                .Request("text to translate", Arg.Is(Language.French))
                .Returns(successfulHttpResponse);

            var stubDeserializer = Substitute.For<IHttpTranslationResponseDeserializer>();
            stubDeserializer
                .DeserializeTranslation(Arg.Is(successfulHttpResponse))
                .Returns("texte à traduire");

            var translator = CreateTranslator(stubRequester, stubDeserializer);

            var actualTranslation = translator.TranslateText(
                "text to translate", 
                Language.French);

            Assert.AreEqual(
                "texte à traduire",
                actualTranslation);
        }

        [Test]
        public void TranslateText_NullText_Throws()
        {
            string nullText = null;
            var translator = CreateTranslator();

            Assert.Throws<ArgumentNullException>(
                () => translator.TranslateText(nullText, Language.French));
        }

        [Test]
        [TestCase("")]
        [TestCase("   ")]
        public void TranslateText_EmptyText_ReturnsEmptyString(string emptyText)
        {
            var translator = CreateTranslator();

            var actualTranslation = translator.TranslateText(emptyText, Language.French);

            Assert.AreEqual(
                string.Empty,
                actualTranslation);
        }

        [Test]
        public void TranslateText_RequesterGivesForbiddenCode_ThrowsAuthenticationException()
        {
            var stubRequester = CreateRequesterGivingCode(HttpStatusCode.Forbidden);

            var translator = CreateTranslator(stubRequester);

            AssertTranslatorThrows<AuthenticationException>(translator);
        }

        [Test]
        public void TranslateText_RequesterGivesTooManyRequestsCode_ThrowsTooManyRequestsException()
        {
            var stubRequester = CreateRequesterGivingCode(HttpStatusCode.TooManyRequests);

            var translator = CreateTranslator(stubRequester);

            AssertTranslatorThrows<TooManyRequestsException>(translator);
        }

        [Test]
        public void TranslateText_RequesterGivesQuotaExceededHttpCode_ThrowsQuotaExceededException()
        {
            var QuotaExceededCode = (HttpStatusCode)456;
            var stubRequester = CreateRequesterGivingCode(QuotaExceededCode);

            var translator = CreateTranslator(stubRequester);

            AssertTranslatorThrows<QuotaExceededException>(translator);
        }

        [Test]
        [TestCase(500)]
        [TestCase(520)]
        public void TranslateText_RequesterGivesInternalError_ThrowsInternalErrorException(int code)
        {
            var InternalErrorCode = (HttpStatusCode)code;
            var stubRequester = CreateRequesterGivingCode(InternalErrorCode);

            var translator = CreateTranslator(stubRequester);

            AssertTranslatorThrows<InternalErrorException>(translator);
        }

        private IHttpTranslationRequester CreateRequesterGivingCode(HttpStatusCode code)
        {
            var forbiddenHttpResponse = new HttpResponseMessage(code);

            var stubRequester = Substitute.For<IHttpTranslationRequester>();
            stubRequester
                .Request(Arg.Is("text to translate"), Arg.Is(Language.French))
                .Returns(forbiddenHttpResponse);

            return stubRequester;
        }

        private void AssertTranslatorThrows<T>(TextTranslator translator)
            where T : Exception
        {
            Assert.Throws<T>(
                () => translator.TranslateText("text to translate", Language.French));
        }

        private TextTranslator CreateTranslator()
        {
            return CreateTranslator(
                Substitute.For<IHttpTranslationRequester>(),
                Substitute.For<IHttpTranslationResponseDeserializer>());
        }

        private TextTranslator CreateTranslator(
            IHttpTranslationRequester httpRequester,
            IHttpTranslationResponseDeserializer deserializer)
        {
            return new TextTranslator(
                httpRequester,
                deserializer,
                AuthenticationKeyTests.CreateKey());
        }

        private TextTranslator CreateTranslator(
            IHttpTranslationRequester httpRequester)
        {
            return CreateTranslator(
                httpRequester,
                Substitute.For<IHttpTranslationResponseDeserializer>());
        }
    }
}
