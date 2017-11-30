namespace TextReader.Parsers
{
    public interface IParser
    {
        string[] FileExtentions { get; }
        string Parse(string row);
    }
}