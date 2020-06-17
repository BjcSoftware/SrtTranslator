using NUnit.Framework;
using System;

namespace SrtTranslator.Core.Tests
{
    [TestFixture]
    public class SubtitleTimestampsTests
    {
        [Test]
        public void Constructor_NullStart_Throws()
        {
            Timestamp nullStart = null;
            Assert.Catch<ArgumentNullException>(
                () => new SubtitleTimestamps(
                    nullStart, 
                    CreateStubTimestamp()));
        }

        [Test]
        public void Constructor_NullEnd_Throws()
        {
            Timestamp nullEnd = null;
            Assert.Catch<ArgumentNullException>(
                () => new SubtitleTimestamps(
                    CreateStubTimestamp(),
                    nullEnd));
        }

        [Test]
        public void Constructor_EndBeforeStart_Throws()
        {
            var start = new Timestamp(
                new HoursTimestamp(5),
                new MinutesTimestamp(0),
                new SecondsTimestamp(0),
                new MillisecondsTimestamp(0));

            var end = new Timestamp(
                new HoursTimestamp(4),
                new MinutesTimestamp(0),
                new SecondsTimestamp(0),
                new MillisecondsTimestamp(0));

            Assert.Catch<ArgumentException>(
                () => new SubtitleTimestamps(start, end));
        }

        [Test]
        public void Equals_EqualTimestamps_ReturnsTrue()
        {
            var t1 = new SubtitleTimestamps(
                TimestampTests.CreateTimestamp(1, 2, 3, 200),
                TimestampTests.CreateTimestamp(5, 2, 3, 200));

            var t2 = new SubtitleTimestamps(
                TimestampTests.CreateTimestamp(1, 2, 3, 200),
                TimestampTests.CreateTimestamp(5, 2, 3, 200));

            Assert.IsTrue(t1.Equals(t2));
        }

        [Test]
        public void Equals_NotEqualTimestamps_ReturnsFalse()
        {
            var t1 = new SubtitleTimestamps(
                TimestampTests.CreateTimestamp(1, 2, 3, 200),
                TimestampTests.CreateTimestamp(5, 2, 3, 200));

            var t2 = new SubtitleTimestamps(
                TimestampTests.CreateTimestamp(2, 2, 3, 2),
                TimestampTests.CreateTimestamp(3, 2, 3, 150));

            Assert.IsFalse(t1.Equals(t2));
        }

        public static SubtitleTimestamps CreateTimestamps()
        {
            return new SubtitleTimestamps(
                TimestampTests.CreateTimestamp(1, 2, 3, 200),
                TimestampTests.CreateTimestamp(5, 2, 3, 200));
        }

        private Timestamp CreateStubTimestamp()
        {
            return new Timestamp(
                new HoursTimestamp(0),
                new MinutesTimestamp(0),
                new SecondsTimestamp(0),
                new MillisecondsTimestamp(0));
        }
    }
}
