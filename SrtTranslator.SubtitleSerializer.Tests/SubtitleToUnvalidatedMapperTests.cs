using NSubstitute;
using NUnit.Framework;
using SrtTranslator.Core;
using SrtTranslator.Core.Tests;
using System;
using System.Collections.Generic;

namespace SrtTranslator.SubtitleSerializer.Tests
{
    using ITimestampsMapper = IMapper<CharacterLine, SubtitleTimestamps>;

    [TestFixture]
    public class SubtitleToUnvalidatedMapperTests
    {
        [Test]
        public void Constructor_NullTimestampsMapper_Throws()
        {
            ITimestampsMapper nullMapper = null;

            Assert.Throws<ArgumentNullException>(
                () => new SubtitleToUnvalidatedMapper(nullMapper));
        }

        [Test]
        public void Map_Always_ReturnsUnvalidatedSubtitle()
        {
            var subtitleToMap = new Subtitle(
                new SubtitleId(1),
                new SubtitleTimestamps(
                    TimestampTests.CreateTimestamp(1, 2, 3, 4),
                    TimestampTests.CreateTimestamp(1, 2, 4, 0)),
                new SubtitleText(
                    new List<CharacterLine> {
                        new CharacterLine("A line"),
                        new CharacterLine("Another line")
                    }));

            var expectedUnvalidatedSubtitle = new UnvalidatedSubtitle(
                new List<CharacterLine> {
                    new CharacterLine("1"),
                    new CharacterLine("01:02:03,004 --> 01:02:04,000"),
                    new CharacterLine("A line"),
                    new CharacterLine("Another line")
                });

            var stubTimestampsMapper = Substitute.For<ITimestampsMapper>();
            stubTimestampsMapper
                .Map(Arg.Any<SubtitleTimestamps>())
                .Returns(new CharacterLine("01:02:03,004 --> 01:02:04,000"));

            var mapper = new SubtitleToUnvalidatedMapper(stubTimestampsMapper);

            var actualUnvalidatedSubtitle = mapper.Map(subtitleToMap);

            Assert.AreEqual(
                expectedUnvalidatedSubtitle,
                actualUnvalidatedSubtitle);
        }

        [Test]
        public void Map_NullSubtitle_Throws()
        {
            Subtitle nullSubtitle = null;
            var mapper = CreateMapper();

            Assert.Throws<ArgumentNullException>(
                () => mapper.Map(nullSubtitle));
        }

        private SubtitleToUnvalidatedMapper CreateMapper()
        {
            return CreateMapper(
                Substitute.For<ITimestampsMapper>());
        }

        private SubtitleToUnvalidatedMapper CreateMapper(
            ITimestampsMapper timestampsMapper)
        {
            return new SubtitleToUnvalidatedMapper(timestampsMapper);
        }
    }
}
