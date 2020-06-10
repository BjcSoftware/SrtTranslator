using SrtSubtitleFileParser.Exceptions;
using SubtitleFileParser.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SrtSubtitleFileParser
{
    public class UnvalidatedSubtitlesFileReader : IUnvalidatedSubtitlesReader
    {
        private readonly IFileLineReader reader;

        public UnvalidatedSubtitlesFileReader(IFileLineReader reader)
        {
            if (reader == null)
                throw new ArgumentNullException(nameof(reader));

            this.reader = reader;
        }

        public UnvalidatedSubtitles ReadUnvalidatedSubtitles(FilePath filePath)
        {
            return new UnvalidatedSubtitles(
                ReadAllLines(filePath)
                .Select(line => new CharacterLine(line))
                .Split(new CharacterLine(string.Empty))
                .Select(subtitle => new UnvalidatedSubtitle(subtitle))
                .ToList());
        }

        private string[] ReadAllLines(FilePath filePath)
        {
            try
            {
                return reader.ReadAllLines(filePath);
            }
            catch (Exception)
            {
                throw new SubtitlesReadingException();
            }
        }
    }

    public static class EnumerableExtension
    {
        public static List<List<T>> Split<T>(this IEnumerable<T> toSplit, T separator)
        {
            var splittedList = new List<List<T>>();
            var currentSublist = new List<T>();
            foreach (var item in toSplit)
            {
                if (item.Equals(separator))
                {
                    splittedList.Add(currentSublist);
                    currentSublist = new List<T>();
                }
                else
                {
                    currentSublist.Add(item);
                }
            }

            if(currentSublist.Count != 0)
            {
                splittedList.Add(currentSublist);
            }

            return splittedList;
        }
    }
}
