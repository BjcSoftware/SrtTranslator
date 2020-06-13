namespace SrtTranslator.SubtitleFileParser
{
    public interface IFileLineReader
    {
        string[] ReadAllLines(FilePath filePath);
    }
}
