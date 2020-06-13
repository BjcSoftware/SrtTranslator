using SrtTranslator.Core;
using SrtTranslator.SubtitleFileParser.Exceptions;
using System;
using System.Reflection;

namespace SrtTranslator.SubtitleFileParser
{
    public class BoundedIntValueObjectParser<TResult, TInput>
        : IParser<TResult, TInput>
            where TResult : BoundedIntBasedValueObject
            where TInput : ValueObject<string>
    {
        private readonly IParser<int, TInput> intParser;

        public BoundedIntValueObjectParser(IParser<int, TInput> intParser)
        {
            if (intParser == null)
                throw new ArgumentNullException(nameof(intParser));

            this.intParser = intParser;
        }

        public TResult Parse(TInput input)
        {
            if (input == null)
                throw new ArgumentNullException(nameof(input));

            return CreateValueObject(
                intParser.Parse(input));
        }

        private TResult CreateValueObject(int value)
        {
            try
            {
                return (TResult)Activator
                    .CreateInstance(
                        typeof(TResult),
                        new object[] { value });
            }
            catch (TargetInvocationException)
            {
                throw new ParsingException("Ouf of range value");
            }
        }
    }
}
