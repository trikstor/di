using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
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
        private readonly int MaxWordQuant = 10;
        private string DictPath;
        private string AffPath;

        [SetUp]
        public void SetUp()
        {
            var context = TestContext.CurrentContext;
            var parsedTestText = ParseTestText(Path.Combine(context.TestDirectory, "test.txt"));
            Parsers = new List<IParser>
            {
                Mock.Of<IParser>(x => x.FileExtentions == new[] {".txt"} 
                && x.Parse(It.IsAny<StreamReader>()) == parsedTestText)
            };
            var filters = new List<IFilter>
            {
                Mock.Of<IFilter>(x => x.FilterTag(It.IsAny<string>()))
            };

            TextReader = new Reader(Parsers, filters);
            var projPath = AppDomain.CurrentDomain.BaseDirectory;
            DictPath = Path.Combine(projPath, "dict\\ru_RU.dic");
            AffPath = Path.Combine(projPath, "dict\\ru_RU.aff");
        }

        private IEnumerable<string> ParseTestText(string path)
        {
            using (var textReader = new StreamReader(path, Encoding.UTF8))
            {
                string currStr;
                while ((currStr = textReader.ReadLine()) != null)
                {
                    foreach (var word in currStr.Split(
                        new[] {' ', '.', ',', ':', ';', '!', '?', '\t', '–'},
                        StringSplitOptions.RemoveEmptyEntries))
                    {
                        yield return word;
                    }
                }
            }
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
                {"в", 40},
                {"и", 38},
                {"на", 12},
                {"с", 10},
                {"где", 10},
                {"но", 8},
                {"не", 9},
                {"о", 7},
                {"от", 7},
                {"тебя", 6}
            };

            var context = TestContext.CurrentContext;
            var combine = Path.Combine(context.TestDirectory, "test.txt");
            var tt = TextReader.Read(combine, MaxWordQuant, DictPath, AffPath);
            TextReader.Read(combine, MaxWordQuant, DictPath, AffPath).ShouldAllBeEquivalentTo(expectedResult);
        }

        [Test]
        public void GiveCorrectTagsWithWeightAndFilters_CorrectPathAndSimpleText()
        {
            var filters = new List<IFilter>
            {
                new BoringWordsFilter(new List<string> {"коль", "твои"})
            };
            var currReader = new Reader(Parsers, filters);

            var expectedResult = new Dictionary<string, int>
            {
                {"тебя", 6},
                {"когда", 5},
                {"свой", 4},
                {"россию", 4},
                {"твоих", 4},
                {"науки", 4},
                {"тебе", 4},
                {"щедроты", 3},
                {"наших", 3},
                {"путь", 3}
            };

            var context = TestContext.CurrentContext;
            var combine = Path.Combine(context.TestDirectory,"test.txt");
            currReader.Read(combine, MaxWordQuant, DictPath, AffPath).ShouldAllBeEquivalentTo(expectedResult);
        }
    }
}