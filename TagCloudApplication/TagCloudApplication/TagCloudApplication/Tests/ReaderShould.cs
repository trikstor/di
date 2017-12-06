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
        private readonly int MaxWordQuant = 100;
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
    }
}