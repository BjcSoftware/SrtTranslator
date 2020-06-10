namespace SubtitleFileParser.Core
{
    public interface IFileLineReader
    {
        string[] ReadAllLines(FilePath filePath);
    }
}
