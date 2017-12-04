using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using FluentAssertions.Common;
using NHunspell;
using TagCloudApplication.Filrters;
using TagCloudApplication.Parsers;

namespace TagCloudApplication.TextReader
{
    public class Reader : IReader
    {
        private List<IParser> TextParsers { get; }
        private List<IFilter> TextFilters { get; }
        private int MaxWordQuant { get; }

        public Reader(Config config)
        {
            MaxWordQuant = config.MaxWordQuant;
            TextParsers = new List<IParser> {new SimpleTextParser()};
            TextFilters = new List<IFilter> {new BoringWordsFilter(new List<string> {"и"})};
        }

        public Dictionary<string, int> Read(string path)
        {
            var nameExtensions = TextParsers.SelectMany(x => x.FileExtentions);
            var currNameExtention = Path.GetExtension(path);
            CheckFilePathAndNameExtention(path, nameExtensions);
            var currParser = TextParsers
                .First(x => x.FileExtentions.Contains(currNameExtention));

            Dictionary<string, int> result;
            using (var textReader = new StreamReader(path, Encoding.UTF8))
            {
                using (var hunspell = new Hunspell(@"C:\Users\Acer\Saved Games\Desktop\di\TagCloudApplication\TagCloudApplication\TagCloudApplication\dict\ru_RU.aff", 
                    @"C:\Users\Acer\Saved Games\Desktop\di\TagCloudApplication\TagCloudApplication\TagCloudApplication\dict\ru_RU.dic"))
                {
                    var parsedWords = currParser.Parse(textReader);
                    result = parsedWords
                        .Select(word =>
                        {
                            var lowWord = word.ToLower();
                            var stem = hunspell.Stem(lowWord);
                            return stem.Any() ? stem[0] : lowWord;
                        })
                        .Where(word => TextFilters?.Any(filter => filter.FilterTag(word)) ?? true)
                        .GroupBy(word => word)
                        .OrderByDescending(word => word.Count())
                        .Take(MaxWordQuant)
                        .ToDictionary(word => word.Key, word => word.Count());
                }
            }
            return result;
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