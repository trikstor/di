using System.Collections.Generic;
using System.Drawing;
using TagCloudApplication.BrushProvider;
using TagCloudApplication.Layouter;

namespace TagCloudApplication.Renderer
{
    public class Renderer : IRenderer
    {
        private ILayouter Layouter { get; }
        private Size ImgSize { get; }
        private IBrushProvider Colorer { get; }

        public Renderer(Config config, IBrushProvider colorer, ILayouter layouter)
        {
            Layouter = layouter;
            ImgSize = config.ImgSize;
            Colorer = colorer;
        }
        
        public Bitmap Draw(Dictionary<string, Font> tagList)
        {
            var bitmap = new Bitmap(ImgSize.Width, ImgSize.Height);
            using (var gr = Graphics.FromImage(bitmap))
            {
                foreach (var tag in tagList)
                    gr.DrawString(tag.Key, tag.Value,
                        Colorer.GetColor(tag.Key),
                        Layouter.PutNextRectangle(GetRectangleSize(gr, tag.Key, tag.Value)));
            }
            return bitmap;
        }

        private Size GetRectangleSize(Graphics gr, string word, Font font)
        {
            var size = gr.MeasureString(word, font);
            return new Size((int)size.Width + 1, (int)size.Height);
        }
        
    }
}
