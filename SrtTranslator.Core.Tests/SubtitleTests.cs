using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace SrtTranslator.Core.Tests
{
    [TestFixture]
    public class SubtitleTests
    {
        [Test]
        public void Constructor_NullId_Throws()
        {
            SubtitleId nullId = null;

            Assert.Catch<ArgumentNullException>(
                () => new Subtitle(
                    nullId,
                    SubtitleTimestampsTests.CreateTimestamps(),
                    CreateStubSubtitleText()));
        }

        [Test]
        public void Constructor_NullTimestamps_Throws()
        {
            SubtitleTimestamps nullTimestamps = null;
            
            Assert.Catch<ArgumentNullException>(
                () => new Subtitle(
                    SubtitleIdTests.CreateId1(),
                    nullTimestamps,
                    CreateStubSubtitleText()));
        }

        [Test]
        public void Constructor_NullText_Throws()
        {
            SubtitleText nullText = null;
            Assert.Catch<ArgumentNullException>(
                () => new Subtitle(
                    SubtitleIdTests.CreateId1(),
                    SubtitleTimestampsTests.CreateTimestamps(), 
                    nullText));
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

        private SubtitleText CreateStubSubtitleText()
        {
            return CreateSingleLineText("stubText");
        }

        private static SubtitleText CreateSingleLineText(string text)
        {
            return new SubtitleText(
                new List<CharacterLine> {
                    new CharacterLine(text)
                });
        }

        public static Subtitle CreateSubtitle1()
        {
            return new Subtitle(
                SubtitleIdTests.CreateId1(),
                new SubtitleTimestamps(
                    TimestampTests.CreateTimestamp(5, 4, 3, 2),
                    TimestampTests.CreateTimestamp(5, 4, 3, 3)),
                CreateSingleLineText("Subtitle1"));
        }

        public static Subtitle CreateSubtitle2()
        {
            return new Subtitle(
                SubtitleIdTests.CreateId2(),
                new SubtitleTimestamps(
                    TimestampTests.CreateTimestamp(10, 0, 0, 0),
                    TimestampTests.CreateTimestamp(10, 0, 5, 0)),
                CreateSingleLineText("Subtitle2"));
        }
    }
}
