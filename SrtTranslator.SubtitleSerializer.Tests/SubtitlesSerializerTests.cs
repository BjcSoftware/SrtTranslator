using NSubstitute;
using NUnit.Framework;
using SrtTranslator.Core;
using SrtTranslator.Core.Tests;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SrtTranslator.SubtitleSerializer.Tests
{
    using ISubtitlesMapper = IMapper<UnvalidatedSubtitles, Subtitles>;

    [TestFixture]
    public class SubtitlesSerializerTests
    {
        [Test]
        public void Constructor_NullWriter_Throws()
        {
            IFileLineWriter nullWriter = null;
            var stubMapper = Substitute.For<ISubtitlesMapper>();

            Assert.Throws<ArgumentNullException>(
                () => new SubtitlesSerializer(nullWriter, stubMapper));
        }

        [Test]
        public void Constructor_NullMapper_Throws()
        {
            var stubWriter = Substitute.For<IFileLineWriter>();
            ISubtitlesMapper nullMapper = null;

            Assert.Throws<ArgumentNullException>(
                () => new SubtitlesSerializer(stubWriter, nullMapper));
        }

        [Test]
        public void Serialize_WritesSubtitlesToGivenFile()
        {
            var subsToSerialize = new UnvalidatedSubtitles(
                new List<UnvalidatedSubtitle> {
                    new UnvalidatedSubtitle(
                        new List<CharacterLine> {
                            new CharacterLine("1"),
                            new CharacterLine("First line"),
                            new CharacterLine("Second line")
                        }),
                    new UnvalidatedSubtitle(
                        new List<CharacterLine> {
                            new CharacterLine("2"),
                            new CharacterLine("Subtitle 2")
                        })
                });

            var stubMapper = Substitute.For<ISubtitlesMapper>();
            stubMapper
                .Map(Arg.Any<Subtitles>())
                .Returns(subsToSerialize);

            var expectedLines = new List<CharacterLine> {
                new CharacterLine("1"),
                new CharacterLine("First line"),
                new CharacterLine("Second line"),
                new CharacterLine(string.Empty),
                new CharacterLine("2"),
                new CharacterLine("Subtitle 2"),
                new CharacterLine(string.Empty)
            };

            var mockWriter = Substitute.For<IFileLineWriter>();
            var serializer = CreateSerializer(mockWriter, stubMapper);

            var stubSubtitles = SubtitlesTests.CreateSubtitles1();
            var stubPath = new FilePath("/a/path.srt");

            serializer.Serialize(stubSubtitles, stubPath);

            mockWriter.Received()
                .WriteLines(
                    Arg.Is(stubPath), 
                    Arg.Is<List<CharacterLine>>(
                        l => l.SequenceEqual(expectedLines)));
        }

        [Test]
        public void Serialize_WriterThrows_ThrowsSameExceptionAsWriter()
        {
            var stubWriter = Substitute.For<IFileLineWriter>();
            var expectedException = new Exception();
            stubWriter
                .When(w => w.WriteLines(Arg.Any<FilePath>(), Arg.Any<IEnumerable<CharacterLine>>()))
                .Do(w => throw expectedException);

            var serializer = CreateSerializer(stubWriter);

            var stubSubtitles = SubtitlesTests.CreateSubtitles1();
            var stubPath = new FilePath("/a/path.srt");

            var actualException = Assert.Throws<Exception>(
                () => serializer.Serialize(stubSubtitles, stubPath));
            Assert.AreEqual(expectedException, actualException);
        }

        [Test]
        public void Serialize_NullSubtitles_Throws()
        {
            var serializer = CreateSerializer();
            Subtitles nullSubtitles = null;
            var stubFilePath = new FilePath("/a/path.srt");

            Assert.Throws<ArgumentNullException>(
                () => serializer.Serialize(nullSubtitles, stubFilePath));
        }

        [Test]
        public void Serialize_NullFilePath_Throws()
        {
            var serializer = CreateSerializer();
            Subtitles stubSubtitles = SubtitlesTests.CreateSubtitles1();
            FilePath nullFilePath = null;

            Assert.Throws<ArgumentNullException>(
                () => serializer.Serialize(stubSubtitles, nullFilePath));
        }

        private SubtitlesSerializer CreateSerializer()
        {
            return CreateSerializer(
                Substitute.For<IFileLineWriter>());
        }

        private SubtitlesSerializer CreateSerializer(IFileLineWriter writer)
        {
            return CreateSerializer(
                writer,
                CreateStubMapper());
        }

        private ISubtitlesMapper CreateStubMapper()
        {
            var stubSubtitles = UnvalidatedSubtitlesTests.CreateUnvalidatedSubtitles1();
            var stubMapper = Substitute.For<ISubtitlesMapper>();
            stubMapper
                .Map(Arg.Any<Subtitles>())
                .Returns(stubSubtitles);

            return stubMapper;
        }

        private SubtitlesSerializer CreateSerializer(
            IFileLineWriter writer, 
            ISubtitlesMapper mapper)
        {
            return new SubtitlesSerializer(writer, mapper);
        }
    }
}
