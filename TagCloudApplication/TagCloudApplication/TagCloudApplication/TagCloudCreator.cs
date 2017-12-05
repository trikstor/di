using System.Collections.Generic;
using System.Drawing;
using TagCloudApplication.Layouter;
using TagCloudApplication.Renderer;
using TagCloudApplication.TextReader;

namespace TagCloudApplication
{
    public class TagCloudCreator : ITagCloudCreator
    {
        public IReader Reader { get; }
        public ITagsCreator TagsCreator { get; }
        public IRenderer Renderer { get; }

        public TagCloudCreator(IReader reader, ITagsCreator tagsCreator, IRenderer renderer)
        {
            Reader = reader;
            TagsCreator = tagsCreator;
            Renderer = renderer;
        }

        public IEnumerable<Tag> CreateTags(Options options)
        {
            var cloudBrushes = new List<Brush> { Brushes.Blue, Brushes.BlueViolet, Brushes.DarkSlateBlue };
            var imgSize = new Size(options.ImgWidth, options.ImgHeight);

            var tagsCollection = Reader.Read(options.InputPath, options.MaxWordQuant, "dict\\ru_RU.aff", "dict\\ru_RU.dic");
            return TagsCreator.Create(tagsCollection, options.MinFontSize, options.MaxFontSize, options.Font);
        }

        public void CreateAndSave(Options options)
        {
            var cloudBrushes = new List<Brush> { Brushes.Blue, Brushes.BlueViolet, Brushes.DarkSlateBlue };
            var imgSize = new Size(options.ImgWidth, options.ImgHeight);

            var tagsCollection = Reader.Read(options.InputPath, options.MaxWordQuant, "dict\\ru_RU.aff", "dict\\ru_RU.dic");
            var tagRectangles = TagsCreator.Create(tagsCollection, options.MinFontSize, options.MaxFontSize, options.Font);
            var bitmap = Renderer.Draw(tagRectangles, imgSize, cloudBrushes);
            bitmap.Save(options.ImgPath);
        }
    }
}
