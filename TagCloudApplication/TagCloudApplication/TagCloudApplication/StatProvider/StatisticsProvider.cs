﻿using System;
using System.Collections.Generic;
using System.Linq;
using TagCloudApplication.Filrters;
using TagCloudApplication.WordsNormalizer;

namespace TagCloudApplication.StatProvider
{
    public class StatisticsProvider : IStatisticsProvider
    {
        private IList<IFilter> TextFilters { get; }
        private INormalizer Normalizer { get; }

        public StatisticsProvider(IList<IFilter> textFilters, INormalizer normalizer)
        {
            TextFilters = textFilters;
            Normalizer = normalizer;
        }

        public Result<Dictionary<string, int>> GetStatistic(string text, int maxWordQuant)
        {
            if (maxWordQuant <= 0)
                return Result.Fail<Dictionary<string, int>>
                    ("Максимальное количество слов не может быть неположительным.");

            return Result.Of(() =>
            {
                return SplitText(text)
                    .Select(word => word.ToLower())
                    .Select(word => Normalizer?.Normalize(word) ?? word)
                    .Where(word => TextFilters?.Any(filter => filter.FilterTag(word)) ?? true)
                    .GroupBy(word => word)
                    .OrderByDescending(word => word.Count())
                    .Take(maxWordQuant)
                    .ToDictionary(word => word.Key, word => word.Count());
            });

        }

        private IEnumerable<string> SplitText(string text)
        {
            return text.Split(
                new[] { ' ', '.', ',', ':', ';', '!', '?', '\t', '\n', '\r', '–' },
                StringSplitOptions.RemoveEmptyEntries);
        }
    }
}