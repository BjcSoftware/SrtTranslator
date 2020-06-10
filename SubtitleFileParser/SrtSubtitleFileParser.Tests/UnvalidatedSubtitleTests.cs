using NUnit.Framework;
using SubtitleFileParser.Core;
using SubtitleFileParser.Core.Tests;
using System;
using System.Collections.Generic;

namespace SrtSubtitleFileParser.Tests
{
    [TestFixture]
    public class UnvalidatedSubtitleTests
        : BaseEnumerableBasedValueObjectTests<CharacterLine>
    {
        protected override IEnumerable<CharacterLine> value1 => CreateUnvalidatedSubtitle1().Value;

        protected override IEnumerable<CharacterLine> value2 => CreateUnvalidatedSubtitle2().Value;


        private static object[] lessThanThreeLinesCases =
        {
            new object[] { new List<CharacterLine>() },
            new object[] { new List<CharacterLine> {
                new CharacterLine("First Line")
            }},
            new object[] { new List<CharacterLine> {
                new CharacterLine("First Line"),
                new CharacterLine("Second Line")
            }}
        };

        [TestCaseSource(nameof(lessThanThreeLinesCases))]
        public void Constructor_LessThanThreeLines_Throws(List<CharacterLine> lines)
        {
            Assert.Throws<ArgumentException>(
                () => new UnvalidatedSubtitle(lines));
        }

        [Test]
        public void TimestampsLine_Always_ReturnsSecondLine()
        {
            var unvalidatedSubtitle = CreateUnvalidatedSubtitle1();

            var expectedLine = new CharacterLine("00:00:01,100 --> 00:00:03,200");

            Assert.AreEqual(
                expectedLine,
                unvalidatedSubtitle.TimestampsLine);
        }

        [Test]
        public void TextLines_Always_ReturnsAllLinesFromThird()
        {
            var unvalidatedSubtitle = CreateUnvalidatedSubtitle1();

            var expectedLines = new List<CharacterLine> {
                new CharacterLine("subtitle 1"),
                new CharacterLine("end of subtitle")
            };

            Assert.AreEqual(
                expectedLines,
                unvalidatedSubtitle.TextLines);
        }

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
