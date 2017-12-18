using System.IO;

namespace TagCloudApplication
{
    public static class SimpleTextFileReader
    {
        public static Result<string> GetTextWords(string path)
        {
            var result = Result.Of(() => File.ReadAllText(path));
            if(result.IsSuccess)
                return result.Value.Length > 0 ? result : Result.Fail<string>("Пустой файл.");
            return result;
        }
    }
}
