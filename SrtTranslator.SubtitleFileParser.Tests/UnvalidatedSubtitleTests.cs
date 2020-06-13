using NUnit.Framework;
using SrtTranslator.Core;
using SrtTranslator.Core.Tests;
using System.Collections.Generic;

namespace SrtTranslator.SubtitleFileParser.Tests
{
    [TestFixture]
    public class UnvalidatedSubtitleTests
        : BaseEnumerableBasedValueObjectTests<CharacterLine>
    {
        protected override IEnumerable<CharacterLine> value1 => CreateUnvalidatedSubtitle1().Value;

        protected override IEnumerable<CharacterLine> value2 => CreateUnvalidatedSubtitle2().Value;

        protected override EnumerableBasedValueObject<CharacterLine> CreateValueObject(
            IEnumerable<CharacterLine> value)
        {
            return new UnvalidatedSubtitle(value);
        }

        public static UnvalidatedSubtitle CreateUnvalidatedSubtitle1()
        {
            return new UnvalidatedSubtitle(
                new List<CharacterLine> {
                    new CharacterLine("1"),
                    new CharacterLine("00:00:01,100 --> 00:00:03,200"),
                    new CharacterLine("subtitle 1"),
                    new CharacterLine("end of subtitle")
                });
        }

        public static UnvalidatedSubtitle CreateUnvalidatedSubtitle2()
        {
            return new UnvalidatedSubtitle(
                new List<CharacterLine> {
                    new CharacterLine("2"),
                    new CharacterLine("00:00:02,100 --> 00:00:04,200"),
                    new CharacterLine("subtitle 2")
                });
        }
    }
}
