﻿﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using FluentAssertions.Common;
using TextReader.Filrters;
using TextReader.Parsers;

namespace TextReader
{
    public class Reader : IReader
    {
        private List<IParser> TextParsers { get; }
        private List<IFilter> TextFilters { get; }
        
        public Reader(List<IParser> textParsers, List<IFilter> textFilters)
        {
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
            using (var textReader = new StreamReader(path))
            {
                result = currParser.Parse(textReader)
                    .Where(word => !TextFilters.Any(filter => filter.FilterTag(word)))
                    .Aggregate(result, AddNewTagOrChangeQuantity);
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
