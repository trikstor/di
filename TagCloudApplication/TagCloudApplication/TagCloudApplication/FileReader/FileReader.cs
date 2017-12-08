using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using FluentAssertions.Common;
using TagCloudApplication.Parsers;

namespace TagCloudApplication.StatProvider
{
    public class FileReader : IFileReader
    {
        private IList<IParser> TextParsers { get; }

        public FileReader(IList<IParser> parsers)
        {
            TextParsers = parsers;
        }

        public IEnumerable<string> Read(string path)
        {
            var nameExtensions = TextParsers.SelectMany(x => x.FileExtentions);
            var currNameExtention = Path.GetExtension(path);
            CheckFilePathAndNameExtention(path, nameExtensions);
            var currParser = TextParsers
                .First(x => x.FileExtentions.Contains(currNameExtention));

            using (var textReader = new StreamReader(path, Encoding.UTF8))
            {
                return currParser.Parse(textReader);
            }
        }

        private void CheckFilePathAndNameExtention(string path, IEnumerable<string> nameExtensions)
        {
            var extension = Path.GetExtension(path);
            if (!nameExtensions.Any(nameEx => extension.IsSameOrEqualTo(nameEx)))
                throw new ArgumentException($"Расширение {extension} не поддерживается.");
            if (!File.Exists(path))
                throw new ArgumentException("Неверный путь к текстовому файлу.");
        }
    }
}