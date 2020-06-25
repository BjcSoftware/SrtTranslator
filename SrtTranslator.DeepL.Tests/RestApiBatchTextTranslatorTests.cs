using NSubstitute;
using NUnit.Framework;
using SrtTranslator.Core.Translator;
using SrtTranslator.DeepL.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;

namespace SrtTranslator.DeepL.Tests
{
    [TestFixture]
    public class RestApiBatchTextTranslatorTests
    {
        static List<string> StubBatch = new List<string> {
            "subtitle 1",
            "subtitle 2"
        };

        [Test]
        public void Constructor_NullSender_Throws()
        {
            IHttpBatchTranslationRequestSender nullRequestSender = null;
            var stubDeserializer = Substitute.For<IHttpBatchTranslationResponseDeserializer>();
            var stubKey = AuthenticationKeyTests.CreateKey();

            Assert.Throws<ArgumentNullException>(
                () => new RestApiBatchTextTranslator(
                    nullRequestSender, stubDeserializer, stubKey));
        }

        [Test]
        public void Constructor_NullDeserializer_Throws()
        {
            var stubRequestSender = Substitute.For<IHttpBatchTranslationRequestSender>();
            IHttpBatchTranslationResponseDeserializer nullDeserializer = null;
            var stubKey = AuthenticationKeyTests.CreateKey();

            Assert.Throws<ArgumentNullException>(
                () => new RestApiBatchTextTranslator(
                    stubRequestSender, nullDeserializer, stubKey));
        }

        [Test]
        public void Constructor_NullAuthKey_Throws()
        {
            var stubRequestSender = Substitute.For<IHttpBatchTranslationRequestSender>();
            var stubDeserializer = Substitute.For<IHttpBatchTranslationResponseDeserializer>();
            AuthenticationKey nullKey = null;

            Assert.Throws<ArgumentNullException>(
                () => new RestApiBatchTextTranslator(
                    stubRequestSender, stubDeserializer, nullKey));
        }

        [Test]
        public void Translate_TranslationSuccessful_ReturnsTranslation()
        {
            var successfulHttpResponse = new HttpResponseMessage(HttpStatusCode.OK);

            var stubRequester = Substitute.For<IHttpBatchTranslationRequestSender>();
            stubRequester
                .SendTranslationRequest(StubBatch, Arg.Is(Language.French))
                .Returns(successfulHttpResponse);

            var expectedTranslation = new List<string> { "traduction" };
            var stubDeserializer = Substitute.For<IHttpBatchTranslationResponseDeserializer>();
            stubDeserializer
                .DeserializeTranslation(Arg.Is(successfulHttpResponse))
                .Returns(expectedTranslation);

            var translator = CreateTranslator(stubRequester, stubDeserializer);

            var actualTranslation = translator.Translate(StubBatch, Language.French);

            Assert.AreEqual(
                expectedTranslation,
                actualTranslation);
        }

        [Test]
        public void Translate_NullBatch_Throws()
        {

        }

        [Test]
        public void Translate_RequestSenderGivesForbiddenCode_ThrowsAuthenticationException()
        {
            var stubRequestSender = CreateRequestSenderGivingCode(HttpStatusCode.Forbidden);

            var translator = CreateTranslator(stubRequestSender);

            AssertTranslatorThrows<AuthenticationException>(translator);
        }

        [Test]
        public void TranslateText_RequestSenderGivesTooManyRequestsCode_ThrowsTooManyRequestsException()
        {
            var stubRequestSender = CreateRequestSenderGivingCode(HttpStatusCode.TooManyRequests);

            var translator = CreateTranslator(stubRequestSender);

            AssertTranslatorThrows<TooManyRequestsException>(translator);
        }

        [Test]
        public void TranslateText_RequestSenderGivesQuotaExceededHttpCode_ThrowsQuotaExceededException()
        {
            var QuotaExceededCode = (HttpStatusCode)456;
            var stubRequestSender = CreateRequestSenderGivingCode(QuotaExceededCode);

            var translator = CreateTranslator(stubRequestSender);

            AssertTranslatorThrows<QuotaExceededException>(translator);
        }

        [Test]
        [TestCase(500)]
        [TestCase(520)]
        public void TranslateText_RequestSenderGivesInternalError_ThrowsInternalErrorException(int code)
        {
            var InternalErrorCode = (HttpStatusCode)code;
            var stubRequester = CreateRequestSenderGivingCode(InternalErrorCode);

            var translator = CreateTranslator(stubRequester);

            AssertTranslatorThrows<InternalErrorException>(translator);
        }

        private IHttpBatchTranslationRequestSender CreateRequestSenderGivingCode(
            HttpStatusCode code)
        {
            var forbiddenHttpResponse = new HttpResponseMessage(code);

            var stubSender = Substitute.For<IHttpBatchTranslationRequestSender>();
            stubSender
                .SendTranslationRequest(
                    Arg.Is<List<string>>(
                        b => b.SequenceEqual(StubBatch)), 
                    Arg.Is(Language.French))
                .Returns(forbiddenHttpResponse);

            return stubSender;
        }

        private void AssertTranslatorThrows<T>(RestApiBatchTextTranslator translator)
            where T : Exception
        {
            Assert.Throws<T>(
                () => translator.Translate(StubBatch, Language.French));
        }

        private RestApiBatchTextTranslator CreateTranslator()
        {
            return CreateTranslator(
                Substitute.For<IHttpBatchTranslationRequestSender>(),
                Substitute.For<IHttpBatchTranslationResponseDeserializer>());
        }

        private RestApiBatchTextTranslator CreateTranslator(
            IHttpBatchTranslationRequestSender httpRequester,
            IHttpBatchTranslationResponseDeserializer deserializer)
        {
            return new RestApiBatchTextTranslator(
                httpRequester,
                deserializer,
                AuthenticationKeyTests.CreateKey());
        }

        private RestApiBatchTextTranslator CreateTranslator(
            IHttpBatchTranslationRequestSender httpRequester)
        {
            return CreateTranslator(
                httpRequester,
                Substitute.For<IHttpBatchTranslationResponseDeserializer>());
        }
    }
}
