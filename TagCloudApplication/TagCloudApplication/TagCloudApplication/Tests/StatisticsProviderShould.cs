using System.Collections.Generic;
using System.IO;
using FluentAssertions;
using Moq;
using NUnit.Framework;
using TagCloudApplication.Filrters;
using TagCloudApplication.StatProvider;
using TagCloudApplication.WordsNormalizer;

namespace TagCloudApplication.Tests
{
    [TestFixture]
    public class StatisticsProviderShould
    {
        private IStatisticsProvider StatProvider;
        private string TestText;
        private readonly int MaxWordQuant = 10;

        [SetUp]
        public void SetUp()
        {
            var context = TestContext.CurrentContext;
            TestText = File.ReadAllText(Path.Combine(context.TestDirectory, "test.txt"));

            var filters = new List<IFilter>
            {
                Mock.Of<IFilter>(x => x.FilterTag(It.IsAny<string>()))
            };
            var normalizer = Mock.Of<INormalizer>();
            StatProvider = new StatisticsProvider(filters, normalizer);
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
            StatProvider.GetStatistic(TestText, MaxWordQuant).ShouldAllBeEquivalentTo(expectedResult);
        }
    }
}