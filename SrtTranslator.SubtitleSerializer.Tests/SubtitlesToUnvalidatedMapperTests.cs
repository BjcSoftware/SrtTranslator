using NSubstitute;
using NUnit.Framework;
using SrtTranslator.Core;
using SrtTranslator.Core.Tests;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SrtTranslator.SubtitleSerializer.Tests
{
    using ISubtitleToUnvalidatedMapper = IMapper<UnvalidatedSubtitle, Subtitle>;

    [TestFixture]
    public class SubtitlesToUnvalidatedMapperTests
    {
        [Test]
        public void Constructor_NullMapper_Throws()
        {
            ISubtitleToUnvalidatedMapper nullMapper = null;

            Assert.Throws<ArgumentNullException>(
                () => new SubtitlesToUnvalidatedMapper(nullMapper));
        }

        [Test]
        public void Map_Always_ReturnsUnvalidatedSubtitles()
        {
            var subsToMap = new Subtitles(
                new List<Subtitle> {
                    SubtitleTests.CreateSubtitle1(),
                    SubtitleTests.CreateSubtitle2()
                });

            var stubMapper = Substitute.For<ISubtitleToUnvalidatedMapper>();

            var expectedUnvalidatedSubs = new UnvalidatedSubtitles(
                new List<UnvalidatedSubtitle> {
                    UnvalidatedSubtitleTests.CreateUnvalidatedSubtitle1(),
                    UnvalidatedSubtitleTests.CreateUnvalidatedSubtitle2()
                });

            stubMapper
                .Map(Arg.Is(subsToMap.Value.ElementAt(0)))
                .Returns(expectedUnvalidatedSubs.Value.ElementAt(0));
            stubMapper
                .Map(Arg.Is(subsToMap.Value.ElementAt(1)))
                .Returns(expectedUnvalidatedSubs.Value.ElementAt(1));

            var mapper = CreateMapper(stubMapper);

            var actualUnvalidatedSubs = mapper.Map(subsToMap);

            Assert.AreEqual(
                expectedUnvalidatedSubs,
                actualUnvalidatedSubs);
        }

        [Test]
        public void Map_NullSubtitles_Throws()
        {
            var mapper = CreateMapper();
            Subtitles nullSubtitles = null;

            Assert.Throws<ArgumentNullException>(
                () => mapper.Map(nullSubtitles));
        }

        private SubtitlesToUnvalidatedMapper CreateMapper()
        {
            return CreateMapper(
                Substitute.For<ISubtitleToUnvalidatedMapper>());
        }

        private SubtitlesToUnvalidatedMapper CreateMapper(ISubtitleToUnvalidatedMapper mapper)
        {
            return new SubtitlesToUnvalidatedMapper(mapper);
        }
    }
}
