﻿namespace SrtTranslator.SubtitleFileParser
{
    public class FileLineReader : IFileLineReader
    {
        public string[] ReadAllLines(FilePath filePath)
        {
            return System.IO.File.ReadAllLines(filePath.Value);
        }
    }
}