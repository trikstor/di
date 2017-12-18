using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using FluentAssertions;
using Moq;
using NUnit.Framework;
using TagCloudApplication.FileReader;
using TagCloudApplication.ImgSaver;
using TagCloudApplication.Layouter;
using TagCloudApplication.Renderer;
using TagCloudApplication.StatProvider;

namespace TagCloudApplication.Tests
{
    [TestFixture]
    public class TagCloudCreatorShould
    {
        private TagCloudCreator TagCloudCreator;
        private Dictionary<string, int> DefaultTags;

        [SetUp]
        public void SetUp()
        {
            DefaultTags = new Dictionary<string, int>
            {
                {"арбуз", 3},
                {"ананас", 1},
                {"груша", 1},
                {"гранат", 1},
                {"яблоко", 1}
            };

            var statProvider = new Mock<IStatisticsProvider>();
            statProvider.Setup(x => x.GetStatistic(It.IsAny<string>(), It.IsAny<int>()))
                .Returns(DefaultTags);
            var renderer = Mock.Of<IRenderer>();
            var imgSaver = Mock.Of<IImgSaver>();
            var layouter = new CircularCloudLayouter();
            layouter.SetCenter(new Point(500, 500));
            var tagsCreator = new TagsCreator(layouter);
            TagCloudCreator = new TagCloudCreator(statProvider.Object, 
                imgSaver, tagsCreator, renderer);
        }

        [Test]
        public void СorrecеDistanceBetweenWords()
        {
            const int indent = 5;
            var options = new Options
            {
                InputPath = "test.txt",
                ImgPath = "testImg.png",
                ImgWidth = 1000,
                ImgHeight = 1000,
                MaxWordQuant = 100,
                MaxFontSize = 50,
                MinFontSize = 8,
                Font = "Arial"
            };
            var tags = TagCloudCreator.CreateTags(default(string), options);

            var fheight = 0;
            var fwidth = indent;

            foreach (var tag in tags.Value)
            {
                var measure = TextRenderer.MeasureText(tag.Word, tag.Font);
                fheight += measure.Height;
                fwidth += measure.Width;
            }
            var fsize = new Size(fwidth, fheight);

            var rheight = 0;
            var rwidth = 0;
            foreach (var tag in tags.Value)
            {
                var measure = tag.Rectangle;
                rheight += measure.Height;
                rwidth += measure.Width;
            }
            var rsize = new Size(rwidth, rheight);

            fsize.ShouldBeEquivalentTo(rsize);
        }
    }
}
