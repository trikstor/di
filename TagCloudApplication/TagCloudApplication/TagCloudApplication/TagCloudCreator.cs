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

        public Result<IEnumerable<Tag>> CreateTags(string text, Options options)
        {
            return StatProvider.GetStatistic(text, options.MaxWordQuant)
                .Then(tags => TagsCreator.Create(tags, options.MinFontSize, options.MaxFontSize, options.Font));
        }

        public void CreateAndSave(string text, Options options)
        {
            var cloudBrushes = new List<Brush> { Brushes.Blue, Brushes.BlueViolet, Brushes.DarkSlateBlue };
            var imgSize = new Size(options.ImgWidth, options.ImgHeight);

            StatProvider.GetStatistic(text, options.MaxWordQuant)
                .Then(tags => TagsCreator.Create(tags, options.MinFontSize, options.MaxFontSize, options.Font))
                .Then(trect => Renderer.Draw(trect, imgSize, cloudBrushes))
                .Then(bmap => bmap.Save(options.ImgPath));
        }
    }
}
