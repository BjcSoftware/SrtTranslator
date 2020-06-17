using NUnit.Framework;
using System.Collections.Generic;

namespace SrtTranslator.Core.Tests
{
    [TestFixture]
    public class UnvalidatedSubtitlesTests
        : BaseEnumerableBasedValueObjectTests<UnvalidatedSubtitle>
    {
        protected override IEnumerable<UnvalidatedSubtitle> value1 => 
            CreateUnvalidatedSubtitles1().Value;

        protected override IEnumerable<UnvalidatedSubtitle> value2 =>
            CreateUnvalidatedSubtitles2().Value;

        protected override EnumerableBasedValueObject<UnvalidatedSubtitle> CreateValueObject(
            IEnumerable<UnvalidatedSubtitle> value)
        {
            return new UnvalidatedSubtitles(value);
        }

        public static UnvalidatedSubtitles CreateUnvalidatedSubtitles1()
        {
            return new UnvalidatedSubtitles(
                new List<UnvalidatedSubtitle> {
                    UnvalidatedSubtitleTests.CreateUnvalidatedSubtitle1(),
                    UnvalidatedSubtitleTests.CreateUnvalidatedSubtitle2()
                });
        }

        public static UnvalidatedSubtitles CreateUnvalidatedSubtitles2()
        {
            return new UnvalidatedSubtitles(
                new List<UnvalidatedSubtitle> {
                    UnvalidatedSubtitleTests.CreateUnvalidatedSubtitle2(),
                    UnvalidatedSubtitleTests.CreateUnvalidatedSubtitle1()
                });
        }
    }
}
