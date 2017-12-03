﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using FluentAssertions.Common;
 using TagCloudApplication.Filrters;
 using TagCloudApplication.Parsers;

namespace TagCloudApplication.TextReader
{
    public class Reader : IReader
    {
        private List<IParser> TextParsers { get; }
        private List<IFilter> TextFilters { get; }
        private IWordsConverter NormalWordConverter { get; }
        private int MaxWordQuant { get; }

        public Reader(IWordsConverter normalWordConverter, Config config)
        {
            NormalWordConverter = normalWordConverter;
            TextParsers = config.TextParsers;
            TextFilters = config.TextFilters;
            MaxWordQuant = config.MaxWordQuant;
        }

        public Reader(IWordsConverter normalWordConverter, List<IParser> textParsers, List<IFilter> textFilters, int maxWordQuant)
        {
            NormalWordConverter = normalWordConverter;
            TextParsers = textParsers;
            TextFilters = textFilters;
            MaxWordQuant = maxWordQuant;
        }

        public Dictionary<string, int> Read(string path)
        {
            var nameExtensions = TextParsers.SelectMany(x => x.FileExtentions);
            var currNameExtention = Path.GetExtension(path);
            CheckFilePathAndNameExtention(path, nameExtensions);
            var currParser = TextParsers.Where(x => x.FileExtentions.Contains(currNameExtention)).ToArray()[0];
            
            Dictionary<string, int> result;
            using (var textReader = new StreamReader(path, Encoding.UTF8))
            {
                var normalizedWords = NormalWordConverter.NormalizeWords(currParser.Parse(textReader));
                    result = normalizedWords
                        .Select(w => w.ToLower())
                        .Where(word => TextFilters?.Any(filter => filter.FilterTag(word)) ?? true)
                        .GroupBy(word => word)
                        .OrderByDescending(word => word.Count())
                        .Take(MaxWordQuant)
                        .ToDictionary(word => word.Key, word => word.Count());
            }
            return result;
        }
        
        private void CheckFilePathAndNameExtention(string path, IEnumerable<string> nameExtensions)
        {
            var extension = Path.GetExtension(path);
            if(!nameExtensions.Any(nameEx => extension.IsSameOrEqualTo(nameEx)))
                throw new ArgumentException($"Расширение {extension} не поддерживается.");
            if(!File.Exists(path))
                throw new ArgumentException("Неверный путь к текстовому файлу.");
        }
    }
}
