using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
        public ILayouter Layouter { get; }

        public TagCloudCreator(IReader reader, ITagsCreator tagsCreator, IRenderer renderer, ILayouter layouter)
        {
            Reader = reader;
            TagsCreator = tagsCreator;
            Renderer = renderer;
            Layouter = layouter;
        }

        public void Create(Options options)
        {
            var cloudBrushes = new List<Brush> { Brushes.Blue, Brushes.BlueViolet, Brushes.DarkSlateBlue };
            var imgSize = new Size(options.ImgWidth, options.ImgHeight);
            var imgCenter = new Point(options.ImgWidth / 2, options.ImgHeight / 2);

            Layouter.SetLayouterSettings(imgCenter);
            var tagsCollection = Reader.Read(options.InputPath, options.MaxWordQuant);
            var tagRectangles = TagsCreator.Create(tagsCollection, options.MinFontSize, options.MaxFontSize, options.Font);
            var bitmap = Renderer.Draw(tagRectangles, imgSize, cloudBrushes);
            bitmap.Save(options.ImgPath);
        }
    }
}
