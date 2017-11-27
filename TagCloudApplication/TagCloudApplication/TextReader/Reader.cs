using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using FluentAssertions.Common;
using TextReader.Filrters;

namespace TextReader
{
    public class Reader : IReader
    {
        private IEnumerable<IParser> TextParsers { get; }
        
        public Reader(IEnumerable<IParser> textParsers)
        {
            TextParsers = textParsers;
        }
        
        public Dictionary<string, int> Read(string path, List<IFilter> filters)
        {
            var nameExtentions = TextParsers.SelectMany(x => x.FileExtentions);
            var currNameExtention = path.Substring(path.Length - 3, 3);
            CheckFilePathAndNameExtention(path, nameExtentions);
            var currParser = TextParsers.Where(x => x.FileExtentions.Contains(currNameExtention)).ToArray()[0];
            
            var result = new Dictionary<string, int>();
            using (var textReader = new StreamReader(path))
            {
                string currStr;
                while ((currStr = textReader.ReadLine()) != null)
                {
                    result = AddNewTagOrChangeQuantity(result, currParser.Parse(currStr).ToLower());
                    result = filters.Aggregate(result, (current, filter) => filter.FilterTags(current));
                }
            }
            return result;
        }

        private void CheckFilePathAndNameExtention(string path, IEnumerable<string> nameExtentions)
        {
            var extention = path.Substring(path.Length - 3, 3);
            if(!nameExtentions.Any(nameExtention => extention.IsSameOrEqualTo(nameExtention)))
                throw new ArgumentException("Неверное расширение текстового файла.");
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
