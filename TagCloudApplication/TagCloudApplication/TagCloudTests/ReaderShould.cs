using System;
using System.Collections.Generic;
using System.IO;
using FluentAssertions;
using NUnit.Framework;
using TextReader;
using TextReader.Filrters;
using TextReader.Parsers;

namespace TagsCloudVisualization
{
    [TestFixture]
    public class ReaderShould
    {
        /*
        private Reader TextReader;

        [SetUp]
        public void SetUp()
        {
            TextReader = new Reader(
                new List<IParser>
                {
                    new SimpleTextParser()
                });
        }

        [Test]
        public void ThrowException_InvalidInputPath()
        {
            Action result = () => { TextReader.Read("/invalidPath/text.txt", null); };
            result.ShouldThrow<ArgumentException>().WithMessage("Неверный путь к текстовому файлу.");
        }

        [Test]
        public void ThrowException_InvalidFileNameExtension()
        {
            Action result = () => { TextReader.Read("/invalidPath/text.pdf", null); };
            result.ShouldThrow<ArgumentException>().WithMessage("Неверное расширение текстового файла.");
        }

        [Test]
        public void GiveCorrectTagsWithWeight_CorrectPathAndSimpleText()
        {
            var expectedResult = new Dictionary<string, int>
            {
                {"loren", 3},
                {"ipsum", 2},
                {"dolor", 1},
                {"sit", 2},
                {"amet", 1}
            };
            
            var context = TestContext.CurrentContext;
            var combine = Path.Combine(
                Directory.GetParent(context.TestDirectory).Parent.FullName,
                "test.txt"
            );
            
            TextReader.Read(combine, null).ShouldAllBeEquivalentTo(expectedResult);
        }
        
        [Test]
        public void GiveCorrectTagsWithWeightAndFilters_CorrectPathAndSimpleText()
        {
            var filters = new List<IFilter>();
            filters.Add(new BoringWordsFilter(new List<string>{"sit", "amet"}));

            var expectedResult = new Dictionary<string, int>
            {
                {"loren", 3},
                {"ipsum", 2},
                {"dolor", 1}
            };
            
            var context = TestContext.CurrentContext;
            var combine = Path.Combine(
                Directory.GetParent(context.TestDirectory).Parent.FullName,
                "test.txt"
            );
            
            TextReader.Read(combine, filters).ShouldAllBeEquivalentTo(expectedResult);
        }
        */
    }
}