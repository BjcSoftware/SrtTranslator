using NSubstitute;
using NSubstitute.ExceptionExtensions;
using NUnit.Framework;
using SrtTranslator.Core;
using SrtTranslator.SubtitleFileParser.Exceptions;
using System;

namespace SrtTranslator.SubtitleFileParser.Tests
{
    using IIntParser = IParser<int, SubcharacterLine>;
    using Bounded0To10IntValueObjectParser = IntBasedValueObjectParser<IntBetween0And10ValueObject, SubcharacterLine>;
    using Input = SubcharacterLine;

    [TestFixture]
    public class BoundedIntValueObjectParserTests
    {
        [Test]
        public void Constructor_NullIntParser_Throws()
        {
            IIntParser nullIntParser = null;

            Assert.Throws<ArgumentNullException>(
                () => new Bounded0To10IntValueObjectParser(
                    nullIntParser));
        }

        [Test]
        public void Parse_NullInput_Throws()
        {
            var parser = CreateParser();
            Input nullInput = null;

            Assert.Throws<ArgumentNullException>(
                () => parser.Parse(nullInput));
        }

        [Test]
        [TestCase(0)]
        [TestCase(1)]
        [TestCase(10)]
        public void Parse_InRangeInput_ReturnsCorrectValueObject(int inRangeInput)
        {
            var stubIntParser = Substitute.For<IIntParser>();
            stubIntParser
                .Parse(Arg.Any<Input>())
                .Returns(inRangeInput);
            var parser = Create0To10BoundedIntParser(stubIntParser);

            var actualValueObject = parser.Parse(new Input("in range input"));

            Assert.AreEqual(
                new IntBetween0And10ValueObject(inRangeInput), 
                actualValueObject);
        }

        [Test]
        [TestCase(-1)]
        [TestCase(11)]
        public void Parse_OutOfRangeInput_Throws(int outOfRangeInput)
        {
            var stubIntParser = Substitute.For<IIntParser>();
            stubIntParser
                .Parse(Arg.Any<Input>())
                .Returns(outOfRangeInput);
            var parser = Create0To10BoundedIntParser(stubIntParser);

            Assert.Throws<ParsingException>(
                () => parser.Parse(
                    new Input("ouf of range input")));
        }

        [Test]
        public void Parse_InvalidInt_Throws()
        {
            var stubIntParser = Substitute.For<IIntParser>();
            stubIntParser
                .Parse(Arg.Any<Input>())
                .Throws<ParsingException>();
            var parser = CreateParser(stubIntParser);

            Assert.Throws<ParsingException>(
                () => parser.Parse(
                    new Input("invalid int")));
        }

        private Bounded0To10IntValueObjectParser CreateParser()
        {
            return Create0To10BoundedIntParser();
        }

        private Bounded0To10IntValueObjectParser CreateParser(IIntParser intParser)
        {
            return Create0To10BoundedIntParser(intParser);
        }

        private Bounded0To10IntValueObjectParser Create0To10BoundedIntParser()
        {
            return Create0To10BoundedIntParser(
                Substitute.For<IIntParser>());
        }

        private Bounded0To10IntValueObjectParser Create0To10BoundedIntParser(IIntParser intParser)
        {
            return new Bounded0To10IntValueObjectParser(intParser);
        }
    }

    class IntBetween0And10ValueObject : BoundedIntBasedValueObject
    {
        public IntBetween0And10ValueObject(int value) 
            : base(value, new NumericRange(0, 10))
        {
        }
    }
}
