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

        [SetUp]
        public void SetUp()
        {
            TextReader = new Reader(
                new NormalFormConverter(Path.Combine(
                    Directory.GetParent(AppDomain.CurrentDomain.BaseDirectory).Parent.Parent.FullName
                    , "mystem.exe")),
                new List<IParser> {new SimpleTextParser()}, null);
        }

        [Test]
        public void ThrowException_InvalidInputPath()
        {
            Action result = () => { TextReader.Read("/invalidPath/text.txt"); };
            result.ShouldThrow<ArgumentException>().WithMessage("Неверный путь к текстовому файлу.");
        }

        [Test]
        public void ThrowException_InvalidFileNameExtension()
        {
            Action result = () => { TextReader.Read("/invalidPath/text.pdf"); };
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
                {"возлюбленная", 3},
                {"тишина", 1}
            };

            var context = TestContext.CurrentContext;
            var combine = Path.Combine(
                Directory.GetParent(context.TestDirectory).Parent.FullName,
                "test.txt"
            );

            TextReader.Read(combine).ShouldAllBeEquivalentTo(expectedResult);
        }

        [Test]
        public void GiveCorrectTagsWithWeightAndFilters_CorrectPathAndSimpleText()
        {
            var filters = new List<IFilter>();
            filters.Add(new BoringWordsFilter(new List<string> { "царь", "тишина" }));
            var currReader = new Reader(new NormalFormConverter(Path.Combine(
                Directory.GetParent(AppDomain.CurrentDomain.BaseDirectory).Parent.Parent.FullName
                , "mystem.exe")), 
                new List<IParser> { new SimpleTextParser() }, filters);

            var expectedResult = new Dictionary<string, int>
            {
                {"царство", 3},
                {"земной", 3},
                {"отрада", 3},
                {"возлюбленная", 3}
            };

            var context = TestContext.CurrentContext;
            var combine = Path.Combine(
                Directory.GetParent(context.TestDirectory).Parent.FullName,
                "test.txt"
            );

            currReader.Read(combine).ShouldAllBeEquivalentTo(expectedResult);
        }

        [Test]
        public void GiveCorrectTagsWithWeightAndFilters_CorrectPathAndBigSimpleText()
        {
            var filters = new List<IFilter>();
            filters.Add(new BoringWordsFilter(new List<string> { "царь", "тишина" }));
            var currReader = new Reader(new NormalFormConverter(Path.Combine(
                    Directory.GetParent(AppDomain.CurrentDomain.BaseDirectory).Parent.Parent.FullName
                    , "mystem.exe")),
                new List<IParser> { new SimpleTextParser() }, filters);

            var expectedResult = new Dictionary<string, int>
            {
                {"царство", 3},
                {"земной", 3},
                {"отрада", 3},
                {"возлюбленная", 3}
            };

            var context = TestContext.CurrentContext;
            var combine = Path.Combine(
                Directory.GetParent(context.TestDirectory).Parent.FullName,
                "bigTest.txt"
            );

            var actual = currReader.Read(combine);
            actual["твой"].Should().Be(14);
        }
    }
}