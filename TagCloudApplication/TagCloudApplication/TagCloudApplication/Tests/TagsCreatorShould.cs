using System.Collections.Generic;
using System.Drawing;
using FluentAssertions;
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
            var layouter = new CircularCloudLayouter();
            layouter.SetLayouterSettings(new Point(500, 500));
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
            var expected = new List<Tag>()
            {
                new Tag("арбуз", new Font(FontName, 45), new Rectangle(new Point(407, 467), new Size(186, 67))),
                new Tag("ананас", new Font(FontName, 30), new Rectangle(new Point(425, 422), new Size(149, 45))),
                new Tag("яблоко", new Font(FontName, 15), new Rectangle(new Point(462, 534), new Size(74, 23)))
            };
            Creator.Create(tagCollection, MinFontSize, MaxFontSize, FontName).ShouldBeEquivalentTo(expected, options => 
                options.Excluding(pr => pr.SelectedMemberInfo.Name == "NativeFont"));   
        }
    }
}