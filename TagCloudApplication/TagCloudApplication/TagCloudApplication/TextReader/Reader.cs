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
        private IList<IParser> TextParsers { get; }
        private IList<IFilter> TextFilters { get; }

        public Reader(IList<IParser> parsers, IList<IFilter> filters)
        {
            TextParsers = parsers;
            TextFilters = filters;
        }

        public Dictionary<string, int> Read(string path, int maxWordQuant, string affPath, string dicPath)
        {
            var nameExtensions = TextParsers.SelectMany(x => x.FileExtentions);
            var currNameExtention = Path.GetExtension(path);
            CheckFilePathAndNameExtention(path, nameExtensions);
            var currParser = TextParsers
                .First(x => x.FileExtentions.Contains(currNameExtention));

            Dictionary<string, int> result;
            using (var textReader = new StreamReader(path, Encoding.UTF8))
            {
                using (var hunspell = new Hunspell(affPath, dicPath))
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
                        .Take(maxWordQuant)
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