using SubtitleFileParser.Core;
using SubtitleFileParser.Core.Exceptions;
using System;
using System.Reflection;

namespace SrtSubtitleFileParser
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

            int value = intParser.Parse(input);
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
