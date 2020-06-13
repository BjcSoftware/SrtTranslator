using SrtTranslator.Core;
using SrtTranslator.SubtitleFileParser;
using SrtTranslator.SubtitleFileParser.Exceptions;
using System;

namespace SubtitleFileParser
{
    class Program
    {
        static void Main(string[] args)
        {
            var parser = CreateSubtitlesParser();

            
            Console.Write("Srt file path: ");
            var path = new FilePath(Console.ReadLine());

            try
            {
                Subtitles subs = parser.Parse(path);
            }
            catch(SubtitlesParsingException e)
            {
                Console.WriteLine(e.IncorrectSubtitleId);
            }

            //var reader = new FileLineReader();
            //reader.ReadAllLines(new FilePath("Nope"));
        }

        private static SubtitlesFileParser CreateSubtitlesParser()
        {
            var intParser = new IntParser<SubcharacterLine>();

            return new SubtitlesFileParser(
                new UnvalidatedSubtitlesFileReader(
                    new FileLineReader()),
                new SubtitlesParser(
                    new SubtitleParser(
                        new SubtitleTimestampsParser(
                            new TimestampParser(
                                new BoundedIntValueObjectParser<HoursTimestamp, SubcharacterLine>(intParser),
                                new BoundedIntValueObjectParser<MinutesTimestamp, SubcharacterLine>(intParser),
                                new BoundedIntValueObjectParser<SecondsTimestamp, SubcharacterLine>(intParser),
                                new MillisecondsParser(
                                    new BoundedIntValueObjectParser<MillisecondsTimestamp, SubcharacterLine>(intParser)),
                                new TimestampFormatValidator())))));
        }
    }
}
