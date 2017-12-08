using System.Collections.Generic;
using System.Drawing;
using TagCloudApplication.Renderer;
using TagCloudApplication.StatProvider;

namespace TagCloudApplication
{
    public class TagCloudCreator : ITagCloudCreator
    {
        private ITagsCreator TagsCreator { get; }
        private IRenderer Renderer { get; }
        private IStatisticsProvider StatProvider { get; }

        public TagCloudCreator(IStatisticsProvider statProvider, ITagsCreator tagsCreator, IRenderer renderer)
        {
            TagsCreator = tagsCreator;
            Renderer = renderer;
            StatProvider = statProvider;
        }

        public IEnumerable<Tag> CreateTags(string text, Options options)
        {
            var tagsCollection = StatProvider.GetStatistic(text, options.MaxWordQuant);
            return TagsCreator.Create(tagsCollection, options.MinFontSize, options.MaxFontSize, options.Font);
        }

        public void CreateAndSave(string text, Options options)
        {
            var cloudBrushes = new List<Brush> { Brushes.Blue, Brushes.BlueViolet, Brushes.DarkSlateBlue };
            var imgSize = new Size(options.ImgWidth, options.ImgHeight);

            var tagsCollection = StatProvider.GetStatistic(text, options.MaxWordQuant);
            var tagRectangles = TagsCreator.Create(tagsCollection, options.MinFontSize, options.MaxFontSize, options.Font);
            var bitmap = Renderer.Draw(tagRectangles, imgSize, cloudBrushes);
            bitmap.Save(options.ImgPath);
        }
    }
}
