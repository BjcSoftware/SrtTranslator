using NUnit.Framework;
using SrtTranslator.Core;
using SrtTranslator.Core.Tests;
using System.Collections.Generic;

namespace SrtTranslator.SubtitleFileParser.Tests
{
    [TestFixture]
    public class UnvalidatedSubtitlesTests
        : BaseEnumerableBasedValueObjectTests<UnvalidatedSubtitle>
    {
        protected override IEnumerable<UnvalidatedSubtitle> value1 => 
            new List<UnvalidatedSubtitle> {
                UnvalidatedSubtitleTests.CreateUnvalidatedSubtitle1(),
                UnvalidatedSubtitleTests.CreateUnvalidatedSubtitle2()
            };

        protected override IEnumerable<UnvalidatedSubtitle> value2 => 
            new List<UnvalidatedSubtitle> {
                UnvalidatedSubtitleTests.CreateUnvalidatedSubtitle2(),
                UnvalidatedSubtitleTests.CreateUnvalidatedSubtitle1()
            };

        protected override EnumerableBasedValueObject<UnvalidatedSubtitle> CreateValueObject(
            IEnumerable<UnvalidatedSubtitle> value)
        {
            return new UnvalidatedSubtitles(value);
        }

        public static UnvalidatedSubtitles CreateUnvalidatedSubtitles()
        {
            return new UnvalidatedSubtitles(
                new List<UnvalidatedSubtitle> {
                    UnvalidatedSubtitleTests.CreateUnvalidatedSubtitle1(),
                    UnvalidatedSubtitleTests.CreateUnvalidatedSubtitle2()
                });
        }
    }
}
