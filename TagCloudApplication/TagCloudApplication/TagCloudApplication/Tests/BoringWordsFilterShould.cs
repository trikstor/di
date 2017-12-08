using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using NUnit.Framework.Internal;
using NUnit.Framework;
using TagCloudApplication.Filrters;

namespace TagCloudApplication.Tests
{
    [TestFixture]
    public class BoringWordsFilterShould
    {
        private IFilter BoringWordsFilter;

        [SetUp]
        public void SetUp()
        {
            BoringWordsFilter = new BoringWordsFilter(new List<string>{"однажды", "скоро"});
        }

        [TestCase("однажды", ExpectedResult = false, TestName = "Скучное слово из списка.")]
        [TestCase("или", ExpectedResult = false, TestName = "Короткое слово.")]
        [TestCase("день", ExpectedResult = true, TestName = "Нескучное слово.")]
        public bool FilterBoringWord(string word)
        {
            return BoringWordsFilter.FilterTag(word);
        }
    }
}
