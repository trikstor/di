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
        private Reader TextReader;

        [SetUp]
        public void SetUp()
        {
            TextReader = new Reader(new NormalFormConverter(Path.Combine(
                Directory.GetParent(AppDomain.CurrentDomain.BaseDirectory).Parent.Parent.FullName
                , "mystem.exe")), 
                new List<IParser>
                {
                    new SimpleTextParser()
                }, null);
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
                {"тьма", 2},
                {"остров", 2},
                {"сеять", 2},
                {"река", 3},
                {"подобен", 3},
                {"океан", 3}
            };
            
            var context = TestContext.CurrentContext;
            var combine = Path.Combine(
                Directory.GetParent(context.TestDirectory).Parent.FullName,
                "test.txt"
            );
            var rr = TextReader.Read(combine);
            TextReader.Read(combine).ShouldAllBeEquivalentTo(expectedResult);
        }
        
        [Test]
        public void GiveCorrectTagsWithWeightAndFilters_CorrectPathAndSimpleText()
        {
            var filters = new List<IFilter>();
            filters.Add(new BoringWordsFilter(new List<string>{"подобен", "океан"}));
            var currReader = new Reader(new NormalFormConverter(Path.Combine(
                Directory.GetParent(AppDomain.CurrentDomain.BaseDirectory).Parent.Parent.FullName
                , "mystem.exe")), new List<IParser>
            {
                new SimpleTextParser()
            }, filters);
            var expectedResult = new Dictionary<string, int>
            {
                {"тьма", 2},
                {"остров", 2},
                {"сеять", 2},
                {"река", 3},
            };
            
            var context = TestContext.CurrentContext;
            var combine = Path.Combine(
                Directory.GetParent(context.TestDirectory).Parent.FullName,
                "test.txt"
            );

            currReader.Read(combine).ShouldAllBeEquivalentTo(expectedResult);
        }
    }
}