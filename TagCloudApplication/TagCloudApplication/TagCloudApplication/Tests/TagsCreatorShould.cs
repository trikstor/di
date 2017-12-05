using System.Collections.Generic;
using System.Drawing;
using FluentAssertions;
using Moq;
using NUnit.Framework;
using TagCloudApplication.Layouter;

namespace TagCloudApplication.Tests
{
    [TestFixture]
    public class TagsCreatorShould
    {
        private TagsCreator Creator;
        private readonly int MinFontSize = 8;
        private readonly int MaxFontSize = 45;
        private readonly string FontName = "Arial";

        [SetUp]
        public void SetUp()
        {
            var layouter = Mock.Of <ILayouter>(x => x.PutNextRectangle(It.IsAny<Size>()) == Rectangle.Empty);
            Creator = new TagsCreator(layouter);
        }
        
        [Test]
        public void GiveCorrespondingFontsForTagCollection()
        {
            var tagCollection = new Dictionary<string, int>
            {
                {"арбуз", 3},
                {"ананас", 2},
                {"яблоко", 1}
            };
            var expected = new List<Tag>
            {
                new Tag("арбуз", new Font(FontName, 45), Rectangle.Empty),
                new Tag("ананас", new Font(FontName, 30), Rectangle.Empty),
                new Tag("яблоко", new Font(FontName, 15), Rectangle.Empty)
            };
            Creator.Create(tagCollection, MinFontSize, MaxFontSize, FontName).ShouldBeEquivalentTo(expected, options => 
                options.Excluding(pr => pr.SelectedMemberInfo.Name == "NativeFont"));   
        }
    }
}