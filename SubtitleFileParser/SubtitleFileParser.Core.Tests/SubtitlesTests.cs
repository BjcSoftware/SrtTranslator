using NUnit.Framework;
using System.Collections.Generic;

namespace SubtitleFileParser.Core.Tests
{
    [TestFixture]
    public class SubtitlesTests
        : BaseEnumerableBasedValueObjectTests<Subtitle>
    {
        protected override IEnumerable<Subtitle> value1 => CreateSubtitles1().Value;

        protected override IEnumerable<Subtitle> value2 => CreateSubtitles2().Value;

        public static Subtitles CreateSubtitles1()
        {
            return new Subtitles(
                new List<Subtitle> {
                    SubtitleTests.CreateSubtitle1()
                });
        }

        public static Subtitles CreateSubtitles2()
        {
            return new Subtitles(
                new List<Subtitle> {
                    SubtitleTests.CreateSubtitle1(),
                    SubtitleTests.CreateSubtitle2()
                });
        }

        protected override EnumerableBasedValueObject<Subtitle> CreateValueObject(IEnumerable<Subtitle> value)
        {
            return new Subtitles(value);
        }
    }
}
