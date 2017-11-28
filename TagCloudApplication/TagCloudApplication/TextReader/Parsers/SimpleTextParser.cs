namespace TextReader.Parsers
{
    public class SimpleTextParser : IParser
    {
        public string[] FileExtentions { get; }

        public SimpleTextParser()
        {
            FileExtentions = new[]
            {
                "txt"
            };
        }
        public string[] Parse(string row)
        {
            return new[] {row};
        }
    }
}