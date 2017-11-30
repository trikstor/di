using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using FluentAssertions;
using NUnit.Framework;
using TextReader;

namespace TagsCloudVisualization
{
    [TestFixture]
    public class NormalFormConvertShould
    {
        private NormalFormConverter Converter;

        [SetUp]
        public void SetUp()
        {
            Converter = new NormalFormConverter(Path.Combine(
                Directory.GetParent(AppDomain.CurrentDomain.BaseDirectory).Parent.Parent.FullName
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