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

        public Reader(IWordsConverter normalWordConverter, Config config)
        {
            NormalWordConverter = normalWordConverter;
            TextParsers = config.TextParsers;
            TextFilters = config.TextFilters;
        }

        public Reader(IWordsConverter normalWordConverter, List<IParser> textParsers, List<IFilter> textFilters)
        {
            NormalWordConverter = normalWordConverter;
            TextParsers = textParsers;
            TextFilters = textFilters;
        }

        public Dictionary<string, int> Read(string path)
        {
            var nameExtensions = TextParsers.SelectMany(x => x.FileExtentions);
            var currNameExtention = Path.GetExtension(path);
            CheckFilePathAndNameExtention(path, nameExtensions);
            var currParser = TextParsers.Where(x => x.FileExtentions.Contains(currNameExtention)).ToArray()[0];
            
            var result = new Dictionary<string, int>();
            using (var textReader = new StreamReader(path, Encoding.UTF8))
            {
                var normalizedWords = NormalWordConverter.NormalizeWords(currParser.Parse(textReader))
                    .OrderByDescending(word => word);

                if (TextFilters != null)
                {
                    normalizedWords
                        .Where(word => TextFilters.Any(filter => filter.FilterTag(word)))
                        .Aggregate(result, AddNewTagOrChangeQuantity);
                }
                else
                {
                    normalizedWords.Aggregate(result, AddNewTagOrChangeQuantity);
                }
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

        private Dictionary<string, int> AddNewTagOrChangeQuantity(Dictionary<string, int> tags, string currString)
        {
            if (tags.ContainsKey(currString))
                tags[currString]++;
            else
                tags.Add(currString, 1);
            return tags;
        }
    }
}
