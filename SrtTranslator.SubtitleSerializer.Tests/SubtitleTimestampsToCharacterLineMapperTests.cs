using NUnit.Framework;
using SrtTranslator.Core;
using SrtTranslator.Core.Tests;
using System;

namespace SrtTranslator.SubtitleSerializer.Tests
{
    [TestFixture]
    public class SubtitleTimestampsToCharacterLineMapperTests
    {
        [Test]
        public void Map_Always_ReturnsCharacterLine()
        {
            var mapper = CreateMapper();

            var timestampsToMap = new SubtitleTimestamps(
                TimestampTests.CreateTimestamp(5, 4, 3, 2),
                TimestampTests.CreateTimestamp(5, 4, 4, 0));
            var expectedLine = new CharacterLine("05:04:03,002 --> 05:04:04,000");

            var actualLine = mapper.Map(timestampsToMap);

            Assert.AreEqual(expectedLine, actualLine);
        }

        [Test]
        public void Map_NullTimestamps_Throws()
        {
            SubtitleTimestamps nullTimestamps = null;
            var mapper = CreateMapper();

            Assert.Throws<ArgumentNullException>(
                () => mapper.Map(nullTimestamps));
        }

        private SubtitleTimestampsToCharacterLineMapper CreateMapper()
        {
            return new SubtitleTimestampsToCharacterLineMapper();
        }
    }
}
