using System.Collections.Generic;
using System.Drawing;

namespace TagCloudApplication.Renderer
{
    public class Renderer : IRenderer
    {
        private Brush TagColor { get; }
        private string TagFontName { get; }
        private Size ImgSize { get; }

        public Renderer(Config config)
        {
            TagColor = config.CloudBrushes[0];
            TagFontName = config.FontName;
            ImgSize = config.ImgSize;
        }
        public Renderer(Brush color, string fontName, Size imgSize)
        {
            TagColor = color;
            TagFontName = fontName;
            ImgSize = imgSize;
        }

        public Bitmap Draw(Dictionary<string, Rectangle> tagList)
        {
            var bitmap = new Bitmap(ImgSize.Width, ImgSize.Height);
            using (var gr = Graphics.FromImage(bitmap))
            {
                foreach (var tag in tagList)
                {
                    gr.DrawString(tag.Key, new Font(TagFontName, GetFontSize(gr, tag.Key, tag.Value)), TagColor,
                        tag.Value.Location);
                }
            }
            return bitmap;
        }

        public int GetFontSize(Graphics gr, string word, Rectangle rectangle)
        {
            return rectangle.Height / 2;
        }
        
    }
}
