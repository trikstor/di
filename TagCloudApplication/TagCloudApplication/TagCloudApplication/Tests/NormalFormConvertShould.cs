using System;
using System.Collections.Generic;
using System.IO;
using FluentAssertions;
using NUnit.Framework;
using TagCloudApplication.TextReader;

namespace TagCloudApplication.Tests
{
    [TestFixture]
    public class NormalFormConvertShould
    {
        private NormalFormConverter Converter;

        [SetUp]
        public void SetUp()
        {
            Converter = new NormalFormConverter(Path.Combine(
                Directory.GetParent(AppDomain.CurrentDomain.BaseDirectory).Parent.Parent.Parent.FullName
                , "mystem.exe"));
        }

        [Test]
        public void ConvertWordsToNormalFormCorrectly()
        {
            var actual = new List<string>
            {
                "мурелки",
                "шлепают",
                "пельсиски" 
            };
            var expected = new List<string>
            {
                "мурелка",
                "шлепать",
                "пельсиска"
            };
            Converter.NormalizeWords(actual).ShouldBeEquivalentTo(expected);
        }
    }
}