using NUnit.Framework;
using SrtTranslator.Core;
using SrtTranslator.Core.Tests;
using System;

namespace SrtTranslator.DeepL.Tests
{
    [TestFixture]
    public class AuthenticationKeyTests
        : BaseNullableTypeBasedValueObjectTests<string>
    {
        protected override string Value1 => "xxxxxxxx-xxxx-xxxx-xxxx-xxxxxxxxxxxx";

        protected override string Value2 => "11111111-1111-1111-1111-111111111111";

        [Test]
        public void Constructor_EmptyKey_Throws()
        {
            string emptyKey = string.Empty;

            Assert.Throws<ArgumentNullException>(
                () => CreateValueObject(emptyKey));
        }

        [Test]
        public void Constructor_WhiteSpacesOnly_Throws()
        {
            string whiteSpaces = "        ";

            Assert.Throws<ArgumentNullException>(
                () => CreateValueObject(whiteSpaces));
        }

        protected override ValueObject<string> CreateValueObject(string value)
        {
            return new AuthenticationKey(value);
        }

        public static AuthenticationKey CreateKey()
        {
            return new AuthenticationKey("xxxxxxxx-xxxx-xxxx-xxxx-xxxxxxxxxxxx");
        }
    }
}
