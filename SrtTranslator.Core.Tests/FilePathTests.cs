using NUnit.Framework;
using System;
using System.IO;
using System.Linq;

namespace SrtTranslator.Core.Tests
{
    [TestFixture]
    public class FilePathTests : BaseValueObjectTests<string>
    {
        protected override string Value1 => "/some/file.srt";

        protected override string Value2 => "/some/other/file.srt";

        [Test]
        public void Constructor_EmptyString_Throws()
        {
            Assert.Throws<ArgumentNullException>(
                () => new FilePath(string.Empty));
        }

        [Test]
        public void Constructor_ContainsOnlyWhiteSpace_Throws()
        {
            var whiteSpaces = "             ";
            Assert.Throws<ArgumentNullException>(
                () => new FilePath(whiteSpaces));
        }

        [Test]
        public void Constructor_ContainsInvalidPathCharacter_Throws()
        {
            var invalidPath = $"/a/path|{Path.GetInvalidPathChars().First()}file.srt";

            Assert.Throws<ArgumentException>(
                () => new FilePath(invalidPath));
        }

        protected override ValueObject<string> CreateValueObject(string value)
        {
            return new FilePath(value);
        }
    }
}
