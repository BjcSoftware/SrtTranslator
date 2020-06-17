using NUnit.Framework;
using System.Collections.Generic;

namespace SrtTranslator.Core.Tests
{
    [TestFixture]
    public class SubtitleTextTests
        : BaseEnumerableBasedValueObjectTests<CharacterLine>
    {
        protected override IEnumerable<CharacterLine> value1 => 
            new List<CharacterLine> {
                new CharacterLine("a line"),
                new CharacterLine("another line")
            };

        protected override IEnumerable<CharacterLine> value2 =>
            new List<CharacterLine> {
                new CharacterLine("some other line")
            };

        protected override EnumerableBasedValueObject<CharacterLine> CreateValueObject(IEnumerable<CharacterLine> value)
        {
            return new SubtitleText(value);
        }
    }
}
