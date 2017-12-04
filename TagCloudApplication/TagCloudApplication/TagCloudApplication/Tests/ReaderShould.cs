using System;
using System.Collections.Generic;
using System.IO;
using FluentAssertions;
using NUnit.Framework;
using TagCloudApplication.Filrters;
using TagCloudApplication.Parsers;
using TagCloudApplication.TextReader;

namespace TagCloudApplication.Tests
{
    [TestFixture]
    public class ReaderShould
    {
        private Reader TextReader;
        private List<IParser> Parsers;
        private List<IFilter> Filters;
        private readonly int MaxWordQuant = 100;

        [SetUp]
        public void SetUp()
        {
            Parsers = new List<IParser>
            {
                new SimpleTextParser()
            };
            Filters = new List<IFilter>
            {
                new BoringWordsFilter()
            };
            TextReader = new Reader(Parsers, null);
        }

        [Test]
        public void ThrowException_InvalidInputPath()
        {
            Action result = () => { TextReader.Read("/invalidPath/text.txt", MaxWordQuant); };
            result.ShouldThrow<ArgumentException>().WithMessage("Неверный путь к текстовому файлу.");
        }

        [Test]
        public void ThrowException_InvalidFileNameExtension()
        {
            Action result = () => { TextReader.Read("/invalidPath/text.pdf", MaxWordQuant); };
            result.ShouldThrow<ArgumentException>().WithMessage("Расширение .pdf не поддерживается.");
        }

        [Test]
        public void GiveCorrectTagsWithWeight_CorrectPathAndSimpleText()
        {
            var expectedResult = new Dictionary<string, int>
            {
                {"царь", 3},
                {"и", 2},
                {"царство", 3},
                {"земной", 3},
                {"отрада", 3},
                {"возлюбленный", 3},
                {"тишина", 1}
            };

            var context = TestContext.CurrentContext;
            var combine = Path.Combine(
                Directory.GetParent(context.TestDirectory).FullName,
                "TagCloudApplication\\test.txt"
            );
            Console.WriteLine(combine);
            TextReader.Read(combine, MaxWordQuant).ShouldAllBeEquivalentTo(expectedResult);
        }

        [Test]
        public void GiveCorrectTagsWithWeightAndFilters_CorrectPathAndSimpleText()
        {
            var filters = new List<IFilter>();
            filters.Add(new BoringWordsFilter(new List<string> { "царь", "тишина" }));
            var currReader = new Reader(Parsers, filters);

            var expectedResult = new Dictionary<string, int>
            {
                {"царство", 3},
                {"земной", 3},
                {"отрада", 3},
                {"возлюбленный", 3}
            };

            var context = TestContext.CurrentContext;
            var combine = Path.Combine(
                Directory.GetParent(context.TestDirectory).FullName,
                "TagCloudApplication\\test.txt"
            );

            currReader.Read(combine, MaxWordQuant).ShouldAllBeEquivalentTo(expectedResult);
        }

        [Test]
        public void GiveCorrectTagsWithWeightAndFilters_CorrectPathAndBigSimpleText()
        {
            var filters = new List<IFilter>();
            filters.Add(new BoringWordsFilter(new List<string> { "царь", "тишина" }));
            var currReader = new Reader(Parsers, Filters);

            var context = TestContext.CurrentContext;
            var combine = Path.Combine(
                Directory.GetParent(context.TestDirectory).FullName,
                "TagCloudApplication\\bigTest.txt"
            );

            var actual = currReader.Read(combine, MaxWordQuant);
            actual["тебя"].Should().Be(6);
        }
    }
}