using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using FluentAssertions;
using Moq;
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
        private IList<IParser> Parsers;
        private IList<IFilter> Filters;
        private readonly int MaxWordQuant = 100;
        private string DictPath;
        private string AffPath;

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
            var projPath = AppDomain.CurrentDomain.BaseDirectory;
            DictPath = Path.Combine(projPath, "dict\\ru_RU.dic");
            AffPath = Path.Combine(projPath, "dict\\ru_RU.aff");
        }

        [Test]
        public void ThrowException_InvalidInputPath()
        {
            Action result = () => { TextReader.Read("/invalidPath/text.txt", MaxWordQuant, DictPath, AffPath); };
            result.ShouldThrow<ArgumentException>().WithMessage("Неверный путь к текстовому файлу.");
        }

        [Test]
        public void ThrowException_InvalidFileNameExtension()
        {
            Action result = () => { TextReader.Read("/invalidPath/text.pdf", MaxWordQuant, DictPath, AffPath); };
            result.ShouldThrow<ArgumentException>().WithMessage("Расширение .pdf не поддерживается.");
        }

        [Test]
        public void GiveCorrectTagsWithWeight_CorrectPathAndSimpleText()
        {
            var expectedResult = new Dictionary<string, int>
            {
                {"царей", 3},
                {"царств", 3},
                {"земных", 3},
                {"отрада", 3},
                {"возлюбленная", 3},
                {"и", 2},
                {"тишина", 1}
            };

            var context = TestContext.CurrentContext;
            var combine = Path.Combine(context.TestDirectory, "test.txt");
            TextReader.Read(combine, MaxWordQuant, DictPath, AffPath).ShouldAllBeEquivalentTo(expectedResult);
        }

        [Test]
        public void GiveCorrectTagsWithWeightAndFilters_CorrectPathAndSimpleText()
        {
            var filters = new List<IFilter>
            {
                new BoringWordsFilter(new List<string> {"земных", "тишина"})
            };
            var currReader = new Reader(Parsers, filters);

            var expectedResult = new Dictionary<string, int>
            {
                {"царей", 3},
                {"царств", 3},
                {"отрада", 3},
                {"возлюбленная", 3}
            };

            var context = TestContext.CurrentContext;
            var combine = Path.Combine(context.TestDirectory,"test.txt");

            var tt = currReader.Read(combine, MaxWordQuant, DictPath, AffPath);
            currReader.Read(combine, MaxWordQuant, DictPath, AffPath).ShouldAllBeEquivalentTo(expectedResult);
        }

        [Test]
        public void GiveCorrectTagsWithWeightAndFilters_CorrectPathAndBigSimpleText()
        {
            var filters = new List<IFilter>();
            filters.Add(new BoringWordsFilter(new List<string> { "царь", "тишина" }));
            var currReader = new Reader(Parsers, Filters);

            var context = TestContext.CurrentContext;
            var combine = Path.Combine(context.TestDirectory, "bigTest.txt");

            var actual = currReader.Read(combine, MaxWordQuant, DictPath, AffPath);
            actual["тебя"].Should().Be(6);
        }
    }
}