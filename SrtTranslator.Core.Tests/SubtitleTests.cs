using NUnit.Framework;
using System;

namespace SrtTranslator.Core.Tests
{
    [TestFixture]
    public class SubtitleTests
    {
        [Test]
        public void Constructor_NullTimestamp_Throws()
        {
            SubtitleTimestamps nullTimestamp = null;
            var stubText = new SubtitleText("stubText");

            Assert.Catch<ArgumentNullException>(
                () => new Subtitle(nullTimestamp, stubText));
        }

        [Test]
        public void Constructor_NullText_Throws()
        {
            SubtitleText nullText = null;

            Assert.Catch<ArgumentNullException>(
                () => new Subtitle(CreateStubTimestamps(), nullText));
        }

        [Test]
        public void Equals_SameSubtitles_ReturnsTrue()
        {
            Subtitle s1 = CreateSubtitle1();
            Subtitle s2 = CreateSubtitle1();

            Assert.IsTrue(s1.Equals(s2));
        }

        [Test]
        public void Equals_DifferentSubtitles_ReturnsFalse()
        {
            Subtitle s1 = CreateSubtitle1();
            Subtitle s2 = CreateSubtitle2();

            Assert.IsFalse(s1.Equals(s2));
        }

        private SubtitleTimestamps CreateStubTimestamps()
        {
            return new SubtitleTimestamps(
                CreateStubTimestamp(),
                CreateStubTimestamp());
        }

        private Timestamp CreateStubTimestamp()
        {
            return new Timestamp(
                new HoursTimestamp(0),
                new MinutesTimestamp(0),
                new SecondsTimestamp(0),
                new MillisecondsTimestamp(0));
        }


        public static Subtitle CreateSubtitle1()
        {
            return new Subtitle(
                new SubtitleTimestamps(
                    TimestampTests.CreateTimestamp(5, 4, 3, 2),
                    TimestampTests.CreateTimestamp(5, 4, 3, 3)),
                new SubtitleText("Subtitle1"));
        }

        public static Subtitle CreateSubtitle2()
        {
            return new Subtitle(
                new SubtitleTimestamps(
                    TimestampTests.CreateTimestamp(10, 0, 0, 0),
                    TimestampTests.CreateTimestamp(10, 0, 5, 0)),
                new SubtitleText("Subtitle2"));
        }
    }
}
