using NUnit.Framework;
using System;

namespace SubtitleFileParser.Core.Tests
{
    [TestFixture]
    public class TimestampTests
    {
        [Test]
        public void Constructor_NullHours_Throws()
        {
            HoursTimestamp nullHours = null;

            Assert.Catch<ArgumentNullException>(
                () => new Timestamp(
                    nullHours,
                    CreateStubMinute(),
                    CreateStubSeconds(),
                    CreateStubMilliseconds()));
        }

        [Test]
        public void Constructor_NullMinutes_Throws()
        {
            MinutesTimestamp nullMinutes = null;

            Assert.Catch<ArgumentNullException>(
                () => new Timestamp(
                    CreateStubHours(),
                    nullMinutes,
                    CreateStubSeconds(),
                    CreateStubMilliseconds()));
        }

        [Test]
        public void Constructor_NullSeconds_Throws()
        {
            SecondsTimestamp nullSeconds = null;

            Assert.Catch<ArgumentNullException>(
                () => new Timestamp(
                    CreateStubHours(),
                    CreateStubMinute(),
                    nullSeconds,
                    CreateStubMilliseconds()));
        }

        [Test]
        public void Constructor_NullMilliseconds_Throws()
        {
            MillisecondsTimestamp nullMilliseconds = null;

            Assert.Catch<ArgumentNullException>(
                () => new Timestamp(
                    CreateStubHours(),
                    CreateStubMinute(),
                    CreateStubSeconds(),
                    nullMilliseconds));
        }

        [Test]
        public void IsBefore_OtherIsAfter_ReturnsTrue()
        {
            Timestamp t = CreateTimestamp(0, 0, 6, 139);
            Timestamp other = CreateTimestamp(0, 0, 10, 644);

            Assert.IsTrue(t.IsBefore(other));
        }

        [Test]
        public void IsBefore_OtherIsBefore_ReturnsFalse()
        {
            Timestamp t = CreateTimestamp(5, 0, 0, 0);
            Timestamp other = CreateTimestamp(4, 0, 0, 0);

            Assert.IsFalse(t.IsBefore(other));
        }

        [Test]
        public void IsBefore_OtherIsEqual_ReturnsFalse()
        {
            Timestamp t = CreateTimestamp(5, 0, 0, 0);
            Timestamp other = CreateTimestamp(5, 0, 0, 0);

            Assert.IsFalse(t.IsBefore(other));
        }

        [Test]
        public void Equals_EqualTimestamps_ReturnsTrue()
        {
            var t1 = CreateTimestamp(5, 1, 2, 0);
            var t2 = CreateTimestamp(5, 1, 2, 0);

            Assert.IsTrue(t1.Equals(t2));
        }

        [Test]
        public void Equals_NotEqualTimestamps1_ReturnsFalse()
        {
            var t1 = CreateTimestamp(4, 1, 2, 0);
            var t2 = CreateTimestamp(5, 2, 3, 1);

            Assert.IsFalse(t1.Equals(t2));
        }

        public static Timestamp CreateTimestamp(int hours, int minutes, int seconds, int milliseconds)
        {
            return new Timestamp(
                new HoursTimestamp(hours),
                new MinutesTimestamp(minutes),
                new SecondsTimestamp(seconds),
                new MillisecondsTimestamp(milliseconds));
        }

        private HoursTimestamp CreateStubHours()
        {
            return new HoursTimestamp(0);
        }

        private MinutesTimestamp CreateStubMinute()
        {
            return new MinutesTimestamp(0);
        }

        private SecondsTimestamp CreateStubSeconds()
        {
            return new SecondsTimestamp(0);
        }

        private MillisecondsTimestamp CreateStubMilliseconds()
        {
            return new MillisecondsTimestamp(0);
        }
    }
}
